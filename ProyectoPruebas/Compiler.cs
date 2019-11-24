using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class Compiler {

        private IVirtualMachine virtualMachine;

        public void compile(string fileName) {
            Scanner scanner = new Scanner(fileName);
            Parser parser = new Parser(scanner);
            parser.Tab = new VarTable(parser);
            parser.Parse();
            Console.WriteLine(parser.errors.count + " errors found");
            virtualMachine.setQuadruples(parser.Tab.codeGenerator.getQuadrupleList());
            //Console.WriteLine(parser.errors.errMsgFormat);
        }
        public Compiler(IVirtualMachine virtualMachine) {
            this.virtualMachine = virtualMachine;
        }
        

    }
}
