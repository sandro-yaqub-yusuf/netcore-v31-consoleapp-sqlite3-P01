using System;
using AutoMapper;
using KITAB.CRUD.Products.Application.Notificadores;
using KITAB.CRUD.Products.Application.Produtos;
using KITAB.CRUD.Products.ConsoleApp.ViewModels;
using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.ConsoleApp
{
    public class ConsoleApplication
    {
        private readonly INotificadorService _notificadorService;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ConsoleApplication(INotificadorService notificadorService,
                                  IProdutoService produtoService,
                                  IMapper mapper)
        {
            _notificadorService = notificadorService;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        public void Run()
        {
            Console.WriteLine("Iniciando o processamento...");
            Console.WriteLine("");

            while (true)
            {
                if (!Produtos_Criar_Tabela()) break;
                if (!Produtos_Inserir_Tabela()) break;
                if (!Produtos_InserirVarios_Tabela()) break;
                if (!Produtos_Alterar_Tabela()) break;
                if (!Produtos_Excluir_Tabela()) break;
                if (!Produtos_ObterTodos_Tabela()) break;
                if (!Produtos_ObterPorId_Tabela()) break;

                break;
            }

            // Verifica se há notificações vinda da camada de negócio
            // Caso tenha notificações, devem ser exibidas ao usuário
            if (_notificadorService.TemNotificacao()) ListarNotificacoes();

            Console.WriteLine("Processo concluído...");
            Console.WriteLine("");
        }

        private bool Produtos_Criar_Tabela()
        {
            Console.WriteLine("Criando a tabela de Produtos...");
            Console.WriteLine("");

            _produtoService.CriarTabelaProduto();

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_Inserir_Tabela()
        {
            Console.WriteLine("Inserindo um produto na tabela de Produtos...");
            Console.WriteLine("");

            var _produtoDTO = new ProdutoViewModel()
            {
                Nome = "Produto 1",
                Descricao = "Descrição do Produto 1",
                Imagem = "9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg",
                Qtde = 1,
                PrecoCusto = 10,
                PrecoVenda = 20,
                DataCadastro = DateTime.Now,
                DataAlteracao = null,
                Situacao = "A"
            };

            var _produto = _mapper.Map<Produto>(_produtoDTO);

            // Insere os dados do produto na tabela "Produto"
            _produtoService.Inserir(_produto);

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_InserirVarios_Tabela()
        {
            Console.WriteLine("Inserindo 100 produtos na tabela de Produtos...");
            Console.WriteLine("");

            // Insere os dados do produto na tabela "Produto"
            var _produtos = "";
            var _contar = 0;

            for (int i = 2; i <= 101; i++)
            {
                _produtos += "INSERT INTO Produto " +
                             "(Nome, Descricao, Imagem, Qtde, PrecoCusto, PrecoVenda, DataCadastro, Situacao) VALUES (" +
                             "'Produto " + i.ToString() + "', " +
                             "'Descrição do Produto " + i.ToString() + "', " +
                             "'9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg', " +
                             (i * 2).ToString() + ", " +
                             (i * 10).ToString() + ", " +
                             (i * 15).ToString() + ", " +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.0000000") + "', " +
                             "'A');";

                _contar++;

                if (_contar >= 50)
                {
                    // Insere 50 produtos por vez na tabela "Produto"
                    _produtoService.ExecuteSQL(_produtos);

                    if (_notificadorService.TemNotificacao()) break;

                    _produtos = "";
                    _contar = 0;
                }
            }

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_Alterar_Tabela()
        {
            Console.WriteLine("Alterando o produto com o ID = 10 na tabela de Produtos...");
            Console.WriteLine("");

            // Altera os dados do produto na tabela "Produto"
            _produtoService.Alterar(new Produto()
            {
                Id = 10,
                Nome = "Produto 10 - Alterado",
                Descricao = "Descrição do Produto 10 - Alterado",
                Imagem = "9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg",
                Qtde = 0,
                PrecoCusto = 0,
                PrecoVenda = 0,
                DataAlteracao = DateTime.Now,
                Situacao = "D"
            });

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_Excluir_Tabela()
        {
            Console.WriteLine("Excluindo o produto com o ID = 11 na tabela de Produtos...");
            Console.WriteLine("");

            // Exclui os dados do produto com o ID = 11 na tabela "Produto"
            _produtoService.Excluir(11);

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_ObterTodos_Tabela()
        {
            Console.WriteLine("Listando todos os produtos da tabela de Produtos...");
            Console.WriteLine("");

            // Pega todos os produtos na tabela "Produto"
            var _listaProdutos = _produtoService.ObterTodos();

            if (!_notificadorService.TemNotificacao())
            {
                foreach (var _produto in _listaProdutos)
                {
                    Console.WriteLine("ID: " + _produto.Id + " - Produto: " + _produto.Descricao + " - " +
                                      "Quantidade: " + _produto.Qtde + " - Preço de Venda: " + _produto.PrecoVenda);
                }
            }

            Console.WriteLine("");

            return (!_notificadorService.TemNotificacao());
        }

        private bool Produtos_ObterPorId_Tabela()
        {
            Console.WriteLine("Listando o produto com o ID = 10 da tabela de Produtos...");
            Console.WriteLine("");

            // Localiza os dados do produto com o ID = 10 na tabela "Produto"
            var _produto = _produtoService.ObterPorId(10);

            var _produtoDTO = _mapper.Map<ProdutoViewModel>(_produto);

            if (!_notificadorService.TemNotificacao())
            {
                Console.WriteLine("ID: " + _produtoDTO.Id + " - " +
                                  "Produto: " + _produtoDTO.Nome + " - " +
                                  "Descrição: " + _produtoDTO.Descricao + " - " +
                                  "Imagem: " + _produtoDTO.Imagem + " - " +
                                  "Quantidade: " + _produtoDTO.Qtde + " - " +
                                  "Preço de Custo: " + _produtoDTO.PrecoCusto + " - " +
                                  "Preço de Venda: " + _produtoDTO.PrecoVenda + " - " +
                                  "Data Cadastro: " + _produtoDTO.DataCadastro.ToString("dd/MM/yyyy") + " - " +
                                  "Data Alteração: " + _produtoDTO.DataAlteracao?.ToString("dd/MM/yyyy") + " - " +
                                  "Situação: " + _produtoDTO.Situacao);

                Console.WriteLine("");
            }

            return (!_notificadorService.TemNotificacao());
        }

        private void ListarNotificacoes()
        {
            Console.WriteLine("Ocorreu um ERRO !!! Listando as notificações...");
            Console.WriteLine("");

            var _notificar = _notificadorService.ObterNotificacoes();

            foreach (var item in _notificar)
            {
                Console.WriteLine(item.Mensagem);
            }

            Console.WriteLine("");
        }
    }
}
