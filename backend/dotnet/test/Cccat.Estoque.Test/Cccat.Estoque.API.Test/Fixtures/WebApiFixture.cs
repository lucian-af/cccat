namespace Cccat.Estoque.API.Test.Fixtures
{
	[CollectionDefinition(nameof(WebApiFixtureCollection))]
	public class WebApiFixtureCollection : ICollectionFixture<WebApiFixture> { }

	public class WebApiFixture
	{
		public readonly HttpClient Client;
		private readonly CustomWebApiFactory<Program> _factory;

		public WebApiFixture()
		{
			_factory = new CustomWebApiFactory<Program>();
			Client = _factory.CreateClient();
			AdicionaAutorizacao();
		}

		private void AdicionaAutorizacao()
			=> Client.DefaultRequestHeaders.Authorization = new("Bearer", _factory.GerarToken());

		public Task LimparBase()
			=> _factory.LimparBase();
	}
}
