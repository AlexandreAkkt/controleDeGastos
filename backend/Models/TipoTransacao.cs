namespace ControleDeGastos.Api.Models;

/// <summary>
/// Tipo de uma transação financeira.
/// Representamos como enum para garantir que apenas os dois valores
/// válidos ("Receita" ou "Despesa") possam existir no sistema.
/// </summary>
public enum TipoTransacao
{
    Receita = 0,
    Despesa = 1
}