using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IHistorico _IHistorico;
        private readonly IServiceHistorico _IServiceHistorico;

        public HistoricoController(IMapper iMapper, IHistorico iHistorico, IServiceHistorico iServiceHistorico)
        {
            _IMapper = iMapper;
            _IHistorico = iHistorico;
            _IServiceHistorico = iServiceHistorico;
        }


        [Produces("application/json")]
        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] HistoricoViewModel historico)
        {

            try
            {
                var filePath = Path.Combine("Storage", historico.CaminhoArquivo.FileName);

                string formato = Path.GetExtension(filePath).Replace(".", "");
                if (formato != TipoHistorico.pdf.ToString() && formato != TipoHistorico.docx.ToString())
                {
                    return Ok("Erro: Formato de arquivo não suportado.");
                }

                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                historico.CaminhoArquivo.CopyTo(fileStream);

                var historicoMap = _IMapper.Map<HistoricoEscolar>(historico);
                historicoMap.Nome = historico.Nome;
                historicoMap.UserDocumento = historico.Documento;
                historicoMap.Formato = Path.GetExtension(filePath);
                historicoMap.CaminhoArquivo = filePath;
                _IHistorico.Add(historicoMap);
                return Ok("Upload feito com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao fazer upload do arquivo, {ex.Message}");
            }
        }



        [Produces("application/json")]
        [HttpGet("Download")]
        public IActionResult Download(string documento)
        {
            //var user = RetornarUsuarioLogado();

            try
            {
                var historico = _IServiceHistorico.GetEntityByUserDocument(documento).Result;

                string application = "application/force-download";
                var dataBytes = System.IO.File.ReadAllBytes("Storage/1.txt");

                if (historico != null)
                {

                    if (historico.Formato.Equals(".pdf"))
                    {
                        application = "application/pdf";
                    }
                    else
                    {
                        application = " application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    }

                    var bytes = System.IO.File.ReadAllBytes(historico.CaminhoArquivo);
                    return File(bytes, application);
                }
                return File(dataBytes, application);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao fazer o download do arquivo, {ex.Message}");
            }
        }


        //Implementado para recuperar o usuário através do token, porém não deu tempo de fazer o login
        private ApplicationUser RetornarUsuarioLogado()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ApplicationUser user = new ApplicationUser();

            user.Nome = tokenS.Claims.FirstOrDefault(claim => claim.Type == "nome")?.Value;
            //user.Id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "idUsuario")?.Value;
            return user;

        }
    }
}
