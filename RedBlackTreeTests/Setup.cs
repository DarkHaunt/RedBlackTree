using RedBlackTreeRealisation.Nodes;
using RedBlackTreeRealisation;

namespace RedBlackTreeTests.TestsSetup
{
    public static class Setup
    {
        public static RedBlackTree CreateTree()
            => new RedBlackTree();   
        
        public static NodeRotator CreateNodeRotator()
            => new NodeRotator();
    }
}
