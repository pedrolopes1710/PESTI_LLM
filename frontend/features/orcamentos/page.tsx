"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Search, RefreshCw, Filter } from "lucide-react"
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { AlertCircle } from "lucide-react"
import { Skeleton } from "@/components/ui/skeleton"
import { Toaster } from "@/components/ui/toaster"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { fetchOrcamentos } from "./api"
import { fetchAtividades } from "@/features/atividades/api"
import type { Orcamento } from "./types"
import type { Atividade } from "@/features/atividades/types"
import { EditOrcamentoDialog } from "./components/edit-orcamento-dialog"

export default function OrcamentosPage() {
  const [orcamentos, setOrcamentos] = useState<Orcamento[]>([])
  const [atividades, setAtividades] = useState<Atividade[]>([])
  const [loading, setLoading] = useState(true)
  const [loadingAtividades, setLoadingAtividades] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [searchTerm, setSearchTerm] = useState("")
  const [selectedAtividadeId, setSelectedAtividadeId] = useState<string | undefined>(undefined)

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
          <AlertTitle>Erro</AlertTitle>
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
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead className="text-left">Category</TableHead>
                  <TableHead className="text-left">Planned Expense</TableHead>
                  <TableHead className="text-right">Actions</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {filteredOrcamentos.map((orcamento) => (
                  <TableRow key={orcamento.id}>
                    <TableCell className="font-medium">{orcamento.rubrica.nome}</TableCell>
                    <TableCell className="text-left">
                      {orcamento.gastoPlaneado.toLocaleString("pt-PT", {
                        style: "currency",
                        currency: "EUR",
                      })}
                    </TableCell>
                    <TableCell className="text-right">
                      <div className="flex justify-end">
                        <EditOrcamentoDialog orcamento={orcamento} onOrcamentoAtualizado={handleRefresh} />
                      </div>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
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
