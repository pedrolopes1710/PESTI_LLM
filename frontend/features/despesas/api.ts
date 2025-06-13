import type { Despesa, CreatingDespesaDto, DeleteDespesaResponse } from "./types"

// Base URL for the API
const API_BASE_URL = "http://localhost:5225/api"

// Create a new despesa (expense)
export async function createDespesa(data: CreatingDespesaDto): Promise<Despesa> {
  try {
    const response = await fetch(`${API_BASE_URL}/Despesas`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })

    if (!response.ok) {
      throw new Error(`Error creating despesa: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Error creating despesa:", error)
    throw error
  }
}

// Delete a despesa
export async function deleteDespesa(id: string): Promise<DeleteDespesaResponse> {
  try {
    const response = await fetch(`${API_BASE_URL}/Despesas/${id}`, {
      method: "DELETE",
    })

    if (!response.ok) {
      throw new Error(`Error deleting despesa: ${response.status}`)
    }

    return { success: true }
  } catch (error) {
    console.error(`Error deleting despesa ${id}:`, error)
    return {
      success: false,
      message: error instanceof Error ? error.message : "Unknown error",
    }
  }
}
