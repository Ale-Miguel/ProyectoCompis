using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas
{
    class Variable
    {


        private string name;    //Variable  que almacena el nombre de la variable
        private int type;       //Variable que guarda el tipo de dato de la variable
        private string value;
        public Variable next;   //Se guarda la referencia a la siguiente variable de la tabla de variables

        public string getName(){
            return this.name;
        }
        public int getType(){
            return this.type;
        }

        public Variable getNext() {
            return this.next;
        }
        public void setNext(Variable next) {

            this.next = next;
        }

        public void setValue(string value) {
            this.value = value;
        }

        public string getValue() {
            return value;
        }
        public Variable(string name, int type) {
            this.name = name;
            this.type = type;
            this.next = null;
        }
    }
}
