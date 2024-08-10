using System;
using System.IO;

class Program
{
    const string CarnetsFile = "carnets.txt";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Agregar jugador");
            Console.WriteLine("2. Listar jugadores");
            Console.WriteLine("3. Buscar jugador");
            Console.WriteLine("4. Modificar datos de un jugador");
            Console.WriteLine("5. Salir");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarJugador();
                    break;
                case "2":
                    ListarJugadores();
                    break;
                case "3":
                    BuscarJugador();
                    break;
                case "4":
                    ModificarJugador();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void AgregarJugador()
    {
        Console.Write("Ingrese el carnet del jugador: ");
        var carnet = Console.ReadLine();
        Console.Write("Ingrese el nombre del jugador: ");
        var nombre = Console.ReadLine();
        Console.Write("Ingrese la edad del jugador: ");
        var edad = Console.ReadLine();
        Console.Write("Ingrese el punteo del jugador: ");
        var punteo = Console.ReadLine();

        // Guardar el carnet en el archivo de carnets si no existe
        if (!File.Exists($"{carnet}.txt"))
        {
            File.AppendAllText(CarnetsFile, carnet + Environment.NewLine);
        }
        else
        {
            Console.WriteLine("Este carnet ya existe. No se puede agregar el jugador.");
            return;
        }

        // Crear un archivo con el carnet y guardar los datos del jugador
        var jugadorFile = $"{carnet}.txt";
        using (StreamWriter sw = new StreamWriter(jugadorFile))
        {
            sw.WriteLine(nombre);
            sw.WriteLine(edad);
            sw.WriteLine(punteo);
        }

        Console.WriteLine("Jugador agregado exitosamente.");
    }

    static void ListarJugadores()
    {
        if (!File.Exists(CarnetsFile))
        {
            Console.WriteLine("No hay jugadores registrados.");
            return;
        }

        var carnets = File.ReadAllLines(CarnetsFile);
        foreach (var carnet in carnets)
        {
            var jugadorFile = $"{carnet}.txt";
            if (File.Exists(jugadorFile))
            {
                var datos = File.ReadAllLines(jugadorFile);
                Console.WriteLine($"Carnet: {carnet}");
                Console.WriteLine($"Nombre: {datos[0]}");
                Console.WriteLine($"Edad: {datos[1]}");
                Console.WriteLine($"Punteo: {datos[2]}");
                Console.WriteLine("-----------------------");
            }
        }
    }

    static void BuscarJugador()
    {
        Console.Write("Ingrese el carnet del jugador que desea buscar: ");
        var carnet = Console.ReadLine();
        var jugadorFile = $"{carnet}.txt";

        if (File.Exists(jugadorFile))
        {
            var datos = File.ReadAllLines(jugadorFile);
            Console.WriteLine($"Carnet: {carnet}");
            Console.WriteLine($"Nombre: {datos[0]}");
            Console.WriteLine($"Edad: {datos[1]}");
            Console.WriteLine($"Punteo: {datos[2]}");
        }
        else
        {
            Console.WriteLine("Jugador no encontrado.");
        }
    }

    static void ModificarJugador()
    {
        Console.Write("Ingrese el carnet del jugador que desea modificar: ");
        var carnet = Console.ReadLine();
        var jugadorFile = $"{carnet}.txt";

        if (File.Exists(jugadorFile))
        {
            var datos = File.ReadAllLines(jugadorFile);

            Console.WriteLine($"Nombre actual: {datos[0]}");
            Console.Write("Ingrese el nuevo nombre (presione Enter para mantenerlo): ");
            var nombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre))
                datos[0] = nombre;

            Console.WriteLine($"Edad actual: {datos[1]}");
            Console.Write("Ingrese la nueva edad (presione Enter para mantenerla): ");
            var edad = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(edad))
                datos[1] = edad;

            Console.WriteLine($"Punteo actual: {datos[2]}");
            Console.Write("Ingrese el nuevo punteo (presione Enter para mantenerlo): ");
            var punteo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(punteo))
                datos[2] = punteo;

            File.WriteAllLines(jugadorFile, datos);
            Console.WriteLine("Datos del jugador actualizados exitosamente.");
        }
        else
        {
            Console.WriteLine("Jugador no encontrado.");
        }
    }
}
