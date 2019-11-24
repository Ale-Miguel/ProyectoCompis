using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Shoot : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.SHOOT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "SHOOT, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
