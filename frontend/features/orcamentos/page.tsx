"use client"

import React from "react"
import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Search, RefreshCw, Filter, ChevronDown, ChevronRight } from "lucide-react"
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { AlertCircle } from "lucide-react"
import { Skeleton } from "@/components/ui/skeleton"
import { Toaster } from "@/components/ui/toaster"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { fetchOrcamentos } from "./api"
import { fetchAtividades } from "@/features/atividades/api"
import type { Orcamento, Despesa } from "./types"
import type { Atividade } from "@/features/atividades/types"
import { EditOrcamentoDialog } from "./components/edit-orcamento-dialog"
import { CreateDespesaDialog } from "./components/create-despesa-dialog"
import { cn } from "@/lib/utils"

export default function OrcamentosPage() {
  const [orcamentos, setOrcamentos] = useState<Orcamento[]>([])
  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [loading, setLoading] = useState(true)
  const [loadingAtividades, setLoadingAtividades] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [searchTerm, setSearchTerm] = useState("")
  const [selectedAtividadeId, setSelectedAtividadeId] = useState<string | undefined>(undefined)
  const [expandedRows, setExpandedRows] = useState<Record<string, boolean>>({})

  // Function to toggle row expansion
  const toggleRowExpansion = (orcamentoId: string) => {
    setExpandedRows((prev) => ({
      ...prev,
      [orcamentoId]: !prev[orcamentoId],
    }))
  }

  // Function to load atividades
  const fetchAtividadesData = async () => {
    try {
      setLoadingAtividades(true)
      const data = await fetchAtividades()
      setAtividades(data)
    } catch (error) {
      console.error("Error fetching atividades:", error)
      // We don't set an error here as it's not critical for the page to function
    } finally {
      setLoadingAtividades(false)
    }
  }

  // Function to load orcamentos with optional filter
  const fetchOrcamentosData = async (atividadeId?: string) => {
    try {
      setLoading(true)
      setError(null)

      const data = await fetchOrcamentos(atividadeId)
      setOrcamentos(data)
    } catch (error) {
      console.error("Error fetching orcamentos:", error)
      setError("Could not load budgets. Please check if the API is running.")
    } finally {
      setLoading(false)
    }
  }

  // Load atividades when component mounts
  useEffect(() => {
    fetchAtividadesData()
  }, [])

  // Load orcamentos when component mounts or when selectedAtividadeId changes
  useEffect(() => {
    fetchOrcamentosData(selectedAtividadeId)
  }, [selectedAtividadeId])

  // Handle atividade selection change
  const handleAtividadeChange = (value: string) => {
    setSelectedAtividadeId(value === "all" ? undefined : value)
  }

  // Handle refresh button click
  const handleRefresh = () => {
    fetchOrcamentosData(selectedAtividadeId)
  }

  // Filter orcamentos based on search term
  const filteredOrcamentos = orcamentos.filter((orcamento) =>
    orcamento.rubrica.nome.toLowerCase().includes(searchTerm.toLowerCase()),
  )

  // Calculate total budget
  const totalBudget = filteredOrcamentos.reduce((total, orcamento) => total + orcamento.gastoPlaneado, 0)

  // Calculate total expenses for a budget
  const calculateTotalExpenses = (despesas?: Despesa[]) => {
    if (!despesas || despesas.length === 0) return 0
    return despesas.reduce((total, despesa) => total + despesa.valor, 0)
  }

  // Loading skeleton component
  const LoadingSkeleton = () => (
    <div className="space-y-2">
      {[1, 2, 3, 4, 5].map((i) => (
        <div key={i} className="flex items-center justify-between p-4 border-b">
          <Skeleton className="h-5 w-64" />
          <Skeleton className="h-5 w-24" />
          <div className="flex items-center gap-2">
            <Skeleton className="h-8 w-8 rounded-md" />
            <Skeleton className="h-8 w-8 rounded-md" />
          </div>
        </div>
      ))}
    </div>
  )

  return (
    <div className="space-y-6">
      <Toaster />
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">Budgets</h1>
        <p className="text-muted-foreground">Manage your project budgets</p>
      </div>

      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input
            placeholder="Search by category..."
            className="pl-9"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="flex items-center gap-2">
          <div className="flex items-center gap-2 min-w-[250px]">
            <Filter className="h-4 w-4 text-muted-foreground" />
            <Select
              value={selectedAtividadeId || "all"}
              onValueChange={handleAtividadeChange}
              disabled={loadingAtividades}
            >
              <SelectTrigger className="w-full">
                <SelectValue placeholder={loadingAtividades ? "Loading..." : "Filter by activity"} />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="all">All activities</SelectItem>
                {atividades.map((atividade) => (
                  <SelectItem key={atividade.id} value={atividade.id}>
                    {atividade.nomeAtividade}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
          <Button variant="outline" onClick={handleRefresh} disabled={loading}>
            <RefreshCw className={`h-4 w-4 mr-2 ${loading ? "animate-spin" : ""}`} />
            Refresh
          </Button>
        </div>
      </div>

      {error && (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertTitle>Error</AlertTitle>
          <AlertDescription className="flex justify-between items-center">
            {error}
            <Button variant="outline" size="sm" onClick={handleRefresh} className="ml-2">
              <RefreshCw className="h-4 w-4 mr-2" />
              Try again
            </Button>
          </AlertDescription>
        </Alert>
      )}

      <Card>
        <CardHeader className="flex flex-row items-center justify-between">
          <CardTitle>
            Budget List
            {selectedAtividadeId && (
              <span className="ml-2 text-sm font-normal text-muted-foreground">
                (Filtered by:{" "}
                {atividades.find((a) => a.id === selectedAtividadeId)?.nomeAtividade || "Selected activity"})
              </span>
            )}
          </CardTitle>
          <div className="text-lg font-semibold">
            Total:{" "}
            {totalBudget.toLocaleString("pt-PT", {
              style: "currency",
              currency: "EUR",
            })}
          </div>
        </CardHeader>
        <CardContent>
          {loading ? (
            <LoadingSkeleton />
          ) : filteredOrcamentos.length > 0 ? (
            <div className="overflow-x-auto">
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead className="w-10"></TableHead>
                    <TableHead>Category</TableHead>
                    <TableHead>Planned Expense</TableHead>
                    <TableHead>Actions</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {filteredOrcamentos.map((orcamento) => (
                    <React.Fragment key={orcamento.id}>
                      <TableRow
                        className={cn("cursor-pointer hover:bg-muted/50", expandedRows[orcamento.id] && "bg-muted/30")}
                        onClick={() => toggleRowExpansion(orcamento.id)}
                      >
                        <TableCell className="p-2">
                          <Button variant="ghost" size="icon" className="h-6 w-6 p-0">
                            {expandedRows[orcamento.id] ? (
                              <ChevronDown className="h-4 w-4" />
                            ) : (
                              <ChevronRight className="h-4 w-4" />
                            )}
                          </Button>
                        </TableCell>
                        <TableCell className="font-medium">{orcamento.rubrica.nome}</TableCell>
                        <TableCell>
                          {orcamento.gastoPlaneado.toLocaleString("pt-PT", {
                            style: "currency",
                            currency: "EUR",
                          })}
                        </TableCell>
                        <TableCell className="text-right">
                          <div className="flex justify-end gap-2" onClick={(e) => e.stopPropagation()}>
                            <EditOrcamentoDialog orcamento={orcamento} onOrcamentoAtualizado={handleRefresh} />
                            <CreateDespesaDialog orcamento={orcamento} onDespesaCreated={handleRefresh} />
                          </div>
                        </TableCell>
                      </TableRow>
                      {expandedRows[orcamento.id] && (
                        <TableRow key={`${orcamento.id}-expenses`} className="bg-muted/10">
                          <TableCell colSpan={4} className="p-0">
                            <div className="p-4 pl-10">
                              <div className="flex justify-between items-center mb-2">
                                <h4 className="text-sm font-semibold">Associated Expenses</h4>
                              </div>
                              {orcamento.despesas && orcamento.despesas.length > 0 ? (
                                <div className="space-y-2">
                                  <div className="grid grid-cols-3 text-xs font-medium text-muted-foreground mb-1">
                                    <div>Description</div>
                                    <div>Value</div>
                                    <div className="text-right">% of Budget</div>
                                  </div>
                                  {orcamento.despesas.map((despesa) => (
                                    <div
                                      key={despesa.id}
                                      className="grid grid-cols-3 text-sm py-1 border-b border-muted"
                                    >
                                      <div>{despesa.descricao}</div>
                                      <div>
                                        {despesa.valor.toLocaleString("pt-PT", {
                                          style: "currency",
                                          currency: "EUR",
                                        })}
                                      </div>
                                      <div className="text-right">
                                        {((despesa.valor / orcamento.gastoPlaneado) * 100).toFixed(1)}%
                                      </div>
                                    </div>
                                  ))}
                                  <div className="grid grid-cols-3 text-sm font-medium pt-2">
                                    <div>Total Expenses</div>
                                    <div>
                                      {calculateTotalExpenses(orcamento.despesas).toLocaleString("pt-PT", {
                                        style: "currency",
                                        currency: "EUR",
                                      })}
                                    </div>
                                    <div className="text-right">
                                      {(
                                        (calculateTotalExpenses(orcamento.despesas) / orcamento.gastoPlaneado) *
                                        100
                                      ).toFixed(1)}
                                      %
                                    </div>
                                  </div>
                                </div>
                              ) : (
                                <div className="text-sm text-muted-foreground py-2">
                                  No expenses associated with this budget.
                                </div>
                              )}
                            </div>
                          </TableCell>
                        </TableRow>
                      )}
                    </React.Fragment>
                  ))}
                </TableBody>
              </Table>
            </div>
          ) : (
            <div className="text-center py-6 text-muted-foreground">
              {searchTerm
                ? "No budgets found for this search."
                : selectedAtividadeId
                  ? "No budgets available for this activity."
                  : "No budgets available."}
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}
