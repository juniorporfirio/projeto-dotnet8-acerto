using Acerto.Produtos.API.Infra;
using Acerto.Produtos.API.Apresentacao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApresentacao();

var app = builder.Build();

//Endpoints
app.AddEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

