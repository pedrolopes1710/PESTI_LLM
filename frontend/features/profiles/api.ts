import type { Profile } from "./types"

const PERFIS_URL = "http://localhost:5225/api/Perfil"

// Função para buscar todos os perfis
export async function fetchProfiles(): Promise<Profile[]> {
  try {
    const response = await fetch(PERFIS_URL)
    if (!response.ok) {
      throw new Error(`Erro ao buscar perfis: ${response.status}`)
    }

    // Obter os dados da resposta
    const data = await response.json()
    console.log("Perfis recebidos da API:", data)

    return data
  } catch (error) {
    console.error("Erro ao buscar perfis:", error)
    throw error
  }
}
