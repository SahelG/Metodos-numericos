using System;

namespace PolinomioInterpoladorDeLagrange{
    class Program{
        static double [] vectorX;
        static double [] vectorY;
        static double [] temp;
        static double [] restaCoeficientes;
        static int numeroDePuntos;
        static int i;
        //static double resultado;
        static double xint;

        public static void LlenarVectorDePuntos(){
            vectorX = new double[numeroDePuntos];
            vectorY = new double[numeroDePuntos];

            for (i = 0; i < numeroDePuntos; i++){
                Console.WriteLine("Punto" + " --------------> " + (i+1));
                Console.WriteLine("Ingrese el valor X" + (i+1) + ": ");
                vectorX[i] = double.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el valor Y" + (i+1) + ": ");
                vectorY[i] = double.Parse(Console.ReadLine());
            }
        }

        public static double [] Lagrange (double []vectorX, double []vectorY, double xint) {
            //xn --> punto cualquiera
            double [,] MLxi = new double[vectorX.Length, vectorX.Length];
            double [] Lxi = new double[vectorX.Length];
            temp = new double[MLxi.GetLength(0)];
            double resultado = 0.0;

            for (int i = 0; i < vectorX.Length; i++){
                Console.Write(vectorY[i] + " * ");
                Lxi[i] = LagrangeExpresion(vectorX, i, vectorX.Length, xint);
                resultado = resultado + Lxi[i] * vectorY[i];
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Resultado de f(" + xint + ") : " + resultado);

            for (int j = 0; j < vectorX.Length; j++) {
                MLxi[j,0] = vectorY[j];
            }

            int p;
            for (int k = 0; k < vectorX.Length - 1; k++) {
                p = 0;
                for (int i = k + 1; i < vectorX.Length; i++) {
                    MLxi[i,k + 1] = (MLxi[i,k] - MLxi[i - 1,k]) / (vectorX[i] - vectorX[p]);
                    p++;
                }   
            }

            Console.WriteLine();
            for (int l = 0; l < MLxi.GetLength(0); l++){
                for (int n = 0; n < MLxi.GetLength(1); n++){
                    if (l == n) {
                        temp[l] = MLxi[l,n];
                    }
                }
            }
            return temp;
        }

        public static double LagrangeExpresion (double []vectorX, int i, int n, double xint) {
            double resultado;
            resultado = 1;

            for (int j = 0; j < n; j++){
                if (i != j) {
                    resultado = resultado * ((xint - vectorX[j]) / (vectorX[i] - vectorX[j]));
                    double mult = vectorX[i] - vectorX[j];
                    Console.Write("((x-" + vectorX[j] + ")/(" + mult + "))");

                    if (j == (n-1)) {
                        Console.Write(" + ");
                    }
                }
            }
            return resultado;
        }

        private static double[] MultiplicarPolinomios (double []a, double []b){
		    double[] resultado = new double[a.Length+b.Length - 1];

            for(int i = 0; i < a.Length; i++){
                for(int j = 0; j< b.Length; j++){
                    resultado[i+j] += a[i] * b[j];
                }
            }
		return resultado;
	    }

        private static double[] OperacionCoeficientes (double []coeficientes, double []x){
            double[] resultado = new double[coeficientes.Length];

            for(int i = 0; i < coeficientes.Length; i++){

                double[] temp = new double[i];
                Array.Copy(x, temp, i);
                temp = OperacionCoeficientes2(temp);

                for(int u =0; u<temp.Length; u++)
                    resultado[numeroDePuntos-temp.Length + u] += temp[u] * coeficientes[i];
            }
            return resultado;
        }

        private static double[] OperacionCoeficientes2 (double[] x) {
            double[] resultado = new double[1];
            resultado[0] = 1;
            double[] temp = new double[2];
            temp[0] = 1;

            for(int i = 0; i <x.Length;i++){
                temp[1] = -x[i];
                resultado = MultiplicarPolinomios(resultado, temp);
            }
            return resultado;
        }

        //Resta el numro del exponente
        private static double count(double[] polinomio, double x){
            double resultado = 0;
            for(int i = 0; i < polinomio.Length; i++){
                resultado += polinomio[i]*Math.Pow(x, numeroDePuntos - 1 - i);
            }
            return resultado;
        }

        public static void ExpresionExpandida () {
            String signo="";
            
            for(i = 0; i < restaCoeficientes.Length; i++){
                if (restaCoeficientes[i] != 0) {

                    if (i == 0) {
                        signo = "";
                        Console.Write(signo + "  ");
                    }else if(restaCoeficientes[i] > 0) {
                        signo = "+";
                        Console.Write(signo + "  ");
                    }
                    
                    if (i == restaCoeficientes.Length-1)
                        Console.Write(restaCoeficientes[i] + "  ");
                    else if (i == restaCoeficientes.Length-2)
                        Console.Write(restaCoeficientes[i] + "x  ");
                    else
                        Console.Write(restaCoeficientes[i] + "x^" + (restaCoeficientes.Length-1-i) + "  ");
                }
            }
        }

        static void Main(string[] args){
            bool bandera = true;
            int opcion;

            while (bandera) {
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- Polinomio interpolador de Lagrange.");
                Console.WriteLine("3.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el numero de puntos: ");
                    numeroDePuntos = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese un valor a evaluar: ");
                    xint = double.Parse(Console.ReadLine());

                    LlenarVectorDePuntos();
                }else if (opcion == 2){
                    Console.WriteLine();
                    Lagrange(vectorX, vectorY, xint);
                    restaCoeficientes =  OperacionCoeficientes(temp, vectorX);
                    ExpresionExpandida();
                    Console.WriteLine();
                }else if (opcion == 3){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else{
                    Console.WriteLine("Opcion no valida.");
                }
            }
            
        }
    }
}
