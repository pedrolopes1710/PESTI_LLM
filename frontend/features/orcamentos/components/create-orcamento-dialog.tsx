"use client"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Plus } from "lucide-react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import { toast } from "@/components/ui/use-toast"
import { createOrcamento } from "../api"
import { fetchRubricas } from "@/features/rubricas/api"
import { fetchAtividades } from "@/features/atividades/api"
import type { Rubrica } from "@/features/rubricas/types"
import type { Atividade } from "@/features/atividades/types"

// Define the form validation schema
const formSchema = z.object({
  gastoPlaneado: z.coerce
    .number({
      required_error: "O valor do gasto planeado é obrigatório.",
      invalid_type_error: "O valor deve ser um número.",
    })
    .positive({
      message: "O valor deve ser positivo.",
    }),
  rubricaId: z.string({
    required_error: "Por favor selecione uma rubrica.",
  }),
  atividadeId: z.string().optional(),
})

type FormValues = z.infer<typeof formSchema>

interface CreateOrcamentoDialogProps {
  onOrcamentoCriado: () => void
}

export function CreateOrcamentoDialog({ onOrcamentoCriado }: CreateOrcamentoDialogProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [rubricas, setRubricas] = useState<Rubrica[]>([])
  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [loadingRubricas, setLoadingRubricas] = useState(false)
  const [loadingAtividades, setLoadingAtividades] = useState(false)

  // Initialize the form with react-hook-form
  const form = useForm<FormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      gastoPlaneado: undefined,
      rubricaId: "",
      atividadeId: undefined,
    },
  })

  // Load rubricas and atividades when dialog opens
  useEffect(() => {
    if (isOpen) {
      const loadData = async () => {
        try {
          setLoadingRubricas(true)
          setLoadingAtividades(true)

          // Load rubricas
          const rubricasData = await fetchRubricas()
          setRubricas(rubricasData)

          // Load atividades
          const atividadesData = await fetchAtividades()
          setAtividades(atividadesData)
        } catch (error) {
          console.error("Error loading data:", error)
          toast({
            variant: "destructive",
            title: "Error loading data",
            description: "Could not load categories or activities.",
          })
        } finally {
          setLoadingRubricas(false)
          setLoadingAtividades(false)
        }
      }

      loadData()
    }
  }, [isOpen])

  // Handle form submission
  async function onSubmit(values: FormValues) {
    try {
      setIsSubmitting(true)

      // Create the DTO to send to the API
      const createDto = {
        gastoPlaneado: values.gastoPlaneado,
        rubricaId: values.rubricaId,
        // Only include atividadeId if it's defined and not empty
        ...(values.atividadeId && { atividadeId: values.atividadeId }),
      }

      await createOrcamento(createDto)

      // Show success message
      toast({
        title: "Budget created successfully!",
        description: `The budget has been created with the amount of ${values.gastoPlaneado.toLocaleString("en-US", {
          style: "currency",
          currency: "EUR",
        })}.`,
      })

      // Reset the form
      form.reset()

      // Close the dialog
      setIsOpen(false)

      // Notify the parent component to update the list
      onOrcamentoCriado()
    } catch (error) {
      console.error("Erro ao criar orçamento:", error)
      toast({
        variant: "destructive",
        title: "Error creating budget",
        description: "An error occurred while creating the budget. Please try again.",
      })
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button>
          <Plus className="h-4 w-4 mr-2" />
          New Budget
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Create New Budget</DialogTitle>
          <DialogDescription>Fill in the fields below to create a new budget.</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4 py-4">
            <FormField
              control={form.control}
              name="gastoPlaneado"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Planned Expense (€)</FormLabel>
                  <FormControl>
                    <Input
                      type="number"
                      step="0.01"
                      placeholder="Enter planned amount"
                      {...field}
                      onChange={(e) => {
                        const value = e.target.value === "" ? undefined : Number.parseFloat(e.target.value)
                        field.onChange(value)
                      }}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="rubricaId"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Category</FormLabel>
                  <Select onValueChange={field.onChange} defaultValue={field.value}>
                    <FormControl>
                      <SelectTrigger>
                        <SelectValue placeholder={loadingRubricas ? "Loading categories..." : "Select a category"} />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      {rubricas.map((rubrica) => (
                        <SelectItem key={rubrica.id} value={rubrica.id}>
                          {rubrica.nome}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="atividadeId"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Activity (Optional)</FormLabel>
                  <Select onValueChange={field.onChange} defaultValue={field.value}>
                    <FormControl>
                      <SelectTrigger>
                        <SelectValue
                          placeholder={loadingAtividades ? "Loading activities..." : "Select an activity (optional)"}
                        />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      <SelectItem value="none">No activity</SelectItem>
                      {atividades.map((atividade) => (
                        <SelectItem key={atividade.id} value={atividade.id}>
                          {atividade.nomeAtividade}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                  <FormMessage />
                </FormItem>
              )}
            />
            <DialogFooter>
              <Button type="button" variant="outline" onClick={() => setIsOpen(false)} disabled={isSubmitting}>
                Cancel
              </Button>
              <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Creating..." : "Create Budget"}
              </Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
