"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Badge } from "@/components/ui/badge"
import {
  Calendar,
  Clock,
  Filter,
  MoreHorizontal,
  Plus,
  Search,
  Trash2,
  Edit2,
} from "lucide-react"
import { fetchProjetos, createProjeto, deleteProjeto, updateProjeto } from "./projetoAPI"

export default function ProjectsPage() {
  const [projects, setProjects] = useState<any[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const [showForm, setShowForm] = useState(false)
  const [nome, setNome] = useState("")
  const [descricao, setDescricao] = useState("")

  // Para edição inline
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

  const getStatusColor = (status: string) => {
    switch (status) {
      case "In Progress":
        return "bg-blue-500/20 text-blue-700 hover:bg-blue-500/30"
      case "On Hold":
        return "bg-amber-500/20 text-amber-700 hover:bg-amber-500/30"
      case "Completed":
        return "bg-green-500/20 text-green-700 hover:bg-green-500/30"
      case "Not Started":
        return "bg-slate-500/20 text-slate-700 hover:bg-slate-500/30"
      default:
        return "bg-gray-500/20 text-gray-700 hover:bg-gray-500/30"
    }
  }

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
    if (!confirm("Tem certeza que quer deletar este projeto?")) return

    try {
      const success = await deleteProjeto(id)
      if (success) {
        setProjects((prev) => prev.filter((p) => p.id !== id))
      }
    } catch (error) {
      console.error("Erro ao deletar projeto:", error)
      alert("Erro ao deletar projeto")
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
        setProjects((prev) =>
            prev.map((p) => (p.id === id ? updated : p))
        )
        setEditingProjectId(null)
      }
    } catch (error) {
      console.error("Erro ao atualizar projeto:", error)
      alert("Erro ao atualizar projeto")
    }
  }

  if (loading) return <p>Carregando projetos...</p>
  if (error) return <p className="text-red-500">{error}</p>

  return (
      <div className="space-y-6">
        <div className="flex flex-col gap-2">
          <h1 className="text-3xl font-bold tracking-tight">Projects</h1>
          <p className="text-muted-foreground">Manage and track all your projects</p>
        </div>

        <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
          <div className="flex items-center w-full max-w-sm gap-2 relative">
            <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
            <Input placeholder="Search projects..." className="pl-9" />
          </div>
          <div className="flex items-center gap-2">
            <Button variant="outline">
              <Filter className="h-4 w-4 mr-2" />
              Filter
            </Button>
            <Button onClick={() => setShowForm(true)} className="flex items-center">
              <Plus className="h-4 w-4 mr-2" />
              New Project
            </Button>
          </div>
        </div>

        {showForm && (
            <div className="border border-gray-300 rounded-md p-4 bg-gray-50 max-w-md w-full space-y-4 shadow-sm">
              <h2 className="text-xl font-semibold text-gray-900">Criar Novo Projeto</h2>
              <Input
                  type="text"
                  placeholder="Nome do projeto"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                  className="w-full"
              />
              <Input
                  type="text"
                  placeholder="Descrição do projeto"
                  value={descricao}
                  onChange={(e) => setDescricao(e.target.value)}
                  className="w-full"
              />
              <div className="flex justify-end gap-2">
                <Button variant="outline" onClick={() => setShowForm(false)}>
                  Cancelar
                </Button>
                <Button onClick={handleCreateProject}>Criar</Button>
              </div>
            </div>
        )}

        <Tabs defaultValue="grid" className="space-y-4">
          <TabsList>
            <TabsTrigger value="grid">Grid View</TabsTrigger>
            <TabsTrigger value="list">List View</TabsTrigger>
          </TabsList>

          {/* Grid View */}
          <TabsContent value="grid" className="space-y-4">
            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              {projects.map((project) => (
                  <Card key={project.id}>
                    <CardHeader className="pb-2">
                      <div className="flex items-start justify-between">
                        <div>
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
                                <CardTitle>{project.nome}</CardTitle>
                                <CardDescription className="mt-1">ID: {project.id}</CardDescription>
                              </>
                          )}
                        </div>

                        <DropdownMenu>
                          <DropdownMenuTrigger asChild>
                            <Button variant="ghost" size="icon">
                              <MoreHorizontal className="h-4 w-4" />
                              <span className="sr-only">Actions</span>
                            </Button>
                          </DropdownMenuTrigger>
                          <DropdownMenuContent align="end">
                            {editingProjectId === project.id ? (
                                <>
                                  <DropdownMenuItem onClick={() => handleSaveEdit(project.id)}>
                                    Save
                                  </DropdownMenuItem>
                                  <DropdownMenuItem onClick={() => setEditingProjectId(null)}>
                                    Cancel
                                  </DropdownMenuItem>
                                </>
                            ) : (
                                <>
                                  <DropdownMenuItem onClick={() => startEditing(project)}>
                                    <Edit2 className="mr-2 h-4 w-4" /> Edit Project
                                  </DropdownMenuItem>
                                  <DropdownMenuItem onClick={() => handleDeleteProject(project.id)}>
                                    <Trash2 className="mr-2 h-4 w-4" /> Delete Project
                                  </DropdownMenuItem>
                                </>
                            )}
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </div>
                    </CardHeader>
                    <CardContent className="space-y-4">
                      {editingProjectId === project.id ? null : (
                          <>
                            <p className="text-sm text-muted-foreground">{project.descricao}</p>

                            <div className="flex items-center justify-between text-sm">
                              <Badge variant="outline" className={getStatusColor("Not Started")}>
                                Not Started
                              </Badge>
                              <div className="flex items-center gap-1 text-muted-foreground">
                                <Clock className="h-3 w-3" />
                                <span>--</span>
                              </div>
                            </div>
                          </>
                      )}
                    </CardContent>
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
                                  <div>
                                    <h3 className="font-medium">{project.nome}</h3>
                                    <p className="text-sm text-muted-foreground">ID: {project.id}</p>
                                  </div>
                                  <Badge variant="outline" className={getStatusColor("Not Started")}>
                                    Not Started
                                  </Badge>
                                </div>
                                <p className="text-sm text-muted-foreground mt-1 line-clamp-1">
                                  {project.descricao}
                                </p>
                              </>
                          )}
                        </div>

                        <div className="flex items-center gap-6">
                          <div className="flex items-center gap-1 text-sm text-muted-foreground">
                            <Calendar className="h-3 w-3" />
                            <span>--</span>
                          </div>

                          {editingProjectId === project.id ? (
                              <>
                                <Button
                                    size="sm"
                                    variant="outline"
                                    onClick={() => setEditingProjectId(null)}
                                    className="mr-2"
                                >
                                  Cancel
                                </Button>
                                <Button size="sm" onClick={() => handleSaveEdit(project.id)}>
                                  Save
                                </Button>
                              </>
                          ) : (
                              <DropdownMenu>
                                <DropdownMenuTrigger asChild>
                                  <Button variant="ghost" size="icon">
                                    <MoreHorizontal className="h-4 w-4" />
                                    <span className="sr-only">Actions</span>
                                  </Button>
                                </DropdownMenuTrigger>
                                <DropdownMenuContent align="end">
                                  <DropdownMenuItem onClick={() => startEditing(project)}>
                                    <Edit2 className="mr-2 h-4 w-4" /> Edit Project
                                  </DropdownMenuItem>
                                  <DropdownMenuItem onClick={() => handleDeleteProject(project.id)}>
                                    <Trash2 className="mr-2 h-4 w-4" /> Delete Project
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
