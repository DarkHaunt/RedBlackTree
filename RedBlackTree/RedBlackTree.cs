using RedBlackTreeRealisation.Extensions;
using RedBlackTreeRealisation.Nodes;
using System;
using RedBlackTreeRealisation.Nullables;


namespace RedBlackTreeRealisation
{
    public class RedBlackTree
    {
        private readonly NodeRotator _rotator;
        private readonly NodeDeleter _deleter;

        public INode Root { get; private set; }


        public RedBlackTree()
        {
            Root = NullNode.Create();

            _rotator = new NodeRotator();
            _deleter = new NodeDeleter(_rotator);
            
            _deleter.OnUnparentedNodeTransplanted += SetRoot;
        }

        ~RedBlackTree()
        {
            _deleter.OnUnparentedNodeTransplanted -= SetRoot;
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
                if (node.Value == value || node.IsNull) // TODO: Write properly float comparer or get from internet
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

            if (targetNode.Value == currentValue)
                return;

            TryToInsertValueInto(targetNode, currentValue);
        }

        private void BalanceAfterInsertion(INode insertedNode)
        {
            var parent = insertedNode.Parent;

            if (parent.Color == Color.Black || insertedNode == Root)
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

                if (grandparent != Root)
                    grandparent.SetColor(Color.Red);

                BalanceAfterInsertion(grandparent);
            }

            void HandleBlackInsertConflict()
            { 
                var localRoot = _rotator.RotateLocalTree(insertedNode);

                if (grandparent == Root)
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