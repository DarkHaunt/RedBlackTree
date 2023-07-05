using RedBlackTreeRealisation.Nodes;
using System;

namespace RedBlackTreeRealisation.Nullables
{
    public class NullNode : INode
    {
        public Color Color => Color.Black;
        public float Value => float.NaN;
        public bool IsNull => true;

        public INode RightChild => throw NullNodeException.Create();
        public INode LeftChild => throw NullNodeException.Create();

        public INode Parent { get; private set; }

        public INode Grandparent
        {
            get
            {
                if (Parent.IsNull)
                    throw new ArgumentException($"Grandparent of node {this} can't be found, because it have null-parent");

                return Parent.Parent;
            }
        }
        
        public INode Sibling
        {
            get
            {
                if (Parent.IsNull)
                    throw new ArgumentException($"Sibling of node {this} can't be found, because it have null-parent");

                var sibling = IsLeftChildOf(Parent) ? Parent.RightChild : Parent.LeftChild;

                return sibling;
            }
        }
        
        public INode Uncle
        { 
            get
            {
                var grandparent = Grandparent;
                var uncle = Parent.IsLeftChildOf(grandparent) ? grandparent.RightChild : grandparent.LeftChild;

                return uncle;
            }
        }


        private NullNode() {}


        public static INode Create()
            => new NullNode();

        public INode GetMinimumOfSubTree()
            => throw NullNodeException.Create();

        public bool IsRightChildOf(INode node)
            => node.RightChild == this;
        
        public bool IsLeftChildOf(INode node)
            => node.LeftChild == this;

        public void SetParent(INode node)
            => Parent = node;

        public void SetColor(Color color)
            => throw NullNodeException.Create();

        public void SwapColor()
            => throw NullNodeException.Create();

        public void SetLeftChild(INode node)
            => throw NullNodeException.Create();

        public void SetRightChild(INode node)
            => throw NullNodeException.Create();


        public override string ToString() => "Null Node";

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Null Node");
            Console.ResetColor();
        }
    }
}