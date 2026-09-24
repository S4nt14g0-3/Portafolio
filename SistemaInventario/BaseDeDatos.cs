using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;
using SistemaInventario.Models;

namespace SistemaInventario
{
    public static class BaseDeDatos
    {
        private static string cadenaConexion = "Data Source=inventario.db";

        // Inicializa las tablas si no existen en el archivo local
        public static void Inicializar()
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string comandoSql = @"
                    CREATE TABLE IF NOT EXISTS Piezas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT,
                        Categoria TEXT,
                        Stock INTEGER,
                        StockMinimo INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS Combustible (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Equipo TEXT,
                        Cantidad REAL,
                        Odometro REAL,
                        Fecha TEXT
                    );
                ";
                using (var comando = new SqliteCommand(comandoSql, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }

        // --- MÉTODOS PARA PIEZAS ---
        public static void GuardarPieza(Pieza pieza)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "INSERT INTO Piezas (Nombre, Categoria, Stock, StockMinimo) VALUES (@nombre, @categoria, @stock, @stockMin)";
                using (var cmd = new SqliteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", pieza.Nombre);
                    cmd.Parameters.AddWithValue("@categoria", pieza.Categoria);
                    cmd.Parameters.AddWithValue("@stock", pieza.Stock);
                    cmd.Parameters.AddWithValue("@stockMin", pieza.StockMinimo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static ObservableCollection<Pieza> ObtenerPiezas()
        {
            var lista = new ObservableCollection<Pieza>();
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT * FROM Piezas";
                using (var cmd = new SqliteCommand(query, conexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pieza
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Categoria = reader.GetString(2),
                                Stock = reader.GetInt32(3),
                                StockMinimo = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // --- MÉTODOS PARA COMBUSTIBLE ---
        public static void GuardarCombustible(RegistroCombustible reg)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "INSERT INTO Combustible (Equipo, Cantidad, Odometro, Fecha) VALUES (@equipo, @cantidad, @odometro, @fecha)";
                using (var cmd = new SqliteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@equipo", reg.Equipo);
                    cmd.Parameters.AddWithValue("@cantidad", reg.Cantidad);
                    cmd.Parameters.AddWithValue("@odometro", reg.Odometro);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static ObservableCollection<RegistroCombustible> ObtenerCombustible()
        {
            var lista = new ObservableCollection<RegistroCombustible>();
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT * FROM Combustible";
                using (var cmd = new SqliteCommand(query, conexion))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RegistroCombustible
                            {
                                Id = reader.GetInt32(0),
                                Equipo = reader.GetString(1),
                                Cantidad = reader.GetDouble(2),
                                Odometro = reader.GetDouble(3),
                                Fecha = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}