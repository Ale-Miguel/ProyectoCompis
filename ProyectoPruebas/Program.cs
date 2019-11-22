using System;

namespace ProyectoPruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            IVirtualMachine vr = new VirtualMachineImpl();
            Compiler compiler = new Compiler(vr);

            compiler.compile("ProgramaExpresiones.txt");
        }
    }
}
