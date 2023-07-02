using RedBlackTreeRealisation.Nullables;
using RedBlackTreeRealisation.Nodes;
using System;

namespace RedBlackTreeRealisation
{
    public class RedBlackTree
    {
        private readonly NodeRotator _rotator;
        private readonly NodeDeleter _deleter;
        
        private INode _root;


        public RedBlackTree()
        {
            _root = NullableContainer.NullNode;

            _rotator = new NodeRotator();
            _deleter = new NodeDeleter(this, _rotator);
        }

        public RedBlackTree(float value)
        {
            CreateRootNode(value);

            _rotator = new NodeRotator();
            _deleter = new NodeDeleter(this, _rotator);
        }


        public void SetRootNode(INode root)
            => _root = root;

        public INode GetRoot()
            => _root;

        public void Insert(float value)
        {
            if (_root.IsNull)
            {
                CreateRootNode(value);
                return;
            }

            TryToInsertValueInto(_root, value);
        }

        public INode Find(float value)
        {
            return FindValue(_root);

            INode FindValue(INode node)
            {
                if (node.Value == value || node.IsNull) // TODO: Write properly float comparer or get from internet
                    return node;

                var nextNode = value > node.Value ? node.RightChild : node.LeftChild;

                return FindValue(nextNode);
            }
        }

        public void DeleteNode(float value)
        {
            var node = Find(value);

            if (node.IsNull)
                return;

            _deleter.DeleteNode(node);
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

            if (parent.Color == Color.Black || insertedNode == _root)
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

                if (grandparent != _root)
                    grandparent.SetColor(Color.Red);

                BalanceAfterInsertion(grandparent);
            }

            void HandleBlackInsertConflict()
            {
                var localRoot = _rotator.RotateLocalTree(insertedNode);

                if (grandparent == _root)
                    _root = localRoot;
                
                BalanceAfterInsertion(localRoot);
            }
        }

        private void CreateRootNode(float value)
        {
            SetRootNode(new Node(value));

            _root.SetColor(Color.Black);
        }


        #region [Printing]

        public void PrintTree()
        {
            PrintNode(_root);
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