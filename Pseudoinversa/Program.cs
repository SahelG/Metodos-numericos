using System;

namespace Pseudoinversa{
    class Program{
        static int numeroEcuaciones, numeroColumnas, funcion;
        static int i, j, p, b;
        static double pivote = 0.0, valorAuxiliar = 0.0;
        static double [,] matrizA;
        static double [,] matrizNueva;
        static double [,] matrizTranspuesta;
        static double [,] auxiliarMultiplicacionTranspuestaXA;
        static double [,] matrizDeIdentidad;
        static double [,] auxiliarMultiplicaionInversaXTranspuesta;
        static double [] vectorY;
        static double [] resultadoFinal;

        public static void LlenadoMatrizA () {
            numeroColumnas = 2;
            matrizA = new double [numeroEcuaciones, numeroColumnas];

            for (i = 0; i < numeroEcuaciones; i++){
                for (j = 0; j < numeroColumnas; j++){
                    matrizA[i,j] = 1;

                    if (j > 0){
                        Console.WriteLine("Ingrese el valor de X" + (i+1) + " : ");
                        matrizA[i,j] = double.Parse(Console.ReadLine());
                    }
                }
            }
        }

        public static void LlenadoVectorY () {
            vectorY = new double [numeroEcuaciones];

            for (i = 0; i < numeroEcuaciones; i++) {
                Console.WriteLine("Ingrese el valor de Y" + (i+1) +  " : ");
                vectorY[i] = double.Parse(Console.ReadLine());

            }
        }

        public static void Juntar (int funcion, double [,]matrizA) {
            if (funcion == 1){
                numeroColumnas = matrizA.GetLength(1);
            }else if (funcion == 2){
                numeroColumnas = matrizA.GetLength(1) + 1;
            }else if (funcion == 3 || funcion == 4) {
                numeroColumnas = matrizA.GetLength(1) * 2;
            }

            matrizNueva = new double [numeroEcuaciones, numeroColumnas];

            for (i = 0; i < numeroEcuaciones; i++) {
                for (j = 0; j < numeroColumnas; j++){
                    matrizNueva[i,0] = matrizA[i,0];
                    matrizNueva[i,1] = matrizA[i,1];

                    if (funcion == 2)
                        matrizNueva[i,2] = matrizA[i,1] * matrizA[i,1];
                    else if (funcion == 3) {
                        matrizNueva[i,2] = matrizA[i,1] * matrizA[i,1];
                        matrizNueva[i,3] = matrizA[i,1] * matrizA[i,1] * matrizA[i,1];
                    }else if (funcion == 4){
                        matrizNueva[i,2] = Math.Sin(matrizA[i,1]);
                        matrizNueva[i,3] = Math.Exp(matrizA[i,1]);
                    }
                }
            }
        }

        public static void LaTranspuesta (double [,]matrizA) {
            matrizTranspuesta = new double [numeroColumnas, numeroEcuaciones];

            for (i = 0; i < matrizNueva.GetLength(0); i++){
                for (j = 0; j < matrizNueva.GetLength(1); j++){
                    matrizTranspuesta[j,i] = matrizNueva[i,j];
                }
            }
        }

        public static void MultiplicacionTranspuestaPorA () {
            int filaMatrizTranspuesta = matrizTranspuesta.GetLength(0);
            int columnaMatrizTranspuesta = matrizTranspuesta.GetLength(1);
            int filaMatrizA = matrizNueva.GetLength(0);
            int columnaMatrizA = matrizNueva.GetLength(1);

            auxiliarMultiplicacionTranspuestaXA = new double[filaMatrizTranspuesta, columnaMatrizA];

            if (columnaMatrizTranspuesta == filaMatrizA){
                for (i = 0; i < filaMatrizTranspuesta; i++) {
                    for (j = 0; j < columnaMatrizA; j++) {
                        double a = 0;
                        for (p = 0; p < columnaMatrizTranspuesta; p++) {
                            a = a + matrizTranspuesta[i,p] * matrizNueva[p,j];
                        }
                        auxiliarMultiplicacionTranspuestaXA[i,j] = a;
                    }
                }
            }
        }

        public static void LlenadoMatrizIdentidad (double [,]auxiliarMultiplicacionTranspuestaXA) {
            //MultiplicacionTranspuestaPorA();

            matrizDeIdentidad = new double [auxiliarMultiplicacionTranspuestaXA.GetLength(0), auxiliarMultiplicacionTranspuestaXA.GetLength(1)];

            for (i = 0; i < matrizDeIdentidad.GetLength(0); i++){
                for (j = 0; j < matrizDeIdentidad.GetLength(1); j++){
                    matrizDeIdentidad[i,j] = 0;

                    if(i == j) {
                        matrizDeIdentidad[i,j] = 1;
                    }                    
                }
            }
        }

        public static void InversaDeLaMultiplicacion () {
            //LlenadoMatrizIdentidad(auxiliarMultiplicacionTranspuestaXA);

            int fila = auxiliarMultiplicacionTranspuestaXA.GetLength(0);
            int columna = auxiliarMultiplicacionTranspuestaXA.GetLength(1);

            for (i = 0; i < fila; i++){
                pivote = auxiliarMultiplicacionTranspuestaXA[i,i];

                for (b = 0; b < columna; b++){
                    auxiliarMultiplicacionTranspuestaXA[i,b] = auxiliarMultiplicacionTranspuestaXA[i,b] / pivote;
                    matrizDeIdentidad[i,b] = matrizDeIdentidad[i,b] / pivote;
                }

                for (j = 0; j < columna; j++){

                    if (i != j){
                        valorAuxiliar = auxiliarMultiplicacionTranspuestaXA[j,i];

                        for (b = 0; b < columna; b++){
                            auxiliarMultiplicacionTranspuestaXA[j,b] = auxiliarMultiplicacionTranspuestaXA[j,b] - valorAuxiliar * auxiliarMultiplicacionTranspuestaXA[i,b];
                            matrizDeIdentidad[j,b] = matrizDeIdentidad[j,b] - valorAuxiliar * matrizDeIdentidad[i,b];
                        }
                    }  
                }
            }
        }

        public static void MultiplicacionInversaPorTranspuesta () {
            //InversaDeLaMultiplicacion();

            int filaMatrizInversa = matrizDeIdentidad.GetLength(0);
            int columnaMatrizInversa = matrizDeIdentidad.GetLength(1);
            int filaMatrizTranspuesta = matrizTranspuesta.GetLength(0);
            int columnaMatrizTranspuesta = matrizTranspuesta.GetLength(1);

            auxiliarMultiplicaionInversaXTranspuesta = new double[filaMatrizInversa, columnaMatrizTranspuesta];

            if (columnaMatrizInversa == filaMatrizTranspuesta){
                for (i = 0; i < filaMatrizInversa; i++) {
                    for (j = 0; j < columnaMatrizTranspuesta; j++) {
                        double a = 0;
                        for (p = 0; p < columnaMatrizInversa; p++) {
                            a = a + matrizDeIdentidad[i,p] * matrizTranspuesta[p,j];
                        }
                        auxiliarMultiplicaionInversaXTranspuesta[i,j] = a;
                    }
                }
            }
        }

        public static void PseudoInversa () {
            Juntar(funcion, matrizA);
            LaTranspuesta(matrizA);
            MultiplicacionTranspuestaPorA();
            LlenadoMatrizIdentidad (auxiliarMultiplicacionTranspuestaXA);
            InversaDeLaMultiplicacion();
            MultiplicacionInversaPorTranspuesta();

            int fila = auxiliarMultiplicaionInversaXTranspuesta.GetLength(0);
            resultadoFinal = new double[fila];

            for (i = 0; i < fila; i++) {
                for (j = 0; j < vectorY.GetLength(0); j++) {
                    // aquí se multiplica la matriz
                    resultadoFinal[i] += auxiliarMultiplicaionInversaXTranspuesta[i,j] * vectorY[j];
                }
            }
            //Imprimir ecuacion
            Console.WriteLine();
            ImprimirVector(resultadoFinal);
        }

        public static void Comprobar (int funcion, double valorDeX) {
            double resultadoOperacion = 0.0;
            
            if (funcion == 1)
                resultadoOperacion = resultadoFinal[0] + resultadoFinal[1] * valorDeX;
            else if (funcion == 2) 
                resultadoOperacion = resultadoFinal[0] + resultadoFinal[1] * valorDeX + resultadoFinal[2] * valorDeX * valorDeX;
            else if (funcion == 3) 
                resultadoOperacion = resultadoFinal[0] + resultadoFinal[1] * valorDeX + resultadoFinal[2] * valorDeX * valorDeX + resultadoFinal[3] * valorDeX * valorDeX * valorDeX;
            else if (funcion == 4)
                resultadoOperacion = resultadoFinal[0] + resultadoFinal[1] * valorDeX + resultadoFinal[2] * Math.Sin(valorDeX) + resultadoFinal[3] * Math.Exp(valorDeX);
            
            Console.WriteLine("y: " + resultadoOperacion);
        }

        public static void ImprimirM (double [,]tipoMatriz) {
            for (i = 0; i < tipoMatriz.GetLength(0); i++){
                for (j = 0; j < tipoMatriz.GetLength(1); j++){
                    Console.Write(tipoMatriz[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void ImprimirVector (double []tipoVector) {
            String signo = "  ";
            if (funcion == 1 || funcion == 2 || funcion == 3) {
                for (i = tipoVector.GetLength(0)-1; i >= 0; i--){
                    if (i == tipoVector.GetLength(0)-1) {
                        signo = "";
                        Console.Write(signo + "  ");
                    }else if(tipoVector[i] > 0) {
                        signo = "+";
                        Console.Write(signo + "  ");
                    }

                    if (i == 0) 
                        Console.Write(tipoVector[i] + "  ");
                    else
                        Console.Write(tipoVector[i] + "x^" + i + "  ");
                }
            }else if (funcion == 4){
                for (i = tipoVector.GetLength(0)-1; i >= 0; i--){
                    if (i == tipoVector.GetLength(0)-1) {
                        signo = "";
                        Console.Write(signo + "  ");
                    }else if(tipoVector[i] > 0) {
                        signo = "+";
                        Console.Write(signo + "  ");
                    }

                    if (i == 0) 
                        Console.Write(tipoVector[i] + "  ");
                    else if (i == 1)
                        Console.Write(tipoVector[i] + "x  ");
                    else if (i == 2)
                        Console.Write(tipoVector[i] + "sen(x)  ");
                    else if (i == 3)
                        Console.Write(tipoVector[i] + "exp(x)  ");
                }
            }
        }



        static void Main(string[] args){
            int opcion = 0;
            bool bandera = true;
            double valorDeX = 0.0;

            while (bandera){
                Console.WriteLine("\n------------------ M E N U ------------------");
                Console.WriteLine("1.- Llenar datos.");
                Console.WriteLine("2.- a + bx");
                Console.WriteLine("3.- a + bx + cx^2");
                Console.WriteLine("4.- a + bx + cx^2 + dx^3");
                Console.WriteLine("5.- a + bx + c*sen(x) + d*exp(x)");
                Console.WriteLine("6.- Comprobar.");
                Console.WriteLine("7.- Salir.");

                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1){
                    Console.WriteLine("Ingrese el numero de valores: ");
                    numeroEcuaciones = int.Parse(Console.ReadLine());
                    LlenadoMatrizA();
                    Console.WriteLine("----------------------------------------");
                    LlenadoVectorY();
                }else if (opcion == 2){
                    funcion = 1;
                    PseudoInversa();
                }else if (opcion == 3){
                    funcion = 2;
                    PseudoInversa();
                }else if (opcion == 4){
                    funcion = 3;
                    PseudoInversa();
                }else if (opcion == 5) {
                    funcion = 4;
                    PseudoInversa();
                }else if (opcion == 6){
                    Console.WriteLine("Ingrese el valor de X: ");
                    valorDeX = double.Parse(Console.ReadLine());
                    Comprobar(funcion, valorDeX);
                }else if (opcion == 7){
                    Console.WriteLine("Hasta luego.");
                    bandera = false;
                }else{
                    Console.WriteLine("Opcion no valida.");
                }
            }       
        }
    }
}
