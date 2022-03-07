using mvpApi.Models;
using System.Collections.Generic;

namespace mvpApi.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        IEnumerable<Usuario> ConsultaUsuariosPorCPF(string cpf);
        IEnumerable<Usuario> ConsultaUsuariosPorEmail(string email);
    }
}
