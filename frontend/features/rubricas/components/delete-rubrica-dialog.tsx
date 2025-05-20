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
import { Trash2 } from "lucide-react"
import { toast } from "@/components/ui/use-toast"
import { deleteRubrica } from "../api"

interface DeleteRubricaDialogProps {
  rubricaId: string
  rubricaName: string
  onRubricaDeleted: () => void
}

export function DeleteRubricaDialog({ rubricaId, rubricaName, onRubricaDeleted }: DeleteRubricaDialogProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [isDeleting, setIsDeleting] = useState(false)

  // Handle deletion
  async function handleDelete() {
    try {
      setIsDeleting(true)

      await deleteRubrica(rubricaId)

      // Show success message
      toast({
        title: "Rubrica excluída com sucesso!",
        description: `A rubrica "${rubricaName}" foi excluída.`,
      })

      // Close the dialog
      setIsOpen(false)

      // Notify the parent component to update the list
      onRubricaDeleted()
    } catch (error) {
      console.error("Erro ao excluir rubrica:", error)
      toast({
        variant: "destructive",
        title: "Erro ao excluir rubrica",
        description: "Ocorreu um erro ao tentar excluir a rubrica. Tente novamente.",
      })
    } finally {
      setIsDeleting(false)
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button variant="ghost" size="icon" className="text-red-500 hover:text-red-700 hover:bg-red-100">
          <Trash2 className="h-4 w-4" />
          <span className="sr-only">Excluir</span>
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Excluir Rubrica</DialogTitle>
          <DialogDescription>
            Tem certeza que deseja excluir a rubrica "{rubricaName}"? Esta ação não pode ser desfeita.
          </DialogDescription>
        </DialogHeader>
        <DialogFooter className="mt-4">
          <Button type="button" variant="outline" onClick={() => setIsOpen(false)} disabled={isDeleting}>
            Cancelar
          </Button>
          <Button type="button" variant="destructive" onClick={handleDelete} disabled={isDeleting}>
            {isDeleting ? "Excluindo..." : "Excluir Rubrica"}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
