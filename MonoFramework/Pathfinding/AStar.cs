using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Pathfinding
{
    public class AStar
    {
        public const byte LINE_COST = 10;

        public List<Node> m_nodes;
        private Node m_start;
        private Node m_end;

        private SortedNodeList<Node> m_openList;

        private NodeList<Node> m_closeList;

        public AStar(IGrid map, int start, int end)
        {
            this.m_nodes = GetNodes(map);

            this.m_start = this.m_nodes.FirstOrDefault(x => x.CellId == start);
            this.m_end = this.m_nodes.FirstOrDefault(x => x.CellId == end);

            this.m_openList = new SortedNodeList<Node>();
            this.m_closeList = new NodeList<Node>();
        }
        public AStar(IGrid map, int start)
        {
            this.m_nodes = GetNodes(map);

            this.m_start = this.m_nodes.FirstOrDefault(x => x.CellId == start);

            this.m_openList = new SortedNodeList<Node>();
            this.m_closeList = new NodeList<Node>();
        }

        public ICell[] FindPath()
        {
            this.m_end.Walkable = true;
            this.m_openList.Add(this.m_start);

            while (this.m_openList.Count > 0)
            {
                var bestNode = this.m_openList.RemoveFirst();
                if (bestNode.CellId == this.m_end.CellId)
                {
                    var path = new List<ICell>();
                    while (bestNode.Parent != null && bestNode != this.m_start)
                    {
                        path.Add(bestNode.Cell);
                        bestNode = bestNode.Parent;
                    }
                    path.Reverse();

                    this.m_end.Walkable = false;
                    return path.ToArray();
                }
                this.m_closeList.Add(bestNode);
                this.AddToOpen(bestNode, this.GetNeighbors(bestNode));
            }
            this.m_end.Walkable = false;
            return new ICell[0];
        }
        public ICell[] FindPath(int target)
        {
            this.m_end = this.m_nodes.FirstOrDefault(x => x.CellId == target);
            this.m_end.Walkable = true;

            this.m_openList.Add(this.m_start);

            while (this.m_openList.Count > 0)
            {
                var bestNode = this.m_openList.RemoveFirst();
                if (bestNode.CellId == this.m_end.CellId)
                {
                    var path = new List<ICell>();
                    while (bestNode.Parent != null && bestNode != this.m_start)
                    {
                        path.Add(bestNode.Cell);
                        bestNode = bestNode.Parent;
                    }
                    path.Reverse();

                    this.m_end.Walkable = false;
                    return path.ToArray();
                }
                this.m_closeList.Add(bestNode);
                this.AddToOpen(bestNode, this.GetNeighbors(bestNode));
            }
            this.m_end.Walkable = false;
            return new ICell[0];
        }

        private List<Node> GetNeighbors(Node node)
        {
            var nodes = new List<Node>();

            foreach (var cell in node.Neighbors)
            {
                var n = this.m_nodes.FirstOrDefault(y => y.CellId == cell.Id);
                if (n != null && n.Walkable)
                {
                    if (n.Parent == null)
                        n.Parent = node;

                    nodes.Add(n);
                }
            }

            return nodes;
        }

        private Node GetBestNode()
        {
            this.m_openList.OrderBy(x => x.H);
            return this.m_openList.First();
        }
        public void PutEntities(int[] cells)
        {
            foreach (var cellId in cells)
            {
                var node = this.m_nodes.FirstOrDefault(x => x.CellId == cellId);
                if (node != this.m_start)
                    node.Walkable = false;
            }

        }

        public static List<Node> GetNodes(IGrid map)
        {
            var nodes = new List<Node>();
            for (int cellId = 0; cellId < map.GridSize.X * map.GridSize.Y; cellId++)
            {
                ICell cell = map.GetCell(cellId);
                var node = new Node(cell);
                nodes.Add(node);
            }
            return nodes;
        }

        private void AddToOpen(Node current, IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                if (!this.m_openList.Contains(node))
                {
                    if (!this.m_closeList.Contains(node))
                        this.m_openList.AddDichotomic(node);
                }
                else
                {
                    if (node.CostWillBe() < this.m_openList[node].G)
                        node.Parent = current;
                }
            }
        }
    }
}