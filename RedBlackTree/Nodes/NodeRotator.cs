

namespace RedBlackTreeRealisation.Nodes
{
    public class NodeRotator
    {
        public INode RotateLocalTree(INode node) // TODO: Move into Tree or refactor name
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var previousGrandparentParent = grandparent.Parent;

            INode localRoot;

            if (parent.IsLeftChildOf(grandparent))
                localRoot = node.IsRightChildOf(parent) ? LeftRightRotation(node) : LeftLeftRotation(grandparent);
            else
                localRoot = node.IsLeftChildOf(parent) ? RightLeftRotation(node) : RightRightRotation(grandparent);

            if (!previousGrandparentParent.IsNull)
            {
                if (grandparent.IsRightChildOf(previousGrandparentParent))
                    previousGrandparentParent.SetRightChild(localRoot);
                else
                    previousGrandparentParent.SetLeftChild(localRoot);
            }

            return localRoot;
        }

        public INode LeftLeftRotation(INode node)
        {
            var parent = node.Parent;
            var grand = node.Grandparent;
            var rightParentChild = parent.RightChild;

            parent.SwapColor();
            parent.SetLeftChild(node);
            parent.SetRightChild(grand);

            grand.SwapColor();
            grand.SetParent(parent);
            grand.SetLeftChild(rightParentChild);

            return parent;
        }

        public INode RightRightRotation(INode node)
        {
            var parent = node.Parent;
            var grand = node.Grandparent;
            var leftParentChild = parent.LeftChild;

            parent.SwapColor();
            parent.SetRightChild(node);
            parent.SetLeftChild(grand);

            grand.SwapColor();
            grand.SetParent(parent);
            grand.SetRightChild(leftParentChild);

            return parent;
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

            return LeftLeftRotation(parent);
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

            return RightRightRotation(parent);
        }
    }
}