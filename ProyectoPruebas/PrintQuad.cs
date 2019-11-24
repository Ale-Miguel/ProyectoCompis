using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class PrintQuad : IQuadruple {

        Variable variable;

        public int getFirstParameter() {
            return OperationTypes.PRINT;
        }

        public Variable getFourthParameter() {
            return variable;
        }

        public string getQuadruple() {
            return "PRINT, _, _, " + variable.getName(); 
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public PrintQuad(Variable variable) {
            this.variable = variable;
        }
    }
}
