# Projeto de API Utilizando Minimal API com .NET 8.0
Este projeto tem como objetivo demonstrar algumas funcionalidades do .NET 8 em um cenário de projeto realista.

## Bibliotecas Utilizadas
Este projeto está utilizando os seguintes pacotes.
- Refit
- FluentValidation
- MySql.EntityFrameworkCore
- Microsoft.EntityFramework.InMemory
- RabbitMq.Client
- Moq
- FluentAssertions
- Swashbuckle.AspNetCore

## Estrutura do Projeto API Produtos
```mermaid
graph TD
start((start))
api(api-Produtos)
camada-apresentacao(Camada Apresentação)
camada-dominio(Camada Dominio)
camada-infra(Camada InfraEstrutura)

start --> api
api --> camada-apresentacao
camada-apresentacao --> camada-infra
camada-apresentacao --> camada-dominio
camada-infra ---> camada-dominio
```

## Estrutura do Projeto API Pedidos
```mermaid
graph TD
start((start))
api(api-Pedidos)
camada-apresentacao(Camada Apresentação)
camada-dominio(Camada Dominio)
camada-infra(Camada InfraEstrutura)

start --> api
api --> camada-apresentacao
camada-apresentacao --> camada-infra
camada-apresentacao --> camada-dominio
camada-infra ---> camada-dominio
```
## Rodando a aplicação local
 Para executar a aplicação localmente é necessário ter o docker instalado para levantar o banco de dados e também a fila RabbitMq.

```bash
docker compose up
```
