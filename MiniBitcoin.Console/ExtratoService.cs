namespace MiniBitcoin.Console;

public class TransacaoRegistro
{
  public DateTime DataHora { get; }
  public string Tipo { get; }
  public decimal ValorBrl { get; }
  public decimal QuantidadeBtc { get; }

  public TransacaoRegistro(string tipo, decimal valorBrl, decimal quantidadeBtc)
  {
    DataHora = DateTime.Now;
    Tipo = tipo;
    ValorBrl = valorBrl;
    QuantidadeBtc = quantidadeBtc;
  }
}

public class ExtratoService
{
  private readonly List<TransacaoRegistro> _historico = new();

  public IReadOnlyCollection<TransacaoRegistro> Historico => _historico.AsReadOnly();

  public void RegistrarOperacao(string tipo, decimal valorBrl, decimal quantidadeBtc)
  {
    _historico.Add(new TransacaoRegistro(tipo, valorBrl, quantidadeBtc));
  }

  public decimal CalcularPatrimonioTotal(Carteira carteira, decimal cotacaoAtualBtc)
  {
    return carteira.SaldoBrl + (carteira.SaldoBtc * cotacaoAtualBtc);
  }
}