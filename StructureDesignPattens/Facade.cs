using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.StructureDesignPattens
{
    /// <summary>
    /// 你可以让自己的代码独立于复杂子系统。
    ///外观可能成为与程序中所有类都耦合的上帝对象。
    ///适用于原有代码过于复杂重构时
    ///由于过于简单直接复制样例代码
    /// </summary>
    class Facade : IDesignPatten
    {
        public void Run()
        {
            Subsystem1 subsystem1 = new Subsystem1();
            Subsystem2 subsystem2 = new Subsystem2();
            FacadeFace facade = new FacadeFace(subsystem1, subsystem2);
            Console.WriteLine( facade.Operation() ); 
        }

        public class FacadeFace
        {
            protected Subsystem1 _subsystem1;

            protected Subsystem2 _subsystem2;

            public FacadeFace(Subsystem1 subsystem1, Subsystem2 subsystem2)
            {
                this._subsystem1 = subsystem1;
                this._subsystem2 = subsystem2;
            }

            // The Facade's methods are convenient shortcuts to the sophisticated
            // functionality of the subsystems. However, clients get only to a
            // fraction of a subsystem's capabilities.
            public string Operation()
            {
                string result = "Facade initializes subsystems:\n";
                result += this._subsystem1.operation1();
                result += this._subsystem2.operation1();
                result += "Facade orders subsystems to perform the action:\n";
                result += this._subsystem1.operationN();
                result += this._subsystem2.operationZ();
                return result;
            }
        }

        // The Subsystem can accept requests either from the facade or client
        // directly. In any case, to the Subsystem, the Facade is yet another
        // client, and it's not a part of the Subsystem.
        public class Subsystem1
        {
            public string operation1()
            {
                return "Subsystem1: Ready!\n";
            }

            public string operationN()
            {
                return "Subsystem1: Go!\n";
            }
        }

        // Some facades can work with multiple subsystems at the same time.
        public class Subsystem2
        {
            public string operation1()
            {
                return "Subsystem2: Get ready!\n";
            }

            public string operationZ()
            {
                return "Subsystem2: Fire!\n";
            }
        }
    }
}
