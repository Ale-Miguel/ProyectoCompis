using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Param : IQuadruple {

        Variable numberParam;
        Variable paramVariable;
        public int getFirstParameter() {
            return OperationTypes.PARAM;
        }

        public Variable getFourthParameter() {
            return paramVariable;
        }

        public string getQuadruple() {
            return "PARAM, " + numberParam.getName() + ", _, " + paramVariable.getName();
        }

        public Variable getSecondParameter() {
            return numberParam;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Param(int numberParam, Variable variable) {
            this.numberParam = new Variable("Parameter" + numberParam, OperationTypes.TYPE_INT, numberParam.ToString());
            this.paramVariable = variable;
        }
    }
}
