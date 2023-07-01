using System;

namespace RedBlackTreeRealisation
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree(41f);

            tree.Insert(22f);
            tree.Insert(45f);
            tree.Insert(78f);
            tree.Insert(99f);
            tree.Insert(11f);
            tree.Insert(12f);
            tree.Insert(44f);
            tree.Insert(43f);

            tree.PrintTree();

            Console.WriteLine("---------\n\n");

            //tree.PrintTree();

            Console.ReadLine();
        }
    }
}