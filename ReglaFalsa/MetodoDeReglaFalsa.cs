using System;

namespace Biseccion{
    class MetodoDeReglaFalsa {

        static double funcion (double x, int i){

            if (i == 1){
                return x - Math.Pow(2, -x);
            }else if (i == 2){
                return Math.Exp(-x) - x;
            }else
                return Math.Pow(x, 3) + 4 * Math.Pow(x, 2) - 10;
        }

        static void MetodoReglaFalsa(double xmin, double xmax, double tolerancia, int i){
            double a, b, fa, fb, error, xnew, xold, fxnew;
            int contador;
            a = xmin;
            b = xmax;
            fa = funcion(a, i);
            fb = funcion(b, i);

            error = 100;
            xnew = 0;
            xold = 0;

            if (fa*fb > 0){
                Console.WriteLine("El intervalo dado no es valido");
            }else{
                contador = 0;

                while (error > tolerancia){
                    xnew = a - ((fa * (b - a))/(fb - fa));
                    fxnew = funcion(xnew, i);

                    if (fa*fxnew > 0){
                        a = xnew;
                        fa = fxnew;
                    }else{
                        b = xnew;
                        fb = fxnew;
                    }

                    contador = contador + 1;
                    error = Math.Abs(((xnew-xold)/xnew)*100);
                    xold = xnew;
                }
                Console.WriteLine("Raiz: " + xold + "\nNumero de iteraciones: " + contador + "\nError: " + error);
            }
        }


        static void Main(string[] args){
            int opcion = 0;
            double xmin = 0, xmax = 0, tolerancia = 0;
            bool bandera = true;

            while (bandera){
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- f(x) = x - 2^(-x)");
                Console.WriteLine("3.- f(x) = exp(-x) - x");
                Console.WriteLine("4.- f(x) = x^3 + 4x^2 - 10");
                Console.WriteLine("5.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el limite inferior: ");
                    xmin = double.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese el limite superior: ");
                    xmax = double.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese la tolerancia: ");
                    tolerancia = double.Parse(Console.ReadLine());

                }else if (opcion == 2){
                    MetodoReglaFalsa(xmin, xmax, tolerancia, 1);
                }else if (opcion == 3){
                    MetodoReglaFalsa(xmin, xmax, tolerancia, 2);
                }else if (opcion == 4){
                    MetodoReglaFalsa(xmin, xmax, tolerancia, 3);
                }else if (opcion == 5){
                    Console.WriteLine("Hasta luego");
                    bandera = false;
                }else{
                    Console.WriteLine("Opcion no valida.");
                }
            }
        }
    }
}

