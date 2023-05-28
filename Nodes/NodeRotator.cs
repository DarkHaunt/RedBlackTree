using RedBlackTree.Nullables;

namespace RedBlackTree.Nodes
{
    public class NodeRotator
    {
        public NodeRotator()
        {
        }


        public void Rotate(Node node)
        {
            var grandparent = node.GetGrandparent();
            var parent = node.Parent;

            if (grandparent.LeftChild == node.Parent)
            {
                if (parent.RightChild == node)
                    LeftRightRotationParent(node);

                LeftRotation(grandparent);
            }
            else
            {
                if (parent.LeftChild == node)
                    RightLeftRotationParent(node);

                RightRotation(grandparent);
            }

            grandparent.SwapColor();
            parent.SwapColor();
        }

        private void LeftRotation(Node node)
        {
            var leftChildOfGrandparent = node.LeftChild;
            var rightChildOfGrandparentLeftChild = leftChildOfGrandparent.RightChild;

            leftChildOfGrandparent.SetRightChild(node);

            node.SetLeftChild(rightChildOfGrandparentLeftChild);
            node.SetParent(leftChildOfGrandparent);

            if (!rightChildOfGrandparentLeftChild.IsNull)
                rightChildOfGrandparentLeftChild.SetParent(node);
        }

        private void RightRotation(Node node)
        {
            var rightChildOfGrandparent = node.RightChild;
            var leftChildOfGrandparentRightChild = rightChildOfGrandparent.LeftChild;

            rightChildOfGrandparent.SetLeftChild(node);

            node.SetRightChild(leftChildOfGrandparentRightChild);
            node.SetParent(rightChildOfGrandparent);

            if (!leftChildOfGrandparentRightChild.IsNull)
                leftChildOfGrandparentRightChild.SetParent(node);
        }

        private void RightLeftRotationParent(Node node)
        {
            var grandparent = node.GetGrandparent();
            var rightChild = node.RightChild;
            var parent = node.Parent;

            grandparent.SetRightChild(node);
            parent.SetLeftChild(rightChild);
            node.SetParent(grandparent);
            node.SetRightChild(parent);
            parent.SetParent(node);
        }

        private void LeftRightRotationParent(Node node)
        {
            var grandparent = node.GetGrandparent();
            var leftChild = node.LeftChild;
            var parent = node.Parent;

            grandparent.SetRightChild(node);
            parent.SetRightChild(leftChild);
            node.SetParent(grandparent);
            node.SetLeftChild(parent);
            parent.SetParent(node);
        }
    }
}