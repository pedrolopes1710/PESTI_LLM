import type { Rubrica } from "../rubricas/types"

export interface Orcamento {
  id: string
  gastoPlaneado: number
  rubrica: Rubrica
}

export interface CreatingOrcamentoDto {
  gastoPlaneado: number
  rubricaId: string
}

export interface EditingOrcamentoDto {
  gastoPlaneado?: number
  rubricaId?: string
  despesaId?: string
}
