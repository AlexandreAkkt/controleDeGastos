import { useState } from "react"
import { PessoasPanel } from "./Components/PessoasPanel"
import { TransacoesPanel } from "./Components/TransacoesPanel"
import { TotaisPanel } from "./Components/TotaisPanel"

type Aba = "pessoas" | "transacoes" | "totais"

function App() {
  const [aba, setAba] = useState<Aba>("pessoas")

  return (
    <div>
      <h1>Controle de Gastos</h1>

      <nav>
        <button onClick={() => setAba("pessoas")}>Pessoas</button>
        <button onClick={() => setAba("transacoes")}>Transações</button>
        <button onClick={() => setAba("totais")}>Totais</button>
      </nav>

      {aba === "pessoas" && <PessoasPanel />}
      {aba === "transacoes" && <TransacoesPanel />}
      {aba === "totais" && <TotaisPanel />}
    </div>
  )
}

export default App