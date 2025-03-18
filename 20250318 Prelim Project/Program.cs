using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250318_Prelim_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] towers = new int[3][];
            string[] discs = { "  |  ", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]", "[10]" };

            int sourceTower = 0;
            int targetTower = 0;

            int t1filled = 3; int t1unfilled = 0;
            int t2filled = 0; int t2unfilled = 3;
            int t3filled = 0; int t3unfilled = 3;

            int disksInput = 0;

            while (disksInput < 3 || disksInput > 10)
            {
                Console.WriteLine("Please enter how many disks you would like to play with.");
                int.TryParse(Console.ReadLine(), out disksInput);

                Console.WriteLine();
                if (disksInput < 3 || disksInput > 10)
                {
                    Console.WriteLine("Your input must be a number (3-9).");
                }
            }

            for (int counter = 0; counter < towers.Length; counter++)
            {
                int[][] 
            }
        }
    }
}
