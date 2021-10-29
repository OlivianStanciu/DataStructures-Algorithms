using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using C_InANutShell.Testing;

namespace C_InANutShell
{
    class Program
    {
        static void Main(string[] args)
        {
             new TestsContainer()
                .AddTest(new DynamicArrayTest())
                .Execute();
        }
    }
}
