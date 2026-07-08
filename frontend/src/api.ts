import type { Pessoa, Transacao, TotalGeral, TipoTransacao } from './types'

// endereço da API
const API_URL = "http://localhost:5151/api"

// lista as pessoas
export async function listarPessoas(): Promise<Pessoa[]> {
  const resposta = await fetch(`${API_URL}/pessoas`)
  return resposta.json()
}

// cria uma pessoa
export async function criarPessoa(nome: string, idade: number) {
  return fetch(`${API_URL}/pessoas`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ nome, idade })
  })
}

// apaga uma pessoa (o backend ja apaga as transacoes dela junto)
export async function deletarPessoa(id: string) {
  await fetch(`${API_URL}/pessoas/${id}`, { method: "DELETE" })
}

// lista as transacoes
export async function listarTransacoes(): Promise<Transacao[]> {
  const resposta = await fetch(`${API_URL}/transacoes`)
  return resposta.json()
}

// cria uma transacao
// tipo tem que ser "Receita" ou "Despesa" (o backend espera string, nao numero)
export async function criarTransacao(
  descricao: string,
  valor: number,
  tipo: TipoTransacao,
  pessoaId: string
) {
  return fetch(`${API_URL}/transacoes`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ descricao, valor, tipo, pessoaId })
  })
}

// pega os totais (receita, despesa e saldo de cada pessoa + total geral)
export async function consultarTotais(): Promise<TotalGeral> {
  const resposta = await fetch(`${API_URL}/totais`)
  return resposta.json()
}