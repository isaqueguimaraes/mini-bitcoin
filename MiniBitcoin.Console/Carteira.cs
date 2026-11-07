namespace MiniBitcoin.Console;

public class Carteira
{
  public decimal SaldoBrl { get; private set; }
  public decimal SaldoBtc { get; private set; }

  public Carteira(decimal saldoBrlInicial = 0m, decimal saldoBtcInicial = 0m)
  {
    if (saldoBrlInicial < 0 || saldoBtcInicial < 0)
      throw new ArgumentException("Os saldos iniciais não podem ser negativos.");

    SaldoBrl = saldoBrlInicial;
    SaldoBtc = saldoBtcInicial;
  }

  public void DepositarBrl(decimal valor)
  {
    if (valor <= 0)
      throw new ArgumentException("O valor do depósito deve ser maior que zero.");

    SaldoBrl += valor;
  }

  public void DebitarBrl(decimal valor)
  {
    if (valor <= 0 || valor > SaldoBrl)
      throw new InvalidOperationException("Saldo em BRL insuficiente ou valor inválido.");

    SaldoBrl -= valor;
  }

  public void CreditarBtc(decimal quantidade)
  {
    if (quantidade <= 0)
      throw new ArgumentException("A quantidade de BTC deve ser maior que zero.");

    SaldoBtc += quantidade;
  }

  public void DebitarBtc(decimal quantidade)
  {
    if (quantidade <= 0 || quantidade > SaldoBtc)
      throw new InvalidOperationException("Saldo em BTC insuficiente ou quantidade inválida.");

    SaldoBtc -= quantidade;
  }
}