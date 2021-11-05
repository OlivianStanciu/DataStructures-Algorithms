using System;
using System.Collections.Generic;

namespace C_InANutShell.Graphs
{
    public class Graph
    {
        private LinkedList<int>[] _adjacencyList;
        public int NumberOfVertices => _adjacencyList.Length;

        public Graph(int nrOfvertices)
        {
            _adjacencyList = new LinkedList<int>[nrOfvertices];
            for (int vertex = 0; vertex < nrOfvertices; vertex++)
            {
                _adjacencyList[vertex] = new LinkedList<int>();
            }
        }

        public void AddEdge(int fromVertex, int toVertex)
        {
            _adjacencyList[fromVertex].AddLast(toVertex);
            //_adjacencyList[toVertex].AddLast(fromVertex);
        }

        public string BreathFirstSearch(int fromVertex)
        {
            bool[] visited = new bool[this.NumberOfVertices];
            
            Queue<int> processingQueue = new Queue<int>();
            
            processingQueue.Enqueue(fromVertex);
            visited[fromVertex] = true;

            var strSequence = "";
            while(processingQueue.TryDequeue(out int vertex))
            {
                strSequence += $"{vertex} -> ";

                var enumerator = _adjacencyList[vertex].GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!visited[enumerator.Current])
                    {
                        processingQueue.Enqueue(enumerator.Current);
                        visited[enumerator.Current] = true;
                    }
                }
                visited[vertex] = true;
            }

            return strSequence.Contains(" -> ") ? 
                strSequence.Remove(strSequence.Length - " -> ".Length, " -> ".Length) :
                strSequence;
        }
    }
}