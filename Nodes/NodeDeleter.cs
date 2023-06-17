using RedBlackTree.Nullables;

namespace RedBlackTree.Nodes
{
    public class NodeDeleter
    {
        private INode _root;

        public NodeDeleter(INode root)
        {
            _root = root;
        }


        public void DeleteNode(INode node)
        {
            var nodeParent = node.Parent;
            var originColor = node.Color;

            INode nodeToTransplate;

            if (node.RightChild.IsNull)
                nodeToTransplate = node.LeftChild;
            else if (node.LeftChild.IsNull)
                nodeToTransplate = node.RightChild;
            //else
                


           // BalanceAfterDelition(nodeToTransplate);
        }

        private void Transplant(INode node, INode transplantNode)
        {
            var parent = node.Parent;

            if (parent.IsNull)
                _root = parent;
            else if (parent.LeftChild == node)
                parent.SetLeftChild(transplantNode);
            else
                parent.SetRightChild(transplantNode);

            transplantNode.SetParent(node);
        }

        private void BalanceAfterDelition(INode node)
        {

        }

        private bool NodeHasNoChildren(INode node)
        {
            var rightChild = node.RightChild;
            var leftChild = node.LeftChild;

            return rightChild.IsNull && leftChild.IsNull;
        }

        private bool NodeHasOneChild(INode node, out INode child)
        {
            var rightChild = node.RightChild;
            var leftChild = node.LeftChild;

            var hasOnlyOneChild = rightChild.IsNull ^ leftChild.IsNull;

            if (!hasOnlyOneChild)
            {
                child = NullableContainer.NullNode;
                return false;
            }

            child = rightChild.IsNull ? leftChild : rightChild;

            return true;
        }

        private bool NodeHasBothChildren(INode node)
        {
            var rightChild = node.RightChild;
            var leftChild = node.LeftChild;

            return !rightChild.IsNull && !leftChild.IsNull;
        }
    }
}