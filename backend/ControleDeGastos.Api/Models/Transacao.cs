namespace ControleDeGastos.Api.Models;

/// <summary>
/// Representa uma transação financeira (receita ou despesa) vinculada a uma pessoa.
/// </summary>
public class Transacao
{
    /// <summary>Identificador único, gerado automaticamente pelo servidor (Guid).</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Descrição livre da transação (ex: "Supermercado", "Salário").</summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>Valor monetário da transação. Sempre armazenado como um número positivo.</summary>
    public decimal Valor { get; set; }

    /// <summary>Tipo da transação: Receita ou Despesa.</summary>
    public TipoTransacao Tipo { get; set; }

    /// <summary>Chave estrangeira para a pessoa dona dessa transação.</summary>
    public Guid PessoaId { get; set; }

    /// <summary>Navegação para a pessoa dona dessa transação.</summary>
    public Pessoa? Pessoa { get; set; }
}