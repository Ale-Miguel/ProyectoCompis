using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {

   enum Operation { OP_UNDEF, EQUAL, PLUS, MINUS, MULTIPLICATION, DIVISION, GREATERR_THAN, LESS_THAN, EQUAL_THAN, 
        GREATER_THAN, _LESS_THAN_, _EQUAL_THAN_, GREATER_OR_EQUAL, LESS_OR_EQUAL, DIFFERENT_THAN}
    class OperationTypes {

        private const int NUMBER_OF_TYPES = 5;         //Constante  que define  la cantidad de tipos de dato
        private const int NUMBER_OF_OPERATORS = 12;    //Constante que define la cantidad de operadores soportados

        //Valores de tipo de dato
        public const int TYPE_UNDEFINED = 0;
        public const int TYPE_INT = 1;
        public const int TYPE_FLOAT = 2;
        public const int TYPE_BOOL = 3;
        public const int TYPE_STRING = 4;

        //Valores de operadores
        public const int OP_UNDEF = 0;         //Operador No reconocido
        public const int EQUAL = 1;            // =
        public const int PLUS = 2;             // +
        public const int MINUS = 3;            // -
        public const int MULTIPLICATION = 4;   // *
        public const int DIVISION = 5;         // /
        public const int GREATER_THAN = 6;     // >
        public const int LESS_THAN = 7;        // <
        public const int EQUAL_THAN = 8;       // ==
        public const int GREATER_OR_EQUAL = 9; // >=
        public const int LESS_OR_EQUAL = 10;    // <=
        public const int DIFFERENT_THAN = 11;   // <>

        //Códigos para palabras reservadas
        public const int PRINT = 12;
        public const int MOVE_FORWARD = 13;
        

        public int[,,] semanticCube = new int[NUMBER_OF_TYPES, NUMBER_OF_TYPES, NUMBER_OF_OPERATORS];

        //Función que sirve de traductor entre los números que asigna el parser a los tokens de tipos y operadores
        //Con las posiciones en el cubo semántico
        public int getCubePosition(int operatorValue) {

            Operation op;

            op = (Operation)1;

            switch (operatorValue) {

                //Tipos de dato

                //Entero
                case Parser._Int:
                    return TYPE_INT;

                case Parser._CTE_I:
                    return TYPE_INT;

                //Flotante
                case Parser._Float:
                    return TYPE_FLOAT;

                case Parser._CTE_F:
                    return TYPE_FLOAT;

                ///Boleano
                case Parser._Bool:
                    return TYPE_BOOL;

                //String
                case Parser._String:
                    return TYPE_STRING;

                case Parser._CTE_S:
                    return TYPE_STRING;

                //Operadores

                //Suma
                case Parser._Plus:
                    return PLUS;

                //Resta
                case Parser._Minus:
                    return MINUS;

                //Multiplicación
                case Parser._Asterisk:
                    return MULTIPLICATION;

                //División
                case Parser._Slash:
                    return DIVISION;

                //Mayor que
                case Parser._GreaterThan:
                    return GREATER_THAN;

                //Mayor o igual
                case Parser._GreaterThanOrEqual:
                    return GREATER_OR_EQUAL;
                //Menor que
                case Parser._LesserThan:
                    return LESS_THAN;
                
                //Menor o igual
                case Parser._LessThanOrEqual:
                    return LESS_OR_EQUAL;
                
                //Igual (asignación)
                case Parser._Equal:
                    return EQUAL;
                
                case Parser._EqualThan:
                    return EQUAL_THAN;
                //Diferente
                case Parser._NotEqual:
                    return DIFFERENT_THAN;
            }
            //En caso de que se haya ingresado un valor que no sea ni tipo de dato ni operador, se regresa undefined
            return TYPE_UNDEFINED;
        }

        //Función que regresa el tipo de dato del resultado de una operación según el cubo semántico
        public int getOperationResult(int value1, int value2, int operatorValue) {
            //int aux1 = getCubePosition(value1);
            //int aux2 = getCubePosition(value2);
            //int aux3 = getCubePosition(operatorValue);

            return semanticCube[value1,value2, operatorValue];
        }
       
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
            semanticCube[TYPE_INT, TYPE_INT, EQUAL] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, PLUS] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, MINUS] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, MULTIPLICATION] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, DIVISION] = TYPE_INT;
            semanticCube[TYPE_INT, TYPE_INT, GREATER_THAN] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, LESS_THAN] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, GREATER_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, LESS_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, EQUAL_THAN] = TYPE_BOOL;
            semanticCube[TYPE_INT, TYPE_INT, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre floats
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, EQUAL] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, PLUS] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, MINUS] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, MULTIPLICATION] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, DIVISION] = TYPE_FLOAT;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, GREATER_THAN] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, LESS_THAN] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, GREATER_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, LESS_OR_EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, EQUAL_THAN] = TYPE_BOOL;
            semanticCube[TYPE_FLOAT, TYPE_FLOAT, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre bools
            semanticCube[TYPE_BOOL, TYPE_BOOL, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, EQUAL_THAN] = TYPE_BOOL;
            semanticCube[TYPE_BOOL, TYPE_BOOL, DIFFERENT_THAN] = TYPE_BOOL;

            //Operaciones entre strings
            semanticCube[TYPE_STRING, TYPE_STRING, EQUAL] = TYPE_BOOL;
            semanticCube[TYPE_STRING, TYPE_STRING, PLUS] = TYPE_STRING; //Concatenación
            semanticCube[TYPE_STRING, TYPE_STRING, EQUAL_THAN] = TYPE_BOOL;
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
