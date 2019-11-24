using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Interact : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.INTERACT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "INTERACT, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
