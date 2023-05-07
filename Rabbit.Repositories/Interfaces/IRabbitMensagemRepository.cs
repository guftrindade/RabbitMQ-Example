using Rabbit.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Repositories.Interfaces
{
    public interface IRabbitMensagemRepository
    {
        void SendMensagem(RabbitMensagem mensagem);
    }
}
