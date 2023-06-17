using System;

namespace RedBlackTree.Nullables
{
    public class NullNodeException : ArgumentNullException
    {
        private NullNodeException(string message) : base(message) {}

        public static NullNodeException Create() => new("This node is null-type!");
    }
}