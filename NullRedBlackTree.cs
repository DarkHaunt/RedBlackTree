
namespace RedBlackTree.Nullables
{
    class NullRedBlackTree : RedBlackTree
    {
        public override bool IsNull => true;


        public NullRedBlackTree() : base(float.NaN) { }
    }
}
