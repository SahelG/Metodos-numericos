using System;

namespace MetodoDeSimpsonTresOctavos{
    class Program{
        static double limiteInferior, limiteSuperior;
        static int numeroIntervalos;
        static double altura, sumador1 = 0.0;

        public static double Funcion (double x) {
            return Math.Exp(Math.Pow(x,2));
        }

        public static double SimpsonTresOctavos (double a, double b, double n) {
            int i;
            double x;
            a = limiteInferior;
            b = limiteSuperior;
            n = numeroIntervalos;

            altura = (b - a) / n;
            
            for (i = 1; i < n; i++){
                x = a + i * altura;

                if (i % 3 == 0){
                    sumador1 += 2 * Funcion(x);
                }else
                    sumador1 += 3 * Funcion(x);
            }
            return (3 * altura / 8) * (Funcion(a) + Funcion(b) + sumador1);
        }
        
        static void Main(string[] args){
            int opcion;
            bool bandera = true;

            while (bandera){
                Console.WriteLine("-------------- Menu --------------");
                Console.WriteLine("\tFuncion: e^(x^2)\n");
                Console.WriteLine("1.- Ingresar valores.");
                Console.WriteLine("2.- Metodo del Simpson 3/8.");
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
                    Console.WriteLine("----------------------------" + "\nResultado: " + SimpsonTresOctavos(limiteInferior, limiteSuperior, numeroIntervalos));
                else if (opcion == 3){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else
                    Console.WriteLine("Opcion incorrecta.");
            }
        }
    }
}
