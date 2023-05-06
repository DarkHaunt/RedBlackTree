﻿using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree(10f);

            tree.Insert(5);
            tree.Insert(20);
            tree.Insert(15);

            tree.PrintTree();

            Console.ReadLine();
        }
    }
}
