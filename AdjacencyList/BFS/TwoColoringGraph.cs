using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdjacencyList
{
    public class TwoColoringGraph : BreadthFirstSearch
    {
        private readonly Color[] _colors;
        private bool _bipartite;

        public TwoColoringGraph(Color[] colors, State[] states, int?[] parrents)
            : base(states, parrents)
        {
            _colors = colors;
            Debug.Assert(_colors.Count() == _parrents.Count() && _parrents.Count() == States.Count());
        }

        public void TwoColor(Graph g)
        {
            Debug.Assert(_colors.All(c => c == Color.Uncolored));
            
            _bipartite = true;

            for (int i = 1; i <= g.NumOfvertices; i++)
            {
                if (_bipartite)
                {
                    if (States[i] == State.Undiscovered)
                    {
                        _colors[i] = Color.White;
                        States[i] = State.Discovered;
                        Bfs(g, i);
                    } 
                }
            }
        }

        protected override void ProcessEdge(int currentVertex, int adjecencyInfo)
        {
            if (_bipartite)
            {
                if (_colors[currentVertex] == _colors[adjecencyInfo])
                {
                    _bipartite = false;
                    Console.WriteLine("Graph is not Bipartite due to following Nodes: {0}------->{1}",
                        currentVertex, adjecencyInfo);
                }
                if (_colors[adjecencyInfo] == Color.Uncolored)
                {
                    _colors[adjecencyInfo] = ComplementColor(currentVertex);
                } 
            }
        }

        private Color ComplementColor(int currentVertex)
        {
            Debug.Assert(_colors[currentVertex] != Color.Uncolored);
            return _colors[currentVertex] == Color.White ? Color.Black : Color.White;
        }

        protected override void ProcessVertex(int currentVertex)
        {
          
        }

        public void PrintGraph()
        {
            for (int i = 0; i < _colors.Count(); i++)
            {
                Console.WriteLine(i + " " + _colors[i].ToString());
            }
        }
    }
}
