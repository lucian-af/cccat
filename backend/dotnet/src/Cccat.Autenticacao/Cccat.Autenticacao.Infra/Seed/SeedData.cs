using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Enums;
using Cccat.Autenticacao.Infra.Database;

namespace Cccat.Autenticacao.Infra.Seed
{
	public static class SeedData
	{
		public static async Task CriarDados(DatabaseContext context)
		{
			await AdicionarUsuario(context);

			await context.SaveChangesAsync();
		}

		private static async Task AdicionarUsuario(DatabaseContext context)
		{
			if (context.Usuarios.Any()) return;

			var usuario = Usuario.Criar("cccat@cccat.com", "c!c@c#@t", SenhaTipo.Pbkdf2);

			await context.Usuarios.AddAsync(new()
			{
				Id = Guid.NewGuid(),
				Email = usuario.Email.Valor,
				SenhaTipo = (int)usuario.SenhaTipo,
				Senha = usuario.Senha.Valor,
				Salt = usuario.Senha.Salt
			});
		}
	}
}
