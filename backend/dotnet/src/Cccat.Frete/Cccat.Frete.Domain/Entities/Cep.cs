﻿namespace Cccat.Frete.Domain.Entities
{
	public class Cep
	{
		public Guid Id { get; private set; }
		public string Codigo { get; private set; }
		public string Rua { get; private set; }
		public string Bairro { get; private set; }
		public Coordenada Coordenadas { get; set; }

		private Cep() { }

		public Cep(string codigo, string rua, string bairro, double latitude, double longitude)
		{
			if (string.IsNullOrWhiteSpace(codigo))
				throw new Exception("Código do CEP inválido.");

			Id = Guid.NewGuid();
			Codigo = codigo;
			Rua = rua;
			Bairro = bairro;
			Coordenadas = new(latitude, longitude);
		}
	}
}
