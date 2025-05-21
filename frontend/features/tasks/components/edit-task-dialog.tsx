"use client"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import { fetchActivities, updateTask, AVAILABLE_STATUSES } from "../api"
import type { ApiActivity, ApiTask } from "../api"
import { toast } from "@/components/ui/use-toast"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { Textarea } from "@/components/ui/textarea"

interface EditTaskDialogProps {
  task: ApiTask
  open: boolean
  onOpenChange: (open: boolean) => void
  onTaskUpdated: () => void
}

export function EditTaskDialog({ task, open, onOpenChange, onTaskUpdated }: EditTaskDialogProps) {
  const [nome, setNome] = useState(task.nome || "")
  const [descricao, setDescricao] = useState("")
  const [status, setStatus] = useState(task.status || "")
  const [atividadeId, setAtividadeId] = useState(task.atividadeId || "")
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [activities, setActivities] = useState<ApiActivity[]>([])
  const [statuses, setStatuses] = useState<string[]>([])
  const [loadingActivities, setLoadingActivities] = useState(false)
  const [loadingStatuses, setLoadingStatuses] = useState(false)

  // Buscar atividades e status disponíveis
  useEffect(() => {
    if (!open) return

    async function loadData() {
      try {
        setLoadingActivities(true)
        setLoadingStatuses(true)

        // Buscar atividades
        const activitiesData = await fetchActivities()
        setActivities(activitiesData)

        // Buscar status disponíveis
        setStatuses(AVAILABLE_STATUSES)
      } catch (error) {
        console.error("Erro ao carregar dados:", error)
        toast({
          variant: "destructive",
          title: "Erro ao carregar dados",
          description: "Não foi possível carregar as atividades ou status.",
        })
      } finally {
        setLoadingActivities(false)
        setLoadingStatuses(false)
      }
    }

    loadData()
  }, [open])

  // Reset form when task changes or dialog opens
  useEffect(() => {
    if (open) {
      console.log("Task recebida para edição:", task)
      setNome(task.nome || "")
      // Aqui está o problema - precisamos verificar qual campo está disponível
      if (task.description) {
        setDescricao(task.description)
      } else if ((task as any).descricao) {
        setDescricao((task as any).descricao)
      } else {
        setDescricao("")
        console.warn("Nenhum campo de descrição encontrado na tarefa:", task)
      }
      setStatus(task.status || "")
      setAtividadeId(task.atividadeId || "")
    }
  }, [task, open])

  async function handleUpdateTask() {
    if (!nome.trim()) {
      toast({
        variant: "destructive",
        title: "Campo obrigatório",
        description: "Por favor, preencha o nome da tarefa.",
      })
      return
    }

    if (!descricao.trim()) {
      toast({
        variant: "destructive",
        title: "Campo obrigatório",
        description: "A descrição da tarefa não pode estar vazia.",
      })
      return
    }

    if (!status) {
      toast({
        variant: "destructive",
        title: "Campo obrigatório",
        description: "Por favor, selecione um status.",
      })
      return
    }

    try {
      setIsSubmitting(true)

      // Create update object
      const updatedTask = {
        id: task.id,
        nome,
        descricao: descricao, // Usando descricao para enviar
        status,
        atividadeId: atividadeId || undefined,
      }

      console.log("Enviando atualização:", updatedTask)
      await updateTask(updatedTask)

      toast({
        title: "Tarefa atualizada com sucesso!",
        description: `A tarefa "${nome}" foi atualizada.`,
      })

      onOpenChange(false)
      onTaskUpdated()
    } catch (error) {
      console.error("Erro ao atualizar tarefa:", error)
      toast({
        variant: "destructive",
        title: "Erro ao atualizar tarefa",
        description: "Ocorreu um erro ao tentar atualizar a tarefa. Verifique o console para mais detalhes.",
      })
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-[500px]">
        <DialogHeader>
          <DialogTitle>Editar Tarefa</DialogTitle>
          <DialogDescription>Atualize os detalhes da tarefa. Clique em salvar quando terminar.</DialogDescription>
        </DialogHeader>
        <div className="space-y-4 py-4">
          <div className="space-y-2">
            <label htmlFor="nome" className="text-sm font-medium">
              Nome da Tarefa *
            </label>
            <Input id="nome" value={nome} onChange={(e) => setNome(e.target.value)} placeholder="Nome da tarefa" />
          </div>
          <div className="space-y-2">
            <label htmlFor="descricao" className="text-sm font-medium">
              Descrição *
            </label>
            <Textarea
              id="descricao"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              placeholder="Descrição da tarefa"
              rows={3}
            />
          </div>
          <div className="space-y-2">
            <label htmlFor="status" className="text-sm font-medium">
              Status *
            </label>
            <Select value={status} onValueChange={setStatus}>
              <SelectTrigger id="status">
                <SelectValue placeholder={loadingStatuses ? "Carregando status..." : "Escolha o Status"} />
              </SelectTrigger>
              <SelectContent>
                <SelectGroup>
                  <SelectLabel>Status</SelectLabel>
                  {statuses.map((statusOption) => (
                    <SelectItem key={statusOption} value={statusOption}>
                      {statusOption.replace("_", " ")}
                    </SelectItem>
                  ))}
                </SelectGroup>
              </SelectContent>
            </Select>
          </div>
          <div className="space-y-2">
            <label htmlFor="atividade" className="text-sm font-medium">
              Atividade
            </label>
            <Select value={atividadeId} onValueChange={setAtividadeId}>
              <SelectTrigger id="atividade">
                <SelectValue
                  placeholder={loadingActivities ? "Carregando atividades..." : "Escolha a Atividade (opcional)"}
                />
              </SelectTrigger>
              <SelectContent>
                <SelectGroup>
                  <SelectLabel>Atividades</SelectLabel>
                  <SelectItem value="none">Nenhuma atividade</SelectItem>
                  {activities.map((activity) => (
                    <SelectItem key={activity.id} value={activity.id}>
                      {activity.nomeAtividade}
                    </SelectItem>
                  ))}
                </SelectGroup>
              </SelectContent>
            </Select>
          </div>
        </div>
        <DialogFooter>
          <Button variant="outline" onClick={() => onOpenChange(false)} disabled={isSubmitting}>
            Cancelar
          </Button>
          <Button onClick={handleUpdateTask} disabled={isSubmitting}>
            {isSubmitting ? "Salvando..." : "Salvar alterações"}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
