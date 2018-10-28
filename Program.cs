using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nodeLeaf7 = new Node(7);
            var nodeLeaf9 = new Node(9);
            var nodeLeaf11 = new Node(11);
            var nodeLeaf15 = new Node(15);

            var node8 = new Node(8, nodeLeaf7, nodeLeaf9);
            var node12 = new Node(12, nodeLeaf11, nodeLeaf15);
            
            var root = new Node(10, node8, node12);

            root.PrintInOrder();
            //root.PrintPreOder();
            //root.PrintPostOrder();

            var maximumWidth = root.MaximumWidth(root);
            Console.WriteLine($"The vertical sum is: {maximumWidth}");
            //var graphNode = new Graph.Node(2);
            //var secondNode = new Graph.Node(3);
            //var thirdNode = new Graph.Node(4);
            //var fourthNode = new Graph.Node(5);
            //var fifthNode = new Graph.Node(6);
            //var sixthNode = new Graph.Node(7);
            //var seventhNode = new Graph.Node(8);
            //var eighthNode = new Graph.Node(9);
            //var ninethNode = new Graph.Node(10);
            //var tenthNode = new Graph.Node(11);
            //var eleventhNode = new Graph.Node(12);

            //graphNode.Adjacent.AddFirst(secondNode);
            //graphNode.Adjacent.AddLast(thirdNode);

            //secondNode.Adjacent.AddFirst(fourthNode);
            //secondNode.Adjacent.AddLast(fifthNode);
            //secondNode.Adjacent.AddLast(sixthNode);

            //thirdNode.Adjacent.AddFirst(seventhNode);
            //thirdNode.Adjacent.AddLast(eighthNode);

            //fourthNode.Adjacent.AddFirst(tenthNode);
            //fifthNode.Adjacent.AddFirst(tenthNode);

            //sixthNode.Adjacent.AddFirst(eleventhNode);

            //seventhNode.Adjacent.AddFirst(ninethNode);
            //eighthNode.Adjacent.AddFirst(ninethNode);

            //var graph = new Graph();
            //graph.NodeLookup.Add(2, graphNode);
            //graph.NodeLookup.Add(3, secondNode);
            //graph.NodeLookup.Add(4, thirdNode);
            //graph.NodeLookup.Add(5, fourthNode);
            //graph.NodeLookup.Add(6, fifthNode);
            //graph.NodeLookup.Add(7, sixthNode);
            //graph.NodeLookup.Add(8, seventhNode);
            //graph.NodeLookup.Add(9, eighthNode);
            //graph.NodeLookup.Add(10, ninethNode);
            //graph.NodeLookup.Add(11, tenthNode);
            //graph.NodeLookup.Add(12, eleventhNode);

            //graph.HasPathBFS(graphNode, ninethNode);
            //if (!graph.HasPathDFS(2, 10))
            //{
            //    Console.WriteLine($"Couldn't find destination");
            //}

            Console.ReadKey();

        }
    }

    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public List<Node> Adjacents { get; set; }

        public Node(int value, Node left = null, Node right = null)
        {
            Data = value;
            Left = left;
            Right = right;
        }

        public void Insert(int value)
        {
            if (value < this.Data)
            {
                if (this.Left == null)
                {
                    Left = new Node(value);
                }
                else
                {
                    Left.Insert(value);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new Node(value);
                }
                else
                {
                    Right.Insert(value);
                }
            }
        }

        public bool Contains(int value)
        {
            if (Data == value)
            {
              return true;
            } else if (value < Data)
            {
                if (Left == null)
                {
                    return false;
                }

                Left.Contains(value);
            } else if(value > Data)
            {
                if (Right == null)
                {
                    return false;
                }

                Right.Contains(value);
            }

            return false;
        }

        public void PrintInOrder()
        {
            Left?.PrintInOrder();
            Console.WriteLine($"Node: {Data}");
            Right?.PrintInOrder();
        }

        public void PrintPreOder()
        {
            Console.WriteLine($"Node: {Data}");
            Left?.PrintPreOder();
            Right?.PrintPreOder();
        }

        public void PrintPostOrder()
        {
            Left?.PrintPostOrder();
            Right?.PrintPostOrder();
            Console.WriteLine($"Node: {Data}");

        }

        public Node ClosestLeafNode(Node root, Node target)
        {
            if (root == null || target == null)
            {
                return null;
            }

            var mapping = new Dictionary<Node, LinkedList<Node>>();
            BuildAdjList(ref mapping, root, null);

            var queue = new Queue<Node>();
            var visited = new List<Node>();

            foreach (var node in mapping.Keys)
            {
                if (node != null && node == target)
                {
                    queue.Enqueue(node);
                    visited.Add(node);
                    break;
                }
            }

            while (queue.Count>0) {
                var node = queue.Dequeue();
                if (node != null) {

                    if (mapping[node].Count <= 1) {
                        return node;
                    }

                    foreach (var adjacent in mapping[node])
                    {
                        if (!visited.Contains(adjacent))
                        {
                            visited.Add(adjacent);
                            queue.Enqueue(adjacent);
                        }
                    }
                }
            }
            return null;
        }

        private void BuildAdjList(ref Dictionary<Node, LinkedList<Node>> mapping, Node node, Node parent)
        {
            if (!mapping.ContainsKey(node)) {
                mapping.Add(node, new LinkedList<Node>());
            }

            if (parent != null)
            {
                if (!mapping.ContainsKey(parent)) {
                    mapping.Add(parent, new LinkedList<Node>());
                }

                if (mapping.TryGetValue(parent, out var nodes))
                {
                    nodes.AddLast(node);
                }
            }

            if (node.Left != null) {
                BuildAdjList(ref mapping, node.Left, node);
            }

            if (node.Right != null) {
                BuildAdjList(ref mapping, node.Right, node);
            }
        }

        public int VerticalSum(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.Left != null && node.Right != null)
            {
                return node.Data + VerticalSum(node.Left.Right) + VerticalSum(node.Right.Left);
            }
            else
            {
                return node.Data;
            }
        }

        public Dictionary<int, int> VerticalSum(Node node, int level, Dictionary<int, int> verticalSum)
        {
            if (verticalSum == null)
            {
                verticalSum = new Dictionary<int, int>();
                level = 0;
                verticalSum.Add(level, node.Data);
            }

            if (node.Left != null)
            {
                level -= 1;
                if (!verticalSum.ContainsKey(level))
                {
                    verticalSum.Add(level, node.Left.Data);
                }
                else
                {
                    verticalSum[level] += node.Left.Data;
                }

                VerticalSum(node.Left, level, verticalSum);
            }
            
            if (node.Right != null)
            {
                level += 2;
                if (!verticalSum.ContainsKey(level))
                {
                    verticalSum.Add(level, node.Right.Data);
                }
                else
                {
                    verticalSum[level] += node.Right.Data;
                }
                VerticalSum(node.Right, level, verticalSum);
            }

            return verticalSum;
        }

        public int MaximumWidth(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            var queue = new Queue<Node>();
            queue.Enqueue(node);
            var maxCount = queue.Count();
            var currentCount = 1;

            while (queue.Count > 0)
            {
                for (int i = 0; i < currentCount; i++)
                {
                    var currentNode = queue.Dequeue();
                    if (currentNode.Left != null)
                    {
                        queue.Enqueue(currentNode.Left);
                    }
                    if (currentNode.Right != null)
                    {
                        queue.Enqueue(currentNode.Right);
                    }
                }

                currentCount = queue.Count;
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                }
            }

            return maxCount;
        }
    }

    public class Graph
    {
        public class Node
        {
            public int Id { get; set; }
            public LinkedList<Node> Adjacent = new LinkedList<Node>();

            public Node(int id)
            {
                this.Id = id;
            }
        }

        public Dictionary<int, Node> NodeLookup = new Dictionary<int, Node>();

        private Node GetNode(int id)
        {
            return NodeLookup.TryGetValue(id, out var node) ? node : null;
        }

        public void AddEdge(int source, int destination)
        {
            var s = GetNode(source);
            var d = GetNode(destination);
            s.Adjacent.AddLast(d);
        }

        public bool HasPathDFS(int source, int destination)
        {
            var s = GetNode(source);
            var d = GetNode(destination);
            var visited = new HashSet<int>();
            return HasPathDFS(s, d, visited);
        }

        private bool HasPathDFS(Node source, Node destination, HashSet<int> visited)
        {
            if (visited.Contains(source.Id))
            {
                return false;
            }

            visited.Add(source.Id);
            Console.WriteLine($"Visited: {source.Id}");

            if (source == destination)
            {
                Console.WriteLine($"Found destination: {source.Id}");

                return true;
            }

            foreach (var node in source.Adjacent)
            {
                if (HasPathDFS(node, destination, visited))
                {
                    return true;
                }
            }
            
            return false; 
        }

        public bool HasPathBFS(Node source, Node destination)
        {
            var visited = new HashSet<int>();
            var children = new Queue<Node>();
            children.Enqueue(source);
            while (children.Count > 0)
            {
                var node = children.Dequeue();
                if (node == destination)
                {
                    Console.WriteLine($"Found destination: {node.Id}");

                    return true;
                }
                if (visited.Contains(node.Id))
                {
                    continue;
                }
                visited.Add(node.Id);
                Console.WriteLine($"Visited: {node.Id}");

                foreach (var child in node.Adjacent)
                {
                    children.Enqueue(child);
                }
            }

            Console.WriteLine($"Couldn't find destination");

            return false; 
        }
    }

}
