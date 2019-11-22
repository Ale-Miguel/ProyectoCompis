using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    class VirtualMachineImpl : IVirtualMachine {
        public void setErrorMessage(string errorMessage) {
            Console.WriteLine(errorMessage);
        }

        public void setQuadruples(List<IQuadruple> quadruples) {
            Console.WriteLine("Quadruples Recieved");
        }

        public VirtualMachineImpl() {

        }
    }
}
