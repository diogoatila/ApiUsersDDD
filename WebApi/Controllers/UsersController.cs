using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Domain.Interfaces;
using System.Text;
using WebApi.Models;
using WebAPIs.Token;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        private readonly IMapper _IMapper;
        private readonly IEscolaridade _IEscolaridade;
        private readonly IApplicationUser _IApplicationUser;

        public UsersController(IMapper iMapper, IEscolaridade iEscolaridade, IApplicationUser iApplicationUser)
        {
            _IMapper = iMapper;
            _IEscolaridade = iEscolaridade;   
            _IApplicationUser = iApplicationUser;
        }



        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UserViewModel  applicationUser)
        {
            try
            {
                bool isValid = Validator(applicationUser);
                if (!isValid) return Ok("Erro: Todos os campos devem ser preenchidos!");

                if (Convert.ToDateTime(applicationUser.DataNascimento) > DateTime.Now)
                return Ok("Erro: A Data de Nascimento não pode ser maior que a data atual!");


          
                var user = new ApplicationUser
                {
                    Nome = applicationUser.Nome,
                    Sobrenome = applicationUser.Sobrenome,
                    DataNascimento = Convert.ToDateTime(applicationUser.DataNascimento),
                    Documento = applicationUser.Documento,
                    Tipo = TipoUsuario.Comum,
                    EscolaridadeId = applicationUser.EscolaridadeId

                };

                await _IApplicationUser.Add(user);

                return Ok("Usuário inserido com sucesso");
            }
            catch (Exception ex)
            {
                return Ok($"Erro ao inserir usuário, {ex.Message}");
            }


        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPut("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UserViewModel applicationUser)
        {

            try
            {
                bool isValid = Validator(applicationUser);
                if (!isValid) return Ok("Erro: Todos os campos devem ser preenchidos!");

                if (Convert.ToDateTime(applicationUser.DataNascimento) > DateTime.Now)
                    return Ok("Erro: A Data de Nascimento não pode ser maior que a data atual!");

                var user = new ApplicationUser
                {
                    Id = applicationUser.Id,
                    Nome = applicationUser.Nome,
                    Sobrenome = applicationUser.Sobrenome,
                    DataNascimento = Convert.ToDateTime(applicationUser.DataNascimento),
                    Documento = applicationUser.Documento,
                    Tipo = TipoUsuario.Comum,
                    EscolaridadeId = applicationUser.EscolaridadeId

                };

                await _IApplicationUser.Update(user);

                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Ok($"Erro ao atualizar usuário, {ex.Message}");
            }
              

        }

        [AllowAnonymous]
        [HttpPost("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuario([FromBody] UserViewModel applicationUser)
        {

            try
            {
                var user = new ApplicationUser
                {
                    Id = applicationUser.Id,
                    Nome = applicationUser.Nome,
                    Sobrenome = applicationUser.Sobrenome,
                    DataNascimento = Convert.ToDateTime(applicationUser.DataNascimento),
                    Documento = applicationUser.Documento,
                    Tipo = TipoUsuario.Comum,
                    EscolaridadeId = applicationUser.EscolaridadeId

                };

                await _IApplicationUser.Delete(user);

                return Ok("Usuário Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return Ok($"Erro ao deletar usuário, {ex.Message}");
            }




        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("ListarUsuarios")]
        public async Task<List<UserViewModel>> ListarUsuarios()
        {

            try
            {
                var usuarios = _IApplicationUser.List().Result;
                foreach (var usuario in usuarios)
                {

                    var escolaridade = _IEscolaridade.GetEntityById(usuario.EscolaridadeId);
                    usuario.Escolaridade = escolaridade.Result.Descricao;

                }

                var userMap = _IMapper.Map<List<UserViewModel>>(usuarios);

                return userMap;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Validator(UserViewModel applicationUser)
        {
            if (string.IsNullOrEmpty(applicationUser.Nome) || string.IsNullOrEmpty(applicationUser.Sobrenome)
                || string.IsNullOrEmpty(applicationUser.Documento) || applicationUser.DataNascimento == null || applicationUser.EscolaridadeId == 0)
                return false;
            else 
                return true;
        }

    }
}
