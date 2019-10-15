using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class OperationTypes {

        private static int NUMBER_OF_TYPES = 5;
        private static int NUMBER_OF_OPERATORS = 10;

        public static int TYPE_UNDEFINED = 0;
        public static int TYPE_INT = 1;
        public static int TYPE_FLOAT = 2;
        public static int TYPE_BOOL = 3;
        public static int TYPE_STRING = 4;

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
            for(int i = 0; i < NUMBER_OF_TYPES; i++) {
                for(int j = 0; j < NUMBER_OF_TYPES; j++) {
                    for(int k = 0; k < NUMBER_OF_OPERATORS; k++) {
                        semanticCube[i, j, k] = TYPE_UNDEFINED;
                    }
                }
            }


        }
    }
}
