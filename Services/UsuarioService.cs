﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using mvpApi.Configuration;
using mvpApi.DTOs;
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
        public void CadastrarUsuario(UsuarioDTO UsuarioDTO)
        {
            throw new NotImplementedException();
        }

        public UsuarioDTO ConsultarUsuarioPorCpfCnpj(string CpfCnpj)
        {
            throw new NotImplementedException();
        }       

        public object GerarToken(
            UsuarioDTO UsuarioDTO,
            [FromServices] SigningConfigurations SigningConfigurations,
            [FromServices] TokenConfigurations TokenConfigurations)
        {
            try
            {
                // TODO: USUARIO REPOSITORY
                //var UsuarioSettings = _IUsuarioRepository.ConsultarPorEmail(UsuarioDTO.email).FirstOrDefault();

                //if (UsuarioSettings == null)
                //{
                //    throw new Exception();
                //}

                //if (UsuarioSettings.UsrEmail.Equals(UsuarioDTO.email)
                //    && UsuarioSettings.UsrSenha.Equals(UsuarioDTO.senha))
                if (UsuarioDTO.email.Equals("mario@santos.com")
                    && UsuarioDTO.senha.Equals("123"))
                    {

                    ClaimsIdentity identity =
                        new ClaimsIdentity(new GenericIdentity(UsuarioDTO.email, "Autenticacao"),
                                            new[] {
                                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                                    new Claim(JwtRegisteredClaimNames.UniqueName, UsuarioDTO.email),
                                            });

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(TokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = TokenConfigurations.Issuer,
                        Audience = TokenConfigurations.Audience,
                        SigningCredentials = SigningConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    return new
                    {
                        sucess = "True",
                        access_token = token,
                        expires_in = TimeSpan.FromMilliseconds(TokenConfigurations.Seconds),
                        token_type = "BearerToken",
                        usuario = UsuarioDTO.nome
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
