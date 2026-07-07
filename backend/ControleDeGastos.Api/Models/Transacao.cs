namespace ControleDeGastos.Api.Models;

/// Representa uma transação financeira (receita ou despesa) vinculada a uma pessoa.

public class Transacao
{
    /// Identificador único, gerado automaticamente pelo servidor 
    public Guid Id { get; set; } = Guid.NewGuid();

    ///Descrição livre da transaçãoex: "Supermercado", "Salário"
    public string Descricao { get; set; } = string.Empty;

    ///Valor monetário da transação. Sempre armazenado como um número positivo.
    public decimal Valor { get; set; }

    ///Tipo da transação: Receita ou Despesa.
    public TipoTransacao Tipo { get; set; }

    /// Chave estrangeira para a pessoa dona dessa transação.
    public Guid PessoaId { get; set; }

    /// Navegação para a pessoa dona dessa transação
    public Pessoa? Pessoa { get; set; }
}