using Xunit;
using MiniBitcoin.Console;

namespace MiniBitcoin.Tests;

public class CarteiraTests
{
  [Fact]
  public void Nao_Deve_Permitir_Carteira_Com_Saldo_Inicial_Negativo()
  {
    Assert.Throws<ArgumentException>(() => new Carteira(-100m, 0m));
  }

  [Fact]
  public void Nao_Deve_Permitir_Debito_Brl_Superior_Ao_Saldo()
  {
    var carteira = new Carteira(500m, 0m);
    Assert.Throws<InvalidOperationException>(() => carteira.DebitarBrl(600m));
  }
}