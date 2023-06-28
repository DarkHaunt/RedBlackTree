using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeRealisation.Nodes;
using RedBlackTreeTests.TestsSetup;
using RedBlackTreeRealisation;


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
        public void Right_Right_Rotation_Case()
        {
            var stubTreeData = new RotationTreeStubData
            {
                GrandparentValue = 2f,
                ParentValue = 3f,
                UncleValue = 1f,
                NodeValue = 4f,
            };

            var node = Setup.CreateStubRotationTree(stubTreeData);
            
            var grandparentNode = node.Grandparent;
            var parent = node.Parent;
            var uncle = node.Uncle;
            
            var parentLeftChild = new Node(2.5f);
            parentLeftChild.SetParent(parent);
            
            parent.SetLeftChild(parentLeftChild);
            parent.SetRightChild(node);

            grandparentNode.SetRightChild(parent);
            grandparentNode.SetLeftChild(uncle);

            _nodeRotator.RightRightRotation(node);

            // Links match
            Assert.AreEqual(grandparentNode.RightChild, parentLeftChild);
            Assert.AreEqual(parent.LeftChild, grandparentNode);
            Assert.AreEqual(grandparentNode.LeftChild, uncle);
            Assert.AreEqual(parent.RightChild, node);

            // Color match
            Assert.AreEqual(grandparentNode.Color, Color.Red);
            Assert.AreEqual(parent.Color, Color.Black);
            Assert.AreEqual(uncle.Color, Color.Black);
            Assert.AreEqual(node.Color, Color.Red);
        }

        [TestMethod]
        public void Left_Left_Rotation_Case()
        {
            var stubTreeData = new RotationTreeStubData
            {
                GrandparentValue = 3f,
                ParentValue = 2f,
                UncleValue = 4f,
                NodeValue = 1f,
            };

            var node = Setup.CreateStubRotationTree(stubTreeData);
            
            var grandparentNode = node.Grandparent;
            var parent = node.Parent;
            var uncle = node.Uncle;
            
            var parentRightChild = new Node(2.5f);
            parentRightChild.SetParent(parent);

            parent.SetRightChild(parentRightChild);
            parent.SetLeftChild(node);
            
            grandparentNode.SetRightChild(uncle);
            grandparentNode.SetLeftChild(parent);

            _nodeRotator.LeftLeftRotation(node);

            // Links match
            Assert.AreEqual(grandparentNode.LeftChild, parentRightChild);
            Assert.AreEqual(parent.RightChild, grandparentNode);
            Assert.AreEqual(grandparentNode.RightChild, uncle);
            Assert.AreEqual(parent.LeftChild, node);
            
            // Color match
            Assert.AreEqual(grandparentNode.Color, Color.Red);
            Assert.AreEqual(parent.Color, Color.Black);
            Assert.AreEqual(uncle.Color, Color.Black);
            Assert.AreEqual(node.Color, Color.Red);
        }

        [TestMethod]
        public void Right_Left_Rotation_Case()
        {
            var stubTreeData = new RotationTreeStubData
            {
                GrandparentValue = 4f,
                ParentValue = 6f,
                UncleValue = 1f,
                NodeValue = 5f,
            };

            var node = Setup.CreateStubRotationTree(stubTreeData);
            
            var grandparentNode = node.Grandparent;
            var parent = node.Parent;
            var uncle = node.Uncle;
            
            var nodeLeftChild = new Node(4.5f);
            nodeLeftChild.SetParent(node);

            grandparentNode.SetRightChild(parent);
            grandparentNode.SetLeftChild(uncle);
            
            node.SetLeftChild(nodeLeftChild);
            parent.SetLeftChild(node);

            _nodeRotator.RightLeftRotation(node);

            // Links match
            Assert.AreEqual(grandparentNode.RightChild, nodeLeftChild);
            Assert.AreEqual(grandparentNode.LeftChild, uncle);
            Assert.AreEqual(node.LeftChild, grandparentNode);
            Assert.AreEqual(node.RightChild, parent);
            
            // Color match
            Assert.AreEqual(grandparentNode.Color, Color.Red);
            Assert.AreEqual(uncle.Color, Color.Black);
            Assert.AreEqual(parent.Color, Color.Red);
            Assert.AreEqual(node.Color, Color.Black);
        }       
        
        [TestMethod]
        public void Left_Right_Rotation_Case()
        {
            var stubTreeData = new RotationTreeStubData
            {
                GrandparentValue = 5f,
                ParentValue = 3f,
                UncleValue = 6f,
                NodeValue = 4f,
            };

            var node = Setup.CreateStubRotationTree(stubTreeData);
            
            var grandparentNode = node.Grandparent;
            var parent = node.Parent;
            var uncle = node.Uncle;
            
            var nodeRightChild = new Node(4.5f);
            nodeRightChild.SetParent(node);

            grandparentNode.SetLeftChild(parent);
            grandparentNode.SetRightChild(uncle);
            
            node.SetRightChild(nodeRightChild);
            parent.SetRightChild(node);

            _nodeRotator.LeftRightRotation(node);

            // Links match
            Assert.AreEqual(grandparentNode.LeftChild, nodeRightChild);
            Assert.AreEqual(grandparentNode.RightChild, uncle);
            Assert.AreEqual(node.RightChild, grandparentNode);
            Assert.AreEqual(node.LeftChild, parent);
            
            // Color match
            Assert.AreEqual(grandparentNode.Color, Color.Red);
            Assert.AreEqual(uncle.Color, Color.Black);
            Assert.AreEqual(parent.Color, Color.Red);
            Assert.AreEqual(node.Color, Color.Black);
        }
    }
}