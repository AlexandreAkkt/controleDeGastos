namespace ControleDeGastos.Api.Models;

///
/// Representa uma pessoa cadastrada no sistema de controle de gastos residenciais.
/// 
public class Pessoa
{
    /// Identificador único, gerado automaticamente pelo servidor 
    public Guid Id { get; set; } = Guid.NewGuid();

    /// Nome completo da pessoa
    public string Nome { get; set; } = string.Empty;

    ///Idade da pessoa, usada para aplicar a regra de menor de idade.
    public int Idade { get; set; }

    /// 
    /// Transações associadas a essa pessoa.
    /// Configurado ver AppDbContext com delete em cascata: ao remover a pessoa,
    /// todas as suas transações são removidas junto.
    /// 
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();

    ///Regra de negócio: pessoa é considerada menor de idade se tiver menos de 18 anos.</summary>
    public bool EhMenorDeIdade => Idade < 18;
}