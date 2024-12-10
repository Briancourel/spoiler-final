using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spoiler_final
{
    internal class Program
    {
        struct Alumno
        {
            public string Nombre;
            public int Materia1;
            public int Materia2;
            public int Materia3;
            public int Materia4;

            public Alumno(string nombre, int materia1, int materia2, int materia3, int materia4)
            {
                Nombre = nombre;
                Materia1 = materia1;
                Materia2 = materia2;
                Materia3 = materia3;
                Materia4 = materia4;

            }
        }

        static Alumno[] Nombres = new Alumno[6];
        static string[] Materias = { "Programacion", "Matematica", "Sistemas operativos", "Base de datos" };
        static int[,] Notas = new int[6,4];
        static List<List<int>> listaNotas = new List<List<int>>();

        static bool cargarAlumnos = false;
        static int opcion;
        static string nombreAlumno;
        static bool listaConvertida;

        static void IngresoDeAlumnos(Alumno[] Nombres)
        {
           for (int i = 0; i < Nombres.Length; i++)
            {
                Console.WriteLine($" Ingrese el nombre del alumno {i + 1} : ");
                nombreAlumno = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(nombreAlumno))
                {
                    Console.WriteLine($" Porfavor ingrese el nombre del alumno {i + 1}: ");
                    nombreAlumno = Console.ReadLine() ;
                }
                Nombres[i] = new Alumno {Nombre = nombreAlumno};
            }
           cargarAlumnos = true;
        }
        static void OrdenarNombres(Alumno[] Nombres)
        {
            if (cargarAlumnos == true)
            {
                for (int i = 0; i < Nombres.Length - i; i++)
                {
                    for (int j = 0; j < Nombres.Length - 1 - i; j++)
                    {
                        if (string.Compare(Nombres[j].Nombre, Nombres[j + 1].Nombre) > 0)
                        {
                            var temp = Nombres[j];
                            Nombres[j] = Nombres[j + 1];
                            Nombres[j + 1] = temp;
                        }
                    }
                }

                Console.WriteLine($" Los nombres ordenados son:");
                foreach (var lista in Nombres)
                {
                    Console.WriteLine(lista.Nombre);
                }
            }
            else { Console.WriteLine("aun no se cargaron los alumnos"); }
        }

        static void IngresarNotas(Alumno[] Nombres,string[] Materias, int[,] Notas)
        {
            for (int i = 0; i < Nombres.Length; i++)
            {
                Console.WriteLine($"alumno {Nombres[i].Nombre} :");

                for (int j = 0; j < Materias.Length; j++)
                {
                    Notas[i, j] = NotaValida(Materias[j]); 
                    
                }

            }
            
        }

        static int NotaValida(string Materias)
        {
            bool EsValida = false;
            int nota;

            do
            {
                Console.WriteLine($"ingrese la nota para la materia {Materias}");
                EsValida = int.TryParse(Console.ReadLine(), out nota) && nota >= 0 && nota <= 10;

                if (!EsValida)
                {
                    Console.WriteLine(" Nota invalida, la nota debe ser entre 0 y 10");
                }
                

            } while (!EsValida);
            return nota;
        }

        static void MostrarNotas(int[,] Notas, string[] MateriaS, Alumno[] Nombres )
        {
            for (int i = 0;i < Nombres.Length;i++)
            {
                Console.WriteLine($" Notas del alumno {Nombres[i].Nombre}:");
                for(int j = 0;j < Notas.GetLength(1); j++)
                {
                    Console.WriteLine($"Materia {Materias[j]} : {Notas[i,j]}");
                }
                Console.WriteLine();
            }

        }

        static void ConvertirALista(List<List<int>> listaNotas, string[] Materias)
        {
            {
                for(int i = 0; i< Notas.GetLength(0); i++)
                {
                    List<int> notasAlumno = new List<int>();
                    
                        for ( int j = 0; j < Notas.GetLength(1); j++)
                        {
                            notasAlumno.Add(Notas[i,j]);
                        }
                        listaNotas.Add(notasAlumno);
                }
                
            }
            Console.WriteLine("listo convertiste todo a una lista dinamica! ahora puede sacar el promedio") ;
        }

        static void MostrarPromedio(List<List<int>> listaNotas, Alumno[] Nombres)
        {
            double promedio = 0;

            //    for (int i = 0; i < listaNotas.Count; i++)
            //    {
            //        promedio = CalcularPromedio(listaNotas[i]);

            //        Console.WriteLine($"Nombre: {Nombres[i].Nombre}. Promedio: {promedio}");
            //    }
            //}
            for (int i = 0; i < listaNotas.Count; i++)
            {
                // Calcular el promedio de las notas del alumno
                promedio = CalcularPromedio(listaNotas[i]);

                // Mostrar el nombre y el promedio
                Console.WriteLine($"Alumno: {Nombres[i].Nombre}, Promedio: {promedio:F2}");
            }
        }

    static double CalcularPromedio( List<int> notasAlumno)
        {
            int sumaNotas = 0 ;
            foreach (var suma in notasAlumno)
            {
                sumaNotas = sumaNotas + suma;
                
            }
            return (double)sumaNotas / notasAlumno.Count;
        }
      
        static void MenuYManejo()
        {
            do
            {
                Console.WriteLine(" -----Bienvenido al programa-----");
                Console.WriteLine("1) Ingresar nombres");
                Console.WriteLine("2) Mostrar nombres ordenados");
                Console.WriteLine("3) Ingresar notas ");
                Console.WriteLine("4) Mostrar notas de los alumnos");
                Console.WriteLine("5) Covertir a lista dinamica ");
                Console.WriteLine("6) Calcular promedio de notas");
                Console.WriteLine("7) Salir del programa");

                 opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                        case 1: IngresoDeAlumnos(Nombres); break;
                        case 2: OrdenarNombres(Nombres); break;
                        case 3: IngresarNotas(Nombres,Materias ,Notas); break;
                        case 4: MostrarNotas(Notas, Materias, Nombres); break;
                        case 5: ConvertirALista(listaNotas, Materias);break;
                        case 6: MostrarPromedio(listaNotas, Nombres); break;
                        case 7: Console.WriteLine(" Saliste del programa"); break;
                    default: Console.WriteLine("TECLA INVALIDA"); break;
                }
            } while (opcion != 7);
        }
        



        static void Main(string[] args)
        {
           MenuYManejo();
        }
    }
}
