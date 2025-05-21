"use client"

import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { MoreHorizontal, Plus, Trash2, Edit2, Save, X} from "lucide-react"
import {
  fetchIndicadores,
  createIndicador,
  updateIndicador,
  deleteIndicador,
} from "./indicadoresAPI"
import { fetchProjetos } from "../projects/projetoAPI"

// 📊 Recharts para gráficos
import {
  BarChart, Bar, XAxis, YAxis, Tooltip, ResponsiveContainer,
  PieChart, Pie, Cell, Legend,
  Line,
  Area,
  LineChart, AreaChart,RadarChart, Radar,
  PolarGrid, PolarAngleAxis, PolarRadiusAxis
} from 'recharts'

export default function IndicadoresPage() {
  const [indicadores, setIndicadores] = useState<any[]>([])
  const [projetos, setProjetos] = useState<any[]>([])
  const [loading, setLoading] = useState(true)
  const [loadingProjetos, setLoadingProjetos] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [errorProjetos, setErrorProjetos] = useState<string | null>(null)

  const [showForm, setShowForm] = useState(false)
  const [projetoId, setProjetoId] = useState("")
  const [nome, setNome] = useState("")
  const [valorAtual, setValorAtual] = useState("")
  const [valorMaximo, setValorMaximo] = useState("")
  const [formErrors, setFormErrors] = useState<any>({})

  const [editingId, setEditingId] = useState<string | null>(null)
  const [editValorAtual, setEditValorAtual] = useState("")

  useEffect(() => {
    async function load() {
      try {
        const data = await fetchIndicadores()
        setIndicadores(data)
      } catch {
        setError("Erro ao carregar indicadores.")
      } finally {
        setLoading(false)
      }

      try {
        const projetosData = await fetchProjetos()
        setProjetos(projetosData)
      } catch {
        setErrorProjetos("Erro ao carregar projetos.")
      } finally {
        setLoadingProjetos(false)
      }
    }
    load()
  }, [])

  function validarFormulario() {
    const errors: any = {}
    if (!projetoId) errors.projetoId = "Selecione um projeto"
    if (!nome.trim()) errors.nome = "Preencha o nome"
    if (!valorAtual || isNaN(Number(valorAtual))) errors.valorAtual = "Insira um número válido"
    if (!valorMaximo || isNaN(Number(valorMaximo))) errors.valorMaximo = "Insira um número válido"
    setFormErrors(errors)
    return Object.keys(errors).length === 0
  }

  async function handleCreate() {
    if (!validarFormulario()) return
    try {
      const novo = await createIndicador({
        projetoId,
        nome,
        valorAtual: Number(valorAtual),
        valorMaximo: Number(valorMaximo),
      })
      setIndicadores((prev) => [...prev, novo])
      setProjetoId("")
      setNome("")
      setValorAtual("")
      setValorMaximo("")
      setShowForm(false)
      setFormErrors({})
    } catch (err) {
      alert("Erro ao criar indicador")
      console.error(err)
    }
  }

  function startEditing(indicador: any) {
    setEditingId(indicador.id)
    setEditValorAtual(indicador.valorAtual.toString())
  }

  async function handleSaveEdit(id: string) {
    if (!editValorAtual.trim() || isNaN(Number(editValorAtual))) {
      alert("Insira um número válido para o valor atual")
      return
    }
    try {
      const updated = await updateIndicador(id, Number(editValorAtual))
      setIndicadores((prev) => prev.map((i) => (i.id === id ? updated : i)))
      setEditingId(null)
    } catch (err) {
      alert("Erro ao atualizar indicador")
      console.error(err)
    }
  }

  async function handleDelete(id: string) {
    if (!confirm("Quer mesmo apagar este indicador?")) return
    try {
      const ok = await deleteIndicador(id)
      if (ok) {
        setIndicadores((prev) => prev.filter((i) => i.id !== id))
      }
    } catch {
      alert("Erro ao apagar indicador")
    }
  }

  if (loading) return <p>🔄 A carregar indicadores...</p>
  if (error) return <p className="text-red-500">{error}</p>

  // 🎨 Cores e dados para os gráficos
  const COLORS = ["#8884d8", "#82ca9d", "#ffc658", "#ff7f50", "#00bcd4", "#ff69b4"]
  const getPieData = (indicador: any) => [
    { name: "Progresso", value: indicador.valorAtual },
    { name: "Falta", value: indicador.valorMaximo - indicador.valorAtual }
  ]

  return (
      <div className="max-w-4xl mx-auto space-y-6 p-4">
        <h1 className="text-2xl font-bold">📊 Indicadores</h1>

        <Button
            onClick={() => setShowForm(!showForm)}
            className="flex items-center gap-2 mb-4 bg-blue-600 text-white hover:bg-blue-700"
        >
          <Plus className="w-4 h-4" /> {showForm ? "Fechar Formulário" : "Novo Indicador"}
        </Button>

        {showForm && (
            <div className="border border-gray-300 rounded-xl p-4 bg-white shadow space-y-4">
              <div>
                <label className="block text-sm font-medium mb-1">Projeto</label>
                <select
                    value={projetoId}
                    onChange={(e) => setProjetoId(e.target.value)}
                    className="w-full border rounded-md p-2"
                >
                  <option value="">Selecione um projeto</option>
                  {projetos.map((p) => (
                      <option key={p.id} value={p.id}>
                        {p.nome}
                      </option>
                  ))}
                </select>
                {formErrors.projetoId && <p className="text-red-500 text-sm">{formErrors.projetoId}</p>}
              </div>

              <div>
                <Input
                    placeholder="ex: Qualidade"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                />
                {formErrors.nome && <p className="text-red-500 text-sm">{formErrors.nome}</p>}
              </div>

              <div>
                <Input
                    placeholder="Valor Atual (ex: 40)"
                    type="text"
                    value={valorAtual}
                    onChange={(e) => setValorAtual(e.target.value)}
                />
                {formErrors.valorAtual && <p className="text-red-500 text-sm">{formErrors.valorAtual}</p>}
              </div>

              <div>
                <Input
                    placeholder="Valor Máximo (ex: 100)"
                    type="text"
                    value={valorMaximo}
                    onChange={(e) => setValorMaximo(e.target.value)}
                />
                {formErrors.valorMaximo && <p className="text-red-500 text-sm">{formErrors.valorMaximo}</p>}
              </div>

              <div className="flex justify-end gap-2">
                <Button variant="outline" onClick={() => setShowForm(false)}>
                  Cancelar
                </Button>
                <Button onClick={handleCreate}>Criar</Button>
              </div>
            </div>
        )}

        {/* Lista de Indicadores */}
        <div className="space-y-4">
          {indicadores.map((indicador) => (
              <Card key={indicador.id} className="shadow-md hover:shadow-lg transition-all duration-200">
                <CardHeader className="flex justify-between items-center">
                  <div>
                    <CardTitle>{indicador.nome}</CardTitle>
                    <CardDescription>ID: {indicador.id}</CardDescription>
                  </div>

                  {editingId === indicador.id ? (
                      <div className="flex items-center space-x-2">
                        <Input
                            type="number"
                            value={editValorAtual}
                            onChange={(e) => setEditValorAtual(e.target.value)}
                            className="w-24"
                        />
                        <Button variant="outline" size="icon" onClick={() => setEditingId(null)}>
                          <X className="w-4 h-4" />
                        </Button>
                        <Button size="icon" onClick={() => handleSaveEdit(indicador.id)}>
                          <Save className="w-4 h-4" />
                        </Button>
                      </div>
                  ) : (
                      <div className="flex items-center space-x-4">
                  <span>
                    Valor Atual: <strong>{indicador.valorAtual}</strong> / {indicador.valorMaximo}
                  </span>
                        <DropdownMenu>
                          <DropdownMenuTrigger asChild>
                            <Button variant="ghost" size="icon">
                              <MoreHorizontal className="w-5 h-5" />
                            </Button>
                          </DropdownMenuTrigger>
                          <DropdownMenuContent align="end">
                            <DropdownMenuItem onClick={() => startEditing(indicador)}>
                              <Edit2 className="w-4 h-4 mr-2" /> Editar Valor
                            </DropdownMenuItem>
                            <DropdownMenuItem onClick={() => handleDelete(indicador.id)}>
                              <Trash2 className="w-4 h-4 mr-2" /> Deletar
                            </DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </div>
                  )}
                </CardHeader>
              </Card>
          ))}
        </div>

        {/* 📊 Dashboard de Indicadores */}
        <div className="mt-10">
          <h2 className="text-xl font-bold mb-4">📈 Dashboard de Indicadores</h2>

          {/* Gráfico de Barras */}
          <div className="bg-white p-4 rounded-xl shadow mb-6">
            <h3 className="font-semibold mb-2">Comparação de Valor Atual</h3>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart data={indicadores}>
                <XAxis dataKey="nome" />
                <YAxis />
                <Tooltip />
                <Bar dataKey="valorAtual" fill="#4f46e5" />
              </BarChart>
            </ResponsiveContainer>
          </div>

          {/* Gráficos de Anel */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {indicadores.map((indicador, index) => (
                <div key={indicador.id} className="bg-white p-4 rounded-xl shadow text-center">
                  <h4 className="font-semibold mb-2">{indicador.nome}</h4>
                  <ResponsiveContainer width="100%" height={200}>
                    <PieChart>
                      <Pie
                          data={getPieData(indicador)}
                          dataKey="value"
                          innerRadius={50}
                          outerRadius={80}
                          startAngle={90}
                          endAngle={-270}
                          paddingAngle={5}
                      >
                        {getPieData(indicador).map((_, i) => (
                            <Cell key={`cell-${i}`} fill={COLORS[i % COLORS.length]} />
                        ))}
                      </Pie>
                      <Legend verticalAlign="bottom" height={36} />
                    </PieChart>
                  </ResponsiveContainer>
                  <p className="mt-2 text-sm text-gray-600">
                    {((indicador.valorAtual / indicador.valorMaximo) * 100).toFixed(1)}% concluído
                  </p>
                </div>
            ))}
          </div>

          {/* 📉 Gráfico de Linha - Evolução simulada */}
          <div className="bg-white p-4 rounded-xl shadow mb-6">
            <h3 className="font-semibold mb-2">Evolução Simulada</h3>
            <ResponsiveContainer width="100%" height={300}>
              <LineChart
                  data={indicadores.map((ind, i) => ({
                    nome: ind.nome,
                    dia1: ind.valorAtual * 0.7,
                    dia2: ind.valorAtual * 0.85,
                    dia3: ind.valorAtual,
                  }))}
              >
                <XAxis dataKey="nome" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Line type="monotone" dataKey="dia1" stroke="#8884d8" />
                <Line type="monotone" dataKey="dia2" stroke="#82ca9d" />
                <Line type="monotone" dataKey="dia3" stroke="#ff7300" />
              </LineChart>
            </ResponsiveContainer>
          </div>


          {/* 🧮 Área Empilhada */}
          <div className="bg-white p-4 rounded-xl shadow mb-6">
            <h3 className="font-semibold mb-2">Valor Atual vs Máximo</h3>
            <ResponsiveContainer width="100%" height={300}>
              <AreaChart data={indicadores}>
                <XAxis dataKey="nome" />
                <YAxis />
                <Tooltip />
                <Area type="monotone" dataKey="valorAtual" stackId="1" stroke="#8884d8" fill="#8884d8" />
                <Area type="monotone" dataKey="valorMaximo" stackId="1" stroke="#ffc658" fill="#ffc658" />
              </AreaChart>
            </ResponsiveContainer>
          </div>

          {/* 🧭 Gráfico Radar */}
          <div className="bg-white p-4 rounded-xl shadow mb-6">
            <h3 className="font-semibold mb-2">Radar de Indicadores</h3>
            <ResponsiveContainer width="100%" height={300}>
              <RadarChart outerRadius={90} data={indicadores}>
                <PolarGrid />
                <PolarAngleAxis dataKey="nome" />
                <PolarRadiusAxis angle={30} domain={[0, Math.max(...indicadores.map(i => i.valorMaximo))]} />
                <Radar name="Atual" dataKey="valorAtual" stroke="#82ca9d" fill="#82ca9d" fillOpacity={0.6} />
                <Radar name="Máximo" dataKey="valorMaximo" stroke="#8884d8" fill="#8884d8" fillOpacity={0.3} />
                <Legend />
                <Tooltip />
              </RadarChart>
            </ResponsiveContainer>
          </div>

          {/* 📦 Barras Horizontais de Progresso */}
          <div className="bg-white p-4 rounded-xl shadow mb-6">
            <h3 className="font-semibold mb-2">Ranking de Progresso</h3>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart layout="vertical" data={indicadores.map((i) => ({
                ...i,
                progresso: Number(((i.valorAtual / i.valorMaximo) * 100).toFixed(1)),
              }))}>
                <XAxis type="number" domain={[0, 100]} unit="%" />
                <YAxis dataKey="nome" type="category" />
                <Tooltip />
                <Bar dataKey="progresso" fill="#4ade80" />
              </BarChart>
            </ResponsiveContainer>
          </div>

        </div>
      </div>
  )
}
