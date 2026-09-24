using System.Collections.ObjectModel;

namespace SistemaInventario.Models
{
    public class Pieza
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
    }

    public class RegistroCombustible
    {
        public int Id { get; set; }
        public string Equipo { get; set; } = string.Empty;
        public double Cantidad { get; set; }
        public double Odometro { get; set; }
        public string Fecha { get; set; } = string.Empty;
    }
}