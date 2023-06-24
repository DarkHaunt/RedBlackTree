using RedBlackTreeRealisation.Nullables;
using System;

namespace RedBlackTreeRealisation.Nodes
{
    public class Node : INode
    {
        public bool IsNull => false;
        public float Value { get; }
        public Color Color { get; private set; }
        public INode Parent { get; private set; }
        public INode RightChild { get; private set; }
        public INode LeftChild { get; private set; }

        public INode Uncle
        {
            get
            {
                var grandparent = Grandparent;
                var uncle = grandparent.LeftChild == Parent ? grandparent.RightChild : grandparent.LeftChild;

                return uncle;
            }
        }

        public INode Grandparent
        {
            get
            {
                if (Parent.IsNull)
                    throw new ArgumentException($"Node {this} have null-parent, so it have not a grandparent");

                return Parent.Parent;
            }
        }


        public Node(float value)
        {
            Value = value;
            Color = Color.Red;
            LeftChild = Parent = RightChild = NullableContainer.NullNode;
        }

        public Node(INode node)
        {
            Value = node.Value;
            Color = node.Color;
            Parent = node.Parent;
            LeftChild = node.LeftChild;
            RightChild = node.RightChild;
        }

        public void SwapColor()
        {
            Color = (Color == Color.Black) ? Color.Red : Color.Black;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public INode GetMinimumOfSubTree()
        {
            INode minimal = this;

            while (!minimal.LeftChild.IsNull)
                minimal = minimal.LeftChild;

            return minimal;
        }

        public void SetParent(INode node)
        {
            Parent = node;
        }

        public void SetLeftChild(INode node)
        {
            if (node.Value > Value)
                throw new ArgumentException("Node that you're trying to insert as LEFT has bigger value, than current node");

            LeftChild = node;
        }

        public void SetRightChild(INode node)
        {
            if (node.Value <= Value)
                throw new ArgumentException("Node that you're trying to insert as RIGHT has lower value, than current node");

            RightChild = node;
        }

        public bool IsLeftChildOf(INode node)
        {
            return node.LeftChild == (INode)this;
        }

        public bool IsRightChildOf(INode node)
        {
            return node.RightChild == (INode)this;
        }


        #region [Printing]
        public override string ToString() => $"{Value}";

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{Value} ");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"(Color - {Color} | ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Parent -  {Parent})\n");

            Console.ResetColor();
        }
        #endregion

        #region [Comparing Override]
        public static bool operator ==(Node a, Node b) => a.Value == b.Value;
        public static bool operator !=(Node a, Node b) => a.Value != b.Value;

        private bool Equals(Node other)
        {
            return Value.Equals(other.Value) &&
                   Color == other.Color &&
                   Equals(Parent, other.Parent) &&
                   Equals(RightChild, other.RightChild) &&
                   Equals(LeftChild, other.LeftChild);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
        #endregion
    }
}
