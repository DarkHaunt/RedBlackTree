using RedBlackTreeRealisation.Nodes;
using RedBlackTreeRealisation;

namespace RedBlackTreeTests.TestsSetup
{
    public static class Setup
    {
        public static RedBlackTree CreateTree()
            => new RedBlackTree();          
        
        public static RedBlackTree CreateTree(float value)
            => new RedBlackTree(value);   
        
        public static NodeRotator CreateNodeRotator()
            => new NodeRotator();

        public static INode CreateStubRotationTree(RotationTreeStubData stubData)
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
            
            return node;
        }
    }

    public struct RotationTreeStubData
    {
        public float GrandparentValue;
        public float ParentValue;
        public float UncleValue;
        
        public float RightChildValue;
        public float LeftChildValue;
        public float NodeValue;
    }
}
