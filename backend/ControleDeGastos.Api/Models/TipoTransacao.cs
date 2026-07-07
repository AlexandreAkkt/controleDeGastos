namespace ControleDeGastos.Api.Models;


/// Tipo de uma transação financeira.
/// Representamos como enum para garantir que apenas os dois valores
/// válidos "Receita" ou "Despesa" possam existir no sistema

public enum TipoTransacao
{
    Receita = 0,
    Despesa = 1
}