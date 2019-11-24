﻿
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
        Variable getTopVariableStack() {

            //Si no se han agregado variables
            if (variableStack.Count == 0) {

                //Se regresa nulo
                return null;
            }

            //Se regresa el top de la pila
            return (Variable)variableStack.Peek();
        }

        //Función que agrega una nueva capa a la tabla de variables
        public void addVariableLayer(Variable variable) {

            //Se le hace push al stack
            variableStack.Push(variable);
        }

        public void addVariableLayer() {

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

            //Variable auxiliar que va a buscar la última variable de la tabla para agregar la nueva variable
            // Variable lastVariable = (Variable)variableStack.Peek();
            Variable lastVariable = findVariableInLastScope(variable.getName());

            //Si se encontró una variable con el mismo nombre
            if (lastVariable != null) {
                //Error, no se pueden tener variables con el mismo nombre en el mismo scope
                parser.SemErr("Sevaral declarations of " + lastVariable.getName());
                return;
            }

            //Se obtiene la referencia a la primera variable del scope
           // lastVariable = getTopVariableStack();

            foreach(Variable i in variableStack) {
                lastVariable = i;
            }

            //Se busca la última variable
            while (lastVariable.getNext() != null) {

                lastVariable = lastVariable.getNext();
            }
           
            //Se guarda la variable a la tabla de variables
            lastVariable.setNext(variable);

            assignGlobalAddress(variable);


        }
        //Función paa agregar variables a la tabla de variables
        public  void addVariable(Variable variable) {

          
            //Si la pila de variables está vacía
            if (variableStack.Count == 0) {
                //Se crea una nueva capa a la tabla de variables
                addVariableLayer(variable);

                assignGlobalAddress(variable);

                //Se termina la ejecución
                return ;
            }

            //Variable auxiliar que va a buscar la última variable de la tabla para agregar la nueva variable
            // Variable lastVariable = (Variable)variableStack.Peek();
            Variable lastVariable = findVariableInLastScope(variable.getName());

            //Si se encontró una variable con el mismo nombre
            if (lastVariable != null) {
                Console.WriteLine("Agregando " + lastVariable.getName());
                //Error, no se pueden tener variables con el mismo nombre en el mismo scope
                parser.SemErr("Sevaral declarations of " + lastVariable.getName());
                return ;
            }

            //Se obtiene la referencia a la primera variable del scope
            lastVariable = getTopVariableStack();

            if(lastVariable != null) {
                //Se busca la última variable
                while (lastVariable.getNext() != null) {
                    
                    lastVariable = lastVariable.getNext();
                   
                }

                //Se guarda la variable a la tabla de variables
                lastVariable.setNext(variable);
            }
            else {
                //Se quita el nulo de la capa
                removeVariableLayer();

                //Se le guarda la variable
                addVariableLayer(variable);
            }
           


            //Si la pila es de tama{o 1, solo hay variables globales
            if(variableStack.Count > 1) {
                assignlocalAddress(variable);
            }
            else {
                assignGlobalAddress(variable);
            }

            Console.WriteLine("ELEMENTS IN STACK " + variableStack.Count);
            /*if(variableStack.Count > 0) {
                Variable aux = (Variable)variableStack.Peek();

                while(aux != null) {
                    Console.WriteLine(aux.getName());
                }

                Console.WriteLine("\n\n");
            }*/
        }

        //Función que busca una variable en la toda la tabla de variables
        public Variable findVariable(string name) {

            //Nos aseguramos que la pila no esté vacía
            if (variableStack.Count > 0) {
                //Debido a que la tabla de variables es una pila, se va iterando en cada capa de la pila
                foreach (Variable i in variableStack) {

                    //Variable auxiliar para iterar sobre la lista de variables
                    Variable actualVar = i;

                    //Mientras existan variables
                    while (actualVar != null) {

                        //Si se encuentra una variable con el mismo nombre del que se está buscando
                        if (actualVar.getName() == name) {
                            //se regresa la variable(el objeto)
                            return actualVar;
                        }

                        actualVar = actualVar.getNext();
                    }

                    //Como no hubo un return, se sigue iterando sobre cada capa
                }
            }
            /*Variable actualVar = variables;

           
            //Mientras actualVar tenga un valor
            while(actualVar != null ) {
                //Si se encuentra la variable que se está buscando por nombre
                //(los nombres de variables deben de ser únicas)
                if(actualVar.getName() == name) {
                    return actualVar;
                }

                actualVar = actualVar.getNext();
            }
            */

            //Si no encontro la variable, actualVar es null
            return null;
        }

        //Función que busca una variable en el scope actual
        public Variable findVariableInLastScope(string name) {

            //Se obtiene la variable del top de la pila
            Variable aux = getTopVariableStack();

            while (aux != null) {

                if (aux.getName() == name) {
                    
                    return aux;
                }

                aux = aux.getNext();
            }

            Console.WriteLine("NO ENCONTRE " + name);
            //Si no se ejecutó el return del while, aux es nulo
            return aux;
        }

        //Función que agrega constantes a la tabla de constantes
        public Variable addConstant(Variable constante) {

            //Se obtiene la referencia al principio de la tabla de constantes
            Variable actualConstant = constants;
            if (constants == null) {
                constants = constante;
            }
            else {
                //Mientras no se encuentre la última constante
                while (actualConstant.getNext() != null) {

                    //Si se encuentra la constante dentro de la lista
                    if (actualConstant.getName() == constante.getName()) {

                        //Se termina la ejecución, no se agrega una constante nueva y regresa el objeto de la constante
                        return actualConstant;
                    }

                    actualConstant = actualConstant.getNext();
                }

                actualConstant.setNext(constante);
            }


            //Se asigna al objeto que es una constante
            constante.setConstant();

            //Se le asigna una dirección constante
            assignGlobalAddress(constante);

            //Se le asigna el valor de la constante
            constante.setValue(constante.getName());

            //Genera un cu{adruplo de asignación para que guarde el valor de la constante en memoria
            codeGenerator.createIntermediateCodeNoTemp(OperationTypes.EQUAL, constante, constante);

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
            /*
           
            if(addVariable(param) != null) {
                //Se agrega a la tabla de parámetros de la función
                actualFunction.addParam(param);
                Console.WriteLine("AGRREGADGFASDFASDF");

            }
            else {
                return null;
            }

          }

    */

            addVariable(param);
            actualFunction.addParam(param);
            return param;
        }

       
    }
}
