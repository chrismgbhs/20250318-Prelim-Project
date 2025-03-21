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
            

            // SETTING THE NUMBER OF DISKS
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
                Console.WriteLine($"Move counter: {moveCounter}");
                Console.WriteLine();

                // PRINTING OF TOWERS
                for (int towerRows = 0; towerRows < disksInput + 2; towerRows++)
                {
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
                Console.WriteLine($"\t-0-\t-1-\t-2-");

                if (moveCounter == 0)
                {
                    Console.WriteLine("Welcome to the Tower of Hanoi!" +
                                      "\nYour goal is to move all the 'Disks' from tower 0 to tower 2." +
                                      "\nRemember! You can't put a disk of higher value on top of a disk with lower value!" +
                                      "\nReady? Press any key to start!");
                    Console.ReadKey();
                }

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
                        Console.WriteLine("From which tower will the disk be coming from? Only input 0, 1 or 2.");
                        sourceParsed = int.TryParse(Console.ReadLine(), out sourceTower);

                        if (sourceParsed == false || sourceTower < 0 || sourceTower > towers.Length - 1)
                        {
                            Console.WriteLine("Invalid input.");
                            backToSourceInput = true;
                        }

                        else
                        {
                            if (towerFilledSlotsCounter[sourceTower] == 0)
                            {
                                Console.WriteLine("There are no disks to move from this tower!");
                                backToSourceInput = true;
                            }
                        }
                    }

                    Console.WriteLine("To which tower will the disk be going to? Only input 0, 1, or 2.");
                    targetParsed = int.TryParse(Console.ReadLine(), out targetTower);

                    if (targetParsed == false || targetTower < 0 || targetTower > towers.Length - 1)
                    {
                        Console.WriteLine("Invalid input.");
                        targetTower = -1; 
                    }

                    else
                    {
                        if (sourceTower == targetTower)
                        {
                            Console.WriteLine("Move is not allowed with the same tower.");
                            targetTower = -1;
                        }

                        else if (towerFilledSlotsCounter[targetTower] == disksInput)
                        {
                            Console.WriteLine("The tower is already filled!");
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
                        Console.WriteLine("The disk that is going to move has a higher value than the disk it is going on top of.");
                        //Console.WriteLine($"Source tower {sourceTower}'s disk ({discs[towers[sourceTower][towerUnfilledSlotsCounter[sourceTower] + 2]]}) that will be moved has a value of {towers[sourceTower][towerUnfilledSlotsCounter[sourceTower] + 2]} while the target tower {targetTower}'s disk ({discs[towers[targetTower][towerUnfilledSlotsCounter[targetTower] + 2]]}) that will be stacked upon has a value of {towers[targetTower][towerUnfilledSlotsCounter[targetTower] + 2]}.");
                    }
                }
            
                if (moveValues)
                {
                    towers[targetTower][towerUnfilledSlotsCounter[targetTower] - 1 + 2] = towers[sourceTower][(disksInput + 2) - towerFilledSlotsCounter[sourceTower]];
                    towers[sourceTower][(disksInput + 2) - towerFilledSlotsCounter[sourceTower]] = 0;
                    towerFilledSlotsCounter[sourceTower]--; towerUnfilledSlotsCounter[sourceTower]++;
                    towerFilledSlotsCounter[targetTower]++; towerUnfilledSlotsCounter[targetTower]--;
                    moveCounter++;

                    //Console.WriteLine($"Source Tower {sourceTower} has {towerFilledSlotsCounter[sourceTower]} filled slots.");
                    //Console.WriteLine($"Source Tower {sourceTower} has {towerUnfilledSlotsCounter[sourceTower]} unfilled slots.");
                    //Console.WriteLine($"Target Tower {targetTower} has {towerFilledSlotsCounter[targetTower]} filled slots.");
                    //Console.WriteLine($"Target Tower {targetTower} has {towerUnfilledSlotsCounter[targetTower]} unfilled slots.");
                }

                if (towerFilledSlotsCounter[2] == disksInput)
                {
                    for (int towerRows = 0; towerRows < disksInput + 2; towerRows++)
                    {
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
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
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
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                            }
                            Console.Write($"\t{discs[towers[towerCounter][towerRows]]}");
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();
                    Console.WriteLine($"\t-0-\t-1-\t-2-");

                    accuracy = (desiredMoves / moveCounter) * 100;
                    Console.WriteLine($"Congratulations! You have completed the tower! {moveCounter} out of {desiredMoves}! {accuracy}% accuracy!!");
                    doLoop = false;
                }
                           
            }
        }
    }
}
