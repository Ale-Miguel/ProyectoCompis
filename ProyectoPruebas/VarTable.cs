
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

      
        public VarTable(Parser parser) {

            this.parser = parser;
            this.variables = new Variable("undefVar", UNDEF_VAR);
            this.functions = new Function("undefFunct", UNDEF_VAR);

            this.variableStack = new Stack();

            codeGenerator = new CodeGeneratorImpl();

            actualFunction = null;

          
        }

   
        bool assignGlobalAddress(Variable var) {
            int tempAddress = 0;
            switch (var.getType()) {
     
                case Parser._CTE_I:
                    tempAddress = intConstAddress;
                    intConstAddress++;
                    break;

                case Parser._CTE_F:
                    tempAddress = floatConstAddress;
                    floatConstAddress++;
                    break;

                case Parser._CTE_S:
                    tempAddress = stringConstAddress;
                    stringConstAddress++;
                    break;

                case Parser._Int:
                    tempAddress = globalIntVarAddress;
                    globalIntVarAddress++;
                    break;

                case Parser._Float:
                    tempAddress = globalFloatVarAddress;
                    globalFloatVarAddress++;
                    break;

                case Parser._Bool:
                    tempAddress = globalBoolVarAddress;
                    globalBoolVarAddress++;
                    break;

                case Parser._String:
                    tempAddress = globalStringVarAddress;
                    globalStringVarAddress++;
                    break;

                default:
                    return false;
            }

            var.setAddress(tempAddress);
            return true;
        }

        bool assignlocalAddress(Variable var) {

            int tempAddress = 0;
            switch (var.getType()) {

                case Parser._CTE_I:
                    tempAddress = intConstAddress;
                    intConstAddress++;
                    break;

                case Parser._CTE_F:
                    tempAddress = floatConstAddress;
                    floatConstAddress++;
                    break;

                case Parser._CTE_S:
                    tempAddress = stringConstAddress;
                    stringConstAddress++;
                    break;

                case Parser._Int:
                    tempAddress = localIntVarAddress;
                    localIntVarAddress++;
                    break;

                case Parser._Float:
                    tempAddress = localFloatVarAddress;
                    localFloatVarAddress++;
                    break;

                case Parser._Bool:
                    tempAddress = localBoolVarAddress;
                    localBoolVarAddress++;
                    break;

                case Parser._String:
                    tempAddress = localStringVarAddress;
                    localStringVarAddress++;
                    break;

                default:
                    return false;
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

        //Función que quita una capa de la tabla de variables (removiendo variables locales)
        public void removeVariableLayer() {

            //Se hace pop a la pila
            variableStack.Pop();
        }
        //Función paa agregar variables a la tabla de variables
        public void addVariable(Variable variable) {

            //Variable lastVariable = variables;
            //Si la pila de variables está vacía
            if (variableStack.Count == 0) {
                //Se crea una nueva capa a la tabla de variables
                addVariableLayer(variable);

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
            lastVariable = getTopVariableStack();

            //Se busca la última variable
            while (lastVariable.getNext() != null) {

                lastVariable = lastVariable.getNext();
            }
            /* //se busca la última variable, igual se va checando que no se encuentre una variable que ya está agregada en el mismo scope
             while (lastVariable.getName() != variable.getName() && lastVariable.getNext() != null) {

                 lastVariable = lastVariable.getNext();
             }

             //En caso de que se encuentre una variable que se llame igual en el mismo scope, no se puede
             if (lastVariable.getNext() != null || lastVariable.getName() == variable.getName()) {

                 //Error, no se pueden declarar 2 o más variables con el mismo nombre
                 parser.SemErr("Sevaral declarations of " + lastVariable.getName());
             }
             */
            //Se busca la ultima variable (variable.next == nulll) o se interrumpe si se ecuentra una variable con el
            //mismo nombre
            /* while (lastVariable.getName() != variable.getName() && lastVariable.getNext() != null) {

                 lastVariable = lastVariable.getNext();
             }


             //Si se encuentra una variable con el mismo nombre (lastVariable != null), es un error, ya que se 
             //está declarando dos veces la variable
             if (lastVariable.getNext() != null || lastVariable.getName() == variable.getName()){
                 parser.SemErr("Sevaral declarations of " + lastVariable.getName());
             }
 */
            //Se guarda la variable a la tabla de variables
            lastVariable.setNext(variable);

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

            //Si no se ejecutó el return del while, aux es nulo
            return aux;
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
        //Función que agrega constantes a la tabla de constantes
        public Variable agregarConstante(Variable constante) {

            //Se obtiene la referencia al principio de la tabla de constantes
            Variable actualConstant = constants;

            //Mientras no se encuentre la última constante
            while (actualConstant.getNext() != null) {

                //Si se encuentra la constante dentro de la lista
                if (actualConstant.getName() == constante.getName()) {

                    //Se termina la ejecución, no se agrega una constante nueva y regresa el objeto de la constante
                    return actualConstant;
                }

                actualConstant = actualConstant.getNext();
            }

            //Se asigna al objeto que es una constante
            constante.setConstant();

            actualConstant.setNext(constante);

            return constante;
        }
    }
}
