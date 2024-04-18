using Acerto.Pedidos.API.Apresentacao;
using Acerto.Pedidos.API.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApresentacao();
builder.Services.AddInfra(builder.Configuration);

var app = builder.Build();

app.AddEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
