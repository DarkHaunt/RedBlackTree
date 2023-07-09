using RedBlackTreeRealisation.Nodes;
using RedBlackTreeRealisation;

namespace RedBlackTreeTests.TestsSetup
{
    public static class Setup
    {
        public static RedBlackTree CreateTree()
            => new ();          
        
        public static NodeRotator CreateNodeRotator() 
            => new();        
        
        public static NodeDeleter CreateNodeDeleter(NodeRotator nodeRotator)
            => new (nodeRotator);

        public static INode CreateStubNodesConstruction(NodeConstructionStubData stubData)
        {
            var grandparentNode = new Node(stubData.GrandparentValue);
            grandparentNode.SetColor(Color.Black);

            var uncle = new Node(stubData.UncleValue);
            uncle.SetParent(grandparentNode);
            uncle.SetColor(Color.Black);

            var parent = new Node(stubData.ParentValue);
            parent.SetParent(grandparentNode);
            parent.SetColor(Color.Red);
            
            var node = new Node(stubData.NodeValue);
            node.SetParent(parent);

            var leftChild = new Node(stubData.LeftChildValue);
            leftChild.SetParent(node);
            
            var rightChild = new Node(stubData.RightChildValue);
            rightChild.SetParent(node);

            node.SetRightChild(rightChild);
            node.SetLeftChild(leftChild);
            
            if(node.Value > parent.Value)
                parent.SetRightChild(node);
            else
                parent.SetLeftChild(node);
            
            return node;
        }
    }

    public struct NodeConstructionStubData
    {
        public float GrandparentValue;
        public float ParentValue;
        public float UncleValue;
        
        public float RightChildValue;
        public float LeftChildValue;
        public float NodeValue;
    }
}
