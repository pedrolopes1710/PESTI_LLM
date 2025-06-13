export interface Despesa {
  id: string
  descricao: string
  valor: number
  orcamentoId?: string
}

export interface CreatingDespesaDto {
  descricao: string
  valor: number
  orcamentoId: string
}

// Adicionar interface para resposta da API após exclusão
export interface DeleteDespesaResponse {
  success: boolean
  message?: string
}
