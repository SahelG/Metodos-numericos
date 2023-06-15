using System;

namespace Euler{
    class Program{

        public static double EcuacionDiferencial (double x, double y) {
            return 2*x*y;
        }

        public static void MetodoEuler (double x0, double y0, double h, double xn) {
            //x0 -> tiempo inicial
            //y0 -> valor inicial
            //h -> tamaño de paso
            //xn -> punto a localizar
            double x, y;
            x = x0;
            y = y0;

            while (x < xn) {
                y += h * EcuacionDiferencial(x, y);
                x += h;
                Console.WriteLine(x + "\r\t\t\t" + y);
            }
        }

        static void Main(string[] args){
            int opcion;
            double x0 = 0.0, y0 = 0.0, h = 0.0;
            double xn = 0;
            bool bandera = true;

            while (bandera) {
                Console.WriteLine("--------------- M E N U ---------------");
                Console.WriteLine("Ecuacion diferencial: 2xy");
                Console.WriteLine("1. Llenar datos.");
                Console.WriteLine("2. Metodo de euler.");
                Console.WriteLine("3. Salir");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1) {
                    Console.WriteLine("Ingrese el valor de X0: ");
                    x0 = double.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el valor de Y0: ");
                    y0 = double.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el valor de los pasos: ");
                    h = double.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el valor de XN: ");
                    xn = double.Parse(Console.ReadLine());
                }else if (opcion == 2) {
                    Console.WriteLine("X\t\t\tY");
                    MetodoEuler(x0, y0, h, xn); 
                }else if (opcion == 3) {
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else{
                    Console.WriteLine("Opcion incorrecta");
                }
            }
        }
    }
}
