using Xunit;
using MiniBitcoin.Console;

namespace MiniBitcoin.Tests;

public class TransacaoTests
{
  [Fact]
  public void Deve_Comprar_Bitcoin_Com_Sucesso_Quando_Ha_Saldo_Brl()
  {
    var carteira = new Carteira(3500m, 0m);
    var corretora = new CorretoraService(350000m);

    bool sucesso = corretora.ComprarBitcoin(carteira, 3500m, out decimal btc);

    Assert.True(sucesso);
    Assert.Equal(0.01m, btc);
    Assert.Equal(0m, carteira.SaldoBrl);
    Assert.Equal(0.01m, carteira.SaldoBtc);
  }

  [Fact]
  public void Nao_Deve_Permitir_Venda_Bitcoin_Sem_Saldo_Btc()
  {
    var carteira = new Carteira(1000m, 0.005m);
    var corretora = new CorretoraService(350000m);

    bool sucesso = corretora.VenderBitcoin(carteira, 0.01m, out _);

    Assert.False(sucesso);
    Assert.Equal(0.005m, carteira.SaldoBtc);
  }
}