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
import { createRubrica } from "../api"

// Define the form validation schema
const formSchema = z.object({
  nome: z.string().min(3, {
    message: "O nome da rubrica deve ter pelo menos 3 caracteres.",
  }),
})

type FormValues = z.infer<typeof formSchema>

interface CreateRubricaDialogProps {
  onRubricaCreated: () => void
}

export function CreateRubricaDialog({ onRubricaCreated }: CreateRubricaDialogProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [isSubmitting, setIsSubmitting] = useState(false)

  // Initialize the form with react-hook-form
  const form = useForm<FormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      nome: "",
    },
  })

  // Handle form submission
  async function onSubmit(values: FormValues) {
    try {
      setIsSubmitting(true)

      await createRubrica(values)

      // Show success message
      toast({
        title: "Rubrica criada com sucesso!",
        description: `A rubrica "${values.nome}" foi criada.`,
      })

      // Reset the form
      form.reset()

      // Close the dialog
      setIsOpen(false)

      // Notify the parent component to update the list
      onRubricaCreated()
    } catch (error) {
      console.error("Erro ao criar rubrica:", error)
      toast({
        variant: "destructive",
        title: "Erro ao criar rubrica",
        description: "Ocorreu um erro ao tentar criar a rubrica. Tente novamente.",
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
          Nova Rubrica
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Criar Nova Rubrica</DialogTitle>
          <DialogDescription>Preencha o campo abaixo para criar uma nova rubrica.</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4 py-4">
            <FormField
              control={form.control}
              name="nome"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Nome da Rubrica</FormLabel>
                  <FormControl>
                    <Input placeholder="Digite o nome da rubrica" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <DialogFooter>
              <Button type="button" variant="outline" onClick={() => setIsOpen(false)} disabled={isSubmitting}>
                Cancelar
              </Button>
              <Button type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Criando..." : "Criar Rubrica"}
              </Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
