using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class AddressManager {
        //Constante con la dirección de inicio de las variables
        public const int START_GLOBAL_ADDRESS = 1000;

        //Direcciones default de las constantes
        public const int STARTING_ADDRESS = 1000;
        private int constIntAddress = 1000;
        private int constFloatAddress = 2000;
        private int constStringAddress = 3000;
        private int maxConstAddress = 3999;

        //Direcciones default de las variables globales
        private int globalIntVarAddress = 4000;
        private int globalFloatVarAddress = 5000;
        private int globalBoolVarAddress = 6000;
        private int globalStringVarAddress = 7000;
        private int maxGlobalAddress = 7999;

        private Stack localAddressStack;

        //Función que inicializa las direcciones de las constantes
        void initConstantAddress() {

            //Dirección inicial de las constantes
            int addressCont = STARTING_ADDRESS;

            constIntAddress = addressCont;

            //Si se quiere cambiar la cantidad de constantes enteras, modificar esta suma
            addressCont += 1000;

            constFloatAddress = addressCont;

            //Si se quiere cambiar la cantidad de constantes flotantes, modificar esta suma
            addressCont += 1000;

            constStringAddress = addressCont;

            //Si se quiere cambiar la cantidad de constantes string, modificar esta suma
            addressCont += 1000;

            maxConstAddress = addressCont - 1;


        }

        //Función que inicializa las direcciones de las variables globales
        void initGlobalVariablesAddress() {

            //Dirección inicial de las variables locales
            int addressCont = maxConstAddress + 1;

            globalIntVarAddress = addressCont;

            //Si se quiere cambiar la cantidad de variables globales enteras, modificar esta suma
            addressCont += 1000;

            globalFloatVarAddress = addressCont;

            //Si se quiere cambiar la cantidad de variables globales flotantes, modificar esta suma
            addressCont += 1000;

            globalStringVarAddress = addressCont;

            //Si se quiere cambiar la cantidad de variables globales string, modificar esta suma
            addressCont += 1000;

            globalBoolVarAddress = addressCont;

            //Si se quiere cambiar la cantidad de variables globales booleanas, modificar esta suma
            addressCont += 1000;

            maxGlobalAddress = addressCont - 1;


        }

        //Función que crea un nuevo elemento de la pila de direcciones locales
        void createNewLocalAddress() {

            LocalAddressManager newLocalAddress = new LocalAddressManager(maxGlobalAddress + 1);

            localAddressStack.Push(newLocalAddress);
        }


        public int getConstIntAddress(){

            constIntAddress++;

            return constIntAddress - 1;
        }

        public int getConstFloatAddress(){

            constFloatAddress++;

            return constFloatAddress - 1;
        }

        public int getConstStringAddress(){

            constStringAddress++;

            return constStringAddress - 1;
        }

        public int getGlobalIntVarAddress(){

            globalIntVarAddress++;

            return globalIntVarAddress -1;
        }

        public int getGlobalFloatVarAddress(){

            globalFloatVarAddress++;

            return globalFloatVarAddress -1;
        }

        public int getGlobalBoolVarAddress(){

            globalBoolVarAddress++;

            return globalBoolVarAddress -1;
        }

        public int getGlobalStringVarAddress(){

            globalStringVarAddress++;

            return globalStringVarAddress -1;
        }
       
        public int getIntAddress(){

            if(localAddressStack.Count == 0){
                createNewLocalAddress();
            }

            LocalAddressManager localAddr = (LocalAddressManager)localAddressStack.Peek();
            return localAddr.getIntAddress();
        }

        public int getFloatAddress(){

            if(localAddressStack.Count == 0){
                createNewLocalAddress();
            }

            LocalAddressManager localAddr = (LocalAddressManager)localAddressStack.Peek();
            return localAddr.getFloatAddress();
        }

        public int getBoolAddress(){

            if(localAddressStack.Count == 0){
                createNewLocalAddress();
            }

            LocalAddressManager localAddr = (LocalAddressManager)localAddressStack.Peek();
            return localAddr.getBoolAddress();
        }

        public int getStringAddress(){

            if(localAddressStack.Count == 0){
                createNewLocalAddress();
            }

            LocalAddressManager localAddr = (LocalAddressManager)localAddressStack.Peek();
            return localAddr.getStringAddress();
        }

        
        public AddressManager() {

            localAddressStack = new Stack();

            initConstantAddress();
            initGlobalVariablesAddress();
            createNewLocalAddress();
        }
    }
}
