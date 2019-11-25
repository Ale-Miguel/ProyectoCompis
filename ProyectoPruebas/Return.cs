using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Return : IQuadruple {

        Variable returnVariable;
        public int getFirstParameter() {
            return OperationTypes.RETURN;
        }

        public Variable getFourthParameter() {
            return returnVariable;
        }

        public string getQuadruple() {
            return "RETURN, _, _, " + returnVariable.getName();
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Return(Variable variable) {
            if(variable != null) {
                returnVariable = variable;
            }
            else {
                returnVariable = new Variable("UNDEF_VARIABLE", OperationTypes.TYPE_UNDEFINED);
            }
            
        }
    }
}
