using Microsoft.AspNetCore.Mvc;
using mvpApi.DTOs;
using mvpApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvpApi.Controllers
{
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
                //if (!string.IsNullOrEmpty(Usuario.cpf_cnpj))
                //{
                //    if (Usuario.cpf_cnpj.Length > 14)
                //    {
                //        Usuario.cnpj_validacao = Usuario.cpf_cnpj;
                //    }
                //    else
                //    {
                //        Usuario.cpf_validacao = Usuario.cpf_cnpj;
                //    }
                //}

                //// Verificação de validação de campos
                //bool isUsuarioValid = CheckValidations(Usuario, ref validationResults);
                //if (!isUsuarioValid)
                //{
                //    return Ok(new { error = validationResults });
                //}

                //_iUsuarioService.CadastrarUsuario(Usuario);

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

    }
}
