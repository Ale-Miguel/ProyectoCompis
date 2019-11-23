using System;

namespace ProyectoPruebas
{
    class Program {
        static void Main(string[] args) {
            IVirtualMachine vm = new VirtualMachineImpl();
            Compiler compiler = new Compiler(vm);

            compiler.compile("ProgramaExpresiones.txt");
        }
    }
}
