using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeTests.TestsSetup;
using RedBlackTreeRealisation;


namespace RedBlackTreeTests
{
    [TestClass]
    public class RedBlackTreeTest
    {
        private RedBlackTree _tree;

        [TestInitialize]
        public void TestInitialize()
        {
            _tree = Setup.CreateTree();
        }

        [TestMethod]
        public void Fail_Found_Returns_NullNode()
        {
            var found = _tree.Find(0f);

            Assert.AreEqual(found.IsNull, true);
        }      
        
        [TestMethod]
        public void Inserted_Element_Found_In_Tree()
        {
            var insertValue = 1f;

            _tree.Insert(insertValue);
            var element = _tree.Find(insertValue);

            Assert.AreEqual(element.Value, insertValue);
        }    
        
        [TestMethod]
        public void Empty_Tree_Root_Is_NullNode()
        {
            var root = _tree.Find(0f);

            Assert.AreEqual(root.IsNull, true);
        }  
        
        [TestMethod]
        public void Deleted_Value_Not_Found()
        {
            var insertValue = 1f;
            var valueToDelete = 2f;
            
            _tree.Insert(insertValue);
            _tree.Insert(valueToDelete);
            
            _tree.DeleteNode(valueToDelete);
            
            var node = _tree.Find(valueToDelete);

            Assert.AreEqual(node.IsNull, true);
        }  
        
        [TestMethod]
        public void Delete_Tree_With_Root_And_One_Child_Child_Become_Root()
        {
            var rootValue = 1f;
            var childValue = 2f;
            
            _tree.Insert(rootValue);
            _tree.Insert(childValue);

            var leftNodeChild = _tree.Find(childValue);
            
            _tree.DeleteNode(rootValue);
            
            Assert.AreEqual(_tree.Root, leftNodeChild);
        }
    }
}