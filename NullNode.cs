using System;


namespace RedBlackTree.Nullables
{
    class NullNode : Node
    {
        public override bool IsNull => true;


        public NullNode() : base(float.NaN) 
        { 
        }


        public override string ToString() => "Null Node";

        public override void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Null Node");
            Console.ResetColor();
        }
    }
}
