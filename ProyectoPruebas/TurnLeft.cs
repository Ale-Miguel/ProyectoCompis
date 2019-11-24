using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class TurnLeft : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.TURN_LEFT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "TURN_LEFT, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
