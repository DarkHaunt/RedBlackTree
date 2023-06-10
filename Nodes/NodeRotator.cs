namespace RedBlackTree.Nodes
{
    public class NodeRotator
    {
        public NodeRotator()
        {
        }


        public INode RotateLocalTree(INode node)
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var previousGrandparentParent = grandparent.Parent;

            grandparent.SwapColor();

            if (grandparent.LeftChild == node.Parent)
            {
                if (parent.RightChild == node)
                    parent = LeftRightRotationParent(node, grandparent);

                grandparent = LeftRotation(grandparent);
            }
            else
            {
                if (parent.LeftChild == node)
                    parent = RightLeftRotationParent(node, grandparent);

                grandparent = RightRotation(grandparent);
            }

            if (!previousGrandparentParent.IsNull)
            {
                if (grandparent.Value > previousGrandparentParent.Value)
                    previousGrandparentParent.SetRightChild(grandparent);
                else
                    previousGrandparentParent.SetLeftChild(grandparent);
            }

            grandparent.SetParent(previousGrandparentParent);
            parent.SwapColor();

            return grandparent;
        }

        private INode LeftRotation(INode node)
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

        private INode RightRotation(INode node)
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

        private INode RightLeftRotationParent(INode node, INode grandparent)
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

        private INode LeftRightRotationParent(INode node, INode grandparent)
        {
            var leftChild = node.LeftChild;
            var grand = node.Grandparent;
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