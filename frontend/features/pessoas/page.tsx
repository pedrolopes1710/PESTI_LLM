"use client"

import { useState, useEffect } from "react"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import {
  Mail,
  MoreHorizontal,
  Plus,
  Search,
  Calendar,
  DollarSign,
  Users,
  UserCheck,
  UserX,
  Eye,
  Edit,
  Trash2,
  RefreshCw,
  AlertCircle,
  Minus,
} from "lucide-react"
import { fetchPessoas, togglePessoaStatus, associarProjetos, desassociarProjetos, fetchProjetos } from "./api"
import type { Pessoa } from "./types"
import { toast } from "@/components/ui/use-toast"
import { Toaster } from "@/components/ui/toaster"
import { Skeleton } from "@/components/ui/skeleton"
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { Checkbox } from "@/components/ui/checkbox"

export default function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [searchTerm, setSearchTerm] = useState("")
  const [statusFilter, setStatusFilter] = useState<"all" | "active" | "inactive">("all")
  const [contractTypeFilter, setContractTypeFilter] = useState<string>("all")
  const [sortBy, setSortBy] = useState<"name" | "email" | "salary" | "contractEnd">("name")
  const [viewMode, setViewMode] = useState<"grid" | "list">("grid")
  const [projetos, setProjetos] = useState<any[]>([])
  const [selectedPessoa, setSelectedPessoa] = useState<Pessoa | null>(null)
  const [selectedProjetos, setSelectedProjetos] = useState<string[]>([])
  const [showProjetosDialog, setShowProjetosDialog] = useState(false)
  const [projetosAction, setProjetosAction] = useState<"associar" | "desassociar">("associar")

  // Carregar pessoas e projetos
  useEffect(() => {
    async function loadData() {
      try {
        setLoading(true)
        setError(null)
        const [pessoasData, projetosData] = await Promise.all([fetchPessoas(), fetchProjetos()])
        setPessoas(pessoasData)
        setProjetos(projetosData)
      } catch (error) {
        console.error("Erro ao carregar dados:", error)
        setError("Não foi possível carregar os dados. Verifique se a API está em execução.")
      } finally {
        setLoading(false)
      }
    }

    loadData()
  }, [])

  // Filtrar e ordenar pessoas
  const filteredAndSortedPessoas = pessoas
    .filter((pessoa) => {
      const matchesSearch =
        pessoa.nome.toLowerCase().includes(searchTerm.toLowerCase()) ||
        pessoa.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
        pessoa.pessoaCienciaId.toLowerCase().includes(searchTerm.toLowerCase())

      const matchesStatus =
        statusFilter === "all" ||
        (statusFilter === "active" && pessoa.ativo) ||
        (statusFilter === "inactive" && !pessoa.ativo)

      const matchesContractType = contractTypeFilter === "all" || pessoa.contrato.tipo === contractTypeFilter

      return matchesSearch && matchesStatus && matchesContractType
    })
    .sort((a, b) => {
      switch (sortBy) {
        case "name":
          return a.nome.localeCompare(b.nome)
        case "email":
          return a.email.localeCompare(b.email)
        case "salary":
          return b.contrato.salario - a.contrato.salario
        case "contractEnd":
          return new Date(a.contrato.dataFim).getTime() - new Date(b.contrato.dataFim).getTime()
        default:
          return 0
      }
    })

  // Obter tipos de contrato únicos
  const contractTypes = [...new Set(pessoas.map((p) => p.contrato.tipo))]

  // Estatísticas
  const stats = {
    total: pessoas.length,
    active: pessoas.filter((p) => p.ativo).length,
    inactive: pessoas.filter((p) => !p.ativo).length,
    averageSalary: pessoas.length > 0 ? pessoas.reduce((sum, p) => sum + p.contrato.salario, 0) / pessoas.length : 0,
  }

  // Função para recarregar dados
  const handleRefresh = async () => {
    try {
      setLoading(true)
      setError(null)
      const data = await fetchPessoas()
      setPessoas(data)
      toast({
        title: "Dados atualizados",
        description: "A lista de pessoas foi atualizada com sucesso.",
      })
    } catch (error) {
      console.error("Erro ao recarregar pessoas:", error)
      setError("Não foi possível recarregar as pessoas.")
    } finally {
      setLoading(false)
    }
  }

  // Função para alternar status da pessoa
  const handleToggleStatus = async (pessoa: Pessoa) => {
    try {
      const novoStatus = !pessoa.ativo
      await togglePessoaStatus(pessoa.id, novoStatus)

      // Atualizar a lista local
      setPessoas((prev) => prev.map((p) => (p.id === pessoa.id ? { ...p, ativo: novoStatus } : p)))

      toast({
        title: `Pessoa ${novoStatus ? "ativada" : "desativada"}`,
        description: `${pessoa.nome} foi ${novoStatus ? "ativada" : "desativada"} com sucesso.`,
      })
    } catch (error) {
      console.error("Erro ao alterar status:", error)
      toast({
        title: "Erro",
        description: "Não foi possível alterar o status da pessoa.",
        variant: "destructive",
      })
    }
  }

  // Função para abrir dialog de projetos
  const handleOpenProjetosDialog = (pessoa: Pessoa, action: "associar" | "desassociar") => {
    setSelectedPessoa(pessoa)
    setProjetosAction(action)
    setSelectedProjetos(action === "desassociar" ? pessoa.projetos.map((p) => p.id) : [])
    setShowProjetosDialog(true)
  }

  // Função para gerenciar projetos
  const handleManageProjetos = async () => {
    if (!selectedPessoa) return

    try {
      if (projetosAction === "associar") {
        await associarProjetos(selectedPessoa.id, selectedProjetos)
      } else {
        await desassociarProjetos(selectedPessoa.id, selectedProjetos)
      }

      // Recarregar dados
      const updatedPessoas = await fetchPessoas()
      setPessoas(updatedPessoas)

      toast({
        title: `Projetos ${projetosAction === "associar" ? "associados" : "desassociados"}`,
        description: `Os projetos foram ${projetosAction === "associar" ? "associados" : "desassociados"} com sucesso.`,
      })

      setShowProjetosDialog(false)
      setSelectedPessoa(null)
      setSelectedProjetos([])
    } catch (error) {
      console.error("Erro ao gerenciar projetos:", error)
      toast({
        title: "Erro",
        description: `Não foi possível ${projetosAction} os projetos.`,
        variant: "destructive",
      })
    }
  }

  // Componente de carregamento
  const LoadingSkeleton = () => (
    <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
      {[1, 2, 3, 4, 5, 6, 7, 8].map((i) => (
        <Card key={i} className="opacity-70 h-[280px]">
          <CardHeader className="pb-3">
            <div className="space-y-2">
              <Skeleton className="h-4 w-full" />
              <Skeleton className="h-3 w-3/4" />
            </div>
          </CardHeader>
          <CardContent>
            <div className="space-y-3">
              <Skeleton className="h-3 w-full" />
              <Skeleton className="h-3 w-full" />
              <Skeleton className="h-3 w-3/4" />
              <Skeleton className="h-3 w-full" />
              <div className="pt-2 border-t space-y-2">
                <Skeleton className="h-3 w-full" />
                <Skeleton className="h-3 w-3/4" />
              </div>
            </div>
          </CardContent>
        </Card>
      ))}
    </div>
  )

  // Renderizar card de pessoa
  const renderPessoaCard = (pessoa: Pessoa) => (
    <Card key={pessoa.id} className="hover:shadow-md transition-shadow h-full flex flex-col">
      <CardHeader className="pb-3 flex-shrink-0">
        <div className="flex items-start justify-between">
          <div className="min-w-0 flex-1">
            <CardTitle className="text-base leading-tight">{pessoa.nome}</CardTitle>
            <CardDescription className="flex items-center gap-1 text-xs">
              <Mail className="h-3 w-3 flex-shrink-0" />
              <span className="truncate">{pessoa.email}</span>
            </CardDescription>
          </div>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" size="icon" className="h-8 w-8 flex-shrink-0">
                <MoreHorizontal className="h-4 w-4" />
                <span className="sr-only">Actions</span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem>
                <Eye className="h-4 w-4 mr-2" />
                Ver Detalhes
              </DropdownMenuItem>
              <DropdownMenuItem>
                <Edit className="h-4 w-4 mr-2" />
                Editar
              </DropdownMenuItem>
              <DropdownMenuItem onClick={() => handleOpenProjetosDialog(pessoa, "associar")}>
                <Plus className="h-4 w-4 mr-2" />
                Associar Projetos
              </DropdownMenuItem>
              {pessoa.projetos && pessoa.projetos.length > 0 && (
                <DropdownMenuItem onClick={() => handleOpenProjetosDialog(pessoa, "desassociar")}>
                  <Minus className="h-4 w-4 mr-2" />
                  Desassociar Projetos
                </DropdownMenuItem>
              )}
              <DropdownMenuItem onClick={() => handleToggleStatus(pessoa)}>
                {pessoa.ativo ? (
                  <>
                    <UserX className="h-4 w-4 mr-2" />
                    Desativar
                  </>
                ) : (
                  <>
                    <UserCheck className="h-4 w-4 mr-2" />
                    Ativar
                  </>
                )}
              </DropdownMenuItem>
              <DropdownMenuItem className="text-red-600">
                <Trash2 className="h-4 w-4 mr-2" />
                Remover
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </CardHeader>

      <CardContent className="space-y-3 flex-1 flex flex-col">
        <div className="space-y-2 flex-1">
          <div className="flex items-center justify-between text-sm">
            <span className="text-muted-foreground">Status:</span>
            <Badge
              variant={pessoa.ativo ? "default" : "secondary"}
              className={`text-xs ${pessoa.ativo ? "bg-green-500/20 text-green-700" : ""}`}
            >
              {pessoa.ativo ? "Ativo" : "Inativo"}
            </Badge>
          </div>

          <div className="flex items-center justify-between text-sm">
            <span className="text-muted-foreground">Tipo:</span>
            <Badge variant="outline" className="text-xs">
              {pessoa.contrato.tipo}
            </Badge>
          </div>

          <div className="flex items-center justify-between text-sm">
            <span className="text-muted-foreground">Salário:</span>
            <span className="font-medium text-sm">€{pessoa.contrato.salario.toLocaleString()}</span>
          </div>

          <div className="flex items-center justify-between text-sm">
            <span className="text-muted-foreground">ID Ciência:</span>
            <span className="font-mono text-xs truncate max-w-[100px]" title={pessoa.pessoaCienciaId}>
              {pessoa.pessoaCienciaId}
            </span>
          </div>
        </div>

        <div className="pt-2 border-t mt-auto">
          <div className="text-xs text-muted-foreground space-y-1">
            <div className="flex justify-between items-center">
              <span>Contrato:</span>
              <span className="text-right">
                {new Date(pessoa.contrato.dataInicio).toLocaleDateString()} -{" "}
                {new Date(pessoa.contrato.dataFim).toLocaleDateString()}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span>Último Pagamento:</span>
              <span>{new Date(pessoa.pessoaUltimoPedPagam).toLocaleDateString()}</span>
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  )

  // Renderizar item de lista
  const renderPessoaListItem = (pessoa: Pessoa) => (
    <Card key={pessoa.id} className="mb-2">
      <CardContent className="p-4">
        <div className="flex items-center justify-between">
          <div className="flex-1 grid grid-cols-1 md:grid-cols-4 gap-4">
            <div>
              <div className="font-medium">{pessoa.nome}</div>
              <div className="text-sm text-muted-foreground flex items-center gap-1">
                <Mail className="h-3 w-3" />
                {pessoa.email}
              </div>
            </div>

            <div className="flex items-center gap-2">
              <Badge
                variant={pessoa.ativo ? "default" : "secondary"}
                className={pessoa.ativo ? "bg-green-500/20 text-green-700" : ""}
              >
                {pessoa.ativo ? "Ativo" : "Inativo"}
              </Badge>
              <Badge variant="outline">{pessoa.contrato.tipo}</Badge>
            </div>

            <div className="text-sm">
              <div className="font-medium">€{pessoa.contrato.salario.toLocaleString()}</div>
              <div className="text-muted-foreground">Salário</div>
            </div>

            <div className="text-sm">
              <div className="font-medium">{new Date(pessoa.contrato.dataFim).toLocaleDateString()}</div>
              <div className="text-muted-foreground">Fim do Contrato</div>
            </div>
          </div>

          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" size="icon">
                <MoreHorizontal className="h-4 w-4" />
                <span className="sr-only">Actions</span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem>
                <Eye className="h-4 w-4 mr-2" />
                Ver Detalhes
              </DropdownMenuItem>
              <DropdownMenuItem>
                <Edit className="h-4 w-4 mr-2" />
                Editar
              </DropdownMenuItem>
              <DropdownMenuItem>
                <Calendar className="h-4 w-4 mr-2" />
                Ver Projetos
              </DropdownMenuItem>
              <DropdownMenuItem className="text-red-600">
                <Trash2 className="h-4 w-4 mr-2" />
                Remover
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </CardContent>
    </Card>
  )

  return (
    <div className="space-y-6">
      <Toaster />

      {/* Header */}
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">Pessoas</h1>
        <p className="text-muted-foreground">Gerencie todas as pessoas e seus contratos</p>
      </div>

      {/* Estatísticas */}
      <div className="grid gap-4 grid-cols-2 lg:grid-cols-4">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between pb-2">
            <CardTitle className="text-sm font-medium">Total de Pessoas</CardTitle>
            <Users className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{stats.total}</div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between pb-2">
            <CardTitle className="text-sm font-medium">Pessoas Ativas</CardTitle>
            <UserCheck className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold text-green-600">{stats.active}</div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between pb-2">
            <CardTitle className="text-sm font-medium">Pessoas Inativas</CardTitle>
            <UserX className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold text-red-600">{stats.inactive}</div>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between pb-2">
            <CardTitle className="text-sm font-medium">Salário Médio</CardTitle>
            <DollarSign className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">€{Math.round(stats.averageSalary).toLocaleString()}</div>
          </CardContent>
        </Card>
      </div>

      {/* Controles */}
      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input
            placeholder="Buscar pessoas..."
            className="pl-9"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>

        <div className="flex items-center gap-2">
          <Select value={statusFilter} onValueChange={(value: any) => setStatusFilter(value)}>
            <SelectTrigger className="w-32">
              <SelectValue placeholder="Status" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Todos</SelectItem>
              <SelectItem value="active">Ativos</SelectItem>
              <SelectItem value="inactive">Inativos</SelectItem>
            </SelectContent>
          </Select>

          <Select value={contractTypeFilter} onValueChange={setContractTypeFilter}>
            <SelectTrigger className="w-40">
              <SelectValue placeholder="Tipo de Contrato" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Todos os Tipos</SelectItem>
              {contractTypes.map((type) => (
                <SelectItem key={type} value={type}>
                  {type}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>

          <Select value={sortBy} onValueChange={(value: any) => setSortBy(value)}>
            <SelectTrigger className="w-32">
              <SelectValue placeholder="Ordenar" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="name">Nome</SelectItem>
              <SelectItem value="email">Email</SelectItem>
              <SelectItem value="salary">Salário</SelectItem>
              <SelectItem value="contractEnd">Fim Contrato</SelectItem>
            </SelectContent>
          </Select>

          <Button variant="outline" onClick={handleRefresh} disabled={loading}>
            <RefreshCw className={`h-4 w-4 mr-2 ${loading ? "animate-spin" : ""}`} />
            Atualizar
          </Button>

          <Button>
            <Plus className="h-4 w-4 mr-2" />
            Nova Pessoa
          </Button>
        </div>
      </div>

      {/* Erro */}
      {error && (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertTitle>Erro</AlertTitle>
          <AlertDescription className="flex justify-between items-center">
            {error}
            <Button variant="outline" size="sm" onClick={handleRefresh} className="ml-2">
              <RefreshCw className="h-4 w-4 mr-2" />
              Tentar novamente
            </Button>
          </AlertDescription>
        </Alert>
      )}

      {/* Tabs para diferentes visualizações */}
      <Tabs value={viewMode} onValueChange={(value: any) => setViewMode(value)} className="space-y-4">
        <TabsList>
          <TabsTrigger value="grid">Visualização em Grade</TabsTrigger>
          <TabsTrigger value="list">Visualização em Lista</TabsTrigger>
        </TabsList>

        <TabsContent value="grid" className="space-y-4">
          {loading ? (
            <LoadingSkeleton />
          ) : filteredAndSortedPessoas.length > 0 ? (
            <div className="grid gap-4 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 auto-rows-fr">
              {filteredAndSortedPessoas.map(renderPessoaCard)}
            </div>
          ) : (
            <Card>
              <CardContent className="flex flex-col items-center justify-center py-12">
                <div className="rounded-full bg-muted p-3 mb-4">
                  <Users className="h-6 w-6 text-muted-foreground" />
                </div>
                <h3 className="text-lg font-medium mb-2">Nenhuma pessoa encontrada</h3>
                <p className="text-muted-foreground text-center max-w-md mb-6">
                  {searchTerm || statusFilter !== "all" || contractTypeFilter !== "all"
                    ? "Não encontramos nenhuma pessoa correspondente aos filtros aplicados."
                    : "Você ainda não tem nenhuma pessoa cadastrada."}
                </p>
                <Button>
                  <Plus className="h-4 w-4 mr-2" />
                  Adicionar Primeira Pessoa
                </Button>
              </CardContent>
            </Card>
          )}
        </TabsContent>

        <TabsContent value="list" className="space-y-4">
          {loading ? (
            <LoadingSkeleton />
          ) : filteredAndSortedPessoas.length > 0 ? (
            <div className="space-y-2">{filteredAndSortedPessoas.map(renderPessoaListItem)}</div>
          ) : (
            <Card>
              <CardContent className="flex flex-col items-center justify-center py-12">
                <div className="rounded-full bg-muted p-3 mb-4">
                  <Users className="h-6 w-6 text-muted-foreground" />
                </div>
                <h3 className="text-lg font-medium mb-2">Nenhuma pessoa encontrada</h3>
                <p className="text-muted-foreground text-center max-w-md mb-6">
                  {searchTerm || statusFilter !== "all" || contractTypeFilter !== "all"
                    ? "Não encontramos nenhuma pessoa correspondente aos filtros aplicados."
                    : "Você ainda não tem nenhuma pessoa cadastrada."}
                </p>
                <Button>
                  <Plus className="h-4 w-4 mr-2" />
                  Adicionar Primeira Pessoa
                </Button>
              </CardContent>
            </Card>
          )}
        </TabsContent>
      </Tabs>
      {/* Dialog para gerenciar projetos */}
      <Dialog open={showProjetosDialog} onOpenChange={setShowProjetosDialog}>
        <DialogContent className="sm:max-w-md">
          <DialogHeader>
            <DialogTitle>{projetosAction === "associar" ? "Associar" : "Desassociar"} Projetos</DialogTitle>
            <DialogDescription>
              {projetosAction === "associar"
                ? `Selecione os projetos para associar a ${selectedPessoa?.nome}`
                : `Selecione os projetos para desassociar de ${selectedPessoa?.nome}`}
            </DialogDescription>
          </DialogHeader>

          <div className="space-y-4">
            <div className="max-h-60 overflow-y-auto space-y-2">
              {(projetosAction === "associar" ? projetos : selectedPessoa?.projetos || []).map((projeto) => (
                <div key={projeto.id} className="flex items-center space-x-2">
                  <Checkbox
                    id={projeto.id}
                    checked={selectedProjetos.includes(projeto.id)}
                    onCheckedChange={(checked) => {
                      if (checked) {
                        setSelectedProjetos((prev) => [...prev, projeto.id])
                      } else {
                        setSelectedProjetos((prev) => prev.filter((id) => id !== projeto.id))
                      }
                    }}
                  />
                  <label
                    htmlFor={projeto.id}
                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                  >
                    {projeto.nome}
                  </label>
                </div>
              ))}
            </div>
          </div>

          <DialogFooter>
            <Button variant="outline" onClick={() => setShowProjetosDialog(false)}>
              Cancelar
            </Button>
            <Button onClick={handleManageProjetos} disabled={selectedProjetos.length === 0}>
              {projetosAction === "associar" ? "Associar" : "Desassociar"}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
}
