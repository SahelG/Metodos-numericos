using System;

namespace PolinomioInterpoladorDeNewton{
    class Program{
        static double [] vectorX;
        static double [] vectorY;
        static double [] temp;
        static double [,] matriz;
        static double [] restaCoeficientes;
        static int numeroDePuntos = 0;
        static int i, j;

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

       /* public static void LlenarVectorY(){
            for (i = 0; i < numeroDePuntos; i++){
                Console.WriteLine("Ingrese el valor Y" + (i+1) + ": ");
                vectorY[i] = double.Parse(Console.ReadLine());
            }
        }*/

        public static double[] TablaDeDiferenciasDivididas(double [,]matriz, double []valoresX) {
            Console.WriteLine("X\tf(X)");
            temp = new double[matriz.GetLength(0)];

            for (i = 0; i < matriz.GetLength(0); i++){
                Console.Write(vectorX[i]);

                for (j = 0; j < matriz.GetLength(1); j++){
                    if (matriz[i,j] >= 0) {
                        Console.Write("\t" + matriz[i,j]);
                    }else
                        Console.Write("\t" + matriz[i,j]);
                }
                Console.WriteLine();
            }

            //Valores de la diagonal
            for (i = 0; i < matriz.GetLength(0); i++){
                for (j = 0; j < matriz.GetLength(1); j++){
                    if (i == j){
                        temp[i] = matriz[i,j];
                    }
                }
            }
            return temp;
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

        public static void Ecuacion (double [,]matriz, double []x) {
            Console.WriteLine();

            for (i = 0; i < matriz.GetLength(0); i++){
                for (j = 0; j < matriz.GetLength(0); j++){
                    if (i == j) {
                        if (i != matriz.GetLength(0) - 1) {
                            Console.Write(matriz[i,j]);
                            X(x,i);
                            Console.Write(" + ");
                        }else{
                            Console.Write(matriz[i,j]);
                            X(x,i);
                        }
                    }
                }
            }
        }

        public static void X (double []x, int j) {
            for (i = 0; i < j; i++){
                Console.Write("(x-" + x[i] + ")");
            }
        }

        public static double PolinomioNewton (double []vectorX, double []vectorY) {
            matriz = new double[vectorX.Length, vectorX.Length];

            for (int j = 0; j < vectorX.Length; j++) {
                matriz[j,0] = vectorY[j];
            }

            int p;
            for (int k = 0; k < vectorX.Length - 1; k++) {
                p = 0;
                for (int i = k + 1; i < vectorX.Length; i++) {
                    matriz[i,k + 1] = (matriz[i,k] - matriz[i - 1,k]) / (vectorX[i] - vectorX[p]);
                    p++;
                }   
            }

            TablaDeDiferenciasDivididas(matriz, vectorX);
            Ecuacion(matriz, vectorX);

        return matriz[vectorX.Length-1, vectorX.Length-1];
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
        
        public static void Main(){
            bool bandera = true;
            int opcion;

            while (bandera) {
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- Polinomio interpolador de Newton.");
                Console.WriteLine("3.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el numero de puntos: ");
                    numeroDePuntos = int.Parse(Console.ReadLine());

                    LlenarVectorDePuntos();
                }else if (opcion == 2){
                    Console.WriteLine();
                    PolinomioNewton(vectorX, vectorY);
                    restaCoeficientes =  OperacionCoeficientes(temp, vectorX);
                    Console.WriteLine();
                    Console.WriteLine();
                    ExpresionExpandida();
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