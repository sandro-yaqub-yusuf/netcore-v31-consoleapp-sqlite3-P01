using System.Collections.Generic;

namespace KITAB.CRUD.Products.Infra
{
    public interface IRepository<T> where T : class
    {
        void Inserir(T obj);
        void Alterar(T obj);
        void Excluir(int id);
        T ObterPorId(int id);
        List<T> ObterTodos();
        void ExecuteSQL(string sql);
    }
}
