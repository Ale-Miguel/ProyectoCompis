using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class EndProc : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.END_PROC;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
            return "END_PROC, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
