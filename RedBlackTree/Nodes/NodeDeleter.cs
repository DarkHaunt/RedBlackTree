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

                transplantNode = rightChildOfTransplant;
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

            transplantNode.SetParent(parent);
        }

        private void BalanceAfterDeletion(INode node, INode originRoot)
        {
            if (node == originRoot || node.Color == Color.Red)
                return;

            var sibling = node.Sibling;
            var parent = node.Parent;

            if (sibling.Color == Color.Red)
                sibling = MaintainRedSiblingColor(sibling, parent);

            var siblingRightChild = sibling.RightChild;
            var siblingLeftChild = sibling.LeftChild;

            if (siblingLeftChild.Color == Color.Black
                && siblingRightChild.Color == Color.Black)
            {
                sibling.SetColor(Color.Red);
                node = sibling.Parent;
            }
            else
            {
                MaintainSiblingChildRedColor(sibling, parent);
                node = originRoot;
            }

            BalanceAfterDeletion(node, originRoot);

            node!.SetColor(Color.Black);
        }

        private INode MaintainRedSiblingColor(INode sibling, INode parent)
        {
            sibling.SwapColor();
            parent.SetColor(Color.Red);

            INode nextNodeToProcess;

            if (sibling.IsRightChildOf(parent))
            {
                _rotator.LeftRotation(parent);
                nextNodeToProcess = parent.RightChild;
            }
            else
            {
                _rotator.RightRotation(parent);
                nextNodeToProcess = parent.LeftChild;
            }

            return nextNodeToProcess;
        }

        private void MaintainSiblingChildRedColor(INode sibling, INode parent)
        {
            var siblingRightChild = sibling.RightChild;
            var siblingLeftChild = sibling.LeftChild;

            if (sibling.IsRightChildOf(parent))
            {
                if (siblingRightChild.Color == Color.Black)
                {
                    siblingLeftChild.SetColor(Color.Black);
                    sibling.SetColor(Color.Red);

                    _rotator.RightRotation(sibling);

                    sibling = parent.RightChild;
                }

                _rotator.LeftRotation(parent);
                
                siblingRightChild.SetColor(Color.Black);
            }
            else
            {
                if (siblingLeftChild.Color == Color.Black)
                {
                    siblingRightChild.SetColor(Color.Black);
                    sibling.SetColor(Color.Red);

                    _rotator.LeftRotation(sibling);

                    sibling = parent.LeftChild;
                }

                _rotator.RightRotation(parent);
                
                siblingLeftChild.SetColor(Color.Black);
            }

            sibling.SetColor(parent.Color);
            parent.SetColor(Color.Black);
        }
    }
}