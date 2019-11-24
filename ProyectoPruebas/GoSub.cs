using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GoSub : IQuadruple {

        Variable funcionVar;
        Variable dirDeInicio;

        Function funcion;
        public int getFirstParameter() {
            return OperationTypes.GOSUB;
        }

        public Variable getFourthParameter() {
            return dirDeInicio;
        }

        public string getQuadruple() {
            return "GOSUB, " + this.funcionVar.getName() + ", _, " + dirDeInicio.getValue();
        }

        public Variable getSecondParameter() {
            return this.funcionVar;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public GoSub(int direccion, Variable funcion) {
            this.funcionVar = funcion;
            this.dirDeInicio = new Variable("DireccionGoSub", OperationTypes.TYPE_INT, direccion.ToString());
            this.funcion = null;
        }

        public GoSub(Function funcion) {

            this.funcion = funcion;

            dirDeInicio = new Variable(funcion.getStartsIn().ToString(), funcion.getReturnType(), funcion.getStartsIn().ToString());
            funcionVar = funcion.getReturnVariable();
        }
    }
}
