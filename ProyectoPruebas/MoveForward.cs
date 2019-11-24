using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class MoveForward : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.MOVE_FORWARD;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "MOVE_FORWARD, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
