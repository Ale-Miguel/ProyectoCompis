using System;

namespace ProyectoPruebas
{
    class Program
    {
        static void Main(string[] args)
        {

            Scanner scanner = new Scanner("Programa.txt");
            Parser parser = new Parser(scanner);
            parser.Tab = new VarTable(parser);
            parser.Parse();
            Console.WriteLine(parser.errors.count + " errors found");
            //Console.WriteLine(parser.errors.errMsgFormat);
        }
    }
}
