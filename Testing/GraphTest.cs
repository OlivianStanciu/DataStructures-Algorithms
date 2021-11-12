using System;
using C_InANutShell.Graphs;

namespace C_InANutShell.Testing
{
    public class GraphTest : ITest
    {
        private int GetVertexForChar(char c)
        {
            return (int)(c - 'a');
        }
        
        private char GetVertexForChar(int v)
        {
            return (char)((int)'a' + v);
        }

        public void Execute()
        {
            Graph g = new Graph(10);
     
            g.AddEdge(GetVertexForChar('a'), GetVertexForChar('b'), 5);
            g.AddEdge(GetVertexForChar('a'), GetVertexForChar('e'), 1);
            g.AddEdge(GetVertexForChar('a'), GetVertexForChar('d'), 4);

            g.AddEdge(GetVertexForChar('b'), GetVertexForChar('d'), 2);
            g.AddEdge(GetVertexForChar('b'), GetVertexForChar('c'), 4);

            g.AddEdge(GetVertexForChar('c'), GetVertexForChar('h'), 4);
            g.AddEdge(GetVertexForChar('c'), GetVertexForChar('i'), 1);
            g.AddEdge(GetVertexForChar('c'), GetVertexForChar('j'), 2);

            g.AddEdge(GetVertexForChar('d'), GetVertexForChar('h'), 2);
            g.AddEdge(GetVertexForChar('d'), GetVertexForChar('g'), 11);
            g.AddEdge(GetVertexForChar('d'), GetVertexForChar('f'), 5);
            g.AddEdge(GetVertexForChar('d'), GetVertexForChar('e'), 2);

            g.AddEdge(GetVertexForChar('e'), GetVertexForChar('f'), 1);

            g.AddEdge(GetVertexForChar('f'), GetVertexForChar('g'), 7);

            g.AddEdge(GetVertexForChar('g'), GetVertexForChar('h'), 1);
            g.AddEdge(GetVertexForChar('g'), GetVertexForChar('i'), 4);

            g.AddEdge(GetVertexForChar('h'), GetVertexForChar('i'), 6);

            g.AddEdge(GetVertexForChar('i'), GetVertexForChar('j'), 0);

            var edges = g.MinimumSpanningTree();

            foreach (var edge in edges)
            {
                System.Console.WriteLine($"{GetVertexForChar(edge.FromVertex)} -> {GetVertexForChar(edge.ToVertex)} (weight: {edge.Weight})");
            }

            //System.Console.WriteLine(g.BreathFirstSearch(2));
        }
    }
}