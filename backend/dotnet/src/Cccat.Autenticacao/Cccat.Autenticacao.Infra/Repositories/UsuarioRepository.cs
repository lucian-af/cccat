using Cccat.Autenticacao.Domain.Entities;
using Cccat.Autenticacao.Domain.Interfaces;
using Cccat.Autenticacao.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Cccat.Autenticacao.Infra.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
	private readonly DatabaseContext _context;

	public UsuarioRepository(DatabaseContext databaseContext)
		=> _context = databaseContext;

	public async Task Cadastrar(Usuario usuario)
	{
		_context.Usuarios.Add(usuario);
		await _context.SaveChangesAsync();
	}

	public async Task<Usuario> ObterUsuarioPorEmail(string email)
		=> await _context.Usuarios.FirstOrDefaultAsync(us => us.Email.Valor.Equals(email));
}
