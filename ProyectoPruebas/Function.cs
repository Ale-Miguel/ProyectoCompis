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

        //public int StartsIn { get => startsIn; set => startsIn = value; }

        public void setStartsIn(int valor) {
            this.startsIn = valor;
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
            /*
            if(functParams == null) {
                functParams = paramVaraible;
                return;
            }
            
            Variable aux = functParams;

           while(aux.getNext() != null) {
                aux = aux.getNext();
            }

            aux.setNext(paramVaraible);*/

            functParams.Add(paramVaraible);
        }

        public Variable getParams() {
            return functParams[0];
        }

        public int getSize() {
            Variable varAux = getParams();

            int tam = 0;

            while (varAux != null) {
                varAux = varAux.getNext();
                tam++;
            }

            return tam;
        }


        public Function(string name, int returnValue) {
            this.name = name;
            this.returnType = returnValue;
            next = null;
            functParams = new List<Variable>();
        }
    }
}
