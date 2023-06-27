using RedBlackTreeRealisation.Nodes;
using System;

namespace RedBlackTreeRealisation.Nullables
{
    public class NullNode : INode
    {
        public Color Color => Color.Black;
        public float Value => float.NaN;
        public bool IsNull => true;
        
        public INode Grandparent => throw NullNodeException.Create();
        public INode RightChild => throw NullNodeException.Create();
        public INode LeftChild => throw NullNodeException.Create();
        public INode Sibling => throw NullNodeException.Create();
        public INode Parent => throw NullNodeException.Create();
        public INode Uncle => throw NullNodeException.Create();

        
        public NullNode() {}


        public INode GetMinimumOfSubTree()
            => throw NullNodeException.Create();

        public bool IsRightChildOf(INode node)
            => throw NullNodeException.Create();

        public bool IsLeftChildOf(INode node)
            => throw NullNodeException.Create();

        public void SetColor(Color color)
            => throw NullNodeException.Create();
        
        public void SwapColor()
            => throw NullNodeException.Create();

        public void SetParent(INode node)
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
