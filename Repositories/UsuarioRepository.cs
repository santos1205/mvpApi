using mvpApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvpApi.Repositories
{  

    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(masterContext dbContext) : base(dbContext) { }

        public IEnumerable<Usuario> ConsultaUsuariosPorCPF(string cpf)
        {
            try
            {
                var Usuarios = _dbContext.Usuario
                            .Where(p => p.CpfCnpj.Equals(cpf));

                return Usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Usuario> ConsultaUsuariosPorEmail(string email)
        {
            try
            {
                var Usuarios = _dbContext.Usuario
                            .Where(p => p.Email.Equals(email));

                return Usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
