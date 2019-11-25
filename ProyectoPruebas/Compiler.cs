using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Compiler {

        private IVirtualMachine virtualMachine;
        private Scanner scanner;
        private Parser parser;
        private ErrorHandler errorHandler;

        public void compile(string fileName) {
            scanner = new Scanner(fileName);
            parser = new Parser(scanner);
            parser.Tab = new VarTable(parser);
            errorHandler = new ErrorHandler(this);
            parser.ErrorHandler = errorHandler;
            parser.Parse();
            Console.WriteLine(parser.errors.count + " errors found");
            virtualMachine.setQuadruples(parser.Tab.codeGenerator.getQuadrupleList());
            //Console.WriteLine(parser.errors.errMsgFormat);
        }
        public Compiler(IVirtualMachine virtualMachine) {
            this.virtualMachine = virtualMachine;
        }
        
        public void sendError(string errorMessage) {
            this.virtualMachine.setErrorMessage(errorMessage);
            parser.SemErr(errorMessage);
            
        }

    }
}
