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
            if(returnVariable == null && returnValue != null) {
                return "RETURN, " + returnValue.getName() + ", _, " + " UNDEF";
            }
            else if(returnValue != null) {
                return "RETURN, " + returnValue.getName() + ", _, " + returnVariable.getName();
            }

            return "RETURN, " + "UNDEF" + ", _, " + "UNDEF";

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

        public Return(Function funcion, Variable returnValue) {
            this.returnVariable = funcion.getReturnVariable();
            this.returnValue = returnValue;
        }
    }
}
