using ControleDeGastos.Api.Data;
using ControleDeGastos.Api.DTOs;
using ControleDeGastos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Api.Controllers;

// Aqui o endpoint  calcula os totais: receita, despesa e saldo de cada pessoa,
//  no final o total geral somando todo mundo
[ApiController]
[Route("api/totais")]
public class TotaisController : ControllerBase
{
    private readonly AppDbContext _context;

    public TotaisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<TotalGeralDto>> Consultar()
    {
        // pego cada pessoa junto com as transacoes dela pra poder somar
        var pessoas = await _context.Pessoas
            .Include(p => p.Transacoes)
            .OrderBy(p => p.Nome)
            .ToListAsync();

        var totaisPorPessoa = pessoas.Select(p => new TotalPessoaDto
        {
            PessoaId = p.Id,
            Nome = p.Nome,
            Idade = p.Idade,
            TotalReceitas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
            TotalDespesas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor)
        }).ToList();

        // soma tudo de novo pra ter o total geral (de todas as pessoas juntas)
        var resultado = new TotalGeralDto
        {
            Pessoas = totaisPorPessoa,
            TotalReceitas = totaisPorPessoa.Sum(t => t.TotalReceitas),
            TotalDespesas = totaisPorPessoa.Sum(t => t.TotalDespesas)
        };

        return Ok(resultado);
    }
}