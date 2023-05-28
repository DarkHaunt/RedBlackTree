using RedBlackTree.Nullables;
using RedBlackTree.Nodes;
using System;


namespace RedBlackTree
{
    public class RedBlackTree
    {
        private readonly NodeRotator _rotator;
        private Node _root;


        public RedBlackTree(float value)
        {
            _rotator = new NodeRotator();
            
            CreateRootNode(value);
        }

        public RedBlackTree()
        {
            _rotator = new NodeRotator();
            
            _root = NullableContainer.NullNode; 
        }


        private void Balance(Node node)
        {
            var parent = node.Parent;

            if (parent.Color == Color.Black || node == _root)
                return;

            var uncle = node.GetUncle();

            switch (uncle.Color)
            {
                case Color.Red:
                    uncle.SetColor(Color.Black);
                    parent.SetColor(Color.Black);

                    var grandparent = node.GetGrandparent();
                    Balance(grandparent);

                    break;
                case Color.Black:
                    _rotator.Rotate(node);
                    break;
                default:
                    throw new ApplicationException("In black-red tree can't be more than Black / Red colors");
            }
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
        }

        private void CreateRootNode(float value)
        {
            _root = new Node(value);
            _root.SetColor(Color.Black);
        }

        private Node BST_Insertion(Node node, float value)
        {
            var isNewNodeIsRight = value > node.Value;

            var targetNode = isNewNodeIsRight ? node.RightChild : node.LeftChild;

            if(targetNode.IsNull)
            {
                targetNode = new Node(value);
                targetNode.SetParent(node);

                if (isNewNodeIsRight)
                    node.SetRightChild(targetNode);
                else
                    node.SetLeftChild(targetNode);

                return targetNode;
            }

            return BST_Insertion(targetNode, value);
        }

        #region [Printing] 
        public void PrintTree()
        {
            PrintNode(_root);
        }

        private void PrintNode(Node node)
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

