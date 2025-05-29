export interface CreateActivityDto {
  dataInicioAtividade: string
  dataFimAtividade: string
  descricaoAtividade: string
  nomeAtividade: string
  orcamentoIds?: string[]
  tarefasIds?: string[]
  entregaveisIds?: string[]
  perfisIds?: string[]
}

export interface Activity {
  id: string
  dataInicioAtividade: string
  dataFimAtividade: string
  descricaoAtividade: string
  nomeAtividade: string
  orcamentoIds?: string[]
  tarefasIds?: string[]
  entregaveisIds?: string[]
  perfisIds?: string[]
}
