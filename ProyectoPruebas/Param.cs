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

        public Param(Variable functionParameter, Variable parameterVariable) {
            this.numberParam = functionParameter;
            this.paramVariable = parameterVariable;
        }

        public Param(int numberParam, Function funcion, Variable parametro) {
            this.numberParam = parametro;
            this.paramVariable = funcion.getParam(numberParam);
        }
    }
}
