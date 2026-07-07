using ControleDeGastos.Api.Data;
using ControleDeGastos.Api.DTOs;
using ControleDeGastos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Api.Controllers;

/// <summary>
/// Endpoint de consulta de totais: receitas, despesas e saldo por pessoa,
/// além do total geral consolidado de todas as pessoas.
/// </summary>
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
        // Carregamos as pessoas com suas transações para calcular os totais em memória.
        // Para o volume de dados esperado nesse desafio isso é simples e legível;
        // em um cenário de produção com grande volume, o agregado seria feito via SQL (GroupBy).
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

        var resultado = new TotalGeralDto
        {
            Pessoas = totaisPorPessoa,
            TotalReceitas = totaisPorPessoa.Sum(t => t.TotalReceitas),
            TotalDespesas = totaisPorPessoa.Sum(t => t.TotalDespesas)
        };

        return Ok(resultado);
    }
}
