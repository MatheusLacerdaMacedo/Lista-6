
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
            Console.WriteLine("5 - Teorema de Binet");
            Console.WriteLine("6 - Transposição");
            Console.WriteLine("7 - Multiplicar linha por escalar (efeito no determinante)");
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

                    Console.WriteLine(
                        $"\nDigite os valores da matriz {ordem}x{ordem}:"
                    );

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            A[i, j] = LerNumero(
                                $"Digite A[{i},{j}]: "
                            );
                        }
                    }

                    matrizLida = true;

                    Console.WriteLine("\nMatriz lida:");
                    ExibirMatriz(A, ordem, ordem);

                    break;

                case 3:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    double determinante = CalcularDeterminante(A, ordem);

                    Console.WriteLine(
                        $"\nDeterminante: {determinante:F2}"
                    );

                    break;

                case 4:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    double det = CalcularDeterminante(A, ordem);

                    if (Math.Abs(det) < 0.000000001)
                    {
                        Console.WriteLine(
                            "\nA matriz não possui inversa."
                        );
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
                    ExibirMatriz(inversa, ordem, ordem);

                    break;

                case 5:
                    Console.WriteLine("\n===== TEOREMA DE BINET =====");

                    Console.WriteLine(
                        "\nDigite a quantidade de linhas da primeira matriz:"
                    );

                    int linhas = LerInteiroPositivo();

                    Console.WriteLine(
                        "Digite a quantidade de colunas da primeira matriz:"
                    );

                    int colunas = LerInteiroPositivo();

                    double[,] B = new double[linhas, colunas];

                    for (int i = 0; i < linhas; i++)
                    {
                        for (int j = 0; j < colunas; j++)
                        {
                            B[i, j] = LerNumero(
                                $"Digite B[{i},{j}]: "
                            );
                        }
                    }

                    Console.WriteLine(
                        "\nDigite a quantidade de linhas da segunda matriz:"
                    );

                    int linhas2 = LerInteiroPositivo();

                    Console.WriteLine(
                        "Digite a quantidade de colunas da segunda matriz:"
                    );

                    int colunas2 = LerInteiroPositivo();

                    double[,] C = new double[linhas2, colunas2];

                    for (int i = 0; i < linhas2; i++)
                    {
                        for (int j = 0; j < colunas2; j++)
                        {
                            C[i, j] = LerNumero(
                                $"Digite C[{i},{j}]: "
                            );
                        }
                    }

                    if (colunas != linhas2)
                    {
                        Console.WriteLine(
                            "\nMultiplicação impossível!"
                        );
                        break;
                    }

                    double[,] AB = new double[linhas, colunas2];

                    for (int i = 0; i < linhas; i++)
                    {
                        for (int j = 0; j < colunas2; j++)
                        {
                            for (int k = 0; k < colunas; k++)
                            {
                                AB[i, j] += B[i, k] * C[k, j];
                            }
                        }
                    }

                    Console.WriteLine("\nMatriz B:");
                    ExibirMatriz(B, linhas, colunas);

                    Console.WriteLine("\nMatriz C:");
                    ExibirMatriz(C, linhas2, colunas2);

                    Console.WriteLine("\nMatriz resultante B x C:");
                    ExibirMatriz(AB, linhas, colunas2);

                    bool BQuadrada = linhas == colunas;
                    bool CQuadrada = linhas2 == colunas2;
                    bool MesmaOrdem = linhas == linhas2;

                    if (!BQuadrada || !CQuadrada || !MesmaOrdem)
                    {
                        Console.WriteLine(
                            "\nNão é possível aplicar o Teorema de Binet."
                        );

                        Console.WriteLine(
                            "As duas matrizes devem ser quadradas " +
                            "e possuir a mesma ordem."
                        );

                        break;
                    }

                    double detB = CalcularDeterminante(B, linhas);
                    double detC = CalcularDeterminante(C, linhas2);
                    double detAB = CalcularDeterminante(AB, linhas);

                    double produtoDeterminantes = detB * detC;

                    Console.WriteLine(
                        $"\nDeterminante de B: {detB:F2}"
                    );

                    Console.WriteLine(
                        $"Determinante de C: {detC:F2}"
                    );

                    Console.WriteLine(
                        $"Determinante de B x C: {detAB:F2}"
                    );

                    Console.WriteLine(
                        $"Det(B) x Det(C): {produtoDeterminantes:F2}"
                    );

                    if (Math.Abs(detAB - produtoDeterminantes) < 0.0000001)
                    {
                        Console.WriteLine(
                            "\nTeorema de Binet confirmado!"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "\nOs resultados são diferentes."
                        );
                    }

                    break;

                case 6:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    double[,] transposta = new double[ordem, ordem];

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            transposta[j, i] = A[i, j];
                        }
                    }

                    Console.WriteLine("\nMatriz original:");
                    ExibirMatriz(A, ordem, ordem);

                    Console.WriteLine("\nMatriz transposta:");
                    ExibirMatriz(transposta, ordem, ordem);

                    break;

                case 7:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    Console.WriteLine("\n===== EFEITO DO ESCALAR NO DETERMINANTE =====");

                    Console.WriteLine("\nMatriz original:");
                    ExibirMatriz(A, ordem, ordem);

                    double detOriginal = CalcularDeterminante(A, ordem);

                    Console.WriteLine($"\nDeterminante original: {detOriginal:F6}");

                    int linha;

                    while (true)
                    {
                        Console.Write($"Digite a linha que deseja multiplicar (1 a {ordem}): ");

                        if (int.TryParse(Console.ReadLine(), out linha)
                            && linha >= 1 && linha <= ordem)
                        {
                            break;
                        }

                        Console.WriteLine("Linha inválida!");
                    }

                    double alfa = LerNumero("Digite o escalar alfa: ");

                    double[,] modificada = new double[ordem, ordem];

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            modificada[i, j] = A[i, j];
                        }
                    }
                    for (int j = 0; j < ordem; j++)
                    {
                        modificada[linha - 1, j] *= alfa;
                    }

                    Console.WriteLine("\nMatriz após a operação:");
                    ExibirMatriz(modificada, ordem, ordem);

                    double detModificado = CalcularDeterminante(modificada, ordem);

                    double detEsperado = alfa * detOriginal;

                    Console.WriteLine($"\nEscalar alfa: {alfa:F6}");
                    Console.WriteLine($"Determinante original: {detOriginal:F6}");
                    Console.WriteLine($"Determinante após a operação: {detModificado:F6}");
                    Console.WriteLine($"Determinante esperado (alfa x det): {detEsperado:F6}");

                    double escala = Math.Max(
                        1.0,
                        Math.Max(Math.Abs(detModificado), Math.Abs(detEsperado))
                    );

                    double tolerancia = 0.000000001 * escala;

                    if (Math.Abs(detModificado - detEsperado) <= tolerancia)
                    {
                        Console.WriteLine("\nPropriedade confirmada!");
                        Console.WriteLine(
                            "Multiplicar uma linha por alfa multiplica " +
                            "o determinante pelo mesmo fator."
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "\nOs valores diferem além da tolerância definida."
                        );
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

    static double LerNumero(string mensagem)
    {
        double valor;

        while (true)
        {
            Console.Write(mensagem);

            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }

            Console.WriteLine("Digite um número válido!");
        }
    }

    static int LerInteiroPositivo()
    {
        int valor;

        while (true)
        {
            Console.Write("Digite um inteiro maior que zero: ");

            if (int.TryParse(Console.ReadLine(), out valor) && valor > 0)
            {
                return valor;
            }

            Console.WriteLine("Dimensão inválida!");
        }
    }

    static void ExibirMatriz(
        double[,] matriz, int linhas, int colunas)
    {
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                Console.Write($"{matriz[i, j],10:F2} ");
            }

            Console.WriteLine();
        }
    }

    static double CalcularDeterminante(double[,] matriz, int ordem)
    {
        if (ordem == 1)
        {
            return matriz[0, 0];
        }

        if (ordem == 2)
        {
            return matriz[0, 0] * matriz[1, 1]
                 - matriz[0, 1] * matriz[1, 0];
        }

        double determinante = 0;

        for (int j = 0; j < ordem; j++)
        {
            double[,] menor = new double[ordem - 1, ordem - 1];

            int colunaMenor = 0;

            for (int coluna = 0; coluna < ordem; coluna++)
            {
                if (coluna == j)
                {
                    continue;
                }

                for (int i = 1; i < ordem; i++)
                {
                    menor[i - 1, colunaMenor] = matriz[i, coluna];
                }

                colunaMenor++;
            }

            double sinal = j % 2 == 0 ? 1 : -1;

            determinante += sinal
                          * matriz[0, j]
                          * CalcularDeterminante(menor, ordem - 1);
        }

        return determinante;
    }
}
