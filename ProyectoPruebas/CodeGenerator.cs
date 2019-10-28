using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    interface CodeGenerator {
        void pushSymbolStack(Variable variable);
        void pushOperatorStack(int operatorValue);

        void pushGoToF(Variable var);

        void createIntermediateCode(int op, Variable var1, Variable var2);

        bool createIntermediateCodeNoTemp(int op, Variable var1, Variable var2);

         bool solveMultAndDiv();
        bool solveSumAndMinus();
        bool solveAssignment();
        bool solveRelOp();

        Variable popSymnbolStack();

        int popOperatorStack();

        int getTopOperatorStack();

        void popJumpStack();
    }
}
