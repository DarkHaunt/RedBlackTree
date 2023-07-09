using RedBlackTreeRealisation.Extensions;
using RedBlackTreeRealisation.Nullables;
using RedBlackTreeRealisation.Nodes;
using System;


namespace RedBlackTreeRealisation
{
    public class RedBlackTree
    {
        private const float Epsilon = 0.0001f;
        
        private readonly NodeRotator _rotator;
        private readonly NodeDeleter _deleter;

        public INode Root { get; private set; }


        public RedBlackTree()
        {
            Root = NullNode.Create();

            _rotator = new NodeRotator();
            _deleter = new NodeDeleter(_rotator);
            
            _deleter.OnUnparentedNodeTransplanted += SetRoot;
            _rotator.OnRotationNodeParentNull += SetRoot;
        }

        ~RedBlackTree()
        {
            _deleter.OnUnparentedNodeTransplanted -= SetRoot;
            _rotator.OnRotationNodeParentNull -= SetRoot;
        }
        

        public void Insert(float value)
        {
            if (Root.IsNull)
            {
                CreateRootNode(value);
                return;
            }

            TryToInsertValueInto(Root, value);
        }

        public INode Find(float value)
        {
            return FindRecursive(Root);

            INode FindRecursive(INode node)
            {
                if (IsNodeHasValue(node, value) || node.IsNull)
                    return node;

                var nextNode = value > node.Value ? node.RightChild : node.LeftChild;

                return FindRecursive(nextNode);
            }
        }

        public void DeleteNode(float value)
        {
            var node = Find(value);

            if (node.IsNull)
                return;

            _deleter.DeleteNode(node, Root);
        }

        private void TryToInsertValueInto(INode root, float currentValue)
        {
            var isNewNodeIsRight = currentValue > root.Value;
            var targetNode = isNewNodeIsRight ? root.RightChild : root.LeftChild;

            if (targetNode.IsNull)
            {
                targetNode = new Node(currentValue);
                targetNode.SetParent(root);

                if (isNewNodeIsRight)
                    root.SetRightChild(targetNode);
                else
                    root.SetLeftChild(targetNode);

                BalanceAfterInsertion(targetNode);
            }

            if (IsNodeHasValue(targetNode, currentValue))
                return;
            
            TryToInsertValueInto(targetNode, currentValue);
        }

        private void BalanceAfterInsertion(INode insertedNode)
        {
            var parent = insertedNode.Parent;

            if (parent.Color == Color.Black || insertedNode.Equals(Root))
                return;

            var grandparent = insertedNode.Grandparent;
            var uncle = insertedNode.Uncle;

            switch (uncle.Color)
            {
                case Color.Red:
                    HandleRedRedConflict();
                    break;
                case Color.Black:
                    HandleBlackInsertConflict();
                    break;
                default:
                    throw new ApplicationException("In black-red tree can't be more than Black / Red colors");
            }

            void HandleRedRedConflict()
            {
                uncle.SetColor(Color.Black);
                parent.SetColor(Color.Black);

                if (!grandparent.Equals(Root))
                    grandparent.SetColor(Color.Red);

                BalanceAfterInsertion(grandparent);
            }

            void HandleBlackInsertConflict()
            { 
                var localRoot = _rotator.RotateLocalTree(insertedNode);

                if (grandparent.Equals(Root))
                    SetRoot(localRoot);
                
                BalanceAfterInsertion(localRoot);
            }
        }

        private void SetRoot(INode root)
        {
            Root = root;
        }
        
        private void CreateRootNode(float value)
        {
            var root = new Node(value)
                .With(node => node.SetColor(Color.Black));
            
            SetRoot(root);
        }

        private bool IsNodeHasValue(INode node, float value)
        {
            return MathF.Abs(node.Value - value) < Epsilon;
        }
        
        #region [Printing]

        public void PrintTree()
        {
            PrintNode(Root);
        }

        private void PrintNode(INode node)
        {
            if (node.IsNull)
                return;

            PrintNode(node.LeftChild);
            node.Print();
            PrintNode(node.RightChild);
        }

        #endregion
    }

    public enum Color
    {
        Black = 0,
        Red = 1
    }
}