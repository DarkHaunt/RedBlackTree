using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree(5f);

            tree.Insert(20f);
            tree.Insert(3f);
            tree.Insert(9f);
            tree.Insert(8f);
            tree.Insert(7f);
            tree.Insert(6f);

            tree.PrintTree();

            Console.ReadLine();
        }
    }
}