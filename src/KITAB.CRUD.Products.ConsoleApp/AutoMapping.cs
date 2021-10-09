using AutoMapper;
using KITAB.CRUD.Products.ConsoleApp.ViewModels;
using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.ConsoleApp
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
