using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cccat.Autenticacao.Application.Models;
using Cccat.Autenticacao.Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace Cccat.Autenticacao.Application.UseCases
{
	public class AutenticaUsuario
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public AutenticaUsuario(IRepositoryFactory factory)
			=> _usuarioRepository = factory.CriarUsuarioRepository();

		public async Task<AutenticaUsuarioOutputDto> Autenticar(AutenticaUsuarioInputDto input)
		{
			var usuario = await _usuarioRepository.ObterUsuarioPorEmail(input.Email)
				?? throw new Exception("Usuário não encontrado.");

			// TODO: remover lib
			var senhaInformada = KeyDerivation.Pbkdf2(input.Senha, Convert.FromHexString(usuario.Salt), KeyDerivationPrf.HMACSHA512, 64, 100);

			if (usuario.Senha.Equals(Convert.ToHexString(senhaInformada)))
			{
				var token = GerarToken(input.Email);
				return new AutenticaUsuarioOutputDto { Token = token };
			}
			else
				throw new Exception("Falha na autenticação.");
		}

		private static string GerarToken(string email)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("<minha-chave-qualquer>");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = "Cccat",
				Audience = "Cccat",
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
				Subject = new ClaimsIdentity(new List<Claim>
				{
					new Claim(JwtRegisteredClaimNames.Email, email)
				})
			};

			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
	}
}
