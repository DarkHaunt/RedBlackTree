using RedBlackTree.Nullables;
using System;


namespace RedBlackTree
{
    class Node : INullable
    {
        public virtual bool IsNull => false;
        public float Value { get; private set; }
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
    }
}
