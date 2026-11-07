namespace MiniBitcoin.Console;

public class CorretoraService
{
  public decimal CotacaoBtcEmBrl { get; set; }

  public CorretoraService(decimal cotacaoInicial = 350000.00m)
  {
    if (cotacaoInicial <= 0)
      throw new ArgumentException("A cotação inicial deve ser maior que zero.");

    CotacaoBtcEmBrl = cotacaoInicial;
  }

  public bool ComprarBitcoin(Carteira carteira, decimal valorEmBrl, out decimal btcComprado)
  {
    btcComprado = 0m;
    if (valorEmBrl <= 0 || valorEmBrl > carteira.SaldoBrl)
      return false;

    btcComprado = valorEmBrl / CotacaoBtcEmBrl;
    carteira.DebitarBrl(valorEmBrl);
    carteira.CreditarBtc(btcComprado);
    return true;
  }

  public bool VenderBitcoin(Carteira carteira, decimal quantidadeBtc, out decimal brlRecebido)
  {
    brlRecebido = 0m;
    if (quantidadeBtc <= 0 || quantidadeBtc > carteira.SaldoBtc)
      return false;

    brlRecebido = quantidadeBtc * CotacaoBtcEmBrl;
    carteira.DebitarBtc(quantidadeBtc);
    carteira.DepositarBrl(brlRecebido);
    return true;
  }
}