using System;

namespace Gauss_Jacobi{
    class Program{
        static double [,] matrizDeCoeficientes;
        static double [,] auxiliarMatrizDiagonal;
        static double [,] auxiliarMatrizInferior;
        static double [,] auxiliarMatrizSuperior;
        static double [,] resultadoRestaMatriz;
        static double [,] resultadoMultiplicacion;
        static double [] resultadoMultiplicacionIB;
        static double [] resultadoMultiplicacionIAVI;
        static double [,] inversaDiagonal;
        static double [] vectorDeResultados;
        static double [] vectorDeInicio;
        static int i, j, k, l;
        static double sumaValoresFila=0, valoresDiagonal=0, auxiliarMatriz, auxiliarVector;
        static int numeroEcuaciones=0, contador;
        static double P;
        static double Pold;
        static double error;

        static void LlenadoMatriz () {
            matrizDeCoeficientes = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    Console.WriteLine("Ingrese el coeficiente " + i + "," + j);
                    matrizDeCoeficientes[i,j] = double.Parse(Console.ReadLine());
                }
            }
        }

        static void LlenadoVectorResultados () {
            vectorDeResultados = new double[numeroEcuaciones];

            for (k = 0; k < numeroEcuaciones; k++){
                Console.WriteLine("Ingrese el resultado " + k);
                vectorDeResultados[k] = double.Parse(Console.ReadLine());
            }
        }

        static void LlenadoVectorInicio () {
            vectorDeInicio = new double[numeroEcuaciones];

            for (l = 0; l < numeroEcuaciones; l++){
                Console.WriteLine("Ingrese el inicio " + l);
                vectorDeInicio[l] = double.Parse(Console.ReadLine());
            }
        }

        static void ImprimirMatriz () {
            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    Console.Write(matrizDeCoeficientes[i,j] + " ");
                }
                Console.WriteLine();
            }  
        }

        static void ImprimirVector (double [] tipoVector) {
            for (k = 0; k < numeroEcuaciones; k++){
                Console.WriteLine(tipoVector[k] + " ");
            }  
        }

        static bool MatrizDominante (double [,]matrizDeCoeficientes, int numeroEcuaciones){
            CambiarFilas();
            bool resultado = false;
            contador = 0;

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    if (i != j)
                        sumaValoresFila += Math.Abs(matrizDeCoeficientes[i,j]);
                    else{
                        valoresDiagonal = Math.Abs(matrizDeCoeficientes[i,j]);
                    }
                }

                if (valoresDiagonal >= sumaValoresFila){
                    contador++;
                }

                sumaValoresFila = 0;
            }


            if (contador == matrizDeCoeficientes.GetLength(0)){
                resultado = true;
            }else{
                resultado = false;
            }

            return resultado;
        }

        static void CambiarFilas () {
            double valorMaximo = 0;
            int columnaDelValorMaximo = 0;

            for (i = 0; i < numeroEcuaciones; i++){
                valorMaximo = Math.Abs(matrizDeCoeficientes[i,i]);
                columnaDelValorMaximo = i;

                for (j = 0; j < numeroEcuaciones; j++){
                    if (valorMaximo < Math.Abs(matrizDeCoeficientes[i,j])){
                        valorMaximo = Math.Abs(matrizDeCoeficientes[i,j]);
                        columnaDelValorMaximo = j;
                    }    
                }

                if(i != columnaDelValorMaximo){
                    for(int m=0; m < numeroEcuaciones; m++){
                        auxiliarMatriz = matrizDeCoeficientes[i,m];
                        matrizDeCoeficientes[i,m] = matrizDeCoeficientes[columnaDelValorMaximo,m];
                        matrizDeCoeficientes[columnaDelValorMaximo,m] = auxiliarMatriz;
                    }
                }

                //if (i != columnaDelValorMaximo){
                    //for (int n = 0; n < numeroEcuaciones; n++){
                        auxiliarVector = vectorDeResultados[i];
                        vectorDeResultados[i] = vectorDeResultados[columnaDelValorMaximo];
                        vectorDeResultados[columnaDelValorMaximo] = auxiliarVector;
                    //}
                //}
            }
        }

        static void MatrizDiagonal () {
            
            auxiliarMatrizDiagonal = new double[numeroEcuaciones, numeroEcuaciones];
            inversaDiagonal = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    if(i == j) {
                        auxiliarMatrizDiagonal[i,j] = matrizDeCoeficientes[i,j];
                        inversaDiagonal[i,j] = 1.0/matrizDeCoeficientes[i,j];   
                    }else{
                        auxiliarMatrizDiagonal[i,j] = 0;
                        inversaDiagonal[i,j] = auxiliarMatrizDiagonal[i,j];
                    }   
                }
            }
        }

        static void MatrizSuperior () {
            
            auxiliarMatrizSuperior = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    if(i == j || i <= j) {
                        auxiliarMatrizSuperior[i,j] = matrizDeCoeficientes[i,j];
                    }else{
                        auxiliarMatrizSuperior[i,j] = 0;
                    }   
                }
            }
        }

        static void MatrizInferior () {
            
            auxiliarMatrizInferior = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    if(i == j || i >= j) {
                        auxiliarMatrizInferior[i,j] = matrizDeCoeficientes[i,j];
                    }else{
                        auxiliarMatrizInferior[i,j] = 0;
                    }   
                }
            }
        }

        static void RestarDiagonalAMatrizCoeficientes () {
            MatrizDiagonal();
            MatrizInferior();
            MatrizSuperior();

            resultadoRestaMatriz = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroEcuaciones; j++){
                    resultadoRestaMatriz[i,j] = matrizDeCoeficientes[i,j] - auxiliarMatrizDiagonal[i,j];
                }
            }
        }

        static void MultiplicacionInversaPorA () {
            //double suma=0;
            resultadoMultiplicacion = new double[numeroEcuaciones, numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++) {
                for (j = 0; j < numeroEcuaciones; j++) {
                    for (int p = 0; p < numeroEcuaciones; p++) {
                    // aquí se multiplica la matriz
                        resultadoMultiplicacion[i,j] += -1*(inversaDiagonal[i,p] * resultadoRestaMatriz[p,j]);
                    }
                }
            }
        }

        static void MultiplicacionInversaPorB () {
            double suma = 0;

            resultadoMultiplicacionIB = new double[numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++) {
                suma=0;
                for (j = 0; j < numeroEcuaciones; j++) {
                    // aquí se multiplica la matriz
                    suma += inversaDiagonal[i,j] * vectorDeResultados[j];
                }
                resultadoMultiplicacionIB[i] = suma;
            }
        }

        static void GaussJacobi () {
            RestarDiagonalAMatrizCoeficientes();
            MultiplicacionInversaPorA();
            MultiplicacionInversaPorB();

            resultadoMultiplicacionIAVI = new double[numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++) {
                for (j = 0; j < numeroEcuaciones; j++) {
                    // aquí se multiplica la matriz
                    resultadoMultiplicacionIAVI[i] += resultadoMultiplicacion[i,j] * vectorDeInicio[j];
                }
            }

            for (i = 0; i < numeroEcuaciones; i++){
                vectorDeInicio[i] = resultadoMultiplicacionIB[i] + resultadoMultiplicacionIAVI[i];
                Console.WriteLine("X" + i + " --> " + vectorDeInicio[i]);
                P = vectorDeInicio[i];
            }

            error = Math.Sqrt((P-Pold)*(P-Pold));
            //error = 100*Math.Abs((P - Pold) / P);
            Pold = P;
            Console.WriteLine("Error:   " + error);

        }


        static void Main(string[] args){
            double tolerancia = 0.01;
            int iteracionesTotales=0;
            int opcion=0;
            bool bandera = true;
            int count=0;
            int h;
            error = 100;
            //bool banderaCiclo = true; 

            while (bandera){
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- Gauss-Jacobi.");
                Console.WriteLine("3.- Imprimir resultados.");
                Console.WriteLine("4.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el numero de ecuaciones: ");
                    numeroEcuaciones = int.Parse(Console.ReadLine());

                    LlenadoMatriz();
                    Console.WriteLine("----------------------------------------");
                    LlenadoVectorResultados();
                    Console.WriteLine("----------------------------------------");
                    LlenadoVectorInicio();
                }else if (opcion == 2){
                    if (MatrizDominante(matrizDeCoeficientes, numeroEcuaciones)){
                        Console.WriteLine("La matriz SI TIENE diagonal dominante");
                        Console.WriteLine("----------------------------------------");

                        Console.WriteLine("Ingrese el numero de iteraciones: ");
                        iteracionesTotales = int.Parse(Console.ReadLine());

                        Console.WriteLine("Ingrese la tolerancia: ");
                        tolerancia = double.Parse(Console.ReadLine());

                        h=0;
                        while(k<iteracionesTotales && error > tolerancia){
                            Console.WriteLine("Iteracion " + (h+1));
                            GaussJacobi();
                            /*if(k>iteracionesTotales || tolerancia>error){
                                banderaCiclo=false;
                            }*/
                        h++;
                        }
                        error = 100;
                    }else{
                        Console.WriteLine("La matriz NO TIENE diagonal dominante.");
                        Console.WriteLine("Ingresar otro sistema de ecuaciones.");
                        count = 1;
                    }
                }else if (opcion == 3){
                    if (count == 1){
                        Console.WriteLine("Ingresar otro sistema de ecuaciones.");
                        count = 0;
                    }else{
                        Console.WriteLine("----- MATRIZ: -----");
                        ImprimirMatriz();
                        Console.WriteLine("----- VECTOR CON RESULTADOS: -----");
                        ImprimirVector(vectorDeInicio);
                        Console.WriteLine("----- VECTOR DE RESULTADOS: -----");
                        ImprimirVector(vectorDeResultados);
                    }
                    
                }else if (opcion == 4){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else{
                    Console.WriteLine("Opcion no valida.");
                }
            }
        }
    }
}
