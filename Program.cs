using DesignPatten.BehaviourPattens;
using DesignPatten.CreationalDesignPatterns;
using DesignPatten.StructureDesignPattens;
using System;
using System.Data;
using System.Net.Sockets;

namespace DesignPatten
{
    class Program
    {
        static void Main(string[] args)
        {
            IDesignPatten designPatten = new MediatorPatten();
            designPatten.Run();
            

        }
    }
}
