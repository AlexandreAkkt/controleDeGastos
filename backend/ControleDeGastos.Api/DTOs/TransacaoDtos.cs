using System.ComponentModel.DataAnnotations;
using ControleDeGastos.Api.Models;

namespace ControleDeGastos.Api.DTOs;

/// Dados necessários para criar uma nova transação.
public class CreateTransacaoDto
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(300)]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "A pessoa é obrigatória.")]
    public Guid PessoaId { get; set; }
}

/// Representação de uma transação retornada pela API.
public class TransacaoDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid PessoaId { get; set; }
    public string? PessoaNome { get; set; }
}