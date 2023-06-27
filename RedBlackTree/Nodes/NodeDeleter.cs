
namespace RedBlackTreeRealisation.Nodes
{
    public class NodeDeleter
    {
        private readonly NodeRotator _rotator;
        private readonly RedBlackTree _tree;


        public NodeDeleter(RedBlackTree tree, NodeRotator nodeRotator)
        {
            _rotator = nodeRotator;
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
                nodeToTransplate = minimalInSubTree;

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
            if (node.Color == Color.Red || node == _tree.GetRoot())
                node.SetColor(Color.Black);
            else
                PerformBlackColor();

            void PerformBlackColor()
            {
                var subling = node.Sibling;
                var parent = node.Parent;

                var isSublingLeftChild = subling.IsLeftChildOf(parent);

                if(AreBothOfSiblingChildrenRed(subling))
                {
                    if (isSublingLeftChild)
                        _rotator.LeftRotation(parent);
                    else
                        _rotator.RightRotation(parent);
                }
                else if (IsOneOfSiblingChildRed(subling, out INode redChild))
                {
                    var isRedChildLeft = redChild.IsLeftChildOf(subling); 

                    if(isSublingLeftChild)
                    {
                        if (isRedChildLeft)
                            _rotator.LeftRotation(parent); // TODO: Refactor argument income
                        else
                            _rotator.LeftRightRotation(redChild);
                    }
                    else
                    {
                        if (isRedChildLeft)
                            _rotator.RightLeftRotation(redChild);
                        else
                            _rotator.RightRotation(parent);
                    }
                }
                else
                {

                }
            }

            bool AreBothOfSiblingChildrenRed(INode subling)
            {
                var isRightChildRed = subling.RightChild.Color == Color.Red;
                var isLeftChildRed = subling.LeftChild.Color == Color.Red;

                return isLeftChildRed && isRightChildRed;
            }

            bool IsOneOfSiblingChildRed(INode subling, out INode child)
            {
                var isRightChildRed = subling.RightChild.Color == Color.Red;
                var isLeftChildRed = subling.LeftChild.Color == Color.Red;

                child = isRightChildRed ? subling.RightChild : subling.LeftChild;

                return isLeftChildRed || isRightChildRed;
            }
        }
    }
}