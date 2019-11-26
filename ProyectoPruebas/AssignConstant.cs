using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class AssignConstant : IQuadruple {

        Variable constant;
        public int getFirstParameter() {
            return OperationTypes.ASSIGN_CONSTANT;
        }

        public Variable getFourthParameter() {
            return null;
        }

        public string getQuadruple() {
           if(constant == null) {
                return "ASSIGN_CONSTANT,  _ _,_";
            }


            return "ASSIGN_CONSTANT, " + constant.getName() + ", _,_";
        }

        public Variable getSecondParameter() {
            return constant;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public AssignConstant(Variable var) {
            this.constant = var;
        }
    }
}
