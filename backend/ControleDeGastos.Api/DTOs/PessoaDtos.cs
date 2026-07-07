using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.Api.DTOs;

/// <summary>Dados necessários para criar uma nova pessoa.</summary>
public class CreatePessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150.")]
    public int Idade { get; set; }
}

/// <summary>Representação de uma pessoa retornada pela API.</summary>
public class PessoaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}