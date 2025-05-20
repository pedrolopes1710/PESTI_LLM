"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Search, RefreshCw } from "lucide-react"
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { AlertCircle } from "lucide-react"
import { Skeleton } from "@/components/ui/skeleton"
import { Toaster } from "@/components/ui/toaster"
import { fetchRubricas } from "./api"
import type { Rubrica } from "./types"
import { CreateRubricaDialog } from "./components/create-rubrica-dialog"
import { DeleteRubricaDialog } from "./components/delete-rubrica-dialog"

export default function RubricasPage() {
  const [rubricas, setRubricas] = useState<Rubrica[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [searchTerm, setSearchTerm] = useState("")

  // Function to load data from API
  const fetchData = async () => {
    try {
      setLoading(true)
      setError(null)

      const data = await fetchRubricas()
      setRubricas(data)
    } catch (error) {
      console.error("Error fetching rubricas:", error)
      setError("Não foi possível carregar as rubricas. Verifique se a API está em execução.")
    } finally {
      setLoading(false)
    }
  }

  // Load data when component mounts
  useEffect(() => {
    fetchData()
  }, [])

  // Filter rubricas based on search term
  const filteredRubricas = rubricas.filter((rubrica) => rubrica.nome.toLowerCase().includes(searchTerm.toLowerCase()))

  // Loading skeleton component
  const LoadingSkeleton = () => (
    <div className="space-y-2">
      {[1, 2, 3, 4, 5].map((i) => (
        <div key={i} className="flex items-center justify-between p-4 border-b">
          <Skeleton className="h-5 w-64" />
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
        <h1 className="text-3xl font-bold tracking-tight">Rubricas</h1>
        <p className="text-muted-foreground">Gerencie as rubricas do seu projeto</p>
      </div>

      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input
            placeholder="Pesquisar rubricas..."
            className="pl-9"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="flex items-center gap-2">
          <Button variant="outline" onClick={fetchData} disabled={loading}>
            <RefreshCw className={`h-4 w-4 mr-2 ${loading ? "animate-spin" : ""}`} />
            Atualizar
          </Button>
          <CreateRubricaDialog onRubricaCreated={fetchData} />
        </div>
      </div>

      {error && (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertTitle>Erro</AlertTitle>
          <AlertDescription className="flex justify-between items-center">
            {error}
            <Button variant="outline" size="sm" onClick={fetchData} className="ml-2">
              <RefreshCw className="h-4 w-4 mr-2" />
              Tentar novamente
            </Button>
          </AlertDescription>
        </Alert>
      )}

      <Card>
        <CardHeader>
          <CardTitle>Lista de Rubricas</CardTitle>
        </CardHeader>
        <CardContent>
          {loading ? (
            <LoadingSkeleton />
          ) : filteredRubricas.length > 0 ? (
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Nome da Rubrica</TableHead>
                  <TableHead className="text-right">Ações</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {filteredRubricas.map((rubrica) => (
                  <TableRow key={rubrica.id}>
                    <TableCell className="font-medium">{rubrica.nome}</TableCell>
                    <TableCell className="text-right">
                      <div className="flex justify-end">
                        <DeleteRubricaDialog
                          rubricaId={rubrica.id}
                          rubricaName={rubrica.nome}
                          onRubricaDeleted={fetchData}
                        />
                      </div>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          ) : (
            <div className="text-center py-6 text-muted-foreground">
              {searchTerm ? "Nenhuma rubrica encontrada para a pesquisa." : "Nenhuma rubrica disponível."}
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}
