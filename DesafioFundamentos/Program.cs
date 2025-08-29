using DesafioFundamentos.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = 0;
decimal precoPorHora = 0;

Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n" +
                  "Digite o preço inicial:");

while (!decimal.TryParse(Console.ReadLine(), out precoInicial) || precoInicial < 0)
{
    WriteError("Valor inválido. Digite um preço inicial válido (número positivo).");
}


Console.WriteLine("Agora digite o preço por hora:");
while (!decimal.TryParse(Console.ReadLine(), out precoPorHora) || precoPorHora < 0)
{
    WriteError("Valor inválido. Digite um preço por hora válido (número positivo).");
}

Estacionamento estacionamento = new(precoInicial, precoPorHora);

bool exibirMenu = true;

while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placaAdicionar = Console.ReadLine();
            try
            {
                estacionamento.AdicionarVeiculo(placaAdicionar);
                WriteSuccess("Veículo cadastrado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                WriteError(ex.Message);
            }
            break;

        case "2":
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placaRemover = Console.ReadLine();

            if (estacionamento.VeiculoExiste(placaRemover))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                if (int.TryParse(Console.ReadLine(), out int horas) && horas >= 0)
                {
                    decimal valorTotal = estacionamento.RemoverVeiculo(placaRemover, horas);
                    WriteSuccess($"O veículo {placaRemover} foi removido e o preço total foi de: R$ {valorTotal:F2}");
                }
                else
                {
                    WriteError("Valor de horas inválido. Operação cancelada.");
                }
            }
            else
            {
                WriteError("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
            break;

        case "3":
            var veiculos = estacionamento.ListarVeiculos();
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Não há veículos estacionados.");
                Console.ResetColor();
            }
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            WriteError("Opção inválida");
            break;
    }

    Console.WriteLine("\nPressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");

// Funções Auxiliares para Cores
void WriteSuccess(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
}

void WriteError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}