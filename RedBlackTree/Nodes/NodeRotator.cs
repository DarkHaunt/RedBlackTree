namespace RedBlackTreeRealisation.Nodes
{
    public class NodeRotator
    {
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
                    previousGrandparentParent.SetRightChild(localRoot);
                else
                    previousGrandparentParent.SetLeftChild(localRoot);
                
                localRoot.SetParent(previousGrandparentParent);
            }

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

            parent.SetLeftChild(rightChild);
            parent.SetParent(node);

            return RightRightRotation(parent);
        }

        public void LeftDeleteRotation(INode node)
        {
            var parent = node.Parent;
            var grandparent = node.Grandparent;
            
            var rightChild = node.RightChild;
            var leftChildOfRight = rightChild.LeftChild;

            node.SetRightChild(leftChildOfRight);
            
            if(parent.IsLeftChildOf(grandparent))
                grandparent.SetLeftChild(node);
            else
                grandparent.SetRightChild(node);
        }        
        
        public void RightDeleteRotation(INode node)
        {
            var parent = node.Parent;
            var rightChild = node.RightChild;
            var grandparent = node.Grandparent;

            parent.SetLeftChild(rightChild);
            node.SetRightChild(parent);
            
            if(parent.IsLeftChildOf(grandparent))
                grandparent.SetLeftChild(node);
            else
                grandparent.SetRightChild(node);
        }
    }
}