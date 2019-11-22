using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPruebas {
    interface IVirtualMachine {

        void setQuadruples(List<IQuadruple> quadruples);

        void setErrorMessage(string errorMessage);
    }
}
