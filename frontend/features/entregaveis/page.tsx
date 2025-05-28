"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { MoreHorizontal, Plus, Trash2, Edit2, Save, X } from "lucide-react"

import {
  fetchEntregaveis,
  createEntregavel,
  updateEntregavel,
  deleteEntregavel,
} from "./entregaveisAPI" // vou supor que você tenha algo assim

import { fetchTiposEntregavel } from "@/features/tiposEntregaveis/tiposEntregaveisAPI" // vou supor que você tenha algo assim

export default function EntregaveisPage() {
  const [entregaveis, setEntregaveis] = useState<any[]>([])
  const [tipoEntregaveis, setTipoEntregaveis] = useState<any[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const [showForm, setShowForm] = useState(false)
  const [nome, setNome] = useState("")
  const [descricao, setDescricao] = useState("")
  const [data, setData] = useState("")
  const [tipoId, setTipoId] = useState("")
  const [atividadeId, setAtividadeId] = useState("")

  const [formErrors, setFormErrors] = useState<any>({})

  const [editingId, setEditingId] = useState<string | null>(null)

  // Para editar, vamos manter os campos do formulário
  // reutilizamos os mesmos states nome, descricao, data, tipoId, atividadeId

  useEffect(() => {
    async function load() {
      try {
        const data = await fetchEntregaveis()
        setEntregaveis(data)
      } catch {
        setError("Erro ao carregar entregáveis")
      }

      try {
        const tipos = await fetchTiposEntregavel()
        setTipoEntregaveis(tipos)
      } catch {
        // Pode ignorar ou tratar erro
      }

      setLoading(false)
    }
    load()
  }, [])

  function validarFormulario() {
    const errors: any = {}
    if (!nome.trim()) errors.nome = "Nome é obrigatório"
    if (!descricao.trim()) errors.descricao = "Descrição é obrigatória"
    if (!data) errors.data = "Data é obrigatória"
    if (!tipoId) errors.tipoId = "Selecione o tipo de entregável"
    setFormErrors(errors)
    return Object.keys(errors).length === 0
  }

  function resetForm() {
    setNome("")
    setDescricao("")
    setData("")
    setTipoId("")
    setAtividadeId("")
    setFormErrors({})
  }

  async function handleCreate() {
    if (!validarFormulario()) return
    try {
      const novo = await createEntregavel({
        nome,
        descricao,
        data,
        tipoEntregavel: { id: tipoId },
        atividadeId: atividadeId || undefined,
      })
      setEntregaveis((prev) => [...prev, novo])
      resetForm()
      setShowForm(false)
    } catch (err) {
      alert("Erro ao criar entregável")
      console.error(err)
    }
  }

  function startEdit(entregavel: any) {
    setEditingId(entregavel.id)
    setNome(entregavel.nome)
    setDescricao(entregavel.descricao)
    setData(entregavel.data.slice(0, 10)) // corta para yyyy-mm-dd
    setTipoId(entregavel.tipoEntregavel?.id || "")
    setAtividadeId(entregavel.atividadeId || "")
    setFormErrors({})
    setShowForm(false) // Fecha form novo, abre edição inline
  }

  async function handleSaveEdit() {
    if (!validarFormulario() || !editingId) return
    try {
      const updated = await updateEntregavel(editingId, {
        id: editingId,
        nome,
        descricao,
        data,
        tipoEntregavel: { id: tipoId },
        atividadeId: atividadeId || undefined,
      })
      setEntregaveis((prev) =>
        prev.map((e) => (e.id === editingId ? updated : e))
      )
      resetForm()
      setEditingId(null)
    } catch (err) {
      alert("Erro ao atualizar entregável")
      console.error(err)
    }
  }

  async function handleDelete(id: string) {
    if (!confirm("Quer mesmo apagar este entregável?")) return
    try {
      const ok = await deleteEntregavel(id)
      if (ok) {
        setEntregaveis((prev) => prev.filter((e) => e.id !== id))
      }
    } catch {
      alert("Erro ao apagar entregável")
    }
  }

  if (loading) return <p>🔄 Carregando entregáveis...</p>
  if (error) return <p className="text-red-500">{error}</p>

  return (
    <div className="max-w-4xl mx-auto p-4 space-y-6">
      <h1 className="text-2xl font-bold">📦 Entregáveis</h1>

      {/* Botão para abrir formulário novo */}
      {!editingId && (
        <Button
          onClick={() => {
            resetForm()
            setShowForm(!showForm)
          }}
          className="flex items-center gap-2 bg-blue-600 text-white hover:bg-blue-700"
        >
          <Plus className="w-4 h-4" /> {showForm ? "Fechar Formulário" : "Novo Entregável"}
        </Button>
      )}

      {/* Formulário novo entregável */}
      {showForm && !editingId && (
        <div className="border border-gray-300 rounded-xl p-4 bg-white shadow space-y-4">
          <Input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
          {formErrors.nome && <p className="text-red-500 text-sm">{formErrors.nome}</p>}

          <Textarea
            placeholder="Descrição"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          />
          {formErrors.descricao && (
            <p className="text-red-500 text-sm">{formErrors.descricao}</p>
          )}

          <label className="block">
            Data
            <Input
              type="date"
              value={data}
              onChange={(e) => setData(e.target.value)}
            />
          </label>
          {formErrors.data && <p className="text-red-500 text-sm">{formErrors.data}</p>}

          <label className="block">
            Tipo de Entregável
            <select
              className="w-full border rounded-md p-2 mt-1"
              value={tipoId}
              onChange={(e) => setTipoId(e.target.value)}
            >
              <option value="">Selecione o tipo</option>
              {tipoEntregaveis.map((tipo) => (
                <option key={tipo.id} value={tipo.id}>
                  {tipo.nome}
                </option>
              ))}
            </select>
          </label>
          {formErrors.tipoId && <p className="text-red-500 text-sm">{formErrors.tipoId}</p>}

          <Input
            placeholder="Atividade (opcional)"
            value={atividadeId}
            onChange={(e) => setAtividadeId(e.target.value)}
          />

          <div className="flex justify-end gap-2">
            <Button variant="outline" onClick={() => setShowForm(false)}>
              Cancelar
            </Button>
            <Button onClick={handleCreate}>Criar</Button>
          </div>
        </div>
      )}

      {/* Edição inline */}
      {editingId && (
        <div className="border border-gray-300 rounded-xl p-4 bg-yellow-50 shadow space-y-4">
          <h2 className="font-semibold text-lg">✏️ Editar Entregável</h2>

          <Input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
          {formErrors.nome && <p className="text-red-500 text-sm">{formErrors.nome}</p>}

          <Textarea
            placeholder="Descrição"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          />
          {formErrors.descricao && (
            <p className="text-red-500 text-sm">{formErrors.descricao}</p>
          )}

          <label className="block">
            Data
            <Input
              type="date"
              value={data}
              onChange={(e) => setData(e.target.value)}
            />
          </label>
          {formErrors.data && <p className="text-red-500 text-sm">{formErrors.data}</p>}

          <label className="block">
            Tipo de Entregável
            <select
              className="w-full border rounded-md p-2 mt-1"
              value={tipoId}
              onChange={(e) => setTipoId(e.target.value)}
            >
              <option value="">Selecione o tipo</option>
              {tipoEntregaveis.map((tipo) => (
                <option key={tipo.id} value={tipo.id}>
                  {tipo.nome}
                </option>
              ))}
            </select>
          </label>
          {formErrors.tipoId && <p className="text-red-500 text-sm">{formErrors.tipoId}</p>}

          <Input
            placeholder="Atividade (opcional)"
            value={atividadeId}
            onChange={(e) => setAtividadeId(e.target.value)}
          />

          <div className="flex justify-end gap-2">
            <Button variant="outline" onClick={() => setEditingId(null)}>
              Cancelar
            </Button>
            <Button onClick={handleSaveEdit}>Salvar</Button>
          </div>
        </div>
      )}

      {/* Lista de entregáveis */}
      <div className="space-y-4">
        {entregaveis.map((entregavel) => (
          <Card key={entregavel.id} className="shadow-md hover:shadow-lg transition-all duration-200">
            <CardHeader className="flex justify-between items-start">
              <div>
                <CardTitle>{entregavel.nome}</CardTitle>
                <CardDescription>
                  {entregavel.descricao} <br />
                  Data: {new Date(entregavel.data).toLocaleDateString()} <br />
                  Tipo: {entregavel.tipoEntregavel?.nome || entregavel.tipoEntregavel?.id || "—"} <br />
                  Atividade: {entregavel.atividadeId || "—"}
                </CardDescription>
              </div>
              <div className="flex items-center space-x-2">
                <Button variant="ghost" size="icon" onClick={() => startEdit(entregavel)}>
                  <Edit2 className="w-5 h-5" />
                </Button>
                <Button variant="ghost" size="icon" onClick={() => handleDelete(entregavel.id)}>
                  <Trash2 className="w-5 h-5" />
                </Button>
              </div>
            </CardHeader>
          </Card>
        ))}
      </div>
    </div>
  )
}
