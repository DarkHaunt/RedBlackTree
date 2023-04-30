using RedBlackTree.Nullables;


namespace RedBlackTree
{
    class RedBlackTree : INullable
    {
        private readonly float _value;

        private Color _color;
        private RedBlackTree _leftChild;
        private RedBlackTree _rightChild;


        public virtual bool IsNull => false;


        public RedBlackTree(float value)
        {
            _value = value;
            _color = Color.Red;

            _leftChild = NullableContainer.NullRedTree;
            _rightChild = NullableContainer.NullRedTree;
        }


        public void Balance()
        {

        }

        public void Insert(float value)
        {

        }

        public void Delete(float value)
        {

        }
    }

    public enum Color
    {
        Black = 0,
        Red = 1
    }
}
