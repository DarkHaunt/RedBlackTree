
namespace RedBlackTree.Nullables
{
    class NullNode : Node
    {
        public override bool IsNull => true;


        public NullNode() : base(float.NaN) 
        { 
        }
    }
}
