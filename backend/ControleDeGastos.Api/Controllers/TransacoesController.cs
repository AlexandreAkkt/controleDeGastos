using ControleDeGastos.Api.Data;
using ControleDeGastos.Api.DTOs;
using ControleDeGastos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Api.Controllers;

/// Endpoints de gerenciamento de transaçõe e criação e listagem.
/// Edição e deleção não fazem parte do desafio.

[ApiController]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }

   
    /// Lista todas as transações cadastradas
    /// Aceita opcionalmente um filtro por pessoa 
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> Listar([FromQuery] Guid? pessoaId)
    {
        var query = _context.Transacoes
            .Include(t => t.Pessoa)
            .AsQueryable();

        if (pessoaId is not null)
        {
            query = query.Where(t => t.PessoaId == pessoaId);
        }

        var transacoes = await query
            .OrderByDescending(t => t.Id)
            .Select(t => new TransacaoDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo,
                PessoaId = t.PessoaId,
                PessoaNome = t.Pessoa != null ? t.Pessoa.Nome : null
            })
            .ToListAsync();

        return Ok(transacoes);
    }

  
    /// Cadastra uma nova transação
    /// Regras de negócio 
    /// 1. A pessoa informada precisa existir previamente no cadastro de pessoas.
    /// 2. Se a pessoa for < de idade  18 anos, somente transações
    ///    do tipo Despesa podem ser cadastradas para ela.
    
    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Criar([FromBody] CreateTransacaoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);
        if (pessoa is null)
        {
            return BadRequest(new { mensagem = "A pessoa informada não existe no cadastro de pessoas." });
        }

        // Regra de negócio: menores de 18 anos só podem ter despesas cadastradas.
        if (pessoa.EhMenorDeIdade && dto.Tipo == TipoTransacao.Receita)
        {
            return BadRequest(new
            {
                mensagem = "Pessoas menores de idade (menos de 18 anos) só podem ter despesas cadastradas, não receitas."
            });
        }

        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Descricao = dto.Descricao.Trim(),
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        var resultado = new TransacaoDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            PessoaId = transacao.PessoaId,
            PessoaNome = pessoa.Nome
        };

        return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
    }
}