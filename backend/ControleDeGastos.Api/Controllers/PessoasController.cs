using ControleDeGastos.Api.Data;
using ControleDeGastos.Api.DTOs;
using ControleDeGastos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Api.Controllers;

/// <summary>
/// Endpoints de gerenciamento de pessoas: criação, listagem e remoção.
/// </summary>
[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PessoasController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todas as pessoas cadastradas.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> Listar()
    {
        var pessoas = await _context.Pessoas
            .OrderBy(p => p.Nome)
            .Select(p => new PessoaDto { Id = p.Id, Nome = p.Nome, Idade = p.Idade })
            .ToListAsync();

        return Ok(pessoas);
    }

    /// <summary>Cadastra uma nova pessoa. O identificador é gerado automaticamente.</summary>
    [HttpPost]
    public async Task<ActionResult<PessoaDto>> Criar([FromBody] CreatePessoaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome.Trim(),
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        var resultado = new PessoaDto { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade };
        return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
    }

    /// <summary>
    /// Remove uma pessoa pelo identificador.
    /// Regra de negócio: como a relação está configurada com delete em cascata
    /// (ver AppDbContext), todas as transações dessa pessoa são removidas junto.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa is null)
        {
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}