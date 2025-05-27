import type { Pessoa, CreatePessoaDto } from "./types"

const PESSOAS_URL = "http://localhost:5225/api/pessoas"

// Função para buscar todas as pessoas
export async function fetchPessoas(): Promise<Pessoa[]> {
  try {
    const response = await fetch(PESSOAS_URL)
    if (!response.ok) {
      throw new Error(`Erro ao buscar pessoas: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar pessoas:", error)
    throw error
  }
}

// Função para buscar uma pessoa específica
export async function fetchPessoa(id: string): Promise<Pessoa> {
  try {
    const response = await fetch(`${PESSOAS_URL}/${id}`)
    if (!response.ok) {
      throw new Error(`Erro ao buscar pessoa: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error(`Erro ao buscar pessoa ${id}:`, error)
    throw error
  }
}

// Função para criar uma nova pessoa
export async function createPessoa(pessoaData: CreatePessoaDto): Promise<Pessoa> {
  try {
    const response = await fetch(PESSOAS_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(pessoaData),
    })

    if (!response.ok) {
      const errorText = await response.text()
      console.error("Resposta da API:", errorText)
      throw new Error(`Erro ao criar pessoa: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao criar pessoa:", error)
    throw error
  }
}

// Função para atualizar uma pessoa
export async function updatePessoa(id: string, pessoaData: Partial<CreatePessoaDto>): Promise<Pessoa> {
  try {
    const response = await fetch(`${PESSOAS_URL}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(pessoaData),
    })

    if (!response.ok) {
      throw new Error(`Erro ao atualizar pessoa: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao atualizar pessoa:", error)
    throw error
  }
}

// Função para deletar uma pessoa
export async function deletePessoa(id: string): Promise<boolean> {
  try {
    const response = await fetch(`${PESSOAS_URL}/${id}`, {
      method: "DELETE",
    })

    if (response.status === 404) return false
    if (!response.ok) {
      throw new Error(`Erro ao deletar pessoa: ${response.status}`)
    }

    return true
  } catch (error) {
    console.error("Erro ao deletar pessoa:", error)
    throw error
  }
}

// Função para ativar/desativar pessoa
export async function togglePessoaStatus(id: string, ativar: boolean): Promise<Pessoa> {
  try {
    const endpoint = ativar ? `${PESSOAS_URL}/${id}/reativar` : `${PESSOAS_URL}/${id}/desativar`
    const method = ativar ? "PUT" : "DELETE"

    const response = await fetch(endpoint, {
      method: method,
    })

    if (!response.ok) {
      throw new Error(`Erro ao ${ativar ? "ativar" : "desativar"} pessoa: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error(`Erro ao ${ativar ? "ativar" : "desativar"} pessoa:`, error)
    throw error
  }
}

// Função para associar projetos
export async function associarProjetos(pessoaId: string, projetosIds: string[]): Promise<Pessoa> {
  try {
    const response = await fetch(`${PESSOAS_URL}/${pessoaId}/associarProjetos`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(projetosIds),
    })

    if (!response.ok) {
      throw new Error(`Erro ao associar projetos: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao associar projetos:", error)
    throw error
  }
}

// Função para desassociar projetos
export async function desassociarProjetos(pessoaId: string, projetosIds: string[]): Promise<Pessoa> {
  try {
    const response = await fetch(`${PESSOAS_URL}/${pessoaId}/desassociarProjetos`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(projetosIds),
    })

    if (!response.ok) {
      throw new Error(`Erro ao desassociar projetos: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error("Erro ao desassociar projetos:", error)
    throw error
  }
}

// Função para buscar todos os projetos (para o seletor)
export async function fetchProjetos(): Promise<any[]> {
  try {
    const response = await fetch("http://localhost:5225/api/projetos")
    if (!response.ok) {
      throw new Error(`Erro ao buscar projetos: ${response.status}`)
    }
    return await response.json()
  } catch (error) {
    console.error("Erro ao buscar projetos:", error)
    throw error
  }
}
