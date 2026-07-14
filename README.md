 Controle de Gastos

Este projeto foi desenvolvido como parte de um desafio técnico para uma vaga de estágio em desenvolvimento.

O objetivo foi criar uma aplicação para controlar gastos residenciais, permitindo cadastrar pessoas, registrar receitas e despesas e consultar os totais de cada pessoa e do sistema.

Além de atender aos requisitos do desafio, aproveitei o projeto para praticar conceitos de C#, .NET, React, TypeScript e desenvolvimento de APIs.



 Funcionalidades

Cadastro de Pessoas

- Cadastrar pessoas
- Listar pessoas cadastradas
- Excluir pessoas
- Cada pessoa possui:
  - Id (gerado automaticamente)
  - Nome
  - Idade

> Ao excluir uma pessoa, todas as suas transações também são removidas.



 Cadastro de Transações

É possível cadastrar e listar transações financeiras.

Cada transação possui:

- Id
- Descrição
- Valor
- Tipo (Receita ou Despesa)
- Pessoa vinculada

### Regra de negócio

Pessoas menores de 18 anos podem cadastrar apenas despesas.

Caso seja informada uma receita para um menor de idade, a API bloqueia o cadastro.

---

Consulta de Totais

O sistema apresenta:

- Total de receitas por pessoa
- Total de despesas por pessoa
- Saldo de cada pessoa
- Totais gerais de receitas, despesas e saldo

---

 Tecnologias utilizadas

 Back-end

- C#
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQLite

### Front-end

- React
- TypeScript



 Estrutura do projeto

```
controleDeGastos/
├── backend/
│   └── ControleDeGastos.Api
└── frontend/
```

---

Como executar

Back-end

```bash
cd backend/ControleDeGastos.Api
dotnet restore
dotnet run
```

### Front-end

```bash
cd frontend
npm install
npm start
```

---

 Regras de negócio

- Toda transação precisa estar vinculada a uma pessoa cadastrada.
- Menores de 18 anos podem possuir apenas despesas.
- Ao excluir uma pessoa, todas as suas transações são removidas automaticamente.
- Conforme solicitado no desafio, as transações podem apenas ser cadastradas e listadas.

---

 Aprendizados

Durante o desenvolvimento deste projeto pude praticar:

- Desenvolvimento de APIs REST com .NET
- Entity Framework Core
- Relacionamento entre entidades
- Persistência de dados com SQLite
- Consumo de API utilizando React e TypeScript
- Organização do projeto em camadas
- Aplicação de regras de negócio no back-end

---

 Observações

Este é um projeto de estudos desenvolvido para colocar em prática os conhecimentos adquiridos durante a faculdade e nos meus estudos de programação.

Ainda existem melhorias que podem ser implementadas no futuro, mas o principal objetivo foi praticar desenvolvimento full stack utilizando.
