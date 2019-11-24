using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProyectoPruebas {
    class CodeGeneratorImpl : CodeGenerator {

        private Stack operatorsStack;   //Stack de operadores
        private Stack symbolStack;      //Stack de símbolos
        private Stack jumpStack;        //Stack de saltos
        private Stack functionCallStack; //Stack de invocaciones a funciones

        private ArrayList quadrupleBuffer;

        VarTable varTable;

        private int tempCont;   //Contador de variables temporales
        private int lineCont; //Contador de lineas de codigo temporal
        private int funcArgCount;


        private List<IQuadruple> quadrupleList;

        private OperationTypes cuboSemantico;



        private string filePath = "IntermediateCode.txt";   //Path y nombre del archivo que guarda el código intermedio

        Variable getTempVar(int type, bool isParsed) {
            //Se crea el nombre de la siguente variable temporal
            string tempVarName = "tempVar" + tempCont;

            

            //Se crea un objeto tipo Variable de la variable temporal
            Variable tempVar = new Variable(tempVarName, type);

            if (!isParsed) {
                parseVariable(tempVar);
            }
            else {
                tempVar.setParsed();
            }
            //Se le asigna una dirección de memoria a la variable temporal
            varTable.assignlocalAddress(tempVar);

            tempCont++;
            return tempVar;
        }
        public void parseVariable(Variable variable) {
            if (variable.isParsed()) {
                return;
            }
            //Se obtiene la posición del tipo de dato equivalente del cubo semántico que le dió a la variable el Parser
            int posSemCube = cuboSemantico.getCubePosition(variable.getType());

            variable.setType(posSemCube);   //Se guarda el nuevo valor de su tipo de dato
            variable.setParsed();           //Se indica que ya fue parseada esta variable
        }
        public int getLineCont() {
            return lineCont;
        }

        //Función que se encarga de resolver las multiplicaciónes y divisiónes
        public bool solveMultAndDiv() {

            //Si lo que está hasta arriba de la pila de operadores es una multiplicación o división
            if (getTopOperatorStack() == OperationTypes.MULTIPLICATION || getTopOperatorStack() == OperationTypes.DIVISION) {

                Variable right_operand = popSymnbolStack(); //Se obtiene el operado derecho
                Variable left_operand = popSymnbolStack();  //Se obtiene el operando izquierdo

                int operatorValue = popOperatorStack();     //Se obtiene el operador

                //Se obtiene el tipo de dato que resulta de aplicar la operación
                int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);

                //Si la operación es válida (que el tipo de dato no sea UNDEFINED)
                if (resultType != OperationTypes.TYPE_UNDEFINED) {

                    //Se crea su cuádruplo
                    createIntermediateCode(operatorValue, left_operand, right_operand);

                    //Se regresa true  en señal de éxito
                    return true;
                }

            }

            //Si hubo algún error en alguna parte, se regresa false
            return false;
        }

        //Función que realiza la asignación
        public bool solveAssignment() {

            // Si lo que está hasta arriba de la pila de operadores no es la asignación
            if (getTopOperatorStack() != OperationTypes.EQUAL) {

                //Se regresa falso en señal de error
                return false;
            }

            Variable right_operand = popSymnbolStack(); //Se obtiene el valor que se va a asignar (valor derecho)
            Variable left_operand = popSymnbolStack();  //Se obtiene la variable a la que se le va a asignar el valor

            int op = popOperatorStack();    //Se obtiene el operador

            //Si no se pudo crear el código intermedio
            if (!createIntermediateCodeNoTemp(op, right_operand, left_operand)) {

                //Se regresa falso en señal de error
                return false;
            }

            //Se regresa true en señal de éxito
            return true;
        }

        //Función que resuelve las sumas y restas
        public bool solveSumAndMinus() {

            //Si lo que está hasta arriba de la pila de operadores es una suma o una resta
            if (getTopOperatorStack() == OperationTypes.PLUS || getTopOperatorStack() == OperationTypes.MINUS) {

                Variable right_operand = popSymnbolStack(); //Se obtiene el operando derecho
                Variable left_operand = popSymnbolStack();  //Se obtiene el operando izquierdo

                int operatorValue = popOperatorStack();     //Se obtiene el operador

                //Se obtierne el tipo de dato de hacer la operación
                int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);

                //Si la operaci{on entre los tipos de datos es válida (que no sea UNDEFINED)
                if (resultType != OperationTypes.TYPE_UNDEFINED) {

                    //Se crea el cuádruplo de la operación
                    createIntermediateCode(operatorValue, left_operand, right_operand);

                    //Se regresa verdadero en señal de éxito
                    return true;
                }
            }

            //Si en algún momento falló algo, se regresa falseo en señal de falla
            return false;
        }

        //Función que maneja la resolución de operadores relacionales
        public bool solveRelOp() {

            //Si lo que está hasta arriba de la pila no es un operador relacional
            if (!isRelOp(getTopOperatorStack())) {
                //Se regresa falso en señal de error
                return false;
            }

            Variable right_operand = popSymnbolStack(); //Se obtiene el operando de la derecha de la pila
            Variable left_operand = popSymnbolStack();  //Se obtiene el operadon de la izquierda de la pila

            //Se obtiene el operador de la pila
            int operatorValue = popOperatorStack();

            //Se obtiene el tipo de dato del resultado de hacer la operación
            int resultType = cuboSemantico.getOperationResult(left_operand.getType(), right_operand.getType(), operatorValue);

            //Si el resultado está definido
            if (resultType != OperationTypes.TYPE_UNDEFINED) {

                //Se crea el cuádruplo de esta operación
                createIntermediateCode(operatorValue, left_operand, right_operand);
            }
            else {
                //Si no estaba definida la operación entre los dos tipos de datos, se regresa falso en señal de error
                return false;
            }

            //Se regresa true en señal de éxito
            return true;
        }

        //Función que indica si lo que se le manda es un operador relacional
        bool isRelOp(int relop) {

            switch (relop) {

                case OperationTypes.GREATER_THAN:       //>
                    return true;

                case OperationTypes.GREATER_OR_EQUAL:   //>=
                    return true;

                case OperationTypes.LESS_THAN:          //<
                    return true;

                case OperationTypes.LESS_OR_EQUAL:      //<=
                    return true;

                case OperationTypes.EQUAL_THAN:         //==
                    return true;

                case OperationTypes.DIFFERENT_THAN:    //<>
                    return true;
            }

            //Si no, regresa false en señal que no es un operador relacional lo que se le mandó como parámetro
            return false;
        }

        //Función que agrega un nuevo elemento a la pila de operadores
        public void pushOperatorStack(int operatorValue) {

            //Se hace la traducción entre lo el valor de la operación que da el Parser con su equivalente en el cubo semántico 
            int semCubeOp = cuboSemantico.getCubePosition(operatorValue);

            //Se hace push a la pila de operadores
            operatorsStack.Push(semCubeOp);

        }

        //Función que agrega un nuevo elemento a la pila de símbolos y su tipo a la pila de tipos
        public void pushSymbolStack(Variable variable) {


            //Verifica si la variable ya fue parseada (traducida a valores de cubo semántico) para que no tenga
            //que volverse a traducir
            if (!variable.isParsed()) {

                parseVariable(variable);
            }

            //Se le hace push a la pila de símbolos
            symbolStack.Push(variable);
        }

        void pushFunctionStack(Function funcion) {
            functionCallStack.Push(funcion);
        }

        Function popFunctionStack() {

            if(functionCallStack.Count == 0) {
                return null;
            }

            return (Function)functionCallStack.Pop();
        }

        Function getTopFunctionStack() {

            if (functionCallStack.Count == 0) {
                return null;
            }

            return (Function)functionCallStack.Peek();
        }

        //Función que genera el cuádruplo con 3 variables (incluye la variable temporal)
        public void createIntermediateCode(int op, Variable var1, Variable var2) {


            //Se obtiene el tipo que guarda la variable temporal. Al ser la variable que va a almacenar el resultado de una
            //operación, su tipo de dato es el que resulte de hacer esa operación
            int resultType = cuboSemantico.semanticCube[var1.getType(), var2.getType(), op];

            Variable tempVar = getTempVar(resultType, true);

            //Se le hace push a la pila de símbolos
            pushSymbolStack(tempVar);

            //Se crea un cádruplo de operación para esta operación
            OperationQuadruple quadruple = new OperationQuadruple();

            quadruple.setOperator(op);                      //Se le guarda la operación que va a realizar
            quadruple.setVariables(var1, var2, tempVar);    //Se guardan las variables involucradas
            quadruple.setLineNumber(lineCont);              //Se le guarda el número de línea  que le corresponde

            //Se manda a  escribir el cuádruplo al archivo de cuádruplos
            writeIntermediateCode(quadruple);



        }

        //Función que prepara un cuádruplo con solo dos variables, sin crear una variable temporal
        public bool createIntermediateCodeNoTemp(int op, Variable var1, Variable var2) {

            //Variable que guarda el resultado de hacer una operación entre dos tipos de dato

            parseVariable(var1);
            parseVariable(var2);

            int resultType = cuboSemantico.semanticCube[var1.getType(), var2.getType(), op];

            //Si el resultado es UNDEFINED
            if (resultType == OperationTypes.TYPE_UNDEFINED) {

                //Se regresa false en señal de error
                return false;
            }

            //Objeto de cuádruplo de operación que genera el cuádruplo a escrubur en el archivo de cuádruplos
            OperationQuadruple quadruple = new OperationQuadruple();

            quadruple.setLineNumber(lineCont);      //Se le asigna el número de línea que le pertenece
            quadruple.setOperator(op);              //Se le asigna el operador que va a utilizar
            quadruple.setVariables(var1, var2);     //Se le asigna las variables que se utilizan en la operación

            //Se manda a escribir en el archivo de cuádruplos
            writeIntermediateCode(quadruple);

            //Se regresa true como operación exitosa
            return true;
        }

        //Función que manda a escribir el código intermedio (cuádruplos)
        void writeIntermediateCode(IQuadruple intermediateCode) {

            //Se agrega el cuádruplo al buffer si no se le pasa nulo
            if (intermediateCode != null) {
                quadrupleBuffer.Add(intermediateCode);
                lineCont++;
            }


            //Si la pila de saltos está vacía
            if (jumpStack.Count == 0) {

                //Se escribe en el archivo cada cuádruplo almacenado en el buffer
                foreach (IQuadruple quadruple in quadrupleBuffer) {

                    quadrupleList.Add(quadruple);
                    writeToFile(quadruple.getQuadruple());
                }

                //Se limpia todo el buffer
                quadrupleBuffer.RemoveRange(0, quadrupleBuffer.Count);
            }
            //Se incrementa el contador de líneas de cuádruplos


        }

        //Función que escribe los cuádruplos al archivo de cuádruplos
        void writeToFile(string text) {

            //Se escribe el cuádruplo en el archivo
            File.AppendAllText(filePath, text + "\n");
        }

        //Función que maneja el  pop de la pila de símbolos
        public Variable popSymnbolStack() {

            //Regresa el pop de la pila de símbolos con el cast de Variable
            
            return (Variable)symbolStack.Pop();
        }

        //Función que maneja la operación de pop de la pila de operadores
        public int popOperatorStack() {

            //Regresa el resultado de hacer pop de la pila de operadores con cast de int
            return (int)operatorsStack.Pop();
        }

        //Función que regresa lo que esté hasta arriba de la pila de operadores
        public int getTopOperatorStack() {

            //Se regresa el Top de la pila de operadores
            if(operatorsStack.Count == 0) {
                return OperationTypes.TYPE_UNDEFINED;
            }
            return (int)operatorsStack.Peek();
        }

        //Función que hace como diccionario, la llave es el número de operador y regresa su símbolo
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

            //Valor por defecto por si no encontró nada de lo que se le pasó
            return "__";
        }

        //Función que se encarga de hacer las acciones necesarias para procesar el For
        public void solveFor() {

            //Se resuelve la parte de la condición
            //El for se comporta como el if, por eso se trata como tal
            lineCont++;
            popJumpStack();
            lineCont--;
            //El siguiente valor de la pila guarda la línea a donde empiezan las operaciones de la condición
            int lineReturn = (int)jumpStack.Pop();

            //Se crea el goTo para regresarse a los cuádruplos de la condición y que se cree el ciclo
            GoTo goTo = new GoTo(lineReturn, lineCont);

            //Se manda a escribir el cuádruplo
            writeIntermediateCode(goTo);

        }

        //Función que hace push a la pila de saltos
        public void pushJumpStack(int line) {

            jumpStack.Push(line);

        }

        //Función que hace push a la pila de saltos con GoToF
        public void pushGoToF(Variable var) {

            //Se crea un objeto tipo GoToF con su variable que va a comparar con falso y su línea que le corresponde
            GotoF jumpF = new GotoF(var, lineCont);

            //Se agrega el cuádruplo al buffer de cuádruplos
            quadrupleBuffer.Add(jumpF);

            //Se hace push a la pila de saltos, el valor es relativo al tamaño del buffer para saber a qué salto
            //se refiere para asignarle la línea a la que va a brincar
            jumpStack.Push(quadrupleBuffer.Count - 1);

            //Se incrementa el contador de líneas
            lineCont++;
        }

        //Función que hace push a la pila de saltos con GoTo
        public void pushGoTo() {

            //Se crea un objeto de tipo GoTo 
            GoTo jump = new GoTo(lineCont);

            //Se agrega el cuádruplo al buffer de cuádruplos
            quadrupleBuffer.Add(jump);

            //Se hace push a la pila de saltos, el valor es relativo al tamaño del buffer para saber a qué salto
            //se refiere para asignarle la línea a la que va a brincar
            jumpStack.Push(quadrupleBuffer.Count - 1);

            //Se incrementa el contador de líneas
            lineCont++;
        }

        //Función que maneja el pop de la pila de saltos
        public void popJumpStack() {
            
            //Si la pila está vacía, no se hace nada
            if(jumpStack.Count == 0) {
                return;
            }

        
            //Se obtiene el índice del salto
            int jumpIndex = (int)jumpStack.Pop();

            //Se crea una referencia al objeto del salto
            Jumps jump = (Jumps)quadrupleBuffer[jumpIndex];

            //Se le asigna la línea a la que va a saltar
            jump.setJump(lineCont -1);


            //Se manda a escribir el cuádruplo al archivo
            writeIntermediateCode(null);


        }

        public void pushGoToMain() {
            pushGoTo();
        }
        public void solveGoToMain() {
            lineCont++;
            popJumpStack();
            lineCont--;
        }

        public void endFunction() {
            EndProc endProc = new EndProc();

            writeIntermediateCode(endProc);
            varTable.destroyContext();
        }
        
        public void functionCall(Function funcion) {
            pushFunctionStack(funcion);

            Era eraQuad = new Era(funcion);

            funcArgCount = 0;

            writeIntermediateCode(eraQuad);
        }

        public void createReturn() {
            Variable returnVariable = popSymnbolStack();
            Return returnObj = new Return(returnVariable);

            writeIntermediateCode(returnObj);
        }

        public bool setFunctParam() {
            Variable parametro = popSymnbolStack();

            Function funcion = getTopFunctionStack();

            Variable funcParameter = funcion.getParam(funcArgCount);

            if(funcParameter == null) {
                return false;
            }


            parseVariable(parametro);
            parseVariable(funcParameter);

            if(parametro.getType() != funcParameter.getType()) {
                return false;
            }

            Param param = new Param(funcArgCount, parametro);

            funcArgCount++;

            return true;
        }

        public void solveFunction() {

            Function funcion = popFunctionStack();
            GoSub goSub = new GoSub(funcion);

            Variable returnFunctVariable = funcion.getReturnVariable();
            //pushSymbolStack(funcion.getReturnVariable());

            writeIntermediateCode(goSub);
            parseVariable(returnFunctVariable);
            Variable returnTempVariable = getTempVar(returnFunctVariable.getType(), returnFunctVariable.isParsed());

            createIntermediateCodeNoTemp(OperationTypes.EQUAL, funcion.getReturnVariable(), returnTempVariable);

            pushSymbolStack(returnTempVariable);
        }


        public List<IQuadruple> getQuadrupleList() {
            return quadrupleList;
        }
        public CodeGeneratorImpl(VarTable varTable) {

            //Se crean las pilas
            this.operatorsStack = new Stack();
            this.symbolStack = new Stack();
            this.jumpStack = new Stack();
            this.functionCallStack = new Stack();

            this.quadrupleBuffer = new ArrayList();
            this.varTable = varTable;
            //Se inicializan los contadores
            tempCont = 1;
            lineCont = 1;
            funcArgCount = 0;

            quadrupleList = new List<IQuadruple>();
            //Se crea un cubo semántico
            cuboSemantico = new OperationTypes();

            //Se crea el archivo o si está, se sobreescribe
            File.WriteAllText(filePath, "");
        }
    }

   
}
