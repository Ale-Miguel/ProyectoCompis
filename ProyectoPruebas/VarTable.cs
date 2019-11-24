
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class VarTable {

        public const int UNDEF_VAR = 0;

        private Variable variables;
        private Variable constants;
        private Function functions;
        private Parser parser;

        private Stack variableStack;

        private Dictionary<string, Variable> variableDiccionary;
        private Dictionary<string, Variable> constantsDiccionary;

        public CodeGeneratorImpl codeGenerator;

        Function actualFunction;

        AddressManager addressManager;

        public VarTable(Parser parser) {

            this.parser = parser;
            this.variables = new Variable("undefVar", UNDEF_VAR);
            this.functions = new Function("undefFunct", UNDEF_VAR);

            this.variableStack = new Stack();

            codeGenerator = new CodeGeneratorImpl(this);

            actualFunction = null;

            this.addressManager = new AddressManager();

            variableDiccionary = new Dictionary<string, Variable>();
            constantsDiccionary = new Dictionary<string, Variable>();
        }

        //Función para crear un contexto nuevo
        public void createContext() {
            //Se crea un nuevo set de variables locales
            addressManager.createNewLocalAddress();
            addVariableLayer();
        }

        public void destroyContext() {
            addressManager.destroyCurrentLocalAddress();
            removeVariableLayer();
        }
   
        bool assignGlobalAddress(Variable var) {

            int tempAddress = 0;

            if (var.isConstant()) {

                switch (var.getType()) {
                    case Parser._CTE_F:
                        tempAddress = addressManager.getConstFloatAddress();
                        break;

                    case Parser._CTE_I:
                        tempAddress = addressManager.getConstIntAddress();
                        break;

                    case Parser._CTE_S:
                        tempAddress = addressManager.getConstStringAddress();
                        break;

                    default:
                        var.setAddress(OperationTypes.TYPE_UNDEFINED);
                        return false;
                }
            }
            else {

                switch (var.getType()) {

                    case Parser._CTE_I:
                        tempAddress = addressManager.getGlobalIntVarAddress();
                        break;

                    case Parser._CTE_F:
                        tempAddress = addressManager.getGlobalFloatVarAddress();
                        break;

                    case Parser._CTE_S:
                        tempAddress = addressManager.getGlobalStringVarAddress();
                        break;

                    case Parser._Int:
                        tempAddress = addressManager.getGlobalIntVarAddress();
                        break;

                    case Parser._Float:
                        tempAddress = addressManager.getGlobalFloatVarAddress();
                        break;

                    case Parser._Bool:
                        tempAddress = addressManager.getGlobalBoolVarAddress();
                        break;

                    case Parser._String:
                        tempAddress = addressManager.getGlobalStringVarAddress();
                        break;

                    default:
                        return false;
                }

            }



            var.setAddress(tempAddress);
            return true;
        }

       public  bool assignlocalAddress(Variable var) {

            int tempAddress = 0;

            if (var.isParsed()) {
                switch (var.getType()) {
                    case OperationTypes.TYPE_BOOL:
                        tempAddress = addressManager.getBoolAddress();
                        break;

                    case OperationTypes.TYPE_FLOAT:
                        tempAddress = addressManager.getFloatAddress();
                        break;

                    case OperationTypes.TYPE_INT:
                        tempAddress = addressManager.getIntAddress();
                        break;

                    case OperationTypes.TYPE_STRING:
                        tempAddress = addressManager.getStringAddress();
                        break;

                    default:

                        var.setAddress(OperationTypes.TYPE_UNDEFINED);
                        return false;
                }

            }
            else {
                switch (var.getType()) {

                    case Parser._CTE_I:
                        tempAddress = addressManager.getConstIntAddress();
                        break;

                    case Parser._CTE_F:
                        tempAddress = addressManager.getConstFloatAddress();
                        break;

                    case Parser._CTE_S:
                        tempAddress = addressManager.getConstStringAddress();
                        break;

                    case Parser._Int:
                        tempAddress = addressManager.getIntAddress();
                        break;

                    case Parser._Float:
                        tempAddress = addressManager.getFloatAddress();
                        break;

                    case Parser._Bool:
                        tempAddress = addressManager.getBoolAddress();
                        break;

                    case Parser._String:
                        tempAddress = addressManager.getStringAddress();
                        break;

                    default:
                        var.setAddress(OperationTypes.TYPE_UNDEFINED);
                        return false;
                }
            }
          

            var.setAddress(tempAddress);

            return true;
        }

        //Función que agrega un nuevo parámetro a la función actual
        public void addFunctParam(Variable variableParam) {


            //Si ya existe una variable que se llama igual, no puede ser
            if (findVariable(variableParam.getName()) != null) {
                parser.SemErr("Several declarations of " + variableParam.getName());
                //return;
            }

            //Si no hay parámetros, se agrega la variable a la tabla de variables la variable local
            //agregando una nueva capa a la pila
            if (actualFunction.getParams() == null) {
                addVariableLayer(variableParam);
            }
            else {
                //Si ya tiene parámetros, solo se agrega en su capa de la tabla de variables
                addVariable(variableParam);
            }

            //Se agrega el parámetro a la función
            actualFunction.addParam(variableParam);

        }

        //Función que regresa la última capa de la lista de variables (top de la pila)
        Dictionary<string, Variable> getTopVariableStack() {

            //Si no se han agregado variables
            if (variableStack.Count == 0) {

                //Se regresa nulo
                return null;
            }

            //Se regresa el top de la pila
            return (Dictionary<string, Variable>)variableStack.Peek();
        }

        //Función que agrega una nueva capa a la tabla de variables
        public void addVariableLayer(Variable variable) {

            Dictionary<string, Variable> aux = new Dictionary<string, Variable>();

            aux.Add(variable.getName(), variable);

            //Se le hace push al stack
            variableStack.Push(aux);
        }

        public void addVariableLayer() {
            //Dictionary<string, Variable> aux = new Dictionary<string, Variable>();
            //Se le hace push al stack
            variableStack.Push(null);
        }
        //Función que quita una capa de la tabla de variables (removiendo variables locales)
        public void removeVariableLayer() {

            //Se hace pop a la pila
            variableStack.Pop();
        }

        public void addGlobalVariable(Variable variable) {
            //Si la pila de variables está vacía
            if (variableStack.Count == 0) {
                //Se crea una nueva capa a la tabla de variables
                addVariableLayer(variable);

                assignGlobalAddress(variable);

                //Se termina la ejecución
                return;
            }

            Dictionary<string, Variable> lastVariable = getTopVariableStack(); ;

            foreach (Dictionary<string, Variable>  i in variableStack) {
                lastVariable = i;
            }
            
            //Se intenta agregar la variable en el diccionario
            try {
                lastVariable.Add(variable.getName(), variable);
                assignGlobalAddress(variable);
            }
            catch (ArgumentException) {
                //En caso de que no se pueda, quiere decir que esa variable ya está declarada, por lo que se manda el error.
                parser.SemErr("Several declarations of " + variable.getName());
            }

        

        }

        //Función paa agregar variables a la tabla de variables
        public  void addVariable(Variable variable) {

            //Si la pila de variables está vacía
            if (variableStack.Count == 0) {
                //Se crea una nueva capa a la tabla de variables
                addVariableLayer(variable);

                assignGlobalAddress(variable);

                //Se termina la ejecución
                return;
            }

            //Si se encontró una variable con el mismo nombre
            if (findVariableInLastScope(variable.getName()) != null) {
    
                //Error, no se pueden tener variables con el mismo nombre en el mismo scope
                parser.SemErr("Sevaral declarations of " + variable.getName());
                return;
            }


            //Se obtiene la referencia a la primera variable del scope
            Dictionary<string, Variable> lastVariable = getTopVariableStack();

            //Si el top de la pila no es nulo, se agrega la variable al diccionario
            if(lastVariable != null) {
                try {
                    lastVariable.Add(variable.getName(), variable);
                }
                catch(ArgumentException) {

                    //Si no se puede agregar es que ya existe una variable con ese nombre
                    parser.SemErr("Sevaral declarations of " + variable.getName());
                    return;
                }
            }
            else {
                //Se quita el nulo de la capa
                removeVariableLayer();

                //Se le guarda la variable
                addVariableLayer(variable);
            }
           


            //Si la pila es de tamaño 1, solo hay variables globales
            if(variableStack.Count > 1) {
                assignlocalAddress(variable);
            }
            else {
                assignGlobalAddress(variable);
            }
        }

        //Función que busca una variable en la toda la tabla de variables
        public Variable findVariable(string name) {

            //En cada diccionario de la pila de variables
            foreach(Dictionary<string, Variable> i in variableStack) {
                try {
                    Variable resultado;
                    //Intenta obtener la variable del diccionario
                    if (i != null && i.TryGetValue(name, out resultado)) {
                        //Si la llave existe, se regresa el valor (la variable)
                        return resultado;
                    }
                }
                catch (ArgumentException) {

                }
            }

            //Si no encontro la variable, se regresa nulo
            return null;
        }

        //Función que busca una variable en el scope actual
        public Variable findVariableInLastScope(string name) {

            //Se obtiene la variable del top de la pila
            Dictionary<string, Variable> aux = getTopVariableStack();
            
            //Si el top es nulo, no hay variables, asi que se regresa nulo
            if(aux == null) {
                return null;
            }

            try {

                Variable resultado;
                //Si existe la llave en el diccionario
                if( aux.TryGetValue(name, out resultado)) {
                    //Se regresa la variable
                    return resultado;
                }
                else {
                    //Si no existe, se regresa nulo
                    return null;
                }
                
            }
            catch (ArgumentException) {
                return null;
            }
            //Si no se ejecutó el return del while, aux es nulo
            return null;
        }

        //Función que agrega constantes a la tabla de constantes
        public Variable addConstant(Variable constante) {

            try {
                //Se agrega la constante al diccionario de constantes
                constantsDiccionary.Add(constante.getName(), constante);

                //Se asigna al objeto que es una constante
                constante.setConstant();

                //Se le asigna una dirección constante
                assignGlobalAddress(constante);

                //Se le asigna el valor de la constante
                constante.setValue(constante.getName());

                //Genera un cu{adruplo de asignación para que guarde el valor de la constante en memoria
                codeGenerator.createIntermediateCodeNoTemp(OperationTypes.EQUAL, constante, constante);

            }
            catch (ArgumentException) {
                //Si hubo un error (la constante ya estaba ahí) se regresa el objeto de constante
                return constantsDiccionary[constante.getName()];
            }
            //Se regresa la constante ya con sus campos llenos
            return constante;
        }
        
        public void addFunction(Function function) {

            Function lastFunction = functions;
            //Se busca la ultima función (function.next == null) o se interrumpe si se ecuentra una función con el
            //mismo nombre
            while (lastFunction.getName() != function.getName() && lastFunction.getNext() != null) {

                lastFunction = lastFunction.getNext();
            }

            //Si se encuentra una función con el mismo nombre (lastFunction != null), es un error, ya que se 
            //está declarando dos veces la función
            if (lastFunction.getNext() != null || lastFunction.getName() == function.getName()) {
                parser.SemErr("Sevaral declarations of function " + lastFunction.getName());
            }

            //Se guarda la función a la tabla de funciones
            lastFunction.setNext(function);

            //Se señala que se está analizando esta función
            actualFunction = function;

            function.setStartsIn(codeGenerator.getLineCont());

            if(function.getReturnType() != Parser._Void) {
                Variable returnVariable = new Variable(function.getName(), function.getReturnType());
                addGlobalVariable(returnVariable);

                function.setReturnVariable(returnVariable);
            }

           

           
        }

        public Function findFunction(string name) {

            Function actualFunction = functions;

            //Mientras actualFunction tenga un valor
            while (actualFunction != null) {
                //Si se encuentra la función que se está buscando por nombre
                //(los nombres de función deben de ser únicas)
                if (actualFunction.getName() == name) {
                    return actualFunction;
                }

                actualFunction = actualFunction.getNext();
            }

            parser.SemErr("Function " + name + " not declared.");
            //Si no encontro la función, actualFunction es null
            return actualFunction;
        }
        
        public Variable addParamToFunction(Variable param) {

            //Variable auxiliar para saber si se encuentra o no en la tabla de variables local
            Variable auxParam = findVariableInLastScope(param.getName());
           
            //Se tuvo que regresar un null si es que esa variable no existe
            if(auxParam != null) {
                
                parser.SemErr("ERROR: Several declarations of " + param.getName());
                //Se regresa nulo en señal de error.
                return null;
            }


            //Se agrega a la tabla de variables
            addVariable(param);

            //Se agrega a la lista de parámetros de la función
            actualFunction.addParam(param);

            //Se regresa la variable
            return param;
        }

       
    }
}
