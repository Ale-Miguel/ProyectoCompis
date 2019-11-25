using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class ErrorHandler {

        Compiler compiler;

        public ErrorHandler(Compiler compiler) {
            this.compiler = compiler;
        }

        public void SemErr(string errorMessage) {

            compiler.sendError(errorMessage);
            
        }

        
    }
}
