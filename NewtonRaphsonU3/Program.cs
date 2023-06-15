using System;

namespace NewtonRaphsonU3{
    class Program{
        static double [] vectorFunciones;
        static double [] vectorInicial;
        static double [] vectorMultiplicacion;
        static double [] vectorResta;
        static double [,] matrizDerivadas;
        static double [,] matrizDeIdentidad;
        static double x = 0.0, y = 0.0, pivote = 0.0, valorAuxiliar = 0.0;
        static int i, b, j, k;

        public static void VectorDeFunciones (double x, double y) {
            vectorFunciones = new double[]{
                Math.Pow(x,2) - 10 * x + Math.Pow(y,2) + 8,
                x * Math.Pow(y,2) + x - 10 * y + 8
            };
        }

        public static void MatrizJacobiana (double x, double y) {
            matrizDerivadas = new double[,]{
                {2 * x - 10, 2 * y},
                {Math.Pow(y, 2) + 1, 2 * x * y - 10}
            };
        }

        public static void LlenadoMatrizDeIdentidad () {
            matrizDeIdentidad = new double[,]{
                {1,0},
                {0,1}
            };
        }

        public static void LlenadoVectorInicio () {
            vectorInicial = new double[2];
            
            for (i = 0; i < vectorInicial.GetLength(0); i++){
                Console.WriteLine("Ingrese el vector inicial: " + (i+1));
                vectorInicial[i] = double.Parse(Console.ReadLine());
            }
        }

        public static void MatrizInversa () {
            VectorDeFunciones(x,y);
            MatrizJacobiana(x,y);
    
            LlenadoMatrizDeIdentidad();

            for (i = 0; i < matrizDerivadas.GetLength(0); i++){
                pivote = matrizDerivadas[i,i];

                for (b = 0; b < matrizDerivadas.GetLength(0); b++){
                    matrizDerivadas[i,b] = matrizDerivadas[i,b] / pivote;
                    matrizDeIdentidad[i,b] = matrizDeIdentidad[i,b] / pivote;
                }

                for (int j = 0; j < matrizDerivadas.GetLength(0); j++){

                    if (i != j){
                        valorAuxiliar = matrizDerivadas[j,i];

                        for (b = 0; b < matrizDerivadas.GetLength(0); b++){
                            matrizDerivadas[j,b] = matrizDerivadas[j,b] - valorAuxiliar * matrizDerivadas[i,b];
                            matrizDeIdentidad[j,b] = matrizDeIdentidad[j,b] - valorAuxiliar * matrizDeIdentidad[i,b];
                        }
                    }  
                }
            }
        }

        /*public static void MultiplicacionMatrizInversaConFuncione (){
            MatrizInversa();
            //double variableVectorMultiplicacion = 0.0;

            vectorMultiplicacion = new double[2];

            for (i = 0; i < matrizDeIdentidad.GetLength(0); i++) {
                for (int j = 0; j < vectorMultiplicacion.GetLength(0); j++) {
                    // aquí se multiplica la matriz
                    vectorMultiplicacion[i] += matrizDeIdentidad[i,j] * vectorFunciones[j];
                }
            }
        }*/

        /*public static void RestaInicialConMultiplicacion () {
            MultiplicacionMatrizInversaConFuncione();

            vectorResta = new double[2];

            for(i = 0; i < vectorResta.GetLength(0); i++){
                vectorResta[i] = vectorInicial[i] - vectorMultiplicacion[i];
            }
        }*/

        public static void ImprimirMatriz (double [,]tipoMatriz) {
            for (i = 0; i < tipoMatriz.GetLength(0); i++){
                for (j = 0; j < tipoMatriz.GetLength(1); j++){
                    Console.Write(tipoMatriz[i,j] + "  ");
                }
                Console.WriteLine();
            }
        }

        public static void ImprimirVector (double []tipoVector) {
            for (i = 0; i < tipoVector.GetLength(0); i++){
                Console.WriteLine(tipoVector[i] + " ");
            }
        }

        public static void NewtonRaphson (int iteracion, double tolerancia) {
            vectorMultiplicacion = new double[2];
            vectorResta = new double[2];

            double error = 1;
            double x2 = 0.0, y2 = 0.0;

            k = 0;

            while (k < iteracion && error > tolerancia) {
                Console.WriteLine();
                Console.WriteLine("Iteracion " + (k+1));
                VectorDeFunciones(x, y);
                MatrizJacobiana(x,y);
                MatrizInversa();

                x2 = x;
                y2 = y;

                for (i = 0; i < matrizDeIdentidad.GetLength(0); i++) {
                    for (int j = 0; j < vectorMultiplicacion.GetLength(0); j++) {
                        // aquí se multiplica la matriz
                        vectorMultiplicacion[i] += matrizDeIdentidad[i,j] * vectorFunciones[j];
                    }
                }

                for(i = 0; i < vectorResta.GetLength(0); i++){
                    vectorResta[i] = vectorInicial[i] - vectorMultiplicacion[i];
                    Console.WriteLine(vectorResta[i] + " ");
                }

                x =vectorResta[0];
                y = vectorResta[1];

                error = Math.Sqrt(Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2));

                Console.WriteLine("Error: " + error);

                k++;
            }
            
        }

        static void Main(string[] args){
            int iteracion = 0;
            double tol = 0.01;;
            bool bandera = true;
            int opcion;

            while (bandera) {
                Console.WriteLine("------------ F U N C I O N E S ------------");
                Console.WriteLine("Funcion 1: x^2 - 10x + y^2 + 8" + "\nFuncion 2: xy^2 + x - 10y + 8");
                Console.WriteLine("------------ D E R I V A D A S ------------");
                Console.WriteLine("Derivada 1: 2x - 10, 2y" + "\nDerivada 2: y^2 + 1, 2xy - 10");
                Console.WriteLine();
                Console.WriteLine("1.- Ingreso de datos.");
                Console.WriteLine("2.- Newton-Raphson.");
                Console.WriteLine("3.- Resultado final.");
                Console.WriteLine("4.- Salir.");
                
                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    LlenadoVectorInicio();
                    Console.WriteLine("Ingrese el numero de iteraciones: ");
                    iteracion = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese la tolerancia: ");
                    tol = double.Parse(Console.ReadLine());
                }else if (opcion == 2)
                    NewtonRaphson(iteracion, tol);
                else if (opcion == 3){
                    Console.WriteLine("------ RESULTADO FINAL ------");
                    ImprimirVector(vectorResta);
                }else if (opcion == 4){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else
                    Console.WriteLine("Opcion invalida.");

            }
        }
    }
}