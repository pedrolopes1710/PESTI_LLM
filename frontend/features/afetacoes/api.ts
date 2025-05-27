import type { TabelaAfetacoesDto, PessoaDto, CreatingBulkCargaMensalDto } from "./types"

// Configuração da base URL da API
const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7284"

// Função para buscar a lista de pessoas (simplificada)
export async function getPessoas(): Promise<PessoaDto[]> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/pessoas`)
    if (!response.ok) {
      throw new Error(`Failed to fetch pessoas: ${response.status} ${response.statusText}`)
    }
    const pessoas: PessoaDto[] = await response.json()
    return pessoas
  } catch (error) {
    console.error("Error fetching pessoas:", error)
    throw error
  }
}

// Função para buscar a tabela de afetações de uma pessoa
export async function getTabelaAfetacoes(pessoaId: string): Promise<TabelaAfetacoesDto> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/tabelaafetacoes/${pessoaId}`)
    if (!response.ok) {
      throw new Error(`Failed to fetch tabela afetacoes: ${response.status} ${response.statusText}`)
    }
    const data: TabelaAfetacoesDto = await response.json()
    return data
  } catch (error) {
    console.error("Error fetching tabela afetacoes:", error)
    throw error
  }
}

// Função para criar cargas mensais em bulk
export async function createCargasMensaisBulk(dto: CreatingBulkCargaMensalDto): Promise<void> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/cargasmensais/bulk`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        JornadaDiaria: dto.jornadaDiaria,
        DiasUteisTrabalhaveis: dto.diasUteisTrabalhaveis,
        FeriasBaixasLicencasFaltas: dto.feriasBaixasLicencasFaltas,
        MesAnoInicio: dto.mesAnoInicio,
        MesAnoFim: dto.mesAnoFim,
        SalarioBase: dto.salarioBase,
        TaxaSocialUnica: dto.taxaSocialUnica,
        PessoaId: dto.pessoaId,
      }),
    })

    if (!response.ok) {
      throw new Error(`Failed to create cargas mensais: ${response.status} ${response.statusText}`)
    }
  } catch (error) {
    console.error("Error creating cargas mensais:", error)
    throw error
  }
}
