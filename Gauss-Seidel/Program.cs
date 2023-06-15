using System;

namespace Gauss_Seidel{
    class Program{
        static double [,] matrizDeCoeficientes;
        static double [] vectorDeResultados;
        static double [] vectorInicioAuxiliar;
        static double [] vectorDeInicio;
        static int numeroEcuaciones=0;
        static int i, j, k, l, contador;
        static double sumaValoresFila=0, valoresDiagonal=0;
        static double suma, auxiliarMatriz, auxiliarVector;
    
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

        static void CambioValoresVectorInicioAuxiliar() {
            vectorInicioAuxiliar= new double[numeroEcuaciones];
            for (i = 0; i < numeroEcuaciones; i++){
                vectorInicioAuxiliar[i] = vectorDeInicio[i];
            }
        }

        static void LlenadoVectorInicio () {
            vectorDeInicio = new double[numeroEcuaciones];

            for (l = 0; l < numeroEcuaciones; l++){
                Console.WriteLine("Ingrese el inicio " + l);
                vectorDeInicio[l] = int.Parse(Console.ReadLine());
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

        static void ImprimirVector( double [] vector ) {
            for (i = 0; i < numeroEcuaciones; i++){
                Console.WriteLine(vector[i] + " ");
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
                  //  for (int n = 0; n < numeroEcuaciones; n++){
                        auxiliarVector = vectorDeResultados[i];
                        vectorDeResultados[i] = vectorDeResultados[columnaDelValorMaximo];
                        vectorDeResultados[columnaDelValorMaximo] = auxiliarVector;
                    //}
                //}
            }
        }

        static double SumatoriaDeP(int i){
            suma=0;

            for(j=0; j<numeroEcuaciones; j++){
                suma += matrizDeCoeficientes[i,j] * vectorInicioAuxiliar[j];
            }
        return suma;
        }

        static void GaussSeidel(int iteraciones, double cantidadError){
            CambioValoresVectorInicioAuxiliar();

            double Pold = 0.0;
            double P = 0.0;
            double error = 100.0;
            //bool banderaCiclo = true;

            k=0;
            while(k < iteraciones && error > cantidadError){
                Console.WriteLine("Iteracion numero " + (k+1));
                Pold = P;
                Console.WriteLine("Valor anterior: " + Pold);
                for(i=0; i<numeroEcuaciones; i++){
                    vectorInicioAuxiliar[i] = (1/matrizDeCoeficientes[i,i]) * (vectorDeResultados[i] + matrizDeCoeficientes[i,i] * vectorInicioAuxiliar[i] - SumatoriaDeP(i));
                    Console.WriteLine("X" + (i+1) + " ---> " + vectorInicioAuxiliar[i]);
                    P = vectorInicioAuxiliar[i];
                }

            error = Math.Sqrt((P-Pold)*(P-Pold));
            Console.WriteLine("ERROR   " + error);
            k++;

            /*if(k > iteraciones || cantidadError > error){
                banderaCiclo=false;
            }*/
           
            }
            error = 100.0;
        }
    

        static void Main(string[] args){
            //double tolerancia = 0;
            int iteracionesTotales=0;
            double epsilon = 0.001;
            int opcion=0;
            bool bandera = true;
            int count=0;

            while (bandera){
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- Gauss-Seidel.");
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
                        epsilon = double.Parse(Console.ReadLine());
                        GaussSeidel(iteracionesTotales, epsilon);
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
                        //CambioValoresVectorInicioAuxiliar();
                        ImprimirVector(vectorInicioAuxiliar);
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
