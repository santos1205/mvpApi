using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvpApi.Configuration;
using mvpApi.DTOs;
using mvpApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvpApi.Controllers
{
    [Authorize("Bearer")]
    public class UsuarioController : ApplicationController
    {
        private readonly IUsuarioService _iUsuarioService;

        public UsuarioController(IUsuarioService IUsuarioService)
        {
            _iUsuarioService = IUsuarioService;
        }

        [HttpPost("api/usuario/cadastrar/")]
        public object Post([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var validationResults = new List<ValidationResult>();

                // Verificação cpf / cnpj
                //if (!string.IsNullOrEmpty(usuarioDTO.cpf_cnpj))
                //{
                //    if (usuarioDTO.cpf_cnpj.Length > 14)
                //    {
                //        usuarioDTO.cnpj_validacao = usuarioDTO.cpf_cnpj;
                //    }
                //    else
                //    {
                //        usuarioDTO.cpf_validacao = usuarioDTO.cpf_cnpj;
                //    }
                //}

                // Verificação de validação de campos
                bool isUsuarioValid = CheckValidations(usuarioDTO, ref validationResults);
                if (!isUsuarioValid)
                {
                    return Ok(new { error = validationResults });
                }

                _iUsuarioService.CadastrarUsuario(usuarioDTO);

                return Ok(new { msg = "Usuário cadastrado com sucesso." });
            }
            catch (Exception ex)
            {
                string outputError;
                if (ex.Message.Contains("inner exception"))
                {
                    outputError = ex.InnerException.Message != null ? ex.InnerException.Message : ex.InnerException.InnerException.Message != null ? ex.InnerException.InnerException.Message : ex.Message;
                }
                else
                {
                    outputError = ex.Message;
                }

                return Ok(new { error = outputError });
            }
        }
        [HttpGet("api/usuario/consultar/")]
        public object ConsultarUsuario([FromQuery(Name = "cpf")] string cpf_cnpj)
        {
            try
            {
                var usuario = _iUsuarioService.ConsultarUsuarioPorCpfCnpj(cpf_cnpj);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/usuario/token/")]
        public object Autenticacao([FromBody] UsuarioDTO usuario,
                            [FromServices] SigningConfigurations signingConfigurations,
                            [FromServices] TokenConfigurations tokenConfigurations)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.email) || string.IsNullOrEmpty(usuario.senha))
                {
                    throw new Exception("Email e senha são obrigatórios");
                }

                var Objeto = new object();

                if (usuario != null)
                {
                    Objeto = _iUsuarioService.GerarToken(usuario, signingConfigurations, tokenConfigurations);
                }

                return Ok(Objeto);
            }
            catch (System.Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
    }
}
