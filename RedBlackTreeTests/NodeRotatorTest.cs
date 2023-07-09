using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeRealisation.Nodes;
using RedBlackTreeTests.TestsSetup;


namespace RedBlackTreeTests
{
    [TestClass]
    public class NodeRotatorTest
    {
        private NodeRotator _nodeRotator;

        [TestInitialize]
        public void TestInitialize()
        {
            _nodeRotator = Setup.CreateNodeRotator();
        }

        [TestMethod]
        public void Right_Rotation_RightChild_Of_LeftChild_Becomes_Node_LeftChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 4.5f,
                LeftChildValue = 3.5f,
                
                ParentValue = 3f,
                NodeValue = 4f
            };

            var rightOfLeftChild = new Node(3.75f);

            var node = Setup.CreateStubNodesConstruction(stubTreeData);
            node.LeftChild.SetRightChild(rightOfLeftChild);

            _nodeRotator.RightRotation(node);

            Assert.AreEqual(rightOfLeftChild, node.LeftChild);
        }    
        
        [TestMethod]
        public void Right_Rotation_LeftChild_Of_Node_Becomes_Parent_RightChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 3.5f,
                LeftChildValue = 2.5f,
                
                ParentValue = 2f,
                NodeValue = 3f
            };

            var node = Setup.CreateStubNodesConstruction(stubTreeData);

            var parent = node.Parent;
            var leftChild = node.LeftChild;
            
            _nodeRotator.RightRotation(node);

            Assert.AreEqual(leftChild, parent.RightChild);
        }        
        
        [TestMethod]
        public void Right_Rotation_LeftChild_Of_Node_Becomes_Parent_LeftChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 2.5f,
                LeftChildValue = 1.5f,
                
                ParentValue = 3f,
                NodeValue = 2f
            };

            var node = Setup.CreateStubNodesConstruction(stubTreeData);

            var parent = node.Parent;
            var leftChild = node.LeftChild;
            
            _nodeRotator.RightRotation(node);

            Assert.AreEqual(leftChild, parent.LeftChild);
        } 
        
        [TestMethod]
        public void Left_Rotation_LeftChild_Of_RightChild_Becomes_Node_RightChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 4.5f,
                LeftChildValue = 3.5f,
                
                ParentValue = 3f,
                NodeValue = 4f
            };

            var leftOfRightChild = new Node(4.25f);

            var node = Setup.CreateStubNodesConstruction(stubTreeData);
            node.RightChild.SetLeftChild(leftOfRightChild);

            _nodeRotator.LeftRotation(node);

            Assert.AreEqual(leftOfRightChild, node.RightChild);
        }    
        
        [TestMethod]
        public void Left_Rotation_RightChild_Of_Node_Becomes_Parent_RightChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 4.5f,
                LeftChildValue = 3.5f,
                
                ParentValue = 3f,
                NodeValue = 4f
            };

            var node = Setup.CreateStubNodesConstruction(stubTreeData);

            var parent = node.Parent;
            var rightChild = node.RightChild;
            
            _nodeRotator.LeftRotation(node);

            Assert.AreEqual(rightChild, parent.RightChild);
        }        
        
        [TestMethod]
        public void Left_Rotation_RightChild_Of_Node_Becomes_Parent_LeftChild()
        {
            var stubTreeData = new NodeConstructionStubData
            {
                RightChildValue = 2.5f,
                LeftChildValue = 1.5f,
                
                ParentValue = 3f,
                NodeValue = 2f
            };

            var node = Setup.CreateStubNodesConstruction(stubTreeData);

            var parent = node.Parent;
            var rightChild = node.RightChild;
            
            _nodeRotator.LeftRotation(node);

            Assert.AreEqual(rightChild, parent.LeftChild);
        } 
    }
}