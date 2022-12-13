using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_renda.CalculadoraDeRenda
{
    internal class CalculadoraDeRenda
    {
        // Propriedades da classe
        double capitalInvestido { get; set; }
        double taxaDeJuros { get; set; }
        double tempo { get; set; }
        double aporteMensal { get; set; }
        int mesesAportados { get; set; }

        
        public void StartAplicacao()
        {
            try
            {
                int opcaoDesejada = 0;

                while (opcaoDesejada != 4)
                {
                    Console.Clear();
                    Console.WriteLine("===========================================================================");
                    Console.WriteLine("=== 1 - Realizar cálculo de júros compostos sem a realização de aportes ===");
                    Console.WriteLine("=== 2 - Realizar cálculo de júros compostos com a realização de aportes ===");
                    Console.WriteLine("=== 3 - Consultar histórico de cálculos                                 ===");
                    Console.WriteLine("=== 4 - Sair da aplicação                                               ===");
                    Console.WriteLine("===========================================================================");

                    Console.Write("\nDigite a opção desejada: ");

                    opcaoDesejada = int.Parse(Console.ReadLine());

                    switch (opcaoDesejada)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("===========================================================================");
                            Console.WriteLine("=== 1 - Realizar cálculo de júros compostos sem a realização de aportes ===");
                            Console.WriteLine("===========================================================================");
                            Console.Write("\nInforme o capital investido: ");
                            capitalInvestido = double.Parse(Console.ReadLine());
                            Console.Write("Informe a taxa de júrus (anual): ");
                            taxaDeJuros = double.Parse(Console.ReadLine());
                            Console.Write("Informe o tempo (em anos): ");
                            tempo = double.Parse(Console.ReadLine());

                            CalculadoraDeRenda montanteSemApostes = new CalculadoraDeRenda();
                            montanteSemApostes.capitalInvestido = capitalInvestido;
                            montanteSemApostes.taxaDeJuros = taxaDeJuros;
                            montanteSemApostes.tempo = tempo;
                            Console.Write($"\nTotal acumulado após {tempo} ano(s): " + Convert.ToString(montanteSemApostes.CalculoSemAportesAnual(montanteSemApostes).ToString("N2")));
                            Console.ReadKey(); // CALCULAR O EQUIVALENTE A TANTOS POR MÊS
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("===========================================================================");
                            Console.WriteLine("=== 2 - Realizar cálculo de júros compostos com a realização de aportes ===");
                            Console.WriteLine("===========================================================================");
                            Console.Write("\nInforme o capital investido: ");
                            capitalInvestido = double.Parse(Console.ReadLine());
                            Console.Write("Informe a taxa de júrus (anual): ");
                            taxaDeJuros = double.Parse(Console.ReadLine());
                            Console.Write("Informe o tempo (em anos): ");
                            tempo = double.Parse(Console.ReadLine());
                            Console.Write("Informe o valor médio dos aportes: ");
                            aporteMensal = double.Parse(Console.ReadLine());
                            Console.Write("Informe quantos meses receberão aportes: ");
                            mesesAportados = int.Parse(Console.ReadLine());

                            CalculadoraDeRenda montanteComAportes = new CalculadoraDeRenda();
                            montanteComAportes.capitalInvestido = capitalInvestido;
                            montanteComAportes.taxaDeJuros = taxaDeJuros;
                            montanteComAportes.tempo = tempo;
                            montanteComAportes.aporteMensal = aporteMensal;
                            montanteComAportes.mesesAportados = mesesAportados;
                            Console.WriteLine($"\nTotal acumulado após {tempo} ano(s): " + Convert.ToString(montanteComAportes.CalculoComAportesAnual(montanteComAportes).ToString("N2")));
                            Console.ReadKey(); 
                            break;

                        case 3:
                            Console.WriteLine("3");
                            break;

                        case 4:
                            Console.WriteLine("4");
                            break;

                        default:
                            Console.WriteLine("\nOpção digitada não existe no contexto atual");
                            break;
                    }
                }

            }
            catch (FormatException /*e*/)
            {
                /*throw new FormatException("\n\nErro: valor digitado não é um valor inteiro!\n\n", e);*/
                Console.WriteLine("\nErro: Valor digitado não é um valor inteiro!\n");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\nErro: Nullo não pode ser passado como valor!");
            }
            catch
            {
                Console.WriteLine("\nErro: Algo inesperado aconteceu, tente novamente");
            }
        }

        public double CalculoSemAportesAnual(CalculadoraDeRenda objeto)
        {
            double montante = objeto.capitalInvestido * (1 + (objeto.taxaDeJuros / 100)) * objeto.tempo;
            return montante;
        }

        public double CalculoComAportesAnual(CalculadoraDeRenda objeto)
        {
            /*     double montante = (objeto.capitalInvestido * (Math.Pow(1 + (objeto.taxaDeJuros / 100), objeto.mesesAportados)) + (objeto.aporteMensal *
                   (((Math.Pow(1 + (objeto.taxaDeJuros / 100), objeto.mesesAportados) - 1) + (objeto.taxaDeJuros / 100))))); */ /*"funcionando"*/

            double montante = (objeto.capitalInvestido * (Math.Pow(1 + (objeto.taxaDeJuros / 100), objeto.mesesAportados)) + (objeto.aporteMensal *
                              (((Math.Pow(1 + (objeto.taxaDeJuros / 100), objeto.mesesAportados) - 1) + (objeto.taxaDeJuros / 100)))));
            // Está dando 1000 reais de diferença
            // https://www.controlacao.com.br/blog/juros-compostos#:~:text=Como%20calcular%20o%20Juros%20Compostos,)%20%C3%B7%20(i%20%2F%20100))%3B
            // https://www.controlacao.com.br/blog/juros-compostos
            return montante;
        }

    }
}
