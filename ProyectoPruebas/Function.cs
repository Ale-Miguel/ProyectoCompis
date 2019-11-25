using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Function {

        private string name;        //Atributo que almacena el nombre de función
        private int returnType;    //Atributo que almacena el tipo de retorno
        private Function next;      //Atributo que almacena la función siguiente de la lista
        private List<Variable> functParams;   //Lista de argumentos
        private int startsIn;

        private Variable returnVariable;

        //public int StartsIn { get => startsIn; set => startsIn = value; }

        public Variable getParam(int param) {

            //Si aún no se le asigna parámetros o no tiene parámetros o se trata de acceder a un número de parámetro que no existe
            if(functParams.Count <= 0 || functParams.Count < param) {

                //Se regresa nulo
                return null;
            }

            return functParams[param - 1];
        }
        public void setReturnVariable(Variable variable) {
            returnVariable = variable;
        }

        public Variable getReturnVariable() {
            return returnVariable;
        }
        public void setStartsIn(int line) {
            this.startsIn = line;
        }

        public int getStartsIn() {
            return this.startsIn;
        }

        public void setNext(Function next) {
            this.next = next;
        }

        public Function getNext() {
            return next;
        }
        public string getName() {
            return name;
        }

        public int getReturnType() {
            return returnType;
        }

        public void addParam(Variable paramVaraible) {
          
            functParams.Add(paramVaraible);
        }

        public int getSize() {
            return functParams.Count;
        }


        public Function(string name, int returnValue) {
            this.name = name;
            this.returnType = returnValue;
            next = null;
            functParams = new List<Variable>();
            this.startsIn = -1;
            this.returnVariable = null;
        }
    }
}
