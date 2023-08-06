using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using bytebank_ATENDIMENTO.bytebank.Util;
using System.Collections;

Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");

#region Iniciando em Arrays

void TestaArrayInt()
{
    int[] idades = new int[5] {30, 20, 19, 45, 35};

    Console.WriteLine($"Tamanho do Array: {idades.Length}");

    int acumulador = 0;

    for ( int i = 0; i < idades.Length; i++)
    {
        int idade = idades[i];
        Console.WriteLine($"Indice [{i}] = {idade}");
        acumulador += idade;
    }

    int media = acumulador / idades.Length;
    Console.WriteLine($"Media das idades: {media}");
}

//TestaArrayInt();

void TestarBuscarPalavras()
{
    string[] arrayDePalavras = new string[5];

    for ( int i = 0; i < arrayDePalavras.Length ; i++)
    {
        Console.Write($"Digite a {i+1}ª palavra do seu array: ");
        arrayDePalavras[i] = Console.ReadLine();
    }
    Console.Write("\nDigite a palavra que deseja buscar: ");
    var busca = Console.ReadLine();

    foreach ( string palavra in arrayDePalavras)
    {
        if ( palavra.Equals(busca))
        {
            Console.WriteLine($"Palavra '{busca}' encontrada");
            break;
        } 
    }
}
//TestarBuscarPalavras();

//double[] amostras = new double[5];
//amostras.SetValue(2.5, 0);
//amostras.SetValue(3.2, 1);
//amostras.SetValue(9.3, 2);
//amostras.SetValue(6.9, 3);
//amostras.SetValue(5, 4);

void Mediana(Array array)
{
    if ((array == null) || (array.Length == 0))
    {
        Console.WriteLine("O array inserido está nulo ou vazio.");
    }

    double[] numerosOrdenados = (double[])array.Clone();
    Array.Sort(numerosOrdenados);

    int metadeArray = numerosOrdenados.Length / 2;
    double mediana = (numerosOrdenados.Length%2 != 0) ? numerosOrdenados[metadeArray] :
                                                        (numerosOrdenados[metadeArray] + numerosOrdenados[metadeArray-1]) / 2;
    Console.WriteLine($"Mediana = {mediana}");
}

double Media(Array array)
{

    double media = 0;
    double total = 0;

    if ((array == null) || (array.Length == 0))
    {
        Console.WriteLine("O array inserido está nulo ou vazio.");
        return 0;
    }

    foreach (double item in array) 
    {
        total += item;
    }

    media = total / array.Length;
    return media;
}

void TestaArrayContas()
{
    ListaDeContasCorrentes arrayDeContas = new ListaDeContasCorrentes();
    arrayDeContas.Adicionar(new ContaCorrente(0149, "235693-N"));
    arrayDeContas.Adicionar(new ContaCorrente(0072, "100231-B"));
    arrayDeContas.Adicionar(new ContaCorrente(0039, "506029-A"));
    arrayDeContas.Adicionar(new ContaCorrente(0039, "590623-A"));
    arrayDeContas.Adicionar(new ContaCorrente(0072, "103402-B"));
    arrayDeContas.Adicionar(new ContaCorrente(0456, "325678-I"));
    var contaTeste = new ContaCorrente(0456, "000101-I");
    arrayDeContas.Adicionar(contaTeste);

    //arrayDeContas.ExibeLista();
    //Console.WriteLine("------------");
    //arrayDeContas.Remover(contaTeste);
    //arrayDeContas.ExibeLista();

    for (int i = 0; i < arrayDeContas.Tamanho; i++)
    {
        ContaCorrente conta = arrayDeContas[i];
        Console.WriteLine($"Indice: {i} -- Conta: {conta.Conta} / {conta.Numero_agencia}");
    }
}

//TestaArrayContas();

#endregion
#region Brincar de Listas:

//List<string> nomesDosEscolhidos = new List<string>()
//{
//    "Bruce Wayne",
//    "Carlos Vilagran",
//    "Richard Grayson",
//    "Bob Kane",
//    "Will Farrel",
//    "Lois Lane",
//    "General Welling",
//    "Perla Letícia",
//    "Uxas",
//    "Diana Prince",
//    "Elisabeth Romanova",
//    "Anakin Wayne"
//};

//static bool BuscaNome(List<string> lista, string nome)
//{
//    if (lista.Contains(nome))
//    {
//        return true;
//    }
//    return false;
//}

//static IList NomesEcontrados(List<string> lista, string[] nomes)
//{
//    List<string> nomesEncontrados = new List<string>();

//    foreach (string nome in nomes)
//    {
//        if (BuscaNome(lista, nome))
//        {
//            nomesEncontrados.Add(nome);
//        }
//    }

//    return nomesEncontrados;
//}

//string[] nomesABuscar = new string[] { "Bruce Wayne", "Vitor Miracca", "Diana Prince", "Anakin Wayne", "Valéria" };

//var novosNomes = NomesEcontrados(nomesDosEscolhidos, nomesABuscar);
//foreach (var nome in novosNomes)
//{
//    Console.WriteLine(nome);
//}
#endregion

List<ContaCorrente> _listaDeContas = new List<ContaCorrente>()
{
    new ContaCorrente(79, "257819-B"){Saldo = 250900.00},
    new ContaCorrente(32, "504328-A")
};

AtendimentoCliente();

void AtendimentoCliente()
{
    try
    {
        char opcao = 'A';
        while (opcao != '0')
        {
            Console.Clear();
            Console.WriteLine($@"
####################################
#       Atendimento ByteBank       #
#                                  #
#    MENU DE OPÇÕES                #
#                                  #
#   (1): Cadastrar uma Conta       #
#   (2): Listar Contas             #
#   (3): Remover Conta             #
#   (4): Ordenar Contas            #
#   (5): Pesquisar Conta           #
#   (0): Sair do Sistema           #
#                                  #
####################################

        ");

            Console.Write("- Digite a opção desejada: ");
            try
            {
                opcao = Console.ReadLine()[0];

            }
            catch (Exception excecao)
            {
                throw new ByteBankException(excecao.Message);
            }

            switch (opcao)
            {
                case '1':
                    CadastrarConta();
                    break;

                case '2':
                    ListarContas();
                    break;

                default:
                    Console.WriteLine("Opcao não implementada.");
                    break;
            }
        }
    }
    catch (ByteBankException excecao)
    {
        Console.WriteLine($"{excecao.Message}");
    }
}

void CadastrarConta()
{
    Console.Clear();
    Console.WriteLine("##################################");
    Console.WriteLine("#        CADASTRO DE CONTAS      #");
    Console.WriteLine("##################################");
    Console.WriteLine("");
    Console.WriteLine("=== Preencha os dados solicitados ===");
    Console.WriteLine("");
    
    Console.Write("- Número da Conta: ");
    string numeroConta = Console.ReadLine();

    Console.Write("- Número da Agência: ");
    int numeroAgencia = int.Parse(Console.ReadLine());

    ContaCorrente conta = new ContaCorrente(numeroAgencia, numeroConta);

    Console.Write("- Informe o Saldo Inicial: ");
    conta.Saldo = double.Parse(Console.ReadLine());

    Console.Write("- Nome do Titular: ");
    conta.Titular.Nome = Console.ReadLine();

    Console.Write("- CPF do Titular: ");
    conta.Titular.Cpf = Console.ReadLine();

    Console.Write("- Profissão do Titular: ");
    conta.Titular.Profissao = Console.ReadLine();

    _listaDeContas.Add(conta);
    Console.Clear();
    Console.WriteLine(@$"
#####################################
#          CONTA ADICIONADA         #
#####################################

#   Número da Conta: {conta.Conta}
#   Número da Agência: {conta.Numero_agencia}
#   Saldo Inicial: {conta.Saldo}
#   Titular: {conta.Titular.Nome} ({conta.Titular.Cpf})
    
    ");
    Console.ReadKey();
}

void ListarContas()
{
    Console.Clear();
    Console.WriteLine("##################################");
    Console.WriteLine("#        LISTAGEM DE CONTAS      #");
    Console.WriteLine("##################################");

    if (_listaDeContas.Count <= 0)
    {
        Console.WriteLine("\n... Não há contas cadastradas! ...");
        Console.ReadLine();
        return;
    }
    foreach (ContaCorrente item in _listaDeContas)
    {
        Console.WriteLine("");
        item.DetalhesConta();
    }
    Console.ReadKey();
}

