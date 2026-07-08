import { useEffect, useState } from "react"
import type { TotalGeral } from "../types"
import { consultarTotais } from "../api"

export function TotaisPanel() {
  const [totais, setTotais] = useState<TotalGeral | null>(null)

  useEffect(() => {
    consultarTotais().then(setTotais)
  }, [])

  if (!totais) return <p>Carregando...</p>

  return (
    <div>
      <h2>Totais</h2>

      <table border={1} cellPadding={8}>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totais.pessoas.map(p => (
            <tr key={p.pessoaId}>
              <td>{p.nome}</td>
              <td>R$ {p.totalReceitas.toFixed(2)}</td>
              <td>R$ {p.totalDespesas.toFixed(2)}</td>
              <td>R$ {p.saldo.toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h3>
        Total geral — Receitas: R$ {totais.totalReceitas.toFixed(2)} |
        Despesas: R$ {totais.totalDespesas.toFixed(2)} |
        Saldo: R$ {totais.saldoLiquido.toFixed(2)}
      </h3>
    </div>
  )
}