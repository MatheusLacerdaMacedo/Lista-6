
using System; // Disponibiliza recursos básicos, como Console, Math e Random.

// Classe principal que reúne o menu e os métodos de operações com matrizes.
class Program
{
    // Método principal: a execução do programa começa aqui.
    static void Main()
    {
        // Controla a repetição do menu. O programa termina quando sair passa a true.
        bool sair = false;

        // Indica se o usuário já informou uma matriz para as opções que precisam dela.
        bool matrizLida = false;

        // Guarda a ordem da matriz A e inicia A como uma matriz vazia.
        int ordem = 0;
        double[,] A = new double[0, 0];

        // Mantém o menu disponível até que o usuário escolha a opção de saída.
        while (!sair)
        {
            // Apresenta as funcionalidades disponíveis no programa.
            Console.WriteLine("\n===== MENU DE MATRIZES =====");
            Console.WriteLine("1 - Ler matriz 2x2");
            Console.WriteLine("2 - Ler matriz 1x1");
            Console.WriteLine("3 - Calcular determinante");
            Console.WriteLine("4 - Calcular matriz inversa");
            Console.WriteLine("5 - Teorema de Binet");
            Console.WriteLine("6 - Transposição");
            Console.WriteLine("7 - Multiplicar linha por escalar (efeito no determinante)");
            Console.WriteLine("8 - Ordem n");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            // Tenta converter a entrada para inteiro; evita erro se o usuário digitar texto.
            if (!int.TryParse(Console.ReadLine(), out int escolha))
            {
                Console.WriteLine("Digite uma opção numérica válida!");
                continue;
            }

            // Direciona a execução conforme a opção selecionada no menu.
            switch (escolha)
            {
                // Opções 1 e 2: leem uma matriz quadrada de ordem 2 ou 1.
                case 1:
                case 2:
                    // Operador condicional: opção 1 define ordem 2; opção 2 define ordem 1.
                    ordem = escolha == 1 ? 2 : 1;

                    // Cria a matriz A com a quantidade de linhas e colunas escolhida.
                    A = new double[ordem, ordem];

                    Console.WriteLine(
                        $"\nDigite os valores da matriz {ordem}x{ordem}:"
                    );

                    // Percorre cada linha da matriz para solicitar todos os seus elementos.
                    for (int i = 0; i < ordem; i++)
                    {
                        // Percorre as colunas da linha atual.
                        for (int j = 0; j < ordem; j++)
                        {
                            // Lê e armazena o elemento na posição [i, j].
                            A[i, j] = LerNumero(
                                $"Digite A[{i},{j}]: "
                            );
                        }
                    }

                    // Registra que já existe uma matriz válida para as outras operações.
                    matrizLida = true;

                    // Mostra a matriz informada pelo usuário.
                    Console.WriteLine("\nMatriz lida:");
                    ExibirMatriz(A, ordem, ordem);

                    break;

                // Opção 3: calcula e exibe o determinante da matriz A.
                case 3:
                    // Impede o cálculo quando nenhuma matriz foi lida.
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    // Chama o método responsável pelo cálculo do determinante.
                    double determinante = CalcularDeterminante(A, ordem);

                    Console.WriteLine(
                        $"\nDeterminante: {determinante:F2}"
                    );

                    break;

                // Opção 4: calcula a matriz inversa de A, quando ela existe.
                case 4:
                    // A inversa só pode ser calculada depois da leitura de uma matriz.
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    // Uma matriz quadrada é invertível quando seu determinante é diferente de zero.
                    double det = CalcularDeterminante(A, ordem);

                    // Usa uma pequena tolerância para tratar valores muito próximos de zero.
                    if (Math.Abs(det) < 0.000000001)
                    {
                        Console.WriteLine(
                            "\nA matriz não possui inversa."
                        );
                        break;
                    }

                    // Reserva uma matriz para armazenar o resultado da inversão.
                    double[,] inversa = new double[ordem, ordem];

                    // Para uma matriz 1x1, a inversa é o inverso multiplicativo do elemento.
                    if (ordem == 1)
                    {
                        inversa[0, 0] = 1.0 / A[0, 0];
                    }
                    else
                    {
                        // Fórmula da inversa de uma matriz 2x2, dividindo os elementos pelo determinante.
                        // Os elementos da diagonal principal são trocados e os demais têm o sinal invertido.
                        inversa[0, 0] = A[1, 1] / det;
                        inversa[0, 1] = -A[0, 1] / det;
                        inversa[1, 0] = -A[1, 0] / det;
                        inversa[1, 1] = A[0, 0] / det;
                    }

                    Console.WriteLine("\nMatriz inversa:");
                    ExibirMatriz(inversa, ordem, ordem);

                    break;

                // Opção 5: verifica o Teorema de Binet, det(B x C) = det(B) x det(C).
                case 5:
                    Console.WriteLine("\n===== TEOREMA DE BINET =====");

                    Console.WriteLine(
                        "\nDigite a quantidade de linhas da primeira matriz:"
                    );

                    // Lê as dimensões e, em seguida, os elementos da primeira matriz B.
                    int linhas = LerInteiroPositivo();

                    Console.WriteLine(
                        "Digite a quantidade de colunas da primeira matriz:"
                    );

                    int colunas = LerInteiroPositivo();

                    // Cria a primeira matriz com as dimensões informadas.
                    double[,] B = new double[linhas, colunas];

                    // Preenche B linha por linha.
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

                    // Lê as dimensões da segunda matriz C.
                    int linhas2 = LerInteiroPositivo();

                    Console.WriteLine(
                        "Digite a quantidade de colunas da segunda matriz:"
                    );

                    int colunas2 = LerInteiroPositivo();

                    // Cria a segunda matriz e solicita seus elementos.
                    double[,] C = new double[linhas2, colunas2];

                    // Preenche C linha por linha.
                    for (int i = 0; i < linhas2; i++)
                    {
                        for (int j = 0; j < colunas2; j++)
                        {
                            C[i, j] = LerNumero(
                                $"Digite C[{i},{j}]: "
                            );
                        }
                    }

                    // A multiplicação B x C exige que o número de colunas de B
                    // seja igual ao número de linhas de C.
                    if (colunas != linhas2)
                    {
                        Console.WriteLine(
                            "\nMultiplicação impossível!"
                        );
                        break;
                    }

                    // A matriz resultante terá o número de linhas de B e de colunas de C.
                    double[,] AB = new double[linhas, colunas2];

                    // Os três laços calculam cada elemento da multiplicação matricial.
                    for (int i = 0; i < linhas; i++)
                    {
                        for (int j = 0; j < colunas2; j++)
                        {
                            // Soma os produtos dos elementos da linha i de B
                            // pelos elementos da coluna j de C.
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

                    // O Teorema de Binet exige que B e C sejam quadradas e da mesma ordem.
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

                    // Calcula os determinantes das duas matrizes e da matriz produto.
                    double detB = CalcularDeterminante(B, linhas);
                    double detC = CalcularDeterminante(C, linhas2);
                    double detAB = CalcularDeterminante(AB, linhas);

                    // Calcula o lado direito da igualdade prevista pelo Teorema de Binet.
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

                    // Compara os dois lados com uma tolerância para pequenas diferenças numéricas.
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

                // Opção 6: calcula a transposta da matriz A.
                case 6:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    // Cria uma matriz para receber a transposta.
                    double[,] transposta = new double[ordem, ordem];

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            // Troca linha por coluna: a posição [i, j] vai para [j, i].
                            transposta[j, i] = A[i, j];
                        }
                    }

                    Console.WriteLine("\nMatriz original:");
                    ExibirMatriz(A, ordem, ordem);

                    Console.WriteLine("\nMatriz transposta:");
                    ExibirMatriz(transposta, ordem, ordem);

                    break;

                // Opção 7: verifica o efeito de multiplicar uma linha por um escalar.
                case 7:
                    if (!matrizLida)
                    {
                        Console.WriteLine("Leia uma matriz primeiro!");
                        break;
                    }

                    Console.WriteLine("\n===== EFEITO DO ESCALAR NO DETERMINANTE =====");

                    Console.WriteLine("\nMatriz original:");
                    ExibirMatriz(A, ordem, ordem);

                    // Guarda o determinante da matriz original para comparação.
                    double detOriginal = CalcularDeterminante(A, ordem);

                    Console.WriteLine($"\nDeterminante original: {detOriginal:F6}");

                    // A linha escolhida pelo usuário é validada para estar entre 1 e a ordem.
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

                    // Lê o fator pelo qual todos os elementos da linha selecionada serão multiplicados.
                    double alfa = LerNumero("Digite o escalar alfa: ");

                    // Copia a matriz original para que A não seja alterada por esta operação.
                    double[,] modificada = new double[ordem, ordem];

                    for (int i = 0; i < ordem; i++)
                    {
                        for (int j = 0; j < ordem; j++)
                        {
                            modificada[i, j] = A[i, j];
                        }
                    }
                    // Multiplica por alfa cada elemento da linha selecionada.
                    // A subtração de 1 converte o número da linha (base 1) para índice (base 0).
                    for (int j = 0; j < ordem; j++)
                    {
                        modificada[linha - 1, j] *= alfa;
                    }

                    Console.WriteLine("\nMatriz após a operação:");
                    ExibirMatriz(modificada, ordem, ordem);

                    // Calcula o determinante depois da alteração da linha.
                    double detModificado = CalcularDeterminante(modificada, ordem);

                    // Propriedade matemática: ao multiplicar uma linha por alfa,
                    // o determinante também deve ser multiplicado por alfa.
                    double detEsperado = alfa * detOriginal;

                    Console.WriteLine($"\nEscalar alfa: {alfa:F6}");
                    Console.WriteLine($"Determinante original: {detOriginal:F6}");
                    Console.WriteLine($"Determinante após a operação: {detModificado:F6}");
                    Console.WriteLine($"Determinante esperado (alfa x det): {detEsperado:F6}");

                    // Define uma escala baseada nos resultados para calcular uma tolerância relativa.
                    double escala = Math.Max(
                        1.0,
                        Math.Max(Math.Abs(detModificado), Math.Abs(detEsperado))
                    );

                    // A tolerância ajuda a evitar falsos erros causados por arredondamentos de double.
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

                // Opção 8: executa o teste de desempenho para diferentes ordens de matriz.
                case 8:
                    // Limpa o console antes de apresentar a tabela do teste.
                    Console.Clear();
                    TestarDesempenhoDeterminante(); // Executa o método de teste separado.
                    break;
                // Opção 0: encerra a repetição do menu.
                case 0:
                    Console.WriteLine("Encerrando o programa...");
                    sair = true; // A condição do while passa a ser falsa.
                    break;

                // Qualquer número que não corresponda às opções disponíveis é inválido.
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    // Lê um número real e repete a solicitação até que a entrada possa ser convertida.
    // O parâmetro mensagem permite mostrar um texto diferente para cada elemento.
    static double LerNumero(string mensagem)
    {
        double valor;

        while (true)
        {
            Console.Write(mensagem);

            // Tenta converter o texto digitado para double.
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }

            Console.WriteLine("Digite um número válido!");
        }
    }

    // Lê uma dimensão inteira maior que zero, repetindo até receber uma entrada válida.
    static int LerInteiroPositivo()
    {
        int valor;

        while (true)
        {
            Console.Write("Digite um inteiro maior que zero: ");

            // Aceita a entrada apenas se for inteira e estritamente positiva.
            if (int.TryParse(Console.ReadLine(), out valor) && valor > 0)
            {
                return valor;
            }

            Console.WriteLine("Dimensão inválida!");
        }
    }

    // Exibe uma matriz usando as quantidades de linhas e colunas recebidas.
    // O formato F2 mostra os valores com duas casas decimais.
    static void ExibirMatriz(
        double[,] matriz, int linhas, int colunas)
    {
        // Cada iteração externa imprime uma linha da matriz.
        for (int i = 0; i < linhas; i++)
        {
            // Exibe todos os elementos da linha atual.
            for (int j = 0; j < colunas; j++)
            {
                // Reserva largura de 10 caracteres para alinhar os números na saída.
                Console.Write($"{matriz[i, j],10:F2} ");
            }

            Console.WriteLine(); // Avança para a próxima linha impressa.
        }
    }

    // Calcula o determinante de uma matriz quadrada.
    // Para ordens maiores que 2, utiliza expansão recursiva por cofatores na primeira linha.
    static double CalcularDeterminante(double[,] matriz, int ordem)
    {
        // Caso-base: o determinante de uma matriz 1x1 é seu único elemento.
        if (ordem == 1)
        {
            return matriz[0, 0];
        }

        // Caso-base: para uma matriz 2x2, det(A) = a*d - b*c.
        if (ordem == 2)
        {
            return matriz[0, 0] * matriz[1, 1]
                 - matriz[0, 1] * matriz[1, 0];
        }

        // Acumulará os termos da expansão do determinante pela primeira linha.
        double determinante = 0;

        // Cada coluna j da primeira linha gera um cofator na expansão.
        for (int j = 0; j < ordem; j++)
        {
            // Cria a matriz menor de ordem n-1, removendo a primeira linha
            // e a coluna j da matriz original.
            double[,] menor = new double[ordem - 1, ordem - 1];

            // Controla em qual coluna da matriz menor os valores serão inseridos.
            int colunaMenor = 0;

            // Percorre todas as colunas originais para copiar apenas as que não são j.
            for (int coluna = 0; coluna < ordem; coluna++)
            {
                // Pula a coluna removida para formar o menor correspondente ao termo atual.
                if (coluna == j)
                {
                    continue;
                }

                // Começa em i = 1 para não copiar a primeira linha, que foi removida.
                for (int i = 1; i < ordem; i++)
                {
                    // Copia cada elemento restante para a matriz menor.
                    menor[i - 1, colunaMenor] = matriz[i, coluna];
                }

                colunaMenor++;
            }

            // Os sinais dos cofatores alternam entre + e - conforme a coluna.
            // Como os índices começam em zero, colunas pares recebem +1 e ímpares -1.
            double sinal = j % 2 == 0 ? 1 : -1;

            // Expansão de Laplace: soma sinal * elemento da primeira linha * determinante do menor.
            // A chamada recursiva reduz a ordem da matriz em uma unidade.
            determinante += sinal
                          * matriz[0, j]
                          * CalcularDeterminante(menor, ordem - 1);
        }

        // Retorna a soma dos termos da expansão por cofatores.
        return determinante;
    }

    // Mede o tempo de cálculo do determinante para matrizes de ordens crescentes.
    static void TestarDesempenhoDeterminante()
    {
        // Maior ordem de matriz incluída no experimento.
        const int ordemMaxima = 9;

        // O valor fixo (semente) torna a sequência de matrizes reproduzível em cada execução.
        Random aleatorio = new Random(42);

        // Aquecimento inicial para reduzir a influência da compilação JIT.
        double[,] matrizAquecimento =
        {
        { 2, 1 },
        { 1, 3 }
    };

        // Executa uma chamada antes da medição para aquecer o método e reduzir
        // parte da interferência inicial da compilação JIT.
        CalcularDeterminante(matrizAquecimento, 2);

        Console.WriteLine("\n===== TESTE DE DESEMPENHO DO DETERMINANTE =====");

        // Cabeçalho da tabela: ordem da matriz, quantidade de repetições,
        // tempo total e tempo médio de cada cálculo.
        Console.WriteLine(
            $"{"Ordem",5} | {"Repetições",10} | {"Tempo total (ms)",16} | {"Tempo médio (ms)",16}"
        );

        Console.WriteLine(new string('-', 60));

        // Testa as ordens em sequência: 1, 2, 3, ... até ordemMaxima.
        for (int n = 1; n <= ordemMaxima; n++)
        {
            // Gera uma matriz quadrada de ordem n.
            double[,] matriz = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Gera um inteiro de -5 a 5 (o limite superior 6 é exclusivo)
                    // e o converte implicitamente para double.
                    matriz[i, j] = aleatorio.Next(-5, 6);
                }
            }

            // Define menos repetições para ordens maiores, pois a expansão por cofatores
            // cresce muito rapidamente em custo conforme n aumenta.
            // Isso ajuda a manter o tempo total do experimento razoável.
            int repeticoes;

            if (n <= 3)
                repeticoes = 10000;
            else if (n == 4)
                repeticoes = 2000;
            else if (n == 5)
                repeticoes = 500;
            else if (n == 6)
                repeticoes = 100;
            else if (n == 7)
                repeticoes = 20;
            else if (n == 8)
                repeticoes = 5;
            else
                repeticoes = 1;

            // Recebe o resultado do cálculo em cada repetição. O valor final não é usado
            // para análise matemática; a chamada é feita para medir seu tempo de execução.
            double determinante = 0;

            // Inicia o cronômetro imediatamente antes do trecho que será medido.
            System.Diagnostics.Stopwatch cronometro =
                System.Diagnostics.Stopwatch.StartNew();

            // Repete o cálculo sobre a mesma matriz para obter uma medição mais estável
            // nas ordens pequenas, cujas execuções individuais podem ser muito rápidas.
            for (int repeticao = 0;
                 repeticao < repeticoes;
                 repeticao++)
            {
                determinante = CalcularDeterminante(matriz, n);
            }

            // Encerra a medição após todas as repetições do cálculo.
            cronometro.Stop();

            // Tempo acumulado de todas as repetições, expresso em milissegundos.
            double tempoTotal = cronometro.Elapsed.TotalMilliseconds;
            // Divide o tempo total pelo número de repetições para estimar o tempo por cálculo.
            double tempoMedio = tempoTotal / repeticoes;

            // F2/F6 formatam a quantidade de casas decimais exibidas na tabela.
            Console.WriteLine(
                $"{n,5} | {repeticoes,10} | {tempoTotal,16:F6} | {tempoMedio,16:F6}"
            );
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine("Teste de desempenho concluído.");
    }
}
