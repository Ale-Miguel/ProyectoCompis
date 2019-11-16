using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class LocalAddressManager {

        //Direcciones default de las variables locales
        private int localIntVarAddress = 8000;
        private int localFloatVarAddress = 9000;
        private int localBoolVarAddress = 10000;
        private int localStringVarAddress = 11000;
        private int maxlocalAddress = 11999;

        public int getIntAddress(){
            //Se incrementa el contador
            localIntVarAddress++;

            //Se compenza el incremento, ya que se quiere la actual dirección disponible
            //no la siguiente
            return localIntVarAddress - 1;
        }

        public int getFloatAddress(){
            //Se incrementa el contador
            localFloatVarAddress++;

            //Se compenza el incremento, ya que se quiere la actual dirección disponible
            //no la siguiente
            return localFloatVarAddress - 1;
        }

        public int getBoolAddress(){
            //Se incrementa el contador
            localBoolVarAddress++;

            //Se compenza el incremento, ya que se quiere la actual dirección disponible
            //no la siguiente
            return localBoolVarAddress - 1;
        }

        public int getStringAddress(){
            //Se incrementa el contador
            localStringVarAddress++;

            //Se compenza el incremento, ya que se quiere la actual dirección disponible
            //no la siguiente
            return localStringVarAddress - 1;
        }

        public LocalAddressManager(int startingAddress) {

            int addressCont = startingAddress;

            localIntVarAddress = addressCont;

            //Para definir la cantidad de variables locales enteras, modificar esta suma
            addressCont += 1000;

            localFloatVarAddress = addressCont;

            //Para definir la cantidad de variables locales flotantes, modificar esta suma
            addressCont += 1000;

            localBoolVarAddress = addressCont;

            //Para definir la cantidad de variables locales booleanas, modificar esta suma
            addressCont += 1000;

            localStringVarAddress = addressCont;

            //Para definir la cantidad de variables locales string, modificar esta suma
            addressCont += 1000;

            maxlocalAddress = addressCont;
        }
    }
}
