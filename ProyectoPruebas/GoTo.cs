using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GoTo : IQuadruple, Jumps{
        private int jumpTo;
        private int lineNumber;


        public string getQuadruple() {

            return lineNumber + " GoTo, _, _, " + jumpTo;
        }

        public void setJump(int line) {

            this.jumpTo = line;
        }

        public void setLineNumber(int line) {

            this.lineNumber = line -1;
        }

        public int getFirstParameter() {
            return OperationTypes.GOTO;
        }

        public Variable getSecondParameter() {
            return null;
        }

        public Variable getThirdParameter() {
            return null;
        }

        public Variable getFourthParameter() {
            return new Variable("GoTo", Variable.ADDRESS_NOT_DEFINED, jumpTo.ToString());
        }

        public GoTo() {
            lineNumber = 0;
            jumpTo = 0;
            
        }

        public GoTo( int jump, int lineNumberVal) {
            
            this.lineNumber = lineNumberVal;
            this.jumpTo = jump;
        }

        public GoTo( int lineNumberVal) {
            this.lineNumber = lineNumberVal;
            this.jumpTo = 0;
        }
    }

}
