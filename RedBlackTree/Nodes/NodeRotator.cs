namespace RedBlackTreeRealisation.Nodes
{
    public class NodeRotator
    {
        public NodeRotator() {}

        
        public INode RotateLocalTree(INode node) // TODO: Extract in Tree or refactor name
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var previousGrandparentParent = grandparent.Parent;

            grandparent.SwapColor();

            if (parent.IsLeftChildOf(grandparent))
            {
                if (node.IsRightChildOf(parent))
                    parent = LeftRightRotation(node);

                grandparent = LeftRotation(grandparent);
            }
            else
            {
                if (node.IsLeftChildOf(parent))
                    parent = RightLeftRotation(node);

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

        public INode LeftRotation(INode node)
        {
            var leftChild = node.LeftChild;
            var rightChildOfLeftChild = leftChild.RightChild;

            leftChild.SetRightChild(node);

            node.SetLeftChild(rightChildOfLeftChild);
            node.SetParent(leftChild);

            if (!rightChildOfLeftChild.IsNull)
                rightChildOfLeftChild.SetParent(node);

            return leftChild;
        }

        public INode RightRotation(INode node)
        {
            var rightChild = node.RightChild;
            var leftChildOfRightChild = rightChild.LeftChild;

            rightChild.SetLeftChild(node);

            node.SetRightChild(leftChildOfRightChild);
            node.SetParent(rightChild);

            if (!leftChildOfRightChild.IsNull)
                leftChildOfRightChild.SetParent(node);

            return rightChild;
        }

        public INode LeftRightRotation(INode node)
        {
            var leftChild = node.LeftChild;
            var grand = node.Grandparent;
            var parent = node.Parent;

            grand.SetLeftChild(node);

            node.SetLeftChild(parent);
            node.SetParent(grand);

            parent.SetRightChild(leftChild);
            parent.SetParent(node);

            return node;
        }

        public INode RightLeftRotation(INode node)
        {
            var rightChild = node.RightChild;
            var grand = node.Grandparent;
            var parent = node.Parent;

            grand.SetRightChild(node);

            node.SetRightChild(parent);
            node.SetParent(grand);

            parent.SetLeftChild(rightChild);
            parent.SetParent(node);

            return node;
        }
    }
}