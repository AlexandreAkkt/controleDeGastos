import { useEffect, useState } from "react"
import type { Pessoa, Transacao, TipoTransacao } from "../types"
import { listarPessoas, listarTransacoes, criarTransacao } from "../api"

export function TransacoesPanel() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [transacoes, setTransacoes] = useState<Transacao[]>([])

  // campos do formulario
  const [pessoaId, setPessoaId] = useState("")
  const [descricao, setDescricao] = useState("")
  const [valor, setValor] = useState("")
  const [tipo, setTipo] = useState<TipoTransacao>("Despesa")
  const [erro, setErro] = useState("")

  async function carregar() {
    setPessoas(await listarPessoas())
    setTransacoes(await listarTransacoes())
  }

  useEffect(() => {
    carregar()
  }, [])

  // acha a pessoa selecionada, pra saber se ela e menor de idade
  const pessoaSelecionada = pessoas.find(p => p.id === pessoaId)
  const menorDeIdade = pessoaSelecionada ? pessoaSelecionada.idade < 18 : false

  async function salvarTransacao() {
    setErro("")

    if (!pessoaId || !descricao || !valor) return

    const resposta = await criarTransacao(descricao, Number(valor), tipo, pessoaId)

    if (!resposta.ok) {
      // o backend recusa receita pra menor de idade, entre outros erros
      const corpo = await resposta.json()
      setErro(corpo.mensagem || "Erro ao cadastrar transação")
      return
    }

    setDescricao("")
    setValor("")
    carregar()
  }

  return (
    <div>
      <h2>Transações</h2>

      <select value={pessoaId} onChange={e => setPessoaId(e.target.value)}>
        <option value="">Selecione a pessoa</option>
        {pessoas.map(p => (
          <option key={p.id} value={p.id}>{p.nome}</option>
        ))}
      </select>

      <input
        placeholder="Descrição"
        value={descricao}
        onChange={e => setDescricao(e.target.value)}
      />

      <input
        type="number"
        placeholder="Valor"
        value={valor}
        onChange={e => setValor(e.target.value)}
      />

      <select value={tipo} onChange={e => setTipo(e.target.value as TipoTransacao)}>
        <option value="Despesa">Despesa</option>
        {/* menor de idade so pode cadastrar despesa, entao escondo a opcao receita */}
        {!menorDeIdade && <option value="Receita">Receita</option>}
      </select>

      <button onClick={salvarTransacao}>Cadastrar</button>

      {erro && <p style={{ color: "red" }}>{erro}</p>}

      <ul>
        {transacoes.map(t => (
          <li key={t.id}>
            {t.descricao} — {t.tipo} — R$ {t.valor.toFixed(2)}
          </li>
        ))}
      </ul>
    </div>
  )
}