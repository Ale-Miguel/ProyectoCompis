using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Wait : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.WAIT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "WAIT, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
