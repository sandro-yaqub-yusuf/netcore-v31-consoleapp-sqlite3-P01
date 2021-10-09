using System.Collections.Generic;
using KITAB.CRUD.Products.Domain.Models;

namespace KITAB.CRUD.Products.Application.Notificadores
{
    public interface INotificadorService
    {
        void Handle(Notificacao notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}
