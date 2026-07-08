using ControleDeGastos.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// banco sqlite salvo em arquivo, os dados ficam guardados mesmo fechando a aplicacao
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controledegastos.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// libera o front-end (rodando na porta 5173) pra poder chamar essa api
const string CorsPolicyName = "FrontEnd";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// garante que o banco e as tabelas existem antes de comecar a aplicacao
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicyName);
app.UseAuthorization();
app.MapControllers();

app.Run();