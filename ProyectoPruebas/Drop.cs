using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Drop : IQuadruple {
        public int getFirstParameter() {
            return OperationTypes.DROP;
        }

        public Variable getFourthParameter() {
           return null;
        }

        public string getQuadruple() {
            return "DROP, _, _, _";
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }
    }
}
