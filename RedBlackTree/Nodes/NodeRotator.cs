using System;

namespace RedBlackTreeRealisation.Nodes
{
    public class NodeRotator
    {
        public event Action<INode> OnRotationNodeParentNull;

        public void LeftRotation(INode node)
        {
            var parent = node.Parent;
            var rightChild = node.RightChild;
            var leftOfRightChild = rightChild.LeftChild;

            node.SetRightChild(leftOfRightChild);

            if (!leftOfRightChild.IsNull)
                leftOfRightChild.SetParent(node);

            rightChild.SetParent(parent);

            if (parent.IsNull)
                OnRotationNodeParentNull?.Invoke(rightChild);
            else if (node.IsLeftChildOf(parent))
                parent.SetLeftChild(rightChild);
            else
                parent.SetRightChild(rightChild);

            rightChild.SetLeftChild(node);
            node.SetParent(rightChild);
        }

        public void RightRotation(INode node)
        {
            var parent = node.Parent;
            var leftChild = node.LeftChild;
            var rightOfLeftChild = leftChild.RightChild;

            node.SetLeftChild(rightOfLeftChild);

            if (!rightOfLeftChild.IsNull)
                rightOfLeftChild.SetParent(node);

            leftChild.SetParent(parent);

            if (parent.IsNull)
                OnRotationNodeParentNull?.Invoke(leftChild);
            else if (node.IsRightChildOf(parent))
                parent.SetRightChild(leftChild);
            else
                parent.SetLeftChild(leftChild);

            leftChild.SetRightChild(node);
            node.SetParent(leftChild);
        }
    }
}