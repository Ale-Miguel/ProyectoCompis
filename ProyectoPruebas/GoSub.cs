using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GoSub : IQuadruple {

        Variable funcion;
        Variable dirDeInicio;
        public int getFirstParameter() {
            return OperationTypes.GOSUB;
        }

        public Variable getFourthParameter() {
            return dirDeInicio;
        }

        public string getQuadruple() {
            return "GOSUB, " + this.funcion.getName() + ", _, " + dirDeInicio.getValue();
        }

        public Variable getSecondParameter() {
            return this.funcion;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public GoSub(int direccion, Variable funcion) {
            this.funcion = funcion;
            this.dirDeInicio = new Variable("DireccionGoSub", OperationTypes.TYPE_INT, direccion.ToString());
        }
    }
}
