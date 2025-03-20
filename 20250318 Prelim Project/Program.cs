using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            string[] discs = { " |  ", " |  ", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]", "[10]" };
            int[] towerValuesSum = new int[3];

            int sourceTower;
            int targetTower;
            int disksInput = 0;
            int moveCounter = 0;

            int t1filled = 0; int t1unfilled = 0;
            int t2filled = 0; int t2unfilled = 3;
            int t3filled = 0; int t3unfilled = 3;

            bool doLoop = true;
            bool checkSourceTower = true;

            while (disksInput < 3 || disksInput > 10)
            {
                Console.WriteLine("Please enter how many disks you would like to play with.");
                int.TryParse(Console.ReadLine(), out disksInput);

                Console.WriteLine();
                if (disksInput < 3 || disksInput > 10)
                {
                    Console.WriteLine("Your input must be a number (3-10).");
                }
            }

            t1filled = disksInput;

            // ASSIGNING OF NUMBER OF SLOTS OF EACH TOWERS
            for (int counter = 0; counter < towers.Length; counter++)
            {
                towers[counter] = new int[disksInput + 2];
            }

            // ASSIGNING OF VALUES FOR EACH TOWERS (FOR THE FIRST TIME)
            for (int towerCounter = 0; towerCounter < towers.Length; towerCounter++)
            {
                if (towerCounter == 0)
                {
                    for (int row = 0; row < disksInput + 2; row++)
                    {
                        towers[towerCounter][row] = row;
                    }
                }

                else
                {
                    for (int row = 0; row < disksInput + 2; row++)
                    {
                        towers[towerCounter][row] = 0;
                    }
                }

            }

            sourceTower = -1;
            targetTower = -1;
            checkSourceTower = true;

            // GETTING THE SUM OF THE VALUES OF EACH TOWER
            for (int counter = 0; counter < towers.Length; counter++) 
            {
                foreach (int rowValue in towers[counter])
                {
                    towerValuesSum[counter] += rowValue;
                }
            }

            Console.WriteLine($"Move counter: {moveCounter}");
            Console.WriteLine();

            // PRINTING OF TOWERS
            for (int towerRows = 0; towerRows < disksInput + 2; towerRows++)
            {
                for (int towerCounter = 0; towerCounter < towers.Length; towerCounter++)
                {
                    Console.Write($"\t{discs[towers[towerCounter][towerRows]]}");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\t-0-\t-1-\t-2-");

            if (moveCounter == 0)
            {
                Console.WriteLine("Welcome to the Tower of Hanoi!" +
                                  "\nYour goal is to move all the 'Disks' from tower 0 to tower 2." +
                                  "\nRemember! You can't put a disk of higher value on top of a disk with lower value!" +
                                  "\nReady? Press any key to start!");
                Console.ReadKey();
            }

            // GETTING THE INPUT FOR SOURCE TOWER
            while (sourceTower < 0 || sourceTower > towers.Length - 1)
            {
                Console.WriteLine("From which tower will the disk be coming from? Only input 0, 1 or 2.");
                int.TryParse(Console.ReadLine(), out sourceTower);

                if (sourceTower < 0 || sourceTower > towers.Length - 1)
                {
                    Console.WriteLine("Invalid input.");
                    sourceTower = -1;
                }

                else
                {
                    if (towerValuesSum[sourceTower] == 0)
                    {
                        Console.WriteLine("There are no disks to move from this tower!");
                        sourceTower = -1;
                    }
                }
            }

            // GETTING THE INPUT FOR TARGET TOWER
            while (targetTower < 0 || targetTower > towers.Length - 1)
            {
                Console.WriteLine("To which tower will the disk be goint to? Only input 0, 1, or 2.");
                int.TryParse(Console.ReadLine(), out targetTower);

                if (targetTower < 0 || targetTower > towers.Length - 1)
                {
                    Console.WriteLine("Invalid input.");
                }

                else
                {
                    if (sourceTower == targetTower)
                    {
                        Console.WriteLine("Move is not allowed with the same tower.");
                        targetTower = -1;
                    }

                    else
                    {
                        if (towerValuesSum[sourceTower] > towerValuesSum[targetTower] && towerValuesSum[targetTower] > 0)
                        {
                            Console.WriteLine("The disk that is going to move has a higher value than the disk it is going on top of.");
                            targetTower = -1;
                        }
                    }
                }
            }
        }
    }
}
