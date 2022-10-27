using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3676();
            Welcome3937();
            Console.ReadKey();
        }

        static partial void Welcome3937();
        private static void Welcome3676()
        {
            Console.Write("Enter your name: ");
            string user_name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", user_name);
        }
    }
}
