
namespace RedBlackTreeRealisation.Nodes
{
    public class NodeDeleter
    {
        private readonly RedBlackTree _tree;


        public NodeDeleter(RedBlackTree tree)
        {
            _tree = tree;
        }


        public void DeleteNode(INode deleteNode)
        {
            var originColor = deleteNode.Color;

            if (IsOneOrMoreChildrenAreNull(deleteNode, out INode nodeToTransplate))
                Transplant(deleteNode, nodeToTransplate);
            else
            {
                var leftSubTree = deleteNode.LeftChild;
                var righSubTree = deleteNode.RightChild;

                var minimalInSubTree = righSubTree.GetMinimumOfSubTree();

                if (IsOneOrMoreChildrenAreNull(minimalInSubTree, out INode transplantChild))
                    Transplant(minimalInSubTree, transplantChild);

                Transplant(deleteNode, minimalInSubTree);

                minimalInSubTree.SetLeftChild(leftSubTree);

                if (!leftSubTree.IsNull)
                    leftSubTree.SetParent(minimalInSubTree);

                if (righSubTree != minimalInSubTree)
                {
                    minimalInSubTree.SetRightChild(righSubTree);
                    righSubTree.SetParent(minimalInSubTree);
                }
            }

            if (originColor == Color.Black)
                BalanceAfterDelition(nodeToTransplate);
        }

        private bool IsOneOrMoreChildrenAreNull(INode node, out INode childToTransplate)
        {
            var leftChildIsNull = node.LeftChild.IsNull;
            var rightChildIsNull = node.RightChild.IsNull;

            childToTransplate = leftChildIsNull ? node.RightChild : node.LeftChild;

            return leftChildIsNull || rightChildIsNull;
        }

        private void Transplant(INode node, INode transplantNode)
        {
            var parent = node.Parent;

            if (parent.IsNull)
                _tree.SetRootNode(transplantNode);
            else if (node.IsLeftChildOf(parent))
                parent.SetLeftChild(transplantNode);
            else
                parent.SetRightChild(transplantNode);

            if (!transplantNode.IsNull)
                transplantNode.SetParent(parent);
        }

        private void BalanceAfterDelition(INode node)
        {

        }
    }
}