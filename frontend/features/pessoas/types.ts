export interface Contrato {
  id: string
  tipo: string
  salario: number
  dataInicio: string
  dataFim: string
  ativo: boolean
}

// DTO para criar contrato (baseado no teu backend)
export interface CreatingContratoDto {
  tipo: string
  salario: number
  dataInicio: string
  dataFim: string
  ativo: boolean
}

// DTO que a API de contrato retorna
export interface ContratoDto {
  id: string
  tipo: string
  salario: number
  dataInicio: string
  dataFim: string
  ativo: boolean
}

export interface Pessoa {
  id: string
  nome: string
  email: string
  pessoaCienciaId: string
  pessoaUltimoPedPagam: string
  ativo: boolean
  contrato: Contrato
  projetos: any[]
  cargasMensais: any[]
}

// DTO para criar pessoa (sem o campo ativo - a API cria por defeito)
export interface CreatingPessoaDto {
  nome: string
  email: string
  pessoaCienciaId: string
  pessoaUltimoPedPagam: string
  contratoId: string
}

export interface CreatePessoaDto {
  nome: string
  email: string
  pessoaCienciaId: string
  pessoaUltimoPedPagam: string
  ativo: boolean
  contratoId: string
}
