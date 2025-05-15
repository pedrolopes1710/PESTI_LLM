import type { Activity, Task } from "./types"

// Interface para os dados da API conforme o exemplo fornecido
export interface ApiTask {
  id: string
  nome: string
  status: string
  descricaoTarefa: string
}

// Função para mapear o status da API para o formato da aplicação
function mapStatus(apiStatus: string): "In Progress" | "Not Started" | "Completed" {
  const statusMap: Record<string, "In Progress" | "Not Started" | "Completed"> = {
    Por_Comecar: "Not Started",
    Em_Progresso: "In Progress",
    Concluido: "Completed",
    // Adicione outros mapeamentos conforme necessário
  }

  return statusMap[apiStatus] || "Not Started"
}

// Função para mapear os dados da API para o formato da aplicação
export function mapApiDataToActivities(apiTasks: ApiTask[]): Activity[] {
  // Como não temos informações de atividade na API, vamos criar uma atividade padrão
  const defaultActivity: Activity = {
    id: 1,
    name: "Tarefas",
    description: "Lista de tarefas",
    tasks: [],
  }

  // Mapear as tarefas da API para o formato da aplicação
  const tasks: Task[] = apiTasks.map((apiTask) => ({
    id: apiTask.id,
    title: apiTask.nome,
    description: apiTask.descricaoTarefa,
    project: "Projeto Padrão", // Como não temos informação de projeto na API
    status: mapStatus(apiTask.status),
    dataInicio: new Date().toISOString(), // Como não temos datas na API, usamos a data atual
    dataFim: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString(), // Data atual + 7 dias
    assignee: {
      name: "Não atribuído", // Como não temos informação de responsável na API
      avatar: "/placeholder.svg?height=32&width=32",
    },
    activityId: 1,
  }))

  // Adicionar as tarefas à atividade padrão
  defaultActivity.tasks = tasks

  return [defaultActivity]
}
