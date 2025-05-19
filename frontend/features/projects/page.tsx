"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { MoreHorizontal, Plus, Search, Trash2, Edit2, Filter } from "lucide-react"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { fetchProjetos, createProjeto, deleteProjeto, updateProjeto } from "./projetoAPI"

export default function ProjectsPage() {
  const [projects, setProjects] = useState<any[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const [showForm, setShowForm] = useState(false)
  const [nome, setNome] = useState("")
  const [descricao, setDescricao] = useState("")

  const [editingProjectId, setEditingProjectId] = useState<string | null>(null)
  const [editNome, setEditNome] = useState("")
  const [editDescricao, setEditDescricao] = useState("")

  useEffect(() => {
    async function loadProjects() {
      try {
        const data = await fetchProjetos()
        setProjects(data)
      } catch (err) {
        setError("Erro ao carregar projetos.")
        console.error(err)
      } finally {
        setLoading(false)
      }
    }
    loadProjects()
  }, [])

  async function handleCreateProject() {
    if (!nome.trim() || !descricao.trim()) {
      alert("Por favor, preencha nome e descrição.")
      return
    }
    try {
      const newProject = await createProjeto({ nome, descricao })
      setProjects((prev) => [...prev, newProject])
      setNome("")
      setDescricao("")
      setShowForm(false)
    } catch (error) {
      console.error("Erro ao criar projeto:", error)
      alert("Erro ao criar projeto")
    }
  }

  async function handleDeleteProject(id: string) {
    if (!confirm("Tem certeza que quer apagar este projeto?")) return
    try {
      const success = await deleteProjeto(id)
      if (success) {
        setProjects((prev) => prev.filter((p) => p.id !== id))
      }
    } catch (error) {
      console.error("Erro ao deletar projeto:", error)
      alert("Erro ao apagar projeto")
    }
  }

  function startEditing(project: any) {
    setEditingProjectId(project.id)
    setEditNome(project.nome)
    setEditDescricao(project.descricao)
  }

  async function handleSaveEdit(id: string) {
    if (!editNome.trim() || !editDescricao.trim()) {
      alert("Preencha nome e descrição para editar.")
      return
    }
    try {
      const updated = await updateProjeto(id, { nome: editNome, descricao: editDescricao })
      if (updated) {
        setProjects((prev) => prev.map((p) => (p.id === id ? updated : p)))
        setEditingProjectId(null)
      }
    } catch (error) {
      console.error("Erro ao atualizar projeto:", error)
      alert("Erro ao atualizar projeto")
    }
  }

  if (loading) return <p>🔄 A carregar projetos...</p>
  if (error) return <p className="text-red-500">{error}</p>

  return (
      <div className="space-y-6">
        <header className="flex flex-col gap-2">
          <h1 className="text-3xl font-bold tracking-tight text-primary">📁 Projetos</h1>
          <p className="text-muted-foreground">Acompanha, edita e organiza os teus projetos facilmente</p>
        </header>

        <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
          <div className="relative w-full max-w-sm">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground" />
            <Input placeholder="    Procurar projetos..." className="pl-9" />
          </div>
          <div className="flex items-center gap-2">
            <Button variant="outline" className="flex items-center gap-1">
              <Filter className="w-4 h-4" />
              Filtro
            </Button>
            <Button
                onClick={() => setShowForm(true)}
                className="flex items-center gap-1 bg-blue-600 text-white hover:bg-blue-700"
            >
              <Plus className="w-4 h-4" />
              Novo Projeto
            </Button>
          </div>
        </div>

        {showForm && (
            <Card className="max-w-md p-6 shadow-lg">
              <h2 className="text-lg font-semibold mb-4">🆕 Criar Novo Projeto</h2>
              <Input
                  placeholder="Nome do projeto"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                  className="mb-3"
              />
              <Input
                  placeholder="Descrição do projeto"
                  value={descricao}
                  onChange={(e) => setDescricao(e.target.value)}
                  className="mb-6"
              />
              <div className="flex justify-end gap-3">
                <Button variant="ghost" onClick={() => setShowForm(false)}>
                  Cancelar
                </Button>
                <Button onClick={handleCreateProject}>Criar</Button>
              </div>
            </Card>
        )}

        <Tabs defaultValue="grid" className="space-y-4">
          <TabsList>
            <TabsTrigger value="grid">🧱 Visualização em Grid</TabsTrigger>
            <TabsTrigger value="list">📄 Lista</TabsTrigger>
          </TabsList>

          {/* Grid View */}
          <TabsContent value="grid">
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {projects.map((project) => (
                  <Card
                      key={project.id}
                      className="shadow-md hover:shadow-lg transition-all duration-200"
                  >
                    <CardHeader>
                      {editingProjectId === project.id ? (
                          <>
                            <Input
                                value={editNome}
                                onChange={(e) => setEditNome(e.target.value)}
                                className="mb-2"
                            />
                            <Input
                                value={editDescricao}
                                onChange={(e) => setEditDescricao(e.target.value)}
                                className="mb-2"
                            />
                            <div className="flex justify-end gap-2">
                              <Button variant="outline" onClick={() => setEditingProjectId(null)}>
                                ❌ Cancelar
                              </Button>
                              <Button onClick={() => handleSaveEdit(project.id)}>Salvar</Button>
                            </div>
                          </>
                      ) : (
                          <div className="flex justify-between items-start">
                            <div>
                              <CardTitle className="text-lg font-semibold text-primary">
                                {project.nome}
                              </CardTitle>
                              <CardDescription className="text-sm text-muted-foreground">
                                ID: {project.id}
                              </CardDescription>
                            </div>
                            <DropdownMenu>
                              <DropdownMenuTrigger asChild>
                                <Button variant="ghost" size="icon">
                                  <MoreHorizontal className="w-4 h-4" />
                                </Button>
                              </DropdownMenuTrigger>
                              <DropdownMenuContent align="end">
                                <DropdownMenuItem onClick={() => startEditing(project)}>
                                  <Edit2 className="w-4 h-4 mr-2" /> Editar
                                </DropdownMenuItem>
                                <DropdownMenuItem onClick={() => handleDeleteProject(project.id)}>
                                  <Trash2 className="w-4 h-4 mr-2" /> Apagar
                                </DropdownMenuItem>
                              </DropdownMenuContent>
                            </DropdownMenu>
                          </div>
                      )}
                    </CardHeader>
                    {!editingProjectId || editingProjectId !== project.id ? (
                        <CardContent className="text-sm text-muted-foreground">{project.descricao}</CardContent>
                    ) : null}
                  </Card>
              ))}
            </div>
          </TabsContent>

          {/* List View */}
          <TabsContent value="list" className="space-y-4">
            <Card>
              <CardContent className="p-0">
                <div className="divide-y">
                  {projects.map((project) => (
                      <div
                          key={project.id}
                          className="flex flex-col sm:flex-row sm:items-center gap-4 p-4 hover:bg-muted/50"
                      >
                        <div className="flex-1 min-w-0">
                          {editingProjectId === project.id ? (
                              <>
                                <Input
                                    value={editNome}
                                    onChange={(e) => setEditNome(e.target.value)}
                                    className="mb-1"
                                />
                                <Input
                                    value={editDescricao}
                                    onChange={(e) => setEditDescricao(e.target.value)}
                                />
                              </>
                          ) : (
                              <>
                                <div className="flex items-start justify-between">
                                  <h3 className="font-medium">{project.nome}</h3>
                                  {/* No status badge */}
                                </div>
                                <p className="text-sm text-muted-foreground mt-1 line-clamp-1">
                                  {project.descricao}
                                </p>
                              </>
                          )}
                        </div>

                        <div className="flex items-center gap-2">
                          {editingProjectId === project.id ? (
                              <>
                                <Button
                                    size="sm"
                                    variant="outline"
                                    onClick={() => setEditingProjectId(null)}
                                    className="mr-2"
                                >
                                  Cancelar
                                </Button>
                                <Button size="sm" onClick={() => handleSaveEdit(project.id)}>
                                  Salvar
                                </Button>
                              </>
                          ) : (
                              <DropdownMenu>
                                <DropdownMenuTrigger asChild>
                                  <Button variant="ghost" size="icon">
                                    <MoreHorizontal className="w-4 h-4" />
                                    <span className="sr-only">Ações</span>
                                  </Button>
                                </DropdownMenuTrigger>
                                <DropdownMenuContent align="end">
                                  <DropdownMenuItem onClick={() => startEditing(project)}>
                                    <Edit2 className="mr-2 w-4 h-4" /> Editar
                                  </DropdownMenuItem>
                                  <DropdownMenuItem onClick={() => handleDeleteProject(project.id)}>
                                    <Trash2 className="mr-2 w-4 h-4" /> Apagar
                                  </DropdownMenuItem>
                                </DropdownMenuContent>
                              </DropdownMenu>
                          )}
                        </div>
                      </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </TabsContent>
        </Tabs>
      </div>
  )
}
