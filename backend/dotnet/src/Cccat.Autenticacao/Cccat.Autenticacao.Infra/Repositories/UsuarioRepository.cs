using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Enums;
using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Infra.Database;
using Cccat.Autenticacao.Infra.Extensions;
using Cccat.Autenticacao.Infra.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.Infra.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
	private readonly DatabaseContext _context;

	public UsuarioRepository(DatabaseContext databaseContext)
		=> _context = databaseContext;

	public async Task Cadastrar(Usuario usuario)
	{
		_context.Usuarios.Add(new UsuarioDb
		{
			Id = usuario.Id,
			Email = usuario.Email.Valor,
			Senha = usuario.Senha.Valor,
			Salt = usuario.Senha.Salt,
			SenhaTipo = usuario.SenhaTipo.GetValue()
		});
		await _context.SaveChangesAsync();
	}

	public async Task<Usuario> ObterUsuarioPorEmail(string email)
	{
		var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(us => us.Email.Equals(email));
		return Usuario.Restaurar(usuarioDb.Id, usuarioDb.Email, usuarioDb.Senha, usuarioDb.Salt, (SenhaTipo)usuarioDb.SenhaTipo);
	}
}
