namespace Cccat.Autenticacao.Infra.Extensions;
public static class EnumExtensions
{
	public static int GetValue(this Enum enumType)
		=> Convert.ToInt16(enumType);
}
