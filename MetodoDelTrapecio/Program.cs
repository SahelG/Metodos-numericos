using System;

namespace MetodoDelTrapecio{
    class Program{
        static double limiteInferior, limiteSuperior;
        static int numeroIntervalos;
        static double altura, sumador;

        public static double Funcion (double x) {
            return (Math.Sin(x)/x);
        }

        public static double Trapecio (double a, double b, double n) {
            int i;
            a = limiteInferior;
            b = limiteSuperior;
            n = numeroIntervalos;

            altura = (b - a) / n;
            sumador = 0.0;

            for (i = 1; i <= n-1; i++)
                sumador += Funcion(a + altura * i);
            

            return altura/2 * (Funcion(a) + Funcion(b)) + altura * sumador;
        }

        static void Main(string[] args){
            int opcion;
            bool bandera = true;

            while (bandera){
                Console.WriteLine("----------- Menu -----------");
                Console.WriteLine("Funcion: sin(x) / x");
                Console.WriteLine("1.- Ingresar valores.");
                Console.WriteLine("2.- Metodo del trapecio.");
                Console.WriteLine("3.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el limite inferior: ");
                    limiteInferior = double.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el limite superior: ");
                    limiteSuperior = double.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el numero de intervalos: ");
                    numeroIntervalos = int.Parse(Console.ReadLine());
                } else if (opcion == 2)
                    Console.WriteLine("----------------------------" + "\nResultado: " + Trapecio(limiteInferior, limiteSuperior, numeroIntervalos));
                else if (opcion == 3){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else
                    Console.WriteLine("Opcion incorrecta.");
            }
        }
    }
}
