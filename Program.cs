using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree(5f);

            tree.Insert(20f);
            tree.Insert(15f);
            tree.Insert(3f);
            tree.Insert(2f);
            
            tree.Insert(4f);
            tree.Insert(30);

            tree.PrintTree();

            Console.ReadLine();
        }
    }
}