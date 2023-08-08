using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;

namespace bytebank_ATENDIMENTO.Bytebank.Atendimento
{
    internal class Atendimento
    {

        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>()
            {
                new ContaCorrente(79, "257819-B"){ Saldo = 250900.00, Titular = new Cliente{Nome = "Vitor", Cpf = "46676845857"} },
                new ContaCorrente(32, "504328-A"){ Saldo = 2507.00, Titular = new Cliente{Nome = "Jota", Cpf = "46875362157"} },
                new ContaCorrente(39, "230794-A"),
                new ContaCorrente(79, "194000-B"){Saldo = 3500100.90, Titular = new Cliente{Nome = "Valéria", Cpf = "06167005800"} }
            };

        public void AtendimentoCliente()
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

                        case '3':
                            RemoverContas();
                            break;

                        case '4':
                            OrdenarContas();
                            break;

                        case '5':
                            PesquisarContas();
                            break;

                        case '0':
                            EncerrarPrograma();
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

        private void EncerrarPrograma()
        {
            Console.WriteLine("");
            Console.WriteLine("... Encerrrando a aplicação. Volte sempre! ...");
            Console.ReadKey();
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

            Console.Write("- Número da Agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());

            ContaCorrente conta = new ContaCorrente(numeroAgencia);

            Console.WriteLine($"Número da nova conta: {conta.Conta}");

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
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }

        void RemoverContas()
        {
            Console.Clear();
            Console.WriteLine("##################################");
            Console.WriteLine("#         REMOVER CONTAS         #");
            Console.WriteLine("##################################\n");

            ContaCorrente conta = null;

            Console.Write("Informe o número da conta que deseja remover: ");
            string numeroConta = Console.ReadLine();

            foreach (var item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }

            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine("... Conta não encontrada ...");
            }
            Console.ReadKey();
        }

        void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de Contas ordenada com sucesso! ...");
            Console.ReadKey();
        }

        void PesquisarContas()
        {
            Console.Clear();
            Console.WriteLine("##################################");
            Console.WriteLine("#       PESQUISA DE CONTAS       #");
            Console.WriteLine("##################################\n");

            Console.Write(@"
# Deseja pesquisar como?
# (1): Número da Conta
# (2): CPF do Titular
# (3): Número da Agência
");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.Write("\nInforme o número da conta que deseja visualizar: ");
                    string numeroConta = Console.ReadLine();
                    ContaCorrente consultaConta = ConsultaContaPorNumero(numeroConta);
                    consultaConta.DetalhesConta();
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Write("\nInforme o CPF do Titular da conta que deseja visualizar: ");
                    string cpfConta = Console.ReadLine();
                    ContaCorrente consultaCpf = ConsultaContaPorCPF(cpfConta);
                    Console.WriteLine(consultaCpf.ToString());
                    Console.ReadKey();
                    break;

                case 3:
                    Console.Write("\nInforme o Nº da Agência que deseja visualizar as contas: ");
                    int numeroAgencia = int.Parse(Console.ReadLine());
                    var contasDaAgencia = ConsultaPorAgencia(numeroAgencia);
                    ExibirListaDeContas(contasDaAgencia);
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Opção não implementada...");
                    break;

            }
        }

        ContaCorrente ConsultaContaPorCPF(string? cpfConta)
        {
            return _listaDeContas.Where(conta => conta.Titular.Cpf == cpfConta).FirstOrDefault();
        }

        ContaCorrente ConsultaContaPorNumero(string? numeroConta)
        {
            return _listaDeContas.Where(conta => conta.Conta == numeroConta).FirstOrDefault();
        }

        List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
        {
            var consulta = (
                    from conta in _listaDeContas
                    where conta.Numero_agencia == numeroAgencia
                    select conta
                ).ToList();

            return consulta;
        }

        void ExibirListaDeContas(List<ContaCorrente> contasDaAgencia)
        {
            if (contasDaAgencia == null)
            {
                Console.WriteLine("... A consulta não retronou dados ...");
            }
            else
            {
                foreach (var conta in contasDaAgencia)
                {
                    Console.WriteLine(conta.ToString());
                }
            }
        }
    }
}
