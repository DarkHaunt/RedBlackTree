namespace RedBlackTreeRealisation.Nodes
{
    public interface INode
    {
        bool IsNull { get; }
        float Value { get; }
        Color Color { get; }
        INode Parent { get; } 
        INode RightChild { get; }
        INode LeftChild { get; }
        INode Uncle { get; }
        INode Grandparent { get; }


        INode GetMinimumOfSubTree();
        bool IsRightChildOf(INode node);
        bool IsLeftChildOf(INode node);


        void SetColor(Color color);
        void SetParent(INode node);
        void SetLeftChild(INode node);
        void SetRightChild(INode node);
        void SwapColor();
        void Print();
    }
}