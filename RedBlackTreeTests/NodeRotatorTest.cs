using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeRealisation;
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
        public void Right_Rotation_Parent_Node_Transparenting()
        {
            var grandparentNode = new Node(2f);
            grandparentNode.SetColor(Color.Black);

            var uncle = new Node(1f);
            uncle.SetColor(Color.Black);

            var parent = new Node(3f);
            parent.SetColor(Color.Red);

            var node = new Node(4f);

            grandparentNode.SetRightChild(parent);
            grandparentNode.SetLeftChild(uncle);
            parent.SetRightChild(node);

            _nodeRotator.RightRotation(node);

            Assert.AreEqual(parent.LeftChild, grandparentNode);
            Assert.AreEqual(grandparentNode.LeftChild, uncle);
            Assert.AreEqual(parent.RightChild, node);
        }
    }
}