using System;
using System.Collections;
using System.IO;
using System.Text;

namespace ProyectoPruebas {
    class CodeGeneratorImpl : CodeGenerator {

        private Stack operatorsStack;   //Stack de operadores
        private Stack symbolStack;      //Stack de símbolos
        private Stack jumpStack;

        private int tempCont;   //Contador de variables temporales
        private int lineCont; //Contador de lineas de codigo temporal

        private OperationTypes cuboSemantico;

        

        //private FileStream IntermediateCodeFile;    //Objeto de archivo que guarda el código intermedio
        private string filePath = "IntermediateCode.txt";   //Path y nombre del archivo que guarda el código intermedio

        public bool solveMultAndDiv() {
            if(getTopOperatorStack() == OperationTypes.MULTIPLICATION || getTopOperatorStack() == OperationTypes.DIVISION) {
                Variable right_operand = popSymnbolStack();
                Variable left_operand = popSymnbolStack();

                int operatorValue = popOperatorStack();

                int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);

               
                if (resultType != OperationTypes.TYPE_UNDEFINED) {
         
                    createIntermediateCode(operatorValue, left_operand, right_operand);
                }
                else {
                    return false;
                }

            }


            return true;
        }

        public bool solveAssignment() {
            if (getTopOperatorStack() == OperationTypes.EQUAL) {
                Variable right_operand = popSymnbolStack();
                Variable left_operand = popSymnbolStack();
                int op = popOperatorStack();

                if (!createIntermediateCodeNoTemp(op, right_operand, left_operand)) {
                    return false;

                }
            }


            return true;
        }

        public bool solveSumAndMinus() {
            if (getTopOperatorStack() == OperationTypes.PLUS || getTopOperatorStack() == OperationTypes.MINUS) {
                Variable right_operand = popSymnbolStack();
                Variable left_operand = popSymnbolStack();

                int operatorValue = popOperatorStack();

                int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);

               
                if (resultType != OperationTypes.TYPE_UNDEFINED) {

                    createIntermediateCode(operatorValue, left_operand, right_operand);
                }
                else {
                    return false;
                }

            }


            return true;
        }

        public bool solveRelOp() {

            if (isRelOp(getTopOperatorStack())) {
                Variable right_operand = popSymnbolStack();
                Variable left_operand = popSymnbolStack();

                int operatorValue = popOperatorStack();

                int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);


                if (resultType != OperationTypes.TYPE_UNDEFINED) {

                    createIntermediateCode(operatorValue, left_operand, right_operand);
                }
                else {
                    return false;
                }

            }


            return true;
        }

        bool isRelOp(int relop) {

            switch (relop) {
                case OperationTypes.GREATER_THAN:
                    return true;

                case OperationTypes.GREATER_OR_EQUAL:
                    return true;

                case OperationTypes.LESS_THAN:
                    return true;

                case OperationTypes.LESS_OR_EQUAL:
                    return true;

                case OperationTypes.EQUAL_THAN:
                    return true;

                case OperationTypes.DIFFERENT_THAN:
                    return true;
            }

            return false;
        }

        //Función que agrega un nuevo elemento a la pila de operadores
        public void pushOperatorStack(int operatorValue) {
             
            int semCubeOp = cuboSemantico.getCubePosition(operatorValue);

            operatorsStack.Push(semCubeOp);
           
        }

        //Función que agrega un nuevo elemento a la pila de símbolos y su tipo a la pila de tipos
        public void pushSymbolStack(Variable variable) {
            if (!variable.isParsed()) {

                int posSemCube = cuboSemantico.getCubePosition(variable.getType());
                variable.setType(posSemCube);
                variable.setParsed();
            }
           
            symbolStack.Push(variable);
        }

        //Función que escribe el código intermedio al archivo que guarda dicho código
        public void createIntermediateCode(int op, Variable var1, Variable var2) {

            string tempVarName = "tempVar" + tempCont;

            int resultType = cuboSemantico.semanticCube[var1.getType(), var2.getType(), op];
            
            Variable tempVar = new Variable(tempVarName, resultType);
            tempVar.setParsed();

            symbolStack.Push(tempVar);

            OperationQuadruple quadruple = new OperationQuadruple();

            quadruple.setOperator(op);
            quadruple.setVariables(var1, var2, tempVar);
            quadruple.setLineNumber(lineCont);
            
            writeIntermediateCode(quadruple);

            tempCont++;
           
        }

       

        public bool createIntermediateCodeNoTemp(int op, Variable var1, Variable var2) {
           // Variable lastSymbol = popSymnbolStack();
           
            int  resultType = cuboSemantico.semanticCube[var1.getType(), var2.getType(), op];
            if(resultType == OperationTypes.TYPE_UNDEFINED) {
            
                return false;
            }
           
            OperationQuadruple quadruple = new OperationQuadruple();

            quadruple.setLineNumber(lineCont);
            quadruple.setOperator(op);
            quadruple.setVariables(var1, var2);

            writeIntermediateCode(quadruple);
       
            return true;


        }

        void writeIntermediateCode(Quadruple intermediateCode) {

            File.AppendAllText(filePath, intermediateCode.getQuadruple());
            lineCont++;

        }
        public Variable popSymnbolStack() {

          
            return (Variable)symbolStack.Pop();
        }

        public int popOperatorStack() {
          
            return (int)operatorsStack.Pop();
        }


        public int getTopOperatorStack() {
            return (int)operatorsStack.Peek();
        }

        string getOperatorSymbol(int op) {
            switch (op) {
                case OperationTypes.EQUAL:
                    return "=";

                case OperationTypes.PLUS:
                    return "+";

                case OperationTypes.MINUS:
                    return "-";

                case OperationTypes.MULTIPLICATION:
                    return "*";

                case OperationTypes.DIVISION:
                    return "/";

                case OperationTypes.GREATER_THAN:
                    return ">";

                case OperationTypes.GREATER_OR_EQUAL:
                    return ">=";

                case OperationTypes.LESS_THAN:
                    return "<";

                case OperationTypes.LESS_OR_EQUAL:
                    return "<=";

                case OperationTypes.EQUAL_THAN:
                    return "==";

                case OperationTypes.DIFFERENT_THAN:
                    return "<>";

            }

            return "__";
        }



        public CodeGeneratorImpl() {

            this.operatorsStack = new Stack();
            this.symbolStack = new Stack();
            this.jumpStack = new Stack();
            //IntermediateCodeFile = File.Create(filePath);

            tempCont = 1;
            lineCont = 1;

            cuboSemantico = new OperationTypes();

            File.WriteAllText(filePath, "");
        }
    }
}
