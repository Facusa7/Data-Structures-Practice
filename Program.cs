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

            //root.PrintInOrder();
            //root.PrintPreOder();
            //root.PrintPostOrder();
            var distance = Node.DistanceBetweenNodes.FindDistance(root, 7, 8);
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

            //var test = new MinimumStack();
            //test.Push(5);
            //test.Push(4);
            //test.Push(3);
            //test.Push(7);
            //test.Push(1);
            //Console.WriteLine($"the mininum value is: {test.Min()}");

            //var minHeap = new MinHeap();
            //minHeap.Add(10);minHeap.Add(8);
            //minHeap.Add(12); minHeap.Add(7);
            //minHeap.Add(9); minHeap.Add(11);
            //minHeap.Add(15);
            //Console.WriteLine($"the mininum value is: {minHeap.Peek()} {18%10}");
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

        public static class DistanceBetweenNodes
        {
            // Returns level of key k if it is present in tree, 
            // otherwise returns -1 
            public static int FindLevel(Node root, int k, int level) 
            { 
                // Base Case 
                if (root == null) 
                    return -1; 
          
                // If key is present at root, or in left subtree or right subtree, 
                // return true; 
                if (root.Data == k) 
                    return level; 
              
                var l = FindLevel(root.Left, k, level + 1); 
                return (l != -1)? l : FindLevel(root.Right, k, level + 1); 
            } 

            // Global static variable 
            static int d1 = -1; 
            static int d2 = -1; 
            static int dist = 0; 
            // This function returns pointer to LCA of two given values n1 and n2.  
            // It also sets d1, d2 and dist if one key is not ancestor of other 
            // d1 --> To store distance of n1 from root 
            // d2 --> To store distance of n2 from root 
            // lvl --> Level (or distance from root) of current node 
            // dist --> To store distance between n1 and n2 
             public static Node FindDistUtil(Node root, int n1, int n2, int lvl)
             { 
                // Base case 
                if (root == null) 
                    return null; 
          
                // If either n1 or n2 matches with root's key, report 
                // the presence by returning root (Note that if a key is 
                // ancestor of other, then the ancestor key becomes LCA 
                if (root.Data == n1){ 
                    d1 = lvl; 
                    return root; 
                } 
                if (root.Data == n2) 
                { 
                    d2 = lvl; 
                    return root; 
                } 
          
                // Look for n1 and n2 in left and right subtrees 
                var leftLca = FindDistUtil(root.Left, n1, n2,  lvl + 1); 
                var rightLca = FindDistUtil(root.Right, n1, n2,  lvl + 1); 
          
                // If both of the above calls return Non-NULL, then one key 
                // is present in once subtree and other is present in other, 
                // So this node is the LCA 
                if (leftLca != null && rightLca != null) 
                { 
                    dist = (d1 + d2) - 2*lvl; 
                    return root; 
                } 
          
                // Otherwise check if left subtree or right subtree is LCA 
                return (leftLca != null)? leftLca : rightLca;     
            } 
      
             // The main function that returns distance between n1 and n2 
             // This function returns -1 if either n1 or n2 is not present in 
             // Binary Tree. 
            public static int FindDistance(Node root, int n1, int n2)
            { 
                d1 = -1; 
                d2 = -1; 
                dist = 0; 
                var lca = FindDistUtil(root, n1, n2, 1); 
          
                // If both n1 and n2 were present in Binary Tree, return dist 
                if (d1 != -1 && d2 != -1) 
                    return dist; 
          
                // If n1 is ancestor of n2, consider n1 as root and find level  
                // of n2 in subtree rooted with n1 
                if (d1 != -1) 
                { 
                    dist = FindLevel(lca, n2, 0); 
                    return dist; 
                } 
          
                // If n2 is ancestor of n1, consider n2 as root and find level  
                // of n1 in subtree rooted with n2 
                if (d2 != -1) 
                { 
                    dist = FindLevel(lca, n1, 0); 
                    return dist; 
                } 
          
                return -1; 
            } 
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

    public class MinimumStack : Stack<int>
    {
        private readonly Stack<int> _stack2;
        public MinimumStack()
        { 
           _stack2 = new Stack<int>(); 
        }

        public new void Push(int value){
            if (value <= Min()) {
               _stack2.Push(value);
            }
            base.Push(value);
        }

        public new int Pop() {
           var value = base.Pop();
           if (value == Min()) {
              _stack2.Pop();
           }
           return value;
        }
        public int Min()
        {
           return _stack2.Count == 0 ? int.MaxValue : _stack2.Peek();
        }
    }

    public class MinHeap
    {
        private static int Capacity { get; set; } = 10;
        private int Size = 0;

        public int[] Items = new int[Capacity];

        private int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Size;
        }

        private bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Size;
        }

        private bool HasParent(int index)
        {
            return GetParentIndex(index) >= 0;
        }

        private int LeftChild(int index)
        {
            return Items[GetLeftChildIndex(index)];
        }

        private int RightChild(int index)
        {
            return Items[GetRightChildIndex(index)];
        }

        private int Parent(int index)
        {
            return Items[GetParentIndex(index)];
        }

        public int Peek()
        {
            if (Size == 0) { throw new Exception("Heap is empty"); }

            return Items[0];
        }

        public int Poll()
        {
            if (Size == 0) { throw new Exception("Heap is empty"); }

            var item = Items[0];
            Items[0] = Items[Size - 1];
            Size--;
            HeapifyDown();
            return item;
        }

        public void Add(int item)
        {
            EnsureExtraCapacity();
            Items[Size] = item;
            Size++;
            HeapifyUp();
        }

        private void EnsureExtraCapacity()
        {
            if (Size != Capacity) return;
            Array.Copy(Items, Items, Capacity * 2);
            Capacity *= 2;
        }

        private void HeapifyUp()
        {
            var index = Size - 1;
            while (HasParent(index) && Parent(index) > Items[index])
            {
               Swap(GetParentIndex(index), index);
               index = GetParentIndex(index);
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = Items[index1];
            Items[index1] = Items[index2];
            Items[index2] = temp;
        }

        private void HeapifyDown()
        {
            var index = 0;
            while (HasLeftChild(index))
            {
                var smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) < LeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (Items[index] < Items[smallerChildIndex])
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }
    }
}
