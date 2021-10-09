using System.Collections.Generic;
using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.Application.Produtos
{
    public interface IProdutoService
    {
        void Inserir(Produto produto);
        void Alterar(Produto produto);
        void Excluir(int id);
        Produto ObterPorId(int id);
        List<Produto> ObterTodos();
        void ExecuteSQL(string sql);
        void CriarTabelaProduto();
    }
}
