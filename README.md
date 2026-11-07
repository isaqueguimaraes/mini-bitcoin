# ₿ MiniBitcoin

Aplicação de console em **C# / .NET 10** que simula uma carteira de Bitcoin: gestão de saldos em BRL e BTC, cotação simulada do mercado, compra e venda de Bitcoin e extrato de transações com patrimônio consolidado. O projeto conta com **testes unitários em xUnit** e histórico de commits seguindo o padrão **Conventional Commits**.

Projeto desenvolvido para a disciplina **Garantia da Qualidade de Software** (Gestão e Qualidade de Software, Ânima Educação), com foco em qualidade de código, testes automatizados e versionamento organizado.

> ⚠️ Este é um projeto acadêmico de **simulação**. Nenhuma operação real é realizada e nenhum valor financeiro real é movimentado.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![xUnit](https://img.shields.io/badge/tests-xUnit-5E2D91)
![Conventional Commits](https://img.shields.io/badge/commits-conventional-FE5196?logo=conventionalcommits&logoColor=white)

---

## 📑 Sumário

- [₿ MiniBitcoin](#-minibitcoin)
  - [📑 Sumário](#-sumário)
  - [📖 Visão geral](#-visão-geral)
  - [🛠 Requisitos técnicos](#-requisitos-técnicos)
  - [🏗 Arquitetura e estrutura do projeto](#-arquitetura-e-estrutura-do-projeto)
  - [🎯 Funcionalidades](#-funcionalidades)
  - [💼 Regras de negócio](#-regras-de-negócio)
    - [Carteira](#carteira)
    - [Compra e venda de Bitcoin](#compra-e-venda-de-bitcoin)
    - [Extrato e portfólio](#extrato-e-portfólio)
  - [🚀 Como clonar e compilar](#-como-clonar-e-compilar)
    - [1. Clonar o repositório](#1-clonar-o-repositório)
    - [2. Acessar a pasta do projeto](#2-acessar-a-pasta-do-projeto)
    - [3. Restaurar as dependências](#3-restaurar-as-dependências)
    - [4. Compilar a solução](#4-compilar-a-solução)
  - [▶️ Como executar a aplicação](#️-como-executar-a-aplicação)
  - [✅ Como executar os testes unitários](#-como-executar-os-testes-unitários)
  - [🧪 Cobertura dos testes](#-cobertura-dos-testes)
  - [🌿 Histórico de commits](#-histórico-de-commits)
  - [🧰 Tecnologias utilizadas](#-tecnologias-utilizadas)
  - [👥 Autor](#-autor)

---

## 📖 Visão geral

O **MiniBitcoin** permite ao usuário gerenciar uma carteira com saldo em moeda fiduciária (**BRL**) e em **Bitcoin**, consultar a cotação atual do BTC, executar ordens de compra e venda e acompanhar o histórico completo de operações, junto com o patrimônio total consolidado em BRL.

Toda a interação acontece por um menu interativo no terminal.

---

## 🛠 Requisitos técnicos

- **[.NET SDK 10.0](https://dotnet.microsoft.com/download)** (ou superior)
- **[Git](https://git-scm.com/downloads)**
- Terminal (PowerShell, CMD, Bash, Zsh etc.)
- IDE opcional: Visual Studio 2022+, VS Code (com extensão C# Dev Kit) ou JetBrains Rider

Para verificar a versão do .NET instalada:

```bash
dotnet --version
```

---

## 🏗 Arquitetura e estrutura do projeto

A solução é dividida em dois projetos: a aplicação de console, que concentra as entidades, as regras de negócio e a interface, e o projeto de testes, que referencia a aplicação.

```text
MiniBitcoin/
├── MiniBitcoin.sln
├── MiniBitcoin.Console/
│   ├── MiniBitcoin.Console.csproj
│   └── Program.cs
├── MiniBitcoin.Tests/
│   └── MiniBitcoin.Tests.csproj
├── .gitignore
└── README.md
```

> As classes de domínio (carteira, transação e extrato) ficam no projeto `MiniBitcoin.Console`, e os testes correspondentes ficam no projeto `MiniBitcoin.Tests`.

| Módulo         | Responsabilidade                                                                             |
| -------------- | -------------------------------------------------------------------------------------------- |
| **Carteira**   | Entidade que mantém os saldos em BRL e Bitcoin e valida entradas e débitos.                  |
| **Transação**  | Lógica de compra e venda de Bitcoin com base na cotação atual, validando fundos disponíveis. |
| **Extrato**    | Registro do histórico de operações e cálculo do patrimônio total consolidado em BRL.         |
| **Program.cs** | Interface de console com menu interativo em loop.                                            |

- **`MiniBitcoin.Console`**: projeto de console (`net10.0`).
- **`MiniBitcoin.Tests`**: projeto de testes xUnit (`net10.0`) que referencia o projeto `MiniBitcoin.Console`.

---

## 🎯 Funcionalidades

| Funcionalidade                     | Descrição                                                                                                      |
| ---------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| **Gestão de carteira**             | Controle do saldo em BRL e do saldo em Bitcoin, com validações para evitar saldos negativos.                   |
| **Cotação e simulação de mercado** | Manutenção e consulta do preço atual do Bitcoin (simulado e atualizável no sistema).                           |
| **Compra e venda de Bitcoin**      | Execução de ordens de compra e venda, validando os fundos disponíveis e atualizando os saldos correspondentes. |
| **Extrato de transações**          | Exibição do histórico completo de operações (compras e vendas) e do patrimônio total consolidado.              |

O menu interativo permite **consultar a cotação**, **ver os saldos**, **comprar Bitcoin**, **vender Bitcoin** e **exibir o extrato**.

---

## 💼 Regras de negócio

### Carteira

- A carteira **não pode ser criada com saldos iniciais negativos**, seja em BRL ou em Bitcoin.
- **Débitos e saques inválidos** (que resultariam em saldo negativo) são rejeitados.

### Compra e venda de Bitcoin

As operações são calculadas a partir da **cotação atual** do Bitcoin:

| Operação               | Regra                                                                                                               |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------- |
| **Compra**             | Exige **saldo em BRL suficiente** para adquirir a quantidade (ou fração) de BTC desejada. Debita BRL e credita BTC. |
| **Venda**              | Exige **saldo em BTC suficiente** para a quantidade vendida. Debita BTC e credita BRL.                              |
| **Transação inválida** | Quando não há fundos suficientes, a operação é recusada e os saldos permanecem inalterados.                         |

### Extrato e portfólio

- Cada operação de **compra** e **venda** realizada é registrada no histórico.
- O **patrimônio total consolidado** é calculado em BRL: saldo em reais somado ao saldo em Bitcoin convertido pela cotação atual.

---

## 🚀 Como clonar e compilar

### 1. Clonar o repositório

```bash
git clone https://github.com/isaqueguimaraes/mini-bitcoin.git
```

### 2. Acessar a pasta do projeto

```bash
cd MiniBitcoin
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Compilar a solução

```bash
dotnet build
```

Se tudo estiver correto, a saída final indicará **Build succeeded** (compilação bem-sucedida), com 0 erros.

---

## ▶️ Como executar a aplicação

Na raiz do repositório, execute:

```bash
dotnet run --project MiniBitcoin.Console/MiniBitcoin.Console.csproj
```

O menu interativo será exibido no terminal, e você poderá consultar a cotação, ver os saldos, comprar e vender Bitcoin e exibir o extrato seguindo as opções apresentadas.

---

## ✅ Como executar os testes unitários

Na raiz do repositório (onde está o arquivo `MiniBitcoin.sln`), execute:

```bash
dotnet test
```

Para uma saída mais detalhada, com o nome de cada teste executado:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Para executar apenas o projeto de testes:

```bash
dotnet test MiniBitcoin.Tests/MiniBitcoin.Tests.csproj
```

Ao final da execução, **todos os testes devem ser aprovados**, sem nenhuma falha.

---

## 🧪 Cobertura dos testes

Seguindo a abordagem de **TDD básico**, os testes de cada etapa foram escritos antes da implementação da funcionalidade correspondente. Eles utilizam o xUnit e cobrem os seguintes cenários:

| Módulo        | Cenários cobertos                                                                                                                                       |
| ------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Carteira**  | Criação com saldos iniciais válidos; bloqueio de valores iniciais negativos em BRL e Bitcoin; bloqueio de saques e débitos inválidos.                   |
| **Transação** | Compra de Bitcoin com sucesso; venda de Bitcoin com sucesso; recusa de compra sem saldo em BRL suficiente; recusa de venda sem saldo em BTC suficiente. |
| **Extrato**   | Registro de cada operação de compra e venda no histórico; cálculo do patrimônio total consolidado em BRL com base na cotação atual.                     |

---

## 🌿 Histórico de commits

O repositório segue rigorosamente o padrão **[Conventional Commits](https://www.conventionalcommits.org/)** (`feat`, `fix`, `test`, `refactor`, `docs`, `chore`). A sequência de commits do projeto é:

| Etapa                   | Tipo       | Commit                                                                             |
| ----------------------- | ---------- | ---------------------------------------------------------------------------------- |
| Estrutura inicial       | `chore`    | `chore: initial project structure with console and xunit apps`                     |
| Etapa 1: Carteira       | `test`     | `test(carteira): add unit tests for wallet balance validation`                     |
| Etapa 1: Carteira       | `feat`     | `feat(carteira): implement wallet entity and initial balance rules`                |
| Etapa 2: Compra e venda | `test`     | `test(transacao): add unit tests for bitcoin buy and sell operations`              |
| Etapa 2: Compra e venda | `feat`     | `feat(transacao): implement bitcoin purchase and sale logic with quote validation` |
| Etapa 3: Extrato        | `test`     | `test(extrato): add unit tests for transaction ledger and portfolio valuation`     |
| Etapa 3: Extrato        | `feat`     | `feat(extrato): implement transaction history and portfolio summary`               |
| Etapa 4: Console        | `feat`     | `feat(console): implement interactive menu for bitcoin wallet application`         |
| Etapa 5: Refatoração    | `refactor` | `refactor(core): improve code readability and organize namespaces`                 |
| Etapa 5: Documentação   | `docs`     | `docs: add comprehensive instructions in README.md`                                |

Para visualizar o histórico:

```bash
git log --oneline
```

---

## 🧰 Tecnologias utilizadas

- [C#](https://learn.microsoft.com/dotnet/csharp/)
- [.NET 10](https://dotnet.microsoft.com/)
- [xUnit](https://xunit.net/)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [Git](https://git-scm.com/) e [GitHub](https://github.com/)

---

## 👥 Autor

Isaque Guimarães