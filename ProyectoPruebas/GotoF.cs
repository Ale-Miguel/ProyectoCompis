using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GotoF : Quadruple, Jumps {

        private int jumpTo;
        private int lineNumber;
        private Variable variable;

        public string getQuadruple() {

            return lineNumber + " GoToFalse, "+ variable.getName() + ", " + jumpTo + "\n";
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
