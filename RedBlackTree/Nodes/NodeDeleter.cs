using System;

namespace RedBlackTreeRealisation.Nodes
{
    public class NodeDeleter
    {
        public event Action<INode> OnUnparentedNodeTransplanted;


        private readonly NodeRotator _rotator;


        public NodeDeleter(NodeRotator nodeRotator)
        {
            _rotator = nodeRotator;
        }


        public void DeleteNode(INode deleteNode, INode originRoot)
        {
            var originColor = deleteNode.Color;

            if (AtLeastOneChildIsNull(deleteNode, out INode transplantNode))
                Transplant(deleteNode, transplantNode);
            else
            {
                var leftSubTree = deleteNode.LeftChild;
                var rightSubTree = deleteNode.RightChild;

                transplantNode = rightSubTree.GetMinimumOfSubTree();
                originColor = transplantNode.Color;

                var rightChildOfTransplant = transplantNode.RightChild;
                Transplant(transplantNode, rightChildOfTransplant);

                if (rightSubTree != transplantNode)
                {
                    transplantNode.SetRightChild(rightSubTree);
                    rightSubTree.SetParent(transplantNode);
                }

                Transplant(deleteNode, transplantNode);

                transplantNode.SetLeftChild(leftSubTree);
                leftSubTree.SetParent(transplantNode);
            }

            if (originColor == Color.Black)
                BalanceAfterDeletion(transplantNode, originRoot);
        }

        private bool AtLeastOneChildIsNull(INode node, out INode childToTransplant)
        {
            var leftChildIsNull = node.LeftChild.IsNull;
            var rightChildIsNull = node.RightChild.IsNull;

            childToTransplant = leftChildIsNull ? node.RightChild : node.LeftChild;

            return leftChildIsNull || rightChildIsNull;
        }

        private void Transplant(INode node, INode transplantNode)
        {
            var parent = node.Parent;

            if (parent.IsNull)
                OnUnparentedNodeTransplanted?.Invoke(transplantNode);
            else if (node.IsLeftChildOf(parent))
                parent.SetLeftChild(transplantNode);
            else
                parent.SetRightChild(transplantNode);

            if (!transplantNode.IsNull)
                transplantNode.SetParent(parent);
        }

        private void BalanceAfterDeletion(INode transplantNode, INode originRoot)
        {
            var node = transplantNode;

            while (node != originRoot && node.Color == Color.Black)
            {
                var sibling = node.Sibling;

                if (sibling.Color == Color.Red)
                {
                    sibling.SetColor(Color.Black);

                    var parent = node.Parent;
                    parent.SetColor(Color.Red);

                    if (sibling.IsRightChildOf(parent))
                    {
                        _rotator.LeftDeleteRotation(parent);
                        sibling = parent.RightChild;
                    }
                    else
                    {
                        _rotator.RightDeleteRotation(parent);
                        sibling = parent.LeftChild;
                    }
                }

                if (sibling.LeftChild.Color == Color.Black && sibling.RightChild.Color == Color.Black)
                {
                    sibling.SetColor(Color.Red);
                    node = node.Parent;
                }
                else
                {
                    if (sibling.RightChild.Color == Color.Black)
                    {
                        sibling.LeftChild.SetColor(Color.Black);
                        sibling.SetColor(Color.Red);

                        _rotator.RightDeleteRotation(sibling);

                        sibling = node.Parent.RightChild;
                    }

                    sibling.SetColor(node.Parent.Color);

                    node.Parent.SetColor(Color.Black);
                    sibling.RightChild.SetColor(Color.Black);

                    _rotator.LeftDeleteRotation(node.Parent);

                    node = originRoot;
                }
            }

            node!.SetColor(Color.Black);
        }
    }
}