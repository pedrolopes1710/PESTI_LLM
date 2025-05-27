export interface Contrato {
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

export interface CreatePessoaDto {
  nome: string
  email: string
  pessoaCienciaId: string
  pessoaUltimoPedPagam: string
  ativo: boolean
  contrato: {
    tipo: string
    salario: number
    dataInicio: string
    dataFim: string
    ativo: boolean
  }
}
