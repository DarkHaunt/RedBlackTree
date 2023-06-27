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
    }
}