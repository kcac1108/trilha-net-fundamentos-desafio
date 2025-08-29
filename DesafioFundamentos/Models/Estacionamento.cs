namespace DesafioFundamentos.Models
{
    public class Estacionamento(decimal precoInicial, decimal precoPorHora)
    {
        private readonly List<string> veiculos = [];

        public void AdicionarVeiculo(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                throw new ArgumentException("A placa não pode ser nula ou vazia.", nameof(placa));
            }
            veiculos.Add(placa.ToUpper());
        }

        public decimal RemoverVeiculo(string placa, int horas)
        {
            if (horas < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(horas), "A quantidade de horas não pode ser negativa.");
            }

            string placaFormatada = placa.ToUpper();
            if (veiculos.Remove(placaFormatada))
            {
                decimal valorTotal = precoInicial + (precoPorHora * horas);
                return valorTotal;
            }
            
            throw new InvalidOperationException("Veículo não encontrado. Confira se a placa foi digitada corretamente.");
        }

        public IReadOnlyList<string> ListarVeiculos()
        {
            return veiculos.AsReadOnly();
        }

        public bool VeiculoExiste(string placa)
        {
            return veiculos.Contains(placa.ToUpper());
        }
    }
}
