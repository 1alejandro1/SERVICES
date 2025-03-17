using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.MODELS;
using BCP.CROSS.SECURITY;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace BcpCrecer.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SegurinetController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ISegurinetService _loginService;
        readonly IConfiguration _configuration;

        public SegurinetController(ILoggerManager logger, ISegurinetService loginService, IConfiguration configuration)
        {
            _logger = logger;
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetLogin([FromBody] LoginRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Login");
            var response = await _loginService.GetLoginAsync(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
               return StatusCode(response.Meta.StatusCode, response);
            }
            BuildToken(ref response);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes ="BasicAuthentication")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            string channel = Request.Headers["Channel"];
            string requestId = Generator.RequestId(Request.Headers["CorrelationId"]);

            _logger.Debug($"[RequestId: {channel} {requestId}] - Obtener Login");
            var response = await _loginService.ChangePassword(request, requestId);
            _logger.Debug($"[ResponseId: {channel} {response.Meta.ResponseId}] - {JsonSerializer.Serialize(response)}");

            if (response.Meta.StatusCode != 200)
            {
                return StatusCode(response.Meta.StatusCode, response);
            }
            return Ok(response);
        }
        private void BuildToken(ref ServiceResponse<LoginResponse> user)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                new Claim("Matricula",user.Data.Matricula),
                new Claim("Nombre",user.Data.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            string[] roles = user.Data.Token.token.Split(',');
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiracion del Token
            var expiration = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:expiration"));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            user.Data.Token=new UserToken
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            };
        }
    }
}