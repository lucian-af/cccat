namespace Cccat.Checkout.Domain.Entities
{
    public class Cep
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Cep(string codigo, string rua, string bairro, double latitude, double longitude)
        {
            if (latitude == longitude)
                throw new Exception("Coordenadas inválidas.");

            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("Código do CEP inválido.");

            Id = Guid.NewGuid();
            Codigo = codigo;
            Rua = rua;
            Bairro = bairro;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
