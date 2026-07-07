namespace ControleDeGastos.Api.DTOs;

/// <summary>Totais de receitas, despesas e saldo de uma pessoa específica.</summary>
public class TotalPessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    /// <summary>Saldo = total de receitas - total de despesas.</summary>
    public decimal Saldo => TotalReceitas - TotalDespesas;
}

/// <summary>Consolidado geral de todas as pessoas cadastradas.</summary>
public class TotalGeralDto
{
    public List<TotalPessoaDto> Pessoas { get; set; } = new();
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    /// <summary>Saldo líquido = total geral de receitas - total geral de despesas.</summary>
    public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
}