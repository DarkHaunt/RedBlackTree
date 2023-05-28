using RedBlackTree.Nullables;
using System;

namespace RedBlackTree.Nodes
{
    public class Node : INullable
    {
        public virtual bool IsNull => false;
        public float Value { get; }
        public Color Color { get; private set; }
        public Node Parent { get; private set; } 
        public Node RightChild { get; private set; }
        public Node LeftChild { get; private set; }


        public Node(float value)
        {
            Value = value;

            if (IsNull)
                return;

            Color = Color.Red;
            LeftChild = Parent = RightChild = NullableContainer.NullNode;
        }


        public void SetColor(Color color)
        {
            Color = color;
        }

        public void SetParent(Node node)
        {
            Parent = node;
        }

        public void SetLeftChild(Node node)
        {
            if (node.Value > Value)
                throw new ArgumentException("Node that you're trying to insert as LEFT has bigger value, than current node");

            LeftChild = node;
        }

        public void SetRightChild(Node node)
        {
            if (node.Value <= Value)
                throw new ArgumentException("Node that you're trying to insert as RIGHT has lower value, than current node");

            RightChild = node;
        }

        public void SwapColor()
        {
            Color = (Color == Color.Black) ? Color.Red : Color.Black;
        }


        public Node GetGrandparent()
        {
            if (Parent.IsNull)
                throw new ArgumentException($"Node {this} have null-parent, so it have not a grandparent");

            return Parent.Parent;
        }

        public Node GetUncle()
        {
            var grandparent = GetGrandparent();
            var uncle = grandparent.LeftChild == Parent ? grandparent.RightChild : grandparent.LeftChild;

            return uncle;
        }


        #region [Printing]
        public override string ToString() => $"{Value}";

        public virtual void Print()
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
