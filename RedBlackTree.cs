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

            var insertedNode = BST_Insertion(_root, value);

            Balance(insertedNode);
            
            INode BST_Insertion(INode node, float currentValue)
            {
                var isNewNodeIsRight = currentValue > node.Value;

                var targetNode = isNewNodeIsRight ? node.RightChild : node.LeftChild;

                if (targetNode.IsNull)
                {
                    targetNode = new Node(currentValue);
                    targetNode.SetParent(node);

                    if (isNewNodeIsRight)
                        node.SetRightChild(targetNode);
                    else
                        node.SetLeftChild(targetNode);

                    return targetNode;
                }

                return BST_Insertion(targetNode, currentValue);
            }
        }

        private void Balance(INode node)
        {
            var parent = node.Parent;

            if (parent.Color == Color.Black || node == _root)
                return;

            var grandparent = node.Grandparent;
            var uncle = node.Uncle;

            switch (uncle.Color)
            {
                case Color.Red:
                    HandleRedRedConflict();
                    Balance(grandparent);
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
            }

            void HandleBlackInsertConflict()
            {
                var localRoot = _rotator.RotateLocalTree(node);

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