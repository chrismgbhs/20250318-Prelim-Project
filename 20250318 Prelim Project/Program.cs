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
            int[] towerFilledSlotsCounter = { 0, 0, 0 };
            int[] towerUnfilledSlotsCounter = { 0, 0, 0 };

            int sourceTower;
            int targetTower;
            int disksInput = 0;
            double moveCounter = 0;
            double desiredMoves = 2;
            double accuracy = 0;

            bool doLoop = true;
            bool backToSourceInput = true;
            bool checkSourceTower = true;
            bool moveValues = true;
            bool sourceParsed;
            bool targetParsed;
            bool gameCompleted = false;
            

            // SETTING THE NUMBER OF DISKS
            while (disksInput < 3 || disksInput > 10)
            {
                Console.WriteLine("\tPlease enter how many disks you would like to play with.");
                Console.Write("\t");
                int.TryParse(Console.ReadLine(), out disksInput);

                if (disksInput < 3 || disksInput > 10)
                {
                    Console.WriteLine();
                    Console.WriteLine("\tYour input must be a number (3-10).");
                }
            }

            // DETERMINING THE DESIRED NUMBER OF MOVES
            for (int counter = 1; counter < disksInput; counter++)
            {
                desiredMoves *= 2;
            }
            desiredMoves--;


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

            towerFilledSlotsCounter[0] = disksInput;
            towerUnfilledSlotsCounter[1] = disksInput;
            towerUnfilledSlotsCounter[2] = disksInput;

            while (doLoop)
            {
                Console.WriteLine();
                Console.WriteLine($"\tMove counter: {moveCounter}");
                Console.WriteLine();

                // PRINTING OF TOWERS
                for (int towerRows = 0; towerRows < disksInput + 2; towerRows++)
                {
                    Console.Write("\t");
                    for (int towerCounter = 0; towerCounter < towers.Length; towerCounter++)
                    {
                        switch (towers[towerCounter][towerRows])
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 7:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                            case 8:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 9:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 10:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 11:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                        }
                        Console.Write($"\t{discs[towers[towerCounter][towerRows]]}");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
                Console.WriteLine($"\t\t-0-\t-1-\t-2-");

                if (gameCompleted == true)
                {
                    accuracy = (desiredMoves / moveCounter) * 100;
                    Console.WriteLine($"\tCongratulations! You have completed the tower! {moveCounter} out of {desiredMoves}! {accuracy}% accuracy!!");
                    doLoop = false;
                }

                if (moveCounter == 0)
                {
                    Console.WriteLine("\tWelcome to the Tower of Hanoi!" +
                                      "\n\tYour goal is to move all the 'Disks' from tower 0 to tower 2." +
                                      "\n\tRemember! You can't put a disk of higher value on top of a disk with lower value!" +
                                      "\n\tReady? Press any key to start!");
                    Console.Write("\t");
                    Console.ReadKey();
                }

                if (gameCompleted == false) 
                {
                    sourceTower = -1;
                    targetTower = -1;
                    checkSourceTower = true;

                    // GETTING THE INPUT FOR TARGET TOWER AND SOURCE TOWER
                    while (targetTower < 0 || targetTower > towers.Length - 1)
                    {
                        backToSourceInput = true;
                        while (backToSourceInput)
                        {
                            backToSourceInput = false;
                            Console.WriteLine();
                            Console.WriteLine("\tFrom which tower will the disk be coming from? Only input 0, 1 or 2.");
                            Console.Write("\t");
                            sourceParsed = int.TryParse(Console.ReadLine(), out sourceTower);

                            if (sourceParsed == false || sourceTower < 0 || sourceTower > towers.Length - 1)
                            {
                                Console.WriteLine("\tInvalid input.");
                                backToSourceInput = true;
                            }

                            else
                            {
                                if (towerFilledSlotsCounter[sourceTower] == 0)
                                {
                                    Console.WriteLine("\tThere are no disks to move from this tower!");
                                    backToSourceInput = true;
                                }
                            }
                        }

                        Console.WriteLine("\tTo which tower will the disk be going to? Only input 0, 1, or 2.");
                        Console.Write("\t");
                        targetParsed = int.TryParse(Console.ReadLine(), out targetTower);

                        if (targetParsed == false || targetTower < 0 || targetTower > towers.Length - 1)
                        {
                            Console.WriteLine("\tInvalid input.");
                            targetTower = -1;
                        }

                        else
                        {
                            if (sourceTower == targetTower)
                            {
                                Console.WriteLine("\tMove is not allowed with the same tower.");
                                targetTower = -1;
                            }

                            else if (towerFilledSlotsCounter[targetTower] == disksInput)
                            {
                                Console.WriteLine("\tThe tower is already filled!");
                                targetTower = -1;
                            }
                        }
                    }

                    moveValues = true;
                    if (towerUnfilledSlotsCounter[targetTower] != disksInput)
                    {
                        if (towers[sourceTower][towerUnfilledSlotsCounter[sourceTower] + 2] > towers[targetTower][towerUnfilledSlotsCounter[targetTower] + 2])
                        {
                            moveValues = false;
                            Console.WriteLine("\tThe disk that is going to move has a higher value than the disk it is going on top of.");
                        }
                    }

                    if (moveValues)
                    {
                        towers[targetTower][towerUnfilledSlotsCounter[targetTower] - 1 + 2] = towers[sourceTower][(disksInput + 2) - towerFilledSlotsCounter[sourceTower]];
                        towers[sourceTower][(disksInput + 2) - towerFilledSlotsCounter[sourceTower]] = 0;
                        towerFilledSlotsCounter[sourceTower]--; towerUnfilledSlotsCounter[sourceTower]++;
                        towerFilledSlotsCounter[targetTower]++; towerUnfilledSlotsCounter[targetTower]--;
                        moveCounter++;
                    }

                    if (towerFilledSlotsCounter[2] == disksInput)
                    {
                        gameCompleted = true;
                    }
                }                         
            }
        }
    }
}
