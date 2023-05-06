using System;


namespace RedBlackTree
{
    class RedBlackTree
    {
        private Node _root;


        public RedBlackTree(float value)
        {
            _root = new Node(value);
            _root.SetColor(Color.Black);
        }


        public void Balance()
        {

        }

        public void Insert(float value)
        {
            if (_root.IsNull) // TODO: Make sure that deleting node will replace node with null-able node
            {
                _root = new Node(value);
                return;
            }

            var node = BinarySearchTreeInsertion(_root, value);

            if (node == _root)
                node.SetColor(Color.Black);
        }

        private Node BinarySearchTreeInsertion(Node node, float value)
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

            BinarySearchTreeInsertion(targetNode, value);

            return targetNode;
        }

        public void Delete(float value)
        {
        }

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
    }

    public enum Color
    {
        Black = 0,
        Red = 1
    }
}

