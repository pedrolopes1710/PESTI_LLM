import type { Deliverable } from "./types"

const ENTREGAVEIS_URL = "http://localhost:5225/api/Entregaveis"

// Função para buscar todos os entregáveis
export async function fetchDeliverables(): Promise<Deliverable[]> {
  try {
    const response = await fetch(ENTREGAVEIS_URL)
    if (!response.ok) {
      throw new Error(`Erro ao buscar entregáveis: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar entregáveis:", error)
    throw error
  }
}
