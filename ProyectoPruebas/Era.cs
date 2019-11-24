using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Era : IQuadruple {

        Variable activationRecord;
        Function funcion;
        public int getFirstParameter() {
            return OperationTypes.ERA;
        }

        public Variable getFourthParameter() {
            return activationRecord;
        }

        public string getQuadruple() {
           
            return "ERA, _, _, " + activationRecord.getName();
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Era(string name) {
            activationRecord = new Variable(name, OperationTypes.TYPE_UNDEFINED);
            funcion = null;
           
        }

        public Era(Variable variable) {
            activationRecord = variable;
            funcion = null;

        }

        public Era(Function funcion) {
            this.funcion = funcion;
            activationRecord = new Variable(funcion.getName(), funcion.getReturnType());
        }

    }
}
