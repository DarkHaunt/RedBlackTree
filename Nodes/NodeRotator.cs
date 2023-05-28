namespace RedBlackTree.Nodes
{
    public class NodeRotator
    {
        public NodeRotator()
        {
        }


        public Node RotateLocalTree(Node node)
        {
            var parent = node.Parent;
            var grandparent = node.GetGrandparent();
            var previousGrandparentParent = grandparent.Parent;

            grandparent.SwapColor();

            if (grandparent.LeftChild == node.Parent)
            {
                if (parent.RightChild == node)
                    parent = LeftRightRotationParent(node, grandparent);

                grandparent = LeftRotation(grandparent);
                previousGrandparentParent.SetLeftChild(grandparent);
            }
            else
            {
                if (parent.LeftChild == node)
                    parent = RightLeftRotationParent(node, grandparent);

                grandparent = RightRotation(grandparent);
                previousGrandparentParent.SetRightChild(grandparent); // TODO: To make  null nude ignore child setting and other operations
            }

            grandparent.SetParent(previousGrandparentParent);
            parent.SwapColor();

            return grandparent;
        }

        private Node LeftRotation(Node node)
        {
            var leftChildOfGrandparent = node.LeftChild;
            var rightChildOfGrandparentLeftChild = leftChildOfGrandparent.RightChild;

            leftChildOfGrandparent.SetRightChild(node);

            node.SetLeftChild(rightChildOfGrandparentLeftChild);
            node.SetParent(leftChildOfGrandparent);

            if (!rightChildOfGrandparentLeftChild.IsNull)
                rightChildOfGrandparentLeftChild.SetParent(node);

            return leftChildOfGrandparent;
        }

        private Node RightRotation(Node node)
        {
            var rightChildOfGrandparent = node.RightChild;
            var leftChildOfGrandparentRightChild = rightChildOfGrandparent.LeftChild;

            rightChildOfGrandparent.SetLeftChild(node);

            node.SetRightChild(leftChildOfGrandparentRightChild);
            node.SetParent(rightChildOfGrandparent);

            if (!leftChildOfGrandparentRightChild.IsNull)
                leftChildOfGrandparentRightChild.SetParent(node);

            return rightChildOfGrandparent;
        }

        private Node RightLeftRotationParent(Node node, Node grandparent)
        {
            var rightChild = node.RightChild;
            var parent = node.Parent;

            grandparent.SetRightChild(node);
            node.SetParent(grandparent);
            node.SetRightChild(parent);

            parent.SetLeftChild(rightChild);
            parent.SetParent(node);

            return node;
        }

        private Node LeftRightRotationParent(Node node, Node grandparent)
        {
            var grand = node.GetGrandparent();
            var leftChild = node.LeftChild;
            var parent = node.Parent;

            grand.SetLeftChild(node);
            node.SetParent(grandparent);
            node.SetLeftChild(parent);

            parent.SetRightChild(leftChild);
            parent.SetParent(node);

            return node;
        }
    }
}