using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree(5f);

            tree.Insert(20);
            tree.Insert(15);
            /*tree.Insert(13);
            tree.Insert(1);*/

            tree.PrintTree();

            Console.ReadLine();
        }
    }
}