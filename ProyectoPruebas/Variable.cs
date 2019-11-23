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
        private string value = "null";   //Variable que guarda el valor de la constante o lo que se quiera guardar
        private bool parsed;    //Variable que indica si esta variable ya ha sido parseada
        private bool constant;  //Variable que indica si esto es una constante o no
        private Variable next;   //Se guarda la referencia a la siguiente variable de la tabla de variables
        
        //Fuci{on que indica si los valores de la variable ya han sido traducidos a valores del cubo semántico
        public bool isParsed() {
            return parsed;
        }

        //Función que marca a la variable como si ya fue traducida
        public void setParsed() {
            parsed = true;
        }

        //Función que indica si lo que almacena el objeto es una variable
        public bool isConstant() {
            return constant;
        }

        //Función que hace que esta variable sea una constante
        public void setConstant() {
            constant = true;
        }

        //Función que regresa el nombre de la variable
        public string getName(){
            return this.name;
        }

        //Función que  regresa el tipo de dato de la variable o constante
        public int getType(){
            return this.type;
        }

        //Función que regresa una referencia a su siguiente variable en la tabla de variables
        public Variable getNext() {
            return this.next;
        }

        //Función que agrega una referencia a su siguiente variable en la tabla de variables
        public void setNext(Variable next) {

            this.next = next;
        }

        //Funci{on que le asigna un valor a la constante
        public void setValue(string value) {
            this.value = value;
        }

        //Función que le asigna qué tipo de dato es la variable o constante
        public void setType(int type) {
            this.type = type;
        }

        //Función que regresa el valor si es una constante
        public string getValue() {
            return value;
        }

        //Función que guarda la dirección de memoria de donde está alojada la variable o constante
        public void setAddress(int address) {
            this.address = address;
        }

        //Función que regresa la dirección de memoria de la variable o constante
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
