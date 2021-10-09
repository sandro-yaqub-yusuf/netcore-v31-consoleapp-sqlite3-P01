using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.Infra.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void CriarTabelaProduto();
    }
}
