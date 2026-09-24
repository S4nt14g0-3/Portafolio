using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SistemaInventario.Models;

namespace SistemaInventario.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BaseDeDatos.Inicializar(); // Inicializa la base de datos al iniciar la aplicación

            ActualizarTablas(); // Carga los datos en los DataGrids al iniciar la aplicación
        }

        private void ActualizarTablas()
        {
            GridPiezas.ItemsSource = BaseDeDatos.ObtenerPiezas();
            GridCombustible.ItemsSource = BaseDeDatos.ObtenerCombustible();
        }

        private void OnGuardarPiezaClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtStock.Text, out int stock)&& int.TryParse(TxtStockMinimo.Text, out int stockMinimo))
            {
                var Nuevapieza = new Pieza
                {
                    Nombre = TxtNombrePieza.Text,
                    Categoria = TxtCategoria.Text,
                    Stock = stock,
                    StockMinimo = stockMinimo
                };
                BaseDeDatos.GuardarPieza(Nuevapieza);
                ActualizarTablas();

                // Limpiar los campos después de guardar
                TxtNombrePieza.Text = string.Empty;
                TxtCategoria.Text = string.Empty;
                TxtStock.Text = string.Empty;
                TxtStockMinimo.Text = string.Empty;
            }
        }

        // Evento cuando se registra combustible
        private void OnRegistrarCombustibleClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtCantidadCombustible.Text, out double cantidad) && double.TryParse(TxtOdometro.Text, out double odometro))
            {
                var Nuevoregistro = new RegistroCombustible
                {
                    Equipo = TxtEquipo.Text,
                    Cantidad = cantidad,
                    Odometro = odometro,
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                BaseDeDatos.GuardarCombustible(Nuevoregistro);
                
                ActualizarTablas();

                // Limpiar los campos después de registrar
                TxtEquipo.Text = string.Empty;
                TxtCantidadCombustible.Text = string.Empty;
                TxtOdometro.Text = string.Empty;
            }
        }
    }
}