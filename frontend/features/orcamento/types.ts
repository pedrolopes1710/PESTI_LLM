export interface Rubrica {
    id: string | number
    nome: string
}

export interface Orcamento {
    id: string | number
    gastoPlaneado: number
    rubrica: Rubrica
}

export interface Despesa {
    id: string | number
    descricao: string
    valor: number
}