using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Return : IQuadruple {

        Variable returnVariable;
        Variable returnValue;
        public int getFirstParameter() {
            return OperationTypes.RETURN;
        }

        public Variable getFourthParameter() {
            return returnVariable;
        }

        public string getQuadruple() {
            if(returnVariable == null) {
                return "RETURN, " + returnValue.getName() + ", _, " + " UNDEF";
            }
            return "RETURN, " + returnValue.getName() + ", _, " + returnVariable.getName();
        }

        public Variable getSecondParameter() {
            return returnValue;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Return(Variable returnVariable, Variable returnValue) {

            this.returnValue = returnValue;

            this.returnVariable = returnVariable;
            
        }

        public Return(Variable returnValue) {
            this.returnVariable = null;
            this.returnValue = returnValue;
        }
    }
}
