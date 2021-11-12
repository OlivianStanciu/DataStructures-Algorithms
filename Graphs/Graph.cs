using System;
using System.Collections.Generic;
using C_InANutShell.Unions;

namespace C_InANutShell.Graphs
{
    public class Edge : IComparable<Edge>
    {
        public int FromVertex { get; set; }
        public int ToVertex { get; set; }
        public int Weight { get; set; }
        public int CompareTo(Edge other) => 
            this.Weight == other.Weight ? 0 : this.Weight < other.Weight ? -1 : 1;
    }
    public class Graph
    {
        private LinkedList<int>[] _adjacencyList; //stores the adjacencyList
        private List<Edge> _edges; //stores the edges to mst

        public int NumberOfVertices {get;}

        public Graph(int nrOfvertices)
        {
            this.NumberOfVertices = nrOfvertices;
            
            _adjacencyList = new LinkedList<int>[nrOfvertices];
            _edges = new List<Edge>();

            for (int vertex = 0; vertex < nrOfvertices; vertex++)
            {
                _adjacencyList[vertex] = new LinkedList<int>();
            }
        }

        public void AddEdge(int fromVertex, int toVertex, int weight)
        {
            _adjacencyList[fromVertex].AddLast(toVertex);
            _edges.Add(new Edge() 
            {
                FromVertex = fromVertex,
                ToVertex = toVertex,
                Weight = weight
            });
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

        public List<Edge> MinimumSpanningTree()
        {   
            List<Edge> visitedEdges = new List<Edge>();

            int[] vertices = new int[this.NumberOfVertices];
            for (int i = 0; i < this.NumberOfVertices; i++)
            {
                vertices[i] = i;
            }
            
            //ascending sort, since Edge.CompareTo() has standard Comparer
            _edges.Sort(); 

            //Kruskall's minimum spanning tree using an UnionFind data structure, for connectiong the vertices in groups
            UnionFind<int> unionFind = new UnionFind<int>(vertices); 

            //parse de edges ascending; from Min Weight to Max Weight
            for (int i = 0; i < _edges.Count; i++)
            {
                //add the two nodes of the edge to the same group, if the nodes are not already connected
                if (!unionFind.Find(_edges[i].FromVertex, _edges[i].ToVertex))
                {
                    unionFind.Union(_edges[i].FromVertex, _edges[i].ToVertex);
                    visitedEdges.Add(_edges[i]);
                }
                
                //all the nodes were connected, therefore the unionFind should have exactly one component
                if(unionFind.ComponentsCount == 1)
                {
                    return visitedEdges;
                }
            }
            
            //no mst found
            return null;
        }
    }
}