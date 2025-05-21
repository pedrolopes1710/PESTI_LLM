import type { CreateActivityDto, Activity } from "./types"

const ATIVIDADES_URL = "http://localhost:5225/api/Atividades"

// Função para criar uma nova atividade
export async function createActivity(activityData: CreateActivityDto): Promise<Activity> {
  try {
    // Validar campos obrigatórios
    if (!activityData.nomeAtividade || activityData.nomeAtividade.trim() === "") {
      throw new Error("O nome da atividade não pode estar vazio")
    }

    if (!activityData.descricaoAtividade || activityData.descricaoAtividade.trim() === "") {
      throw new Error("A descrição da atividade não pode estar vazia")
    }

    if (!activityData.dataInicioAtividade) {
      throw new Error("A data de início é obrigatória")
    }

    if (!activityData.dataFimAtividade) {
      throw new Error("A data de fim é obrigatória")
    }

    console.log("Enviando dados para API:", activityData)

    const response = await fetch(ATIVIDADES_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(activityData),
    })

    if (!response.ok) {
      const errorText = await response.text()
      console.error("Resposta da API:", errorText)
      throw new Error(`Erro ao criar atividade: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao criar atividade:", error)
    throw error
  }
}

// Função para buscar todas as atividades
export async function fetchActivities(): Promise<Activity[]> {
  try {
    const response = await fetch(ATIVIDADES_URL)
    if (!response.ok) {
      throw new Error(`Erro ao buscar atividades: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar atividades:", error)
    throw error
  }
}

// Função para buscar uma atividade específica
export async function fetchActivity(id: string): Promise<Activity> {
  try {
    const response = await fetch(`${ATIVIDADES_URL}/${id}`)
    if (!response.ok) {
      throw new Error(`Erro ao buscar atividade: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error(`Erro ao buscar atividade ${id}:`, error)
    throw error
  }
}

// Função para atualizar uma atividade
export async function updateActivity(id: string, activityData: Partial<CreateActivityDto>): Promise<Activity> {
  try {
    const response = await fetch(`${ATIVIDADES_URL}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(activityData),
    })

    if (!response.ok) {
      throw new Error(`Erro ao atualizar atividade: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao atualizar atividade:", error)
    throw error
  }
}

// Função para deletar uma atividade
export async function deleteActivity(id: string): Promise<boolean> {
  try {
    const response = await fetch(`${ATIVIDADES_URL}/${id}`, {
      method: "DELETE",
    })

    if (response.status === 404) return false
    if (!response.ok) {
      throw new Error(`Erro ao deletar atividade: ${response.status}`)
    }

    return true
  } catch (error) {
    console.error("Erro ao deletar atividade:", error)
    throw error
  }
}
