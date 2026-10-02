using System.Security.Cryptography.X509Certificates;

namespace RegistroDatos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ruta = "Gastos.csv";
            string opcion;
            List<Gasto> gastos = Cargar(ruta);
            int siguienteId = 1;

            foreach (Gasto Gast in gastos)
            {
                if (Gast.id >= siguienteId)
                {
                    siguienteId = Gast.id + 1;
                }
            }
            do
            {
                opcion = MenuGastos();
                switch (opcion)
                {
                    case "1":
                        Agregar(gastos, ref siguienteId);
                        break;
                    case "2":
                        Listar(gastos);
                        break;
                    case "3":
                        Buscar(gastos);
                        break;
                    case "4":
                        Guardar(gastos, ruta);
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }
            }
            while (opcion != "4");
        }
        
            public static string MenuGastos()
        {
            Console.WriteLine();
            Console.WriteLine("||| REGISTRO DE GASTOS |||");
            Console.WriteLine("1. Agregar gasto");
            Console.WriteLine("2. listar gastos");
            Console.WriteLine("3. Buscar por categoria");
            Console.WriteLine("4. Salir");
            Console.Write("Ingrese una opción: ");
            return Console.ReadLine();
        }

        public static void Agregar(List<Gasto> Gast, ref int id)
        {
            Console.WriteLine();
            Console.Write("Descripcion: ");
            string Descripcion = Console.ReadLine();
            Console.Write("Monto: ");
            bool Montook = decimal.TryParse(Console.ReadLine(), out decimal Monto);
            Console.Write("Categoria: ");
            string Categoria = Console.ReadLine();

            if (!Montook || Monto < 0 || string.IsNullOrWhiteSpace(Descripcion) || string.IsNullOrWhiteSpace(Categoria))
            {
                Console.WriteLine("Datos no validos");
                return;
            }
            Gast.Add(new Gasto
            {
                id = id++,
                descripcion = Descripcion,
                monto = Monto,
                categoria = Categoria
            }
                );
            id++;
            Console.WriteLine("Gasto agregado correctamente.");
        }

        public static void Listar(List<Gasto> Gast)
        { 
            int ListaElementos = Gast.Count;
            decimal Total = 0;
            if(ListaElementos == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
                
            }
            foreach (Gasto Gasto in Gast)
            {
                Console.WriteLine(Gasto);
                Total += Gasto.monto;
            }
            Console.WriteLine($"Total: {Total}");
        }

        public static void Buscar(List<Gasto> Gast)
        {
            Console.WriteLine("Categoria: ");
            string Buscar = Console.ReadLine().Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(Buscar))
            {
                Console.WriteLine("Datos invalidos");
                return;
            }
            bool encontrado = false;
            foreach(Gasto Gasto in Gast)
            {
                if (Gasto.categoria.ToUpper().Contains(Buscar))
                {
                    Console.WriteLine(Gasto);
                    encontrado = true;
                }
                if (!encontrado)
                {
                    Console.WriteLine("No se encontraron coincidencias");
                }
            }
        }

        private static void Guardar(List<Gasto> Gast, string ruta)
        {
            var lineas = new List<string>();

            foreach (Gasto Gasto in Gast)
            {
                lineas.Add($"{Gasto.id}; {Gasto.descripcion}; {Gasto.monto}; {Gasto.categoria}");
            }
            File.WriteAllLines(ruta, lineas);
            Console.WriteLine($"Gastos Guardados en {ruta} {(Gast.Count)} Gastos");
        }

        private static List<Gasto> Cargar(string ruta)
        {
            var Gast = new List<Gasto>();
            if (!File.Exists(ruta)) return Gast; 

            foreach (string linea in File.ReadAllLines(ruta))
            {
                string[] campos = linea.Split(';');

                if (campos.Length != 4) continue;
                Gasto NuevoGasto = new Gasto()
                {
                    id = int.Parse(campos[0]),
                    descripcion = campos[1],
                    monto = decimal.Parse(campos[2]),
                    categoria = campos[3]
                };
                Gast.Add(NuevoGasto);
            }
                Console.WriteLine($"Total {Gast.Count} productos cargados");
                return Gast;
        }
    }
}
