using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GotoF : IQuadruple, Jumps {

        private int jumpTo;
        private int lineNumber;
        private Variable variable;

        public string getQuadruple() {

            return lineNumber + " GoToFalse, "+ variable.getName() + ", " + jumpTo;
        }

        public void setJump(int line) {

            this.jumpTo = line + 1;
        }

        public void setVariable(Variable var) {

            this.variable = var;
        }

        public void setLineNumber(int line) {

            this.lineNumber = line;
        }

        public int getFirstParameter() {
            return OperationTypes.GOTO_F;
        }

        public Variable getSecondParameter() {
            return variable;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Variable getFourthParameter() {
            return new Variable("GoToF", Variable.ADDRESS_NOT_DEFINED, jumpTo.ToString());
        }

        public GotoF() {
            lineNumber = 0;
            jumpTo = 0;
            variable = new Variable("__", 0);
        }

        public GotoF(Variable var, int jump, int lineNumberVal) {
            this.variable = var;
            this.lineNumber = lineNumberVal;
            this.jumpTo = jump + 1;
        }

        public GotoF(Variable var, int lineNumberVal) {
            this.variable = var;
            this.lineNumber = lineNumberVal;
            this.jumpTo = 0;
        }
    }
}
