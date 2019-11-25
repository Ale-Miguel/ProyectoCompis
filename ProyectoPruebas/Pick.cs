using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Pick : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.PICK;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "PICK, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
