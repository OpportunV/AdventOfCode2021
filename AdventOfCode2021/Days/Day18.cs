using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day18
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            var nodes = lines.Select(line => Node.FromString(null, line)).ToList();
            var result = nodes.Aggregate(Node.Add);
            
            return result.Magnitude();
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var nodes = lines.Select(line => Node.FromString(null, line)).ToList();

            var max = nodes.DoubleIteration().Max(a =>
                Math.Max(Node.Add(a.first, a.second).Magnitude(), Node.Add(a.second, a.first).Magnitude()));
            return max;
        }
    }

    public class Node
    {
        private int Value { get; set; } = -1;
        private Node Parent { get; set; }
        private Node Left { get; set; }
        private Node Right { get; set; }
        
        private int Depth
        {
            get
            {
                if (Parent is null)
                {
                    return 1;
                }

                return Parent.Depth + 1;
            }
        }

        private bool CanSplit => Value != -1 && Value > 9;
        private bool CanExplode => Depth > 4 && Value == -1;
        
        public static Node Add(Node l, Node r)
        {
            var left = FromString(null, l.ToString());
            var right = FromString(null, r.ToString());

            var ret = new Node
            {
                Parent = null,
                Left = left,
                Right = right
            };
            left.Parent = ret;
            right.Parent = ret;
            ret.Reduce();
            return ret;
        }
        
        private void Explode()
        {
            var nodes = new Dictionary<int, Queue<Node>>();
            var rootPath = new HashSet<Node>();
            var visited = new HashSet<Node>();
            nodes[1] = new Queue<Node>();
            nodes[2] = new Queue<Node>();
            nodes[3] = new Queue<Node>();
            nodes[3].Enqueue(Parent);
            visited.Add(this);
            var current = this;
            while (current != null)
            {
                rootPath.Add(current);
                current = current.Parent;
            }

            while (nodes.Sum(pair => pair.Value.Count) > 0)
            {
                Node node = null;
                for (var i = 1; i <= 3; i++)
                {
                    if (nodes[i].Count > 0)
                    {
                        node = nodes[i].Dequeue();
                        break;
                    }
                }

                if (node is null)
                {
                    Console.WriteLine($"No nodes ");
                    break;
                }

                if (visited.Contains(node))
                {
                    continue;
                }

                visited.Add(node);

                if (node.Value != -1)
                {
                    node.Value += Left.Value;
                    break;
                }

                if (node.Right != null && !rootPath.Contains(node) && !visited.Contains(node.Right))
                {
                    nodes[1].Enqueue(node.Right);
                }

                if (node.Left != null && !visited.Contains(node.Left))
                {
                    nodes[2].Enqueue(node.Left);
                }

                if (node.Parent != null && !visited.Contains(node.Parent))
                {
                    nodes[3].Enqueue(node.Parent);
                }
            }

            nodes = new Dictionary<int, Queue<Node>>();
            visited = new HashSet<Node>();
            nodes[1] = new Queue<Node>();
            nodes[2] = new Queue<Node>();
            nodes[3] = new Queue<Node>();
            nodes[3].Enqueue(Parent);
            visited.Add(this);


            while (nodes.Sum(pair => pair.Value.Count) > 0)
            {
                Node node = null;
                for (var i = 1; i <= 3; i++)
                {
                    if (nodes[i].Count > 0)
                    {
                        node = nodes[i].Dequeue();
                        break;
                    }
                }

                if (node is null)
                {
                    Console.WriteLine($"No nodes ");
                    break;
                }
                
                if (visited.Contains(node))
                {
                    continue;
                }

                visited.Add(node);

                if (node.Value != -1)
                {
                    node.Value += Right.Value;
                    break;
                }

                if (node.Left != null && !rootPath.Contains(node) && !visited.Contains(node.Left))
                {
                    nodes[1].Enqueue(node.Left);
                }

                if (node.Right != null && !visited.Contains(node.Right))
                {
                    nodes[2].Enqueue(node.Right);
                }

                if (node.Parent != null && !visited.Contains(node.Parent))
                {
                    nodes[3].Enqueue(node.Parent);
                }
            }

            Value = 0;
            Left = null;
            Right = null;
        }
        
        private void Split()
        {
            if (!CanSplit)
            {
                Console.WriteLine("Not Splittable node");
                return;
            }

            Left = new Node
            {
                Value = Value / 2,
                Parent = this
            };

            Right = new Node
            {
                Value = (int) Math.Ceiling(Value / 2d),
                Parent = this
            };

            Value = -1;
        }
        
        private void Reduce()
        {
            bool didSomething;
            do
            {
                didSomething = false;
                var explodeNode = FindExplodeNode();
                var splitNode = FindSplitNode();
                if (explodeNode != null)
                {
                    explodeNode.Explode();
                    didSomething = true;
                    continue;
                }
                if (splitNode != null)
                {
                    splitNode.Split();
                    didSomething = true;
                }

            } while (didSomething);
        }
        
        private Node FindSplitNode()
        {
            var canSplitLeft = Left?.FindSplitNode();
            if (canSplitLeft != null)
            {
                return canSplitLeft;
            }
            if (CanSplit)
            {
                return this;
            }

            var canSplitRight = Right?.FindSplitNode();
            return canSplitRight;
        }

        private Node FindExplodeNode()
        {
            var canExplodeLeft = Left?.FindExplodeNode();
            if (canExplodeLeft != null)
            {
                return canExplodeLeft;
            }
            if (CanExplode)
            {
                return this;
            }

            var canExplodeRight = Right?.FindExplodeNode();
            return canExplodeRight;
        }
        
        public static Node FromString(Node parent, string str)
        {
            str = str.Replace(" ", "");
            var root = new Node
            {
                Parent = parent
            };
            if (str.Contains(','))
            {
                str = str.Substring(1, str.Length - 2);
                var midPoint = FindMidPoint(str);
                root.Left = FromString(root, str.Substring(0, midPoint));
                root.Right = FromString(root, str.Substring(midPoint + 1));
            }
            else
            {
                root.Value = int.Parse(str);
            }
            
            return root;
        }
        
        private static int FindMidPoint(string str)
        {
            var lowestIdx = -1;
            var lowestDepth = int.MaxValue;
            var currentDepth = 0;
            for (var i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '[':
                        currentDepth++;
                        break;
                    case ']':
                        currentDepth--;
                        break;
                    case ',':
                    {
                        if (currentDepth < lowestDepth)
                        {
                            lowestDepth = currentDepth;
                            lowestIdx = i;
                        }

                        break;
                    }
                }
            }
            
            return lowestIdx;
        }
        
        public override string ToString()
        {
            if (Value == -1)
            {
                return $"[{Left}, {Right}]";
            }
            
            return Value.ToString();
        }

        public int Magnitude()
        {
            if (Value != -1)
            {
                return Value;
            }

            return 3 * Left.Magnitude() + 2 * Right.Magnitude();
        }
    }
}

