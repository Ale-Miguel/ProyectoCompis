using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Function {

        private string name;        //Atributo que almacena el nombre de función
        private int returnType;    //Atributo que almacena el tipo de retorno
        private Function next;      //Atributo que almacena la función siguiente de la lista

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


        public Function(string name, int returnValue) {
            this.name = name;
            this.returnType = returnValue;
            next = null;
        }
    }
}
