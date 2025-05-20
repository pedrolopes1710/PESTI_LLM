"use client"

import { useState } from "react"
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
import { Plus } from "lucide-react"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from "zod"
import { toast } from "@/components/ui/use-toast"
import { createDespesa } from "../api"
import type { Orcamento } from "../types"
import type { CreatingDespesaDto } from "../../despesas/types"

// Define the form validation schema
const formSchema = z.object({
  descricao: z.string().min(3, {
    message: "Description must be at least 3 characters.",
  }),
  valor: z.coerce
    .number({
      required_error: "Amount is required.",
      invalid_type_error: "Amount must be a number.",
    })
    .positive({
      message: "Amount must be positive.",
    }),
})

type FormValues = z.infer<typeof formSchema>

interface CreateDespesaDialogProps {
  orcamento: Orcamento
  onDespesaCreated: () => void
}

export function CreateDespesaDialog({ orcamento, onDespesaCreated }: CreateDespesaDialogProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [isSubmitting, setIsSubmitting] = useState(false)

  // Initialize the form with react-hook-form
  const form = useForm<FormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      descricao: "",
      valor: undefined,
    },
  })

  // Handle form submission
  async function onSubmit(values: FormValues) {
    try {
      setIsSubmitting(true)

      // Create the DTO to send to the API
      const createDto: CreatingDespesaDto = {
        descricao: values.descricao,
        valor: values.valor,
        orcamentoId: orcamento.id,
      }

      await createDespesa(createDto)

      // Show success message
      toast({
        title: "Expense created successfully!",
        description: `The expense "${values.descricao}" has been created with the amount of ${values.valor.toLocaleString(
          "en-US",
          {
            style: "currency",
            currency: "EUR",
          },
        )}.`,
      })

      // Reset the form
      form.reset()

      // Close the dialog
      setIsOpen(false)

      // Notify the parent component to update the list
      onDespesaCreated()
    } catch (error) {
      console.error("Error creating expense:", error)
      toast({
        variant: "destructive",
        title: "Error creating expense",
        description: "An error occurred while creating the expense. Please try again.",
      })
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button size="sm" variant="outline" className="ml-2">
          <Plus className="h-4 w-4 mr-2" />
          Add Expense
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Add New Expense</DialogTitle>
          <DialogDescription>
            Create a new expense for the budget "{orcamento.rubrica.nome}" with planned amount of{" "}
            {orcamento.gastoPlaneado.toLocaleString("en-US", {
              style: "currency",
              currency: "EUR",
            })}
            .
          </DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4 py-4">
            <FormField
              control={form.control}
              name="descricao"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Description</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter expense description" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="valor"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Amount (€)</FormLabel>
                  <FormControl>
                    <Input
                      type="number"
                      step="0.01"
                      placeholder="Enter expense amount"
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
            <DialogFooter>
              <Button type="button" variant="outline" onClick={() => setIsOpen(false)} disabled={isSubmitting}>
                Cancel
              </Button>
              <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Creating..." : "Create Expense"}
              </Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
