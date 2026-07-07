import { useEffect, useState } from "react"
import type { Pessoa } from "../types"
import * as api from "../api"

// tela de cadastro de pessoas
export function PessoasPanel() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [nome, setNome] = useState("")
  const [idade, setIdade] = useState("")

  // carrega as pessoas quando abre a tela
  useEffect(() => {
    carregarPessoas()
  }, [])

  async function carregarPessoas() {
    const dados = await api.listarPessoas()
    setPessoas(dados)
  }

  async function salvarPessoa() {
    if (nome === "" || idade === "") {
      alert("Preencha nome e idade")
      return
    }

    await api.criarPessoa(nome, Number(idade))

    setNome("")
    setIdade("")

    carregarPessoas()
  }

  async function apagarPessoa(id: string) {
    await api.deletarPessoa(id)
    carregarPessoas()
  }

  return (
    <div>
      <h2>Pessoas</h2>

      <input
        placeholder="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
      />

      <input
        placeholder="Idade"
        type="number"
        value={idade}
        onChange={(e) => setIdade(e.target.value)}
      />

      <button onClick={salvarPessoa}>Cadastrar</button>

      <ul>
        {pessoas.map((pessoa) => (
          <li key={pessoa.id}>
            {pessoa.nome} - {pessoa.idade} anos
            <button onClick={() => apagarPessoa(pessoa.id)}>
              Excluir
            </button>
          </li>
        ))}
      </ul>
    </div>
  )
}