using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.Infra.Produtos
{
    public class ProdutoRepository : Repository, IProdutoRepository
    {
        public void Inserir(Produto produto)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();
                
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Insere o dado do produto na tabela "Produto"
                        var _sql = "INSERT INTO Produto " +
                                   "(Nome, Descricao, Imagem, Qtde, PrecoCusto, PrecoVenda, DataCadastro, DataAlteracao, Situacao) " +
                                   "VALUES (@Nome, @Descricao, @Imagem, @Qtde, @PrecoCusto, @PrecoVenda, @DataCadastro, @DataAlteracao, @Situacao);" +
                                   "select last_insert_rowid();";

                        produto.Id = cnn.Query<int>(_sql, produto).First();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void Alterar(Produto produto)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Altera o dado do produto na tabela "Produto"
                        var _sql = "UPDATE Produto SET " +
                                   "Nome = @Nome, " +
                                   "Descricao = @Descricao, " +
                                   "Imagem = @Imagem, " +
                                   "Qtde = @Qtde, " +
                                   "PrecoCusto = @PrecoCusto, " +
                                   "PrecoVenda = @PrecoVenda, " +
                                   "DataAlteracao = @DataAlteracao, " +
                                   "Situacao = @Situacao " +
                                   "WHERE Id = @Id;";

                        cnn.Execute(_sql, produto);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void Excluir(int id)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Exclui o dado do produto na tabela "Produto"
                        var _sql = "DELETE FROM Produto WHERE Id = @id;";

                        cnn.Execute(_sql, new {id});

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public Produto ObterPorId(int id)
        {
            Produto _produto = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var _sql = "SELECT * FROM Produto WHERE Id = @id;";

                    _produto = cnn.Query<Produto>(_sql, new {id}).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                cnn?.Close();
                cnn?.Dispose();
            }

            return _produto;
        }

        public List<Produto> ObterTodos()
        {
            List<Produto> _produto = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var _sql = "SELECT * FROM Produto ORDER BY Id ASC;";

                    _produto = cnn.Query<Produto>(_sql).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                cnn?.Close();
                cnn?.Dispose();
            }

            return _produto;
        }

        public void ExecuteSQL(string sql)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Executa as instruções _sql na tabela "Produto"
                        cnn.Execute(sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void CriarTabelaProduto()
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();
                
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Cria a tabela "Produto"
                        var _sql = "CREATE TABLE IF NOT EXISTS Produto (" +
                                   "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                   "Nome VARCHAR(200) NOT NULL, " +
                                   "Descricao VARCHAR(1000) NOT NULL, " +
                                   "Imagem VARCHAR(200) NOT NULL, " +
                                   "Qtde INTEGER NOT NULL, " +
                                   "PrecoCusto DOUBLE NOT NULL, " +
                                   "PrecoVenda DOUBLE NOT NULL, " +
                                   "DataCadastro DATETIME NOT NULL, " +
                                   "DataAlteracao DATETIME NULL," +
                                   "Situacao CHAR(1) NOT NULL DEFAULT 'A');";

                        cnn.Execute(_sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }
    }
}
