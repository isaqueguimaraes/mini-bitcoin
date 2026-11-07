using Xunit;
using MiniBitcoin.Console;

namespace MiniBitcoin.Tests;

public class ExtratoTests
{
  [Fact]
  public void Deve_Calcular_Patrimonio_Total_Consolidado_Corretamente()
  {
    var carteira = new Carteira(1000m, 0.01m);
    var extrato = new ExtratoService();

    decimal patrimonioTotal = extrato.CalcularPatrimonioTotal(carteira, 300000m);

    Assert.Equal(4000m, patrimonioTotal);
  }
}