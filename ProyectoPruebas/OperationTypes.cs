using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class OperationTypes {

        private static int NUMBER_OF_TYPES = 5;
        private static int NUMBER_OF_OPERATORS = 10;

        //Valores de tipo de dato
        public static int TYPE_UNDEFINED = 0;
        public static int TYPE_INT = 1;
        public static int TYPE_FLOAT = 2;
        public static int TYPE_BOOL = 3;
        public static int TYPE_STRING = 4;

        //Valores de operadores
        public static int PLUS = 0;
        public static int MINUS = 1;
        public static int MULTIPLICATION = 2;
        public static int DIVISION = 3;
        public static int GREATER_THAN = 4;
        public static int LESS_THAN = 5;
        public static int EQUAL = 6;
        public static int GREATER_OR_EQUAL = 7;
        public static int LESS_OR_EQUAL = 8;
        public static int DIFFERENT_THAN = 9;

        public int[,,] semanticCube = new int[NUMBER_OF_TYPES, NUMBER_OF_TYPES, NUMBER_OF_OPERATORS];

        public OperationTypes() {

            //Se inicializa el cubo con valores por defecto TYPE_UNDEFINED
            for (int i = 0; i < NUMBER_OF_TYPES; i++) {
                for (int j = 0; j < NUMBER_OF_TYPES; j++) {
                    for (int k = 0; k < NUMBER_OF_OPERATORS; k++) {
                        semanticCube[i, j, k] = TYPE_UNDEFINED;
                    }
                }
            }

            //Operaciones entre enteros
            semanticCube[TYPE_INT, TYPE_INT, PLUS] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, MINUS] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, MULTIPLICATION] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, DIVISION] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, GREATER_THAN] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, LESS_THAN] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, GREATER_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, LESS_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre floats
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, PLUS] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, MINUS] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, MULTIPLICATION] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, DIVISION] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, GREATER_THAN] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, LESS_THAN] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, GREATER_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, LESS_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre bools
            semanticCube[TYPE_BOOL, TYPE_BOOL, GREATER_THAN] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, LESS_THAN] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, GREATER_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, LESS_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre strings
            semanticCube[TYPE_STRING, TYPE_STRING, PLUS] = TYPE_STRING; //Concatenación
            semanticCube[TYPE_STRING, TYPE_STRING, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_STRING, TYPE_STRING, DIFFERENT_THAN] = TYPE_BOOL;

            //Resultado entre distintos tipos de dato para la suma
            semanticCube[TYPE_INT, TYPE_FLOAT, PLUS] =  TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_INT, PLUS] =  TYPE_FLOAT;
            semanticCube[TYPE_STRING, TYPE_INT, PLUS] =  TYPE_STRING;
            semanticCube[TYPE_INT, TYPE_STRING, PLUS] =  TYPE_STRING;
            semanticCube[TYPE_STRING, TYPE_FLOAT, PLUS] =  TYPE_STRING;
            semanticCube[TYPE_FLOAT, TYPE_STRING, PLUS] =  TYPE_STRING;

            //Resultado entre distintos tipos de dato para la resta
            semanticCube[TYPE_INT, TYPE_FLOAT, MINUS] =  TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_INT, MINUS] =  TYPE_FLOAT;
            
            //Resultado entre distintos tipos de dato para la multiplicación
            semanticCube[TYPE_INT, TYPE_FLOAT, MULTIPLICATION] =  TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_INT, MULTIPLICATION] =  TYPE_FLOAT;

            //Resultado entre distintos tipos de dato para la división
            semanticCube[TYPE_INT, TYPE_FLOAT, DIVISION] =  TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_INT, DIVISION] =  TYPE_FLOAT;
            
            //Resultado entre distintos tipos de dato para las comparaciones
            semanticCube[TYPE_INT, TYPE_FLOAT, GREATER_THAN] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, GREATER_THAN] =  TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_FLOAT, LESS_THAN] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, LESS_THAN] =  TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_FLOAT, EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_FLOAT, GREATER_OR_EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, GREATER_OR_EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_FLOAT, LESS_OR_EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, LESS_OR_EQUAL] =  TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_FLOAT, DIFFERENT_THAN] =  TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_INT, DIFFERENT_THAN] =  TYPE_BOOL;

        }
    }
}
