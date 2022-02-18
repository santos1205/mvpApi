using Microsoft.AspNetCore.Mvc;
using mvpApi.Configuration;
using mvpApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvpApi.Services.Interfaces
{
    public interface IUsuarioService
    {
        void CadastrarUsuario(UsuarioDTO UsuarioDTO);
        UsuarioDTO ConsultarUsuarioPorCpfCnpj(string CpfCnpj);
        object GerarToken(UsuarioDTO UsuarioDTO,
                            [FromServices] SigningConfigurations SigningConfigurations,
                            [FromServices] TokenConfigurations TokenConfigurations);
    }
}
