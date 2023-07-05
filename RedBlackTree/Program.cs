using System;

namespace RedBlackTreeRealisation
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree();
            
            tree.Insert(8f);
            tree.Insert(5f);
            tree.Insert(15f);
            tree.Insert(12f);
            tree.Insert(19f);
            tree.Insert(9f);
            tree.Insert(13f);
            tree.Insert(23f);
            tree.Insert(10f);
            
            tree.PrintTree();

            Console.WriteLine("---------\n\n");
            
            tree.DeleteNode(12f);
            tree.PrintTree();

            Console.ReadLine();
        }
    }
}