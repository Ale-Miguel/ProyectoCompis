
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class VarTable {

        public const int UNDEF_VAR = 0;

        private Variable variables;
        private Function functions;
        private Parser parser;
        

        public VarTable(Parser parser) {
           
            this.parser = parser;
            this.variables = new Variable("undefVar", UNDEF_VAR);
            this.functions = new Function("undefFunct", UNDEF_VAR);
        }
        
        //Función paa agregar variables a la tabla de variables
        public void addVariable(Variable variable) {

            Variable lastVariable = variables;

            //Se busca la ultima variable (variable.next == nulll) o se interrumpe si se ecuentra una variable con el
            //mismo nombre
            while(lastVariable.getName() != variable.getName() && lastVariable.getNext() != null) {

                lastVariable = lastVariable.getNext();
            }

            //Si se encuentra una variable con el mismo nombre (lastVariable != null), es un error, ya que se 
            //está declarando dos veces la variable
            if (lastVariable.getNext() != null || lastVariable.getName() == variable.getName()){
                parser.SemErr("Sevaral declarations of " + lastVariable.getName());
            }

            //Se guarda la variable a la tabla de variables
            lastVariable.setNext(variable);

        }
        
        public Variable findVariable(string name) {

            Variable actualVar = variables;

            //Mientras actualVar tenga un valor
            while(actualVar != null ) {
                //Si se encuentra la variable que se está buscando por nombre
                //(los nombres de variables deben de ser únicas)
                if(actualVar.getName() == name) {
                    return actualVar;
                }

                actualVar = actualVar.getNext();
            }

            parser.SemErr("Variable " + name + " not declared.");
            //Si no encontro la variable, actualVar es null
            return actualVar;
        }

        public void addFunction(Function function) {

            Function lastFunction = functions;

            //Se busca la ultima función (function.next == nulll) o se interrumpe si se ecuentra una función con el
            //mismo nombre
            while (lastFunction.getName() != function.getName() && lastFunction.getNext() != null) {

                lastFunction = lastFunction.getNext();
            }

            //Si se encuentra una función con el mismo nombre (lastFunction != null), es un error, ya que se 
            //está declarando dos veces la función
            if (lastFunction.getNext() != null || lastFunction.getName() == function.getName()) {
                parser.SemErr("Sevaral declarations of " + lastFunction.getName());
            }

            //Se guarda la función a la tabla de funciones
            lastFunction.setNext(function);

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
    }
}
