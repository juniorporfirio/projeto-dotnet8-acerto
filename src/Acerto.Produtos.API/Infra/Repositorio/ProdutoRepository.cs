using Acerto.Produtos.API.Dominio.Interfaces.Infra.Repositorio;
using Acerto.Produtos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Acerto.Produtos.API.Infra
{
    public class ProdutoRepository(AcertoContext context) : IProdutosRepository
    {
        private readonly AcertoContext _context = context;

        public async Task AdicionarNovo(Produto produto)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Produto?> PorId(Guid Id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstAsync(x=>x.Id == Id);
            return produto;
        }

        public async Task Remove(Guid Id)
        {
            Produto produto = await _context.Produtos.FindAsync(Id) ?? throw new ArgumentException("Id invalido");
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> Todos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
    }
}