using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class TurnRight : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.TURN_RIGHT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "TURN_RIGHT, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
