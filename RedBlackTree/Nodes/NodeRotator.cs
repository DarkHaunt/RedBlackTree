using System;

namespace RedBlackTreeRealisation.Nodes
{
    public class NodeRotator
    {
        public event Action<INode> OnRotationNodeParentNull;
        
        public INode RotateLocalTree(INode node)
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var previousGrandparentParent = grandparent.Parent;

            INode localRoot;

            if (parent.IsLeftChildOf(grandparent))
                localRoot = node.IsLeftChildOf(parent) ? LeftLeftRotation(node) : LeftRightRotation(node);
            else
                localRoot = node.IsLeftChildOf(parent) ? RightLeftRotation(node) : RightRightRotation(node);

            if (!previousGrandparentParent.IsNull)
            {
                if (grandparent.IsRightChildOf(previousGrandparentParent))
                    grandparent.SetRightChild(localRoot);
                else
                    grandparent.SetLeftChild(localRoot);
            }

            localRoot.SetParent(previousGrandparentParent);

            return localRoot;
        }

        public INode LeftLeftRotation(INode node)
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var rightParentChild = parent.RightChild;

            parent.SwapColor();
            parent.SetRightChild(grandparent);

            grandparent.SwapColor();
            grandparent.SetParent(parent);
            grandparent.SetLeftChild(rightParentChild);

            return parent;
        }

        public INode RightRightRotation(INode node)
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            var leftParentChild = parent.LeftChild;

            parent.SwapColor();
            parent.SetLeftChild(grandparent);

            grandparent.SwapColor();
            grandparent.SetParent(parent);
            grandparent.SetRightChild(leftParentChild);

            return parent;
        }

        public INode LeftRightRotation(INode node)
        {
            var parent = node.Parent;
            var leftChild = node.LeftChild;
            var grandparent = node.Grandparent;

            grandparent.SetLeftChild(node);

            node.SetLeftChild(parent);
            node.SetParent(grandparent);

            leftChild.SetParent(parent);
    
            parent.SetRightChild(leftChild);
            parent.SetParent(node);

            return LeftLeftRotation(parent);
        }

        public INode RightLeftRotation(INode node)
        {
            var parent = node.Parent;
            var rightChild = node.RightChild;
            var grandparent = node.Grandparent;

            grandparent.SetRightChild(node);

            node.SetRightChild(parent);
            node.SetParent(grandparent);

            rightChild.SetParent(parent);

            parent.SetLeftChild(rightChild);
            parent.SetParent(node);

            return RightRightRotation(parent);
        }

        public void LeftDeleteRotation(INode node)
        {
            var parent = node.Parent;
            var rightChild = node.RightChild;
            var leftOfRightChild = rightChild.LeftChild;
            
            node.SetRightChild(leftOfRightChild);
            
            if(!leftOfRightChild.IsNull)
                leftOfRightChild.SetParent(node);
            
            rightChild.SetParent(parent);
            
            if(parent.IsNull)
                OnRotationNodeParentNull?.Invoke(rightChild);
            else if(node.IsLeftChildOf(parent))
                parent.SetLeftChild(rightChild);
            else
                parent.SetRightChild(rightChild);
            
            rightChild.SetLeftChild(node);
            node.SetParent(rightChild);
        }

        public void RightDeleteRotation(INode node)
        {
            var parent = node.Parent;
            var leftChild = node.LeftChild;
            var rightOfLeftChild = leftChild.RightChild;
            
            node.SetLeftChild(rightOfLeftChild);
            
            if(!rightOfLeftChild.IsNull)
                rightOfLeftChild.SetParent(node);
            
            leftChild.SetParent(parent);
            
            if(parent.IsNull)
                OnRotationNodeParentNull?.Invoke(leftChild);
            else if(node.IsRightChildOf(parent))
                parent.SetLeftChild(leftChild);
            else
                parent.SetRightChild(leftChild);
            
            leftChild.SetRightChild(node);
            node.SetParent(leftChild);
        }
    }
}