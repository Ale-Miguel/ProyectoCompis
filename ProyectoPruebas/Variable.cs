using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas
{
    class Variable
    {

        public const int ADDRESS_NOT_DEFINED = -1;  //Constante que tiene el valor que indica que no se ha asignado una dirección de memoria

        private string name;    //Variable  que almacena el nombre de la variable
        private int type;       //Variable que guarda el tipo de dato de la variable
        private int address;
        private string value;
        private bool parsed;
        private bool constant;
        private Variable next;   //Se guarda la referencia a la siguiente variable de la tabla de variables
        

        public bool isParsed() {
            return parsed;
        }

        public void setParsed() {
            parsed = true;
        }

        public bool isConstant() {
            return constant;
        }

        public void setConstant() {
            constant = true;
        }


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

        public void setType(int type) {
            this.type = type;
        }

        public string getValue() {
            return value;
        }

        public void setAddress(int address) {
            this.address = address;
        }

        public int getAddress() {
            return this.address;
        }

        public Variable(string name, int type) {
            this.name = name;
            this.type = type;
            this.next = null;
            this.parsed = false;
            this.address = ADDRESS_NOT_DEFINED;
            constant = false;
        }
    }
}
