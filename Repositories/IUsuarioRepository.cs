using mvpApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvpApi.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        IEnumerable<Usuario> ConsultaUsuariosPorCPF(string cpf);
    }
}
