using System;

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
            var originRoot = _tree.GetRoot();

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

            /*if (originColor == Color.Black)
                BalanceAfterDeletion(deleteNode, originRoot);*/
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
                _tree.SetRootNode(transplantNode);
            else if (node.IsLeftChildOf(parent))
                parent.SetLeftChild(transplantNode);
            else
                parent.SetRightChild(transplantNode);

            if (!transplantNode.IsNull)
                transplantNode.SetParent(parent);
        }

        private void BalanceAfterDeletion(INode deletionNode, INode originRoot)
        {
            var node = deletionNode;

            while (node != originRoot && node.Color == Color.Black)
            {
                var sibling = deletionNode.Sibling;

                if (sibling.Color == Color.Red)
                {
                    sibling.SwapColor();
                    node.Parent.SetColor(Color.Red);

                    _rotator.LeftDeleteRotation(node.Parent);

                    sibling = node.Parent.RightChild;
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