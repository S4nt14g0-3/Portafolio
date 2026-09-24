namespace SistemaInventario.Models
{
    public class Pieza
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Categoria { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
    }

    public class RegistroCombustible
    {
        public int Id { get; set; }
        public string? Equipo { get; set; }
        public double Cantidad { get; set; }
        public double Odometro { get; set; }
        public string? Fecha { get; set; }
    }
}