using MiniBitcoin.Console;

var carteira = new Carteira(5000.00m, 0.00m);
var corretora = new CorretoraService(350000.00m);
var extrato = new ExtratoService();
bool executando = true;

while (executando)
{
  Console.WriteLine("\n=== MINI BITCOIN WALLET ===");
  Console.WriteLine($"Cotação Atual BTC: R$ {corretora.CotacaoBtcEmBrl:N2}");
  Console.WriteLine($"Saldo BRL: R$ {carteira.SaldoBrl:N2} | Saldo BTC: {carteira.SaldoBtc:F8} BTC");
  Console.WriteLine("---------------------------------------------");
  Console.WriteLine("1. Depositar BRL");
  Console.WriteLine("2. Comprar Bitcoin");
  Console.WriteLine("3. Vender Bitcoin");
  Console.WriteLine("4. Exibir Extrato e Patrimônio Total");
  Console.WriteLine("5. Atualizar Cotação do BTC");
  Console.WriteLine("6. Sair");
  Console.Write("Escolha uma opção: ");

  string? opcao = Console.ReadLine();

  switch (opcao)
  {
    case "1":
      Console.Write("Valor para depósito em BRL: R$ ");
      if (decimal.TryParse(Console.ReadLine(), out decimal valorDep) && valorDep > 0)
      {
        carteira.DepositarBrl(valorDep);
        extrato.RegistrarOperacao("DEPÓSITO BRL", valorDep, 0m);
        Console.WriteLine("Depósito efetuado com sucesso!");
      }
      else
      {
        Console.WriteLine("Valor inválido.");
      }
      break;

    case "2":
      Console.Write("Valor em BRL que deseja investir em BTC: R$ ");
      if (decimal.TryParse(Console.ReadLine(), out decimal valorCompra))
      {
        if (corretora.ComprarBitcoin(carteira, valorCompra, out decimal btcGerado))
        {
          extrato.RegistrarOperacao("COMPRA BTC", valorCompra, btcGerado);
          Console.WriteLine($"Compra realizada! {btcGerado:F8} BTC adicionados à carteira.");
        }
        else
        {
          Console.WriteLine("Falha na compra. Saldo em BRL insuficiente ou valor inválido.");
        }
      }
      break;

    case "3":
      Console.Write("Quantidade em BTC que deseja vender: ");
      if (decimal.TryParse(Console.ReadLine(), out decimal qtdVenda))
      {
        if (corretora.VenderBitcoin(carteira, qtdVenda, out decimal brlRecebido))
        {
          extrato.RegistrarOperacao("VENDA BTC", brlRecebido, qtdVenda);
          Console.WriteLine($"Venda realizada! R$ {brlRecebido:N2} adicionados à carteira.");
        }
        else
        {
          Console.WriteLine("Falha na venda. Saldo em BTC insuficiente ou quantidade inválida.");
        }
      }
      break;

    case "4":
      Console.WriteLine("\n--- EXTRATO DE TRANSAÇÕES ---");
      if (!extrato.Historico.Any())
      {
        Console.WriteLine("Nenhuma transação registrada.");
      }
      else
      {
        foreach (var t in extrato.Historico)
        {
          Console.WriteLine($"[{t.DataHora:dd/MM/yyyy HH:mm}] {t.Tipo} - R$ {t.ValorBrl:N2} | {t.QuantidadeBtc:F8} BTC");
        }
      }

      decimal patrimonio = extrato.CalcularPatrimonioTotal(carteira, corretora.CotacaoBtcEmBrl);
      Console.WriteLine("---------------------------------------------");
      Console.WriteLine($"PATRIMÔNIO TOTAL CONSOLIDADO: R$ {patrimonio:N2}");
      break;

    case "5":
      Console.Write("Nova cotação do BTC em BRL: R$ ");
      if (decimal.TryParse(Console.ReadLine(), out decimal novaCotacao) && novaCotacao > 0)
      {
        corretora.CotacaoBtcEmBrl = novaCotacao;
        Console.WriteLine("Cotação atualizada com sucesso!");
      }
      break;

    case "6":
      executando = false;
      break;

    default:
      Console.WriteLine("Opção inválida.");
      break;
  }
}