using System;
using C_InANutShell.Graphs;

namespace C_InANutShell.Testing
{
    public class GraphTest : ITest
    {
        public void Execute()
        {
            Graph g = new Graph(4);
     
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 3);

            System.Console.WriteLine(g.BreathFirstSearch(2));
        }
    }
}