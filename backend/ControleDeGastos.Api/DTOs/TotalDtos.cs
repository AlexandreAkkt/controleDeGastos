namespace ControleDeGastos.Api.DTOs;

/// Totais de receitas, despesas e saldo de uma pessoa específica
public class TotalPessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    ///Saldo = total de receitas - total de despesas.
    public decimal Saldo => TotalReceitas - TotalDespesas;
}

/// Consolidado geral de todas as pessoas cadastradas.
public class TotalGeralDto
{
    public List<TotalPessoaDto> Pessoas { get; set; } = new();
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    /// Saldo líquido = total geral de receitas - total geral de despesas
    public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
}