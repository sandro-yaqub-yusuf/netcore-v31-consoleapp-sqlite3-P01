using System;
using System.Collections.Generic;
using KITAB.CRUD.Products.Application.Notificadores;
using KITAB.CRUD.Products.Domain.Models;
using KITAB.CRUD.Products.Infra.Produtos;

namespace KITAB.CRUD.Products.Application.Produtos
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificadorService notificadorService) : base(notificadorService)
        {
            _produtoRepository = produtoRepository;
        }

        public void Inserir(Produto produto)
        {
            try
            {
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = null;

                _produtoRepository.Inserir(produto);
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }
        }

        public void Alterar(Produto produto)
        {
            try
            {
                produto.DataAlteracao = DateTime.Now;

                _produtoRepository.Alterar(produto);
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                _produtoRepository.Excluir(id);
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }
        }

        public Produto ObterPorId(int id)
        {
            Produto _produto = null;

            try
            {
                _produto = _produtoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }

            return _produto;
        }

        public List<Produto> ObterTodos()
        {
            List<Produto> _produto = null;

            try
            {
                _produto = _produtoRepository.ObterTodos();
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }

            return _produto;
        }

        public void ExecuteSQL(string sql)
        {
            try
            {
                _produtoRepository.ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }
        }

        public void CriarTabelaProduto()
        {
            try
            {
                _produtoRepository.CriarTabelaProduto();
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
            }
        }
    }
}
