namespace ControleDeGastos.Api.Models;

/// <summary>
/// Representa uma pessoa cadastrada no sistema de controle de gastos residenciais.
/// </summary>
public class Pessoa
{
    /// <summary>Identificador único, gerado automaticamente pelo servidor (Guid).</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Nome completo da pessoa.</summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>Idade da pessoa, usada para aplicar a regra de menor de idade.</summary>
    public int Idade { get; set; }

    /// <summary>
    /// Transações associadas a essa pessoa.
    /// Configurado (ver AppDbContext) com delete em cascata: ao remover a pessoa,
    /// todas as suas transações são removidas junto.
    /// </summary>
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

    /// <summary>Regra de negócio: pessoa é considerada menor de idade se tiver menos de 18 anos.</summary>
    public bool EhMenorDeIdade => Idade < 18;
}