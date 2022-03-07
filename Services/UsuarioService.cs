using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using mvpApi.Configuration;
using mvpApi.DTOs;
using mvpApi.Models;
using mvpApi.Repositories;
using mvpApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace mvpApi.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuarioService(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        public void CadastrarUsuario(UsuarioDTO UsuarioDTO)
        {
            // TODO: otimizar usando autoMapper
            var usuarioBase = new Usuario();

            usuarioBase.Id = (int)UsuarioDTO.id;
            usuarioBase.Nome = UsuarioDTO.nome;
            usuarioBase.CpfCnpj = UsuarioDTO.cpf_cnpj;
            usuarioBase.Email = UsuarioDTO.email;
            // senha deve ser criptografada
            usuarioBase.Senha = UsuarioDTO.senha;

            _iUsuarioRepository.Insert(usuarioBase);
        }

        public UsuarioDTO ConsultarUsuarioPorCpfCnpj(string CpfCnpj)
        {
            // TODO: Otimizar com autoMapper
            var usuarioBase = _iUsuarioRepository.ConsultaUsuariosPorCPF(CpfCnpj).FirstOrDefault();
            var usuarioDTO = new UsuarioDTO();

            usuarioDTO.id = usuarioBase.Id;

            usuarioDTO.nome = usuarioBase.Nome;
            usuarioDTO.email = usuarioBase.Email;
            usuarioDTO.cpf_cnpj = usuarioBase.CpfCnpj;
            usuarioDTO.senha = usuarioBase.Senha;

            return usuarioDTO;
        }

        public UsuarioDTO ConsultarUsuarioPorEmail(string email)
        {
            // TODO: Otimizar com autoMapper
            var usuarioBase = _iUsuarioRepository.ConsultaUsuariosPorEmail(email).FirstOrDefault();
            var usuarioDTO = new UsuarioDTO();

            usuarioDTO.id = usuarioBase.Id;

            usuarioDTO.nome = usuarioBase.Nome;
            usuarioDTO.email = usuarioBase.Email;
            usuarioDTO.cpf_cnpj = usuarioBase.CpfCnpj;
            usuarioDTO.senha = usuarioBase.Senha;

            return usuarioDTO;
        }

        public object GerarToken(
            UsuarioDTO usuarioDTO,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            try
            {
                // TODO: USUARIO REPOSITORY
                var UsuarioSettings = _iUsuarioRepository.ConsultaUsuariosPorEmail(usuarioDTO.email).FirstOrDefault();

                if (UsuarioSettings == null)
                {
                    throw new Exception();
                }

                if (UsuarioSettings.Email.Equals(usuarioDTO.email)
                    && UsuarioSettings.Senha.Equals(usuarioDTO.senha))                    
                    {

                    ClaimsIdentity identity =
                        new ClaimsIdentity(new GenericIdentity(usuarioDTO.email, "Autenticacao"),
                                            new[] {
                                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                                    new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDTO.email),
                                            });

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    usuarioDTO.nome = UsuarioSettings.Nome;
                    
                    return new
                    {
                        success = true,
                        access_token = token,
                        expires_in = TimeSpan.FromMilliseconds(tokenConfigurations.Seconds),
                        token_type = "BearerToken",
                        usuario = usuarioDTO
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                return new
                {
                    sucess = "false",
                    authenticated = false,
                    message = "falha ao autenticar"
                };
            }
        }
    }
}
