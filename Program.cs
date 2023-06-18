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

            // tree.DeleteNode(3f); // Left root subtree + // TODO: Write unit tests
            // tree.DeleteNode(6f); // Most left in right root subtree +
            // tree.DeleteNode(20f);// Node, with single right parent +
            // tree.DeleteNode(7f); // Delete 2 child node +
            // tree.DeleteNode(5f); // Delete root node +

            Console.WriteLine("---------\n\n");

            tree.PrintTree();

            Console.ReadLine();
        }
    }
}