using System;

class Program
{
    static void Main()
    {
        bool sair = false;
        bool matrizLida = false;

        int ordem = 0;
        double[,] A = new double[0, 0];

        while (!sair)
        {
            Console.WriteLine("\n===== MENU DE MATRIZES =====");
            Console.WriteLine("1 - Ler matriz 2x2");
            Console.WriteLine("2 - Ler matriz 1x1");
            Console.WriteLine("3 - Calcular determinante");
            Console.WriteLine("4 - Calcular matriz inversa");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out int escolha))
            {
                Console.WriteLine("Digite uma opção numérica válida!");
                continue;
            }

            switch (escolha)
            {
                case 1:
                case 2:
                    ordem = escolha == 1 ? 2 : 1;

                    A = new double[ordem, ordem];

                    Console.WriteLine($"\nDigite os valores da matriz {ordem}x{ordem}:");

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            Console.Write($"[{i},{j}]: ");
                            A[i, j] = double.Parse(Console.ReadLine());
                        }
                    }

                    matrizLida = true;

                    Console.WriteLine("\nMatriz:");

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            Console.Write($"{A[i, j],8} ");
                        }

                        Console.WriteLine();
                    }

                    break;

                case 3:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    double determinante;

                    if (ordem == 1)
                    {
                        determinante = A[0, 0];
                    }
                    else
                    {
                        determinante =
                            A[0, 0] * A[1, 1] -
                            A[0, 1] * A[1, 0];
                    }

                    Console.WriteLine($"\nDeterminante: {determinante}");

                    break;

                case 4:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    double det;

                    if (ordem == 1)
                    {
                        det = A[0, 0];
                    }
                    else
                    {
                        det =
                            A[0, 0] * A[1, 1] -
                            A[0, 1] * A[1, 0];
                    }

                    if (det == 0)
                    {
                        Console.WriteLine("\nA matriz não possui inversa.");
                        break;
                    }

                    double[,] inversa = new double[ordem, ordem];

                    if (ordem == 1)
                    {
                        inversa[0, 0] = 1.0 / A[0, 0];
                    }
                    else
                    {
                        inversa[0, 0] = A[1, 1] / det;
                        inversa[0, 1] = -A[0, 1] / det;
                        inversa[1, 0] = -A[1, 0] / det;
                        inversa[1, 1] = A[0, 0] / det;
                    }

                    Console.WriteLine("\nMatriz inversa:");

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            Console.Write($"{inversa[i, j],8} ");
                        }

                        Console.WriteLine();
                    }

                    break;

                case 0:
                    Console.WriteLine("Encerrando o programa...");
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}