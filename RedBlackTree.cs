using RedBlackTree.Nodes;
using System;


namespace RedBlackTree
{
    public class RedBlackTree
    {
        private readonly NodeRotator _rotator;
        private INode _root;


        public RedBlackTree(float value)
        {
            _rotator = new NodeRotator();

            CreateRootNode(value);
        }


        public void Insert(float value)
        {
            if (_root.IsNull) // TODO: Make sure that deleting node will replace node with null-able node
            {
                CreateRootNode(value);
                return;
            }

            TryToInsertValueInto(_root, value);
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
            }
        }

        private void CreateRootNode(float value)
        {
            _root = new Node(value);
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