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
        private int address;    //Variable que guarda la dirección de memoria asignada a esta variable
        private string value;   //Variable que guarda el valor de la constante o lo que se quiera guardar
        private bool parsed;    //Variable que indica si esta variable ya ha sido parseada
        private bool constant;  //Variable que indica si esto es una constante o no
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
            this.constant = false;
        }

        public Variable(string name, int type, string value) {
            this.name = name;
            this.type = type;
            this.next = null;
            this.parsed = false;
            this.address = ADDRESS_NOT_DEFINED;
            this.constant = false;
            this.value = value;
        }

        public Variable() {
            this.name = "undefVar";
            this.type = OperationTypes.TYPE_UNDEFINED;
            this.next = null;
            this.parsed = true;
            this.address = ADDRESS_NOT_DEFINED;
            this.constant = false;
        }
    }
}
