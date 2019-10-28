using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class GoTo : Quadruple, Jumps{
        private int jumpTo;
        private int lineNumber;


        public string getQuadruple() {

            return lineNumber + " GoTo, " + jumpTo;
        }

        public void setJump(int line) {

            this.jumpTo = line;
        }

        public void setLineNumber(int line) {

            this.lineNumber = line -1;
        }

        public GoTo() {
            lineNumber = 0;
            jumpTo = 0;
            
        }

        public GoTo( int jump, int lineNumberVal) {
            
            this.lineNumber = lineNumberVal;
            this.jumpTo = jump -1;
        }

        public GoTo( int lineNumberVal) {
            this.lineNumber = lineNumberVal;
            this.jumpTo = 0;
        }
    }

}
