// tipos usados no front-end

export type TipoTransacao = 0 | 1

export type Pessoa = {
  id: string
  nome: string
  idade: number
}

export type Transacao = {
  id: string
  descricao: string
  valor: number
  tipo: TipoTransacao
  pessoaId: string
  pessoa?: Pessoa
}

export type TotalPessoa = {
  pessoaId: string
  nome: string
  totalReceitas: number
  totalDespesas: number
  saldo: number
}

export type TotalGeral = {
  totalReceitas: number
  totalDespesas: number
  saldo: number
  pessoas: TotalPessoa[]
}