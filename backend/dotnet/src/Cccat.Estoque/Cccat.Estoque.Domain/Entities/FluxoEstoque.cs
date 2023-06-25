using Cccat.Estoque.Domain.Enums;

namespace Cccat.Estoque.Domain.Entities;
public class FluxoEstoque
{
	public Guid Id { get; private set; }
	public int IdProduto { get; private set; }
	public TipoOperacao Operacao { get; private set; }
	public int Quantidade { get; private set; }

	public FluxoEstoque(int idProduto, TipoOperacao operacao, int quantidade)
	{
		if (quantidade < 1)
			throw new Exception("Quantidade inválida.");

		if (idProduto <= 0)
			throw new Exception("Identificador do produto inválido.");

		Id = Guid.NewGuid();
		IdProduto = idProduto;
		Operacao = operacao;
		Quantidade = quantidade;
	}
}
