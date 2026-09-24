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

            try
            {
                BaseDeDatos.Inicializar();
                ActualizarTablas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar: " + ex.Message);
            }
        }

        private void ActualizarTablas()
        {
            try
            {
                if (GridPiezas != null)
                {
                    GridPiezas.ItemsSource = BaseDeDatos.ObtenerPiezas();
                }
                if (GridCombustible != null)
                {
                    GridCombustible.ItemsSource = BaseDeDatos.ObtenerCombustible();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar tablas: " + ex.Message);
            }
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

        private void OnEliminarPiezaClick(object sender, RoutedEventArgs e)
        {
            if (GridPiezas.SelectedItem is Pieza piezaSeleccionada)
            {
                BaseDeDatos.EliminarPieza(piezaSeleccionada.Id);
                ActualizarTablas();
            }
        }

        private void OnEliminarCombustibleClick(object sender, RoutedEventArgs e)
        {
            if (GridCombustible.SelectedItem is RegistroCombustible registroSeleccionado)
            {
                BaseDeDatos.EliminarCombustible(registroSeleccionado.Id);
                ActualizarTablas();
            }
        }
    }
}