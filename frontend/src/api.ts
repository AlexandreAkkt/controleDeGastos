// endereço da API
const API_URL = "http://localhost:5151/api"

// lista as pessoas
export async function listarPessoas() {
  const resposta = await fetch(`${API_URL}/pessoas`)
  return resposta.json()
}

// cria uma pessoa
export async function criarPessoa(nome: string, idade: number) {
  await fetch(`${API_URL}/pessoas`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ nome, idade })
  })
}

// apaga uma pessoa
export async function deletarPessoa(id: string) {
  await fetch(`${API_URL}/pessoas/${id}`, {
    method: "DELETE"
  })
}