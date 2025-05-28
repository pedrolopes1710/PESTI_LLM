"use client"

import { Calendar } from "@/components/ui/calendar"
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover"
import { Button } from "@/components/ui/button"
import { CalendarIcon } from "@radix-ui/react-icons"
import { cn } from "@/lib/utils"
import { format } from "date-fns"
import { useState } from "react"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import type { Orcamento } from "@/types"
import { CreateOrcamentoDialog } from "@/features/orcamentos/components/create-orcamento-dialog"
import { Plus } from "lucide-react"

interface CreateActivityFormProps {
  orcamentos: Orcamento[]
  loadOrcamentos: () => void
}

export function CreateActivityForm({ orcamentos, loadOrcamentos }: CreateActivityFormProps) {
  const [date, setDate] = useState<Date | undefined>(new Date())
  const [selectedOrcamento, setSelectedOrcamento] = useState<string | null>(null)
  const [orcamentoId, setOrcamentoId] = useState<string | null>(null)
  const [loadingOrcamentos, setLoadingOrcamentos] = useState(false)
  const [showCreateTarefa, setShowCreateTarefa] = useState(false)
  const [showCreateEntregavel, setShowCreateEntregavel] = useState(false)

  const handleOrcamentoCriado = () => {
    loadOrcamentos()
  }

  return (
    <div className="grid gap-4">
      <div className="grid grid-cols-2 gap-4">
        <div>
          <Label htmlFor="name">Nome</Label>
          <Input type="text" id="name" placeholder="Nome da atividade" />
        </div>
        <div>
          <Label htmlFor="date">Data</Label>
          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant={"outline"}
                className={cn("w-[280px] justify-start text-left font-normal", !date && "text-muted-foreground")}
              >
                <CalendarIcon className="mr-2 h-4 w-4" />
                {date ? format(date, "PPP") : <span>Pick a date</span>}
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="start">
              <Calendar
                mode="single"
                selected={date}
                onSelect={setDate}
                disabled={(date) => date > new Date()}
                initialFocus
              />
            </PopoverContent>
          </Popover>
        </div>
      </div>

      <div>
        <Label htmlFor="orcamento">Orçamento</Label>
        <div className="flex gap-2">
          <Select value={orcamentoId} onValueChange={setOrcamentoId}>
            <SelectTrigger className="flex-1">
              <SelectValue placeholder={loadingOrcamentos ? "Carregando..." : "Selecione um orçamento"} />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="none">Nenhum orçamento</SelectItem>
              {orcamentos.map((orcamento) => (
                <SelectItem key={orcamento.id} value={orcamento.id}>
                  {orcamento.gastoPlaneado?.toLocaleString("pt-PT", {
                    style: "currency",
                    currency: "EUR",
                  })}{" "}
                  - {orcamento.rubrica?.nome || "Sem categoria"}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <CreateOrcamentoDialog onOrcamentoCriado={handleOrcamentoCriado} />
        </div>
      </div>

      <div>
        <Label htmlFor="description">Descrição</Label>
        <Input type="text" id="description" placeholder="Descrição da atividade" />
      </div>
      <div>
        <Label htmlFor="tarefas">Tarefas</Label>
        <Button
          type="button"
          variant="ghost"
          size="sm"
          onClick={() => setShowCreateTarefa(true)}
          className="h-6 text-xs px-2 bg-green-600 hover:bg-green-700 text-black [&_svg]:text-black"
        >
          <Plus className="h-3 w-3 mr-1" />
          Criar Nova Tarefa
        </Button>
      </div>
      <div>
        <Label htmlFor="entregaveis">Entregáveis</Label>
        <Button
          type="button"
          variant="ghost"
          size="sm"
          onClick={() => setShowCreateEntregavel(true)}
          className="h-6 text-xs px-2 bg-green-600 hover:bg-green-700 text-black [&_svg]:text-black"
        >
          <Plus className="h-3 w-3 mr-1" />
          Criar Novo Entregável
        </Button>
      </div>
    </div>
  )
}
