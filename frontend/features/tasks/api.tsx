import type { Activity, Task } from "./types"

// Interface para os dados da API de tarefas
export interface ApiTask {
  id: string
  nome: string
  status: string
  descricaoTarefa: string
  atividadeId: string
}

// Interface para os dados da API de atividades
export interface ApiActivity {
  id: string
  nomeAtividade: string
  descricaoAtividade: string
  dataInicioAtividade: string
  dataFimAtividade: string
  progress: number
}

// Função para mapear o status da API para o formato da aplicação
function mapStatus(apiStatus: string): "In Progress" | "Not Started" | "Completed" {
  const statusMap: Record<string, "In Progress" | "Not Started" | "Completed"> = {
    Por_Comecar: "Not Started",
    A_Decorrer: "In Progress",
    Terminado: "Completed",
    // Adicione outros mapeamentos conforme necessário
  }

  return statusMap[apiStatus] || "Not Started"
}

// Função para buscar todas as tarefas
export async function fetchTasks(): Promise<ApiTask[]> {
  try {
    const response = await fetch("http://localhost:5225/api/Tarefas")
    if (!response.ok) {
      throw new Error(`Erro ao buscar tarefas: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar tarefas:", error)
    throw error
  }
}

// Função para buscar todas as atividades
export async function fetchActivities(): Promise<ApiActivity[]> {
  try {
    const response = await fetch("https://localhost:7284/api/Atividades")
    if (!response.ok) {
      throw new Error(`Erro ao buscar atividades: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar atividades:", error)
    throw error
  }
}

// Função para buscar os detalhes de uma atividade
export async function fetchActivity(id: string): Promise<ApiActivity | null> {
  try {
    const response = await fetch(`https://localhost:7284/api/Atividades/${id}`)
    if (!response.ok) {
      throw new Error(`Erro ao buscar atividade: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error(`Erro ao buscar atividade ${id}:`, error)
    return null
  }
}

// Função para criar uma nova tarefa
export async function createTask(taskData: Omit<ApiTask, "id">): Promise<ApiTask> {
  try {
    const response = await fetch("http://localhost:5225/api/Tarefas", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(taskData),
    })

    if (!response.ok) {
      throw new Error(`Erro ao criar tarefa: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao criar tarefa:", error)
    throw error
  }
}

// Função para mapear uma tarefa da API para o formato da aplicação
export function mapApiTaskToTask(apiTask: ApiTask): Task {
  return {
    id: apiTask.id,
    title: apiTask.nome,
    description: apiTask.descricaoTarefa,
    project: "Projeto Padrão", // Como não temos informação de projeto na API
    status: mapStatus(apiTask.status),
    dataInicio: new Date().toISOString(), // Será substituído pelos dados da atividade
    dataFim: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString(), // Será substituído pelos dados da atividade
    assignee: {
      name: "Não atribuído", // Como não temos informação de responsável na API
      avatar: "/placeholder.svg?height=32&width=32",
    },
    atividadeId: apiTask.atividadeId,
  }
}

// Função para agrupar tarefas por atividade
export async function groupTasksByActivity(apiTasks: ApiTask[]): Promise<Activity[]> {
  // Extrair IDs únicos de atividades
  const activityIds = [...new Set(apiTasks.map((task) => task.atividadeId))].filter(Boolean)

  // Criar um mapa para armazenar atividades
  const activitiesMap = new Map<string, Activity>()

  // Buscar detalhes de cada atividade
  for (const activityId of activityIds) {
    const activityData = await fetchActivity(activityId)

    if (activityData) {
      activitiesMap.set(activityId, {
        id: activityData.id,
        name: activityData.nomeAtividade,
        description: activityData.descricaoAtividade,
        dataInicio: activityData.dataInicioAtividade,
        dataFim: activityData.dataFimAtividade,
        tasks: [],
        progress:activityData.progress
      })
    } else {
      // Criar uma atividade padrão se não conseguirmos buscar os detalhes
      activitiesMap.set(activityId, {
        id: activityId,
        name: `Atividade ${activityId.substring(0, 8)}...`,
        description: "Detalhes não disponíveis",
        dataInicio: new Date().toISOString(),
        dataFim: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toISOString(),
        tasks: [],
        progress:100,
      })
    }
  }

  // Criar uma atividade "Sem atividade" para tarefas sem atividadeId
  const noActivityId = "no-activity"
  activitiesMap.set(noActivityId, {
    id: noActivityId,
    name: "Tarefas sem atividade",
    description: "Tarefas que não estão associadas a nenhuma atividade",
    dataInicio: new Date().toISOString(),
    dataFim: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toISOString(),
    tasks: [],
    progress:100,
  })

  // Mapear e agrupar as tarefas por atividade
  for (const apiTask of apiTasks) {
    const task = mapApiTaskToTask(apiTask)
    const activityId = apiTask.atividadeId || noActivityId

    if (activitiesMap.has(activityId)) {
      const activity = activitiesMap.get(activityId)!

      // Atualizar as datas da tarefa com as datas da atividade
      task.dataInicio = activity.dataInicio
      task.dataFim = activity.dataFim

      activity.tasks.push(task)
    } else {
      // Se por algum motivo a atividade não estiver no mapa, adicionar à categoria "Sem atividade"
      const noActivity = activitiesMap.get(noActivityId)!
      noActivity.tasks.push(task)
    }
  }

  // Converter o mapa em um array de atividades
  return Array.from(activitiesMap.values())
}

// Função para buscar tarefas e agrupá-las por atividade
export async function fetchTasksGroupedByActivity(): Promise<Activity[]> {
  try {
    const tasks = await fetchTasks()
    return await groupTasksByActivity(tasks)
  } catch (error) {
    console.error("Erro ao buscar e agrupar tarefas:", error)
    throw error
  }
}
