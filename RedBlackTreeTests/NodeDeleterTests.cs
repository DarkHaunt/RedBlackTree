using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeRealisation.Nullables;
using RedBlackTreeRealisation.Nodes;
using RedBlackTreeTests.TestsSetup;


namespace RedBlackTreeTests
{
    [TestClass]
    public class NodeDeleterTests
    {
        private NodeDeleter _nodeDeleter;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var rotator = Setup.CreateNodeRotator();
            
            _nodeDeleter = Setup.CreateNodeDeleter(rotator);
        }

        [TestMethod]
        public void Delete_Leaf_Node_Gives_Parent_Null_Child()
        {
            var nodeConstructData = new NodeConstructionStubData
            {
                NodeValue = 2f,
                LeftChildValue = 1f,
            };
            
            var nodeConstruct = Setup.CreateStubNodesConstruction(nodeConstructData);
            
            _nodeDeleter.DeleteNode(nodeConstruct.LeftChild, nodeConstruct);

            Assert.AreEqual(nodeConstruct.LeftChild, NullableContainer.NullNode);
        }
    }
}