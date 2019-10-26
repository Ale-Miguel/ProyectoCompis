using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class OperationQuadruple : Quadruple {

        private Variable variable1;
        private Variable variable2;
        private Variable resultVariable;

        private int operatorNumber;
        private int lineNumber;

        public string getQuadruple() {

            return lineNumber + " " + operatorNumber + ", " + variable1.getName() + ", " + variable2.getName() + ", " + resultVariable.getName() + "\n";

        }

        public void setLineNumber(int lineNumberVal) {

            this.lineNumber = lineNumberVal;

        }

        public void setOperator(int operatorValue) {

            this.operatorNumber = operatorValue;

        }

        public void setVariables(Variable var1, Variable var2, Variable resultVar) {

            this.variable1 = var1;
            this.variable2 = var2;
            this.resultVariable = resultVar;

        }

        public void setVariables(Variable var1, Variable resultVar) {

            this.variable1 = var1;
            this.resultVariable = resultVar;

        }

        public OperationQuadruple() {

            //Guiones bajos para simbolizar que no se utiliza
            //-1 como valor para simbolizar que no se ha asignado

            this.variable1 = new Variable("__", -1);          
            this.variable2 = new Variable("__", -1);
            this.resultVariable = new Variable("__", -1);
            this.operatorNumber = -1;      
            this.lineNumber = -1;

        }

        public OperationQuadruple(Variable var1, Variable var2, Variable resultVar, int operationValue, int lineNumberVal) {

            //Guiones bajos para simbolizar que no se utiliza
            //-1 como valor para simbolizar que no se ha asignado

            this.variable1 = var1;
            this.variable2 = var2;
            this.resultVariable = resultVar;
            this.operatorNumber = operationValue;
            this.lineNumber = lineNumberVal;

        }

        public OperationQuadruple(Variable var1, Variable resultVar, int operationValue, int lineNumberVal) {

            //Guiones bajos para simbolizar que no se utiliza
            //-1 como valor para simbolizar que no se ha asignado

            this.variable1 = var1;
            this.variable2 = new Variable("__", 0);
            this.resultVariable = resultVar;
            this.operatorNumber = operationValue;
            this.lineNumber = lineNumberVal;

        }
    }
}
