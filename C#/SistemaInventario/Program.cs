using System;
using Avalonia;

namespace SistemaInventario
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, mantendrá la consola abierta mostrando el problema exacto
                Console.WriteLine("CRASH EN LA APLICACIÓN:");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\nPresiona ENTER para salir...");
                Console.ReadLine();
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}