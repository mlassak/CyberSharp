using System;
using System.Threading;
using CyberSharp.Helpers;

namespace CyberSharp
{
    public class Game : IGame
    {

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Wake up, hackerman, we've got a city to burn. I actually meant that as a metaphor, whatever...");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Enter your name: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            var playerName = Console.ReadLine();   
            if (playerName.ToLower().Equals("thason"))
            {
                Console.WriteLine("Pardon my slightly arrogant behavior kind sir, I'm just a poorly programmed \"NPC\" who worked overtime throughout last week");
            }

            var player = new Player(playerName);


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Available commands are: find, hack, send, bribe, learn, info, win, surrender, help");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("In case you need a manual, call me using 'help' command");
            Console.WriteLine("One more thing, you get caught, we've never met. That's how this business works");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Good luck out there!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(player.StatsToString());
            bool isPlaying = true;
            AbstractPerson target = null;

            while (isPlaying) 
            {
                if (player.CriminalityLevel == 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Looks like you've been flying too close to the sun. Game over!");
                    Console.WriteLine("Don't forget to forget about knowing me of course");
                    break;
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{player.Name}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("]: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                var command = Console.ReadLine();        
                switch (command) 
                {
                    case "find":
                        isPlaying = Find(player, ref target);       
                        break;
                    case "hack":
                        Hack(player, ref target);
                        break;
                    case "send":
                        Send(player, ref target);
                        break;
                    case "bribe":
                        Bribe(player);
                        break;
                    case "learn":
                        Learn(player);
                        break;
                    case "info":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(player.StatsToString());
                        break;
                    case "win":
                        isPlaying = !IsWin(player);
                        break;
                    case "help":
                        PrintHelp();
                        break;
                    case "surrender":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Chickened out already? I'll count this a loss, thought you had some potential...");
                        isPlaying = false;
                        break;
                    case "":
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid command. (a little hint: you might want to use 'help')");
                        break;
                }
            }
        }


        private static void Send(Player player, ref AbstractPerson target)           
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (target == null)
            {
                Console.WriteLine("You have no target to steal from. Find one first, then try your shenanigans");
                return;
            }

            if (target.BtcVallet == null)
            {
                Console.WriteLine("You may have hit a brick wall. Or an empty, non-existent bag.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hack more to find out what happened, or find a new target");
                return;
            }

            //attempt to send
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("**********************************************");                //note: feel free to remove the Sleeps, they only add to the effect of the final act of the grand theft
            Thread.Sleep(750);
            Console.WriteLine($"Attempting to transfer {target.BtcVallet.Balance} BTC");
            Console.WriteLine("**********************************************");
            Thread.Sleep(750);
            Console.WriteLine($"From: { target.BtcVallet.Adress}");
            Console.WriteLine("**********************************************");
            Thread.Sleep(750);
            Console.WriteLine($"  To: {player.BtcVallet.Adress}");
            Console.WriteLine("**********************************************");
            Thread.Sleep(750);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Black;
            if (RNGesus.RNG.Next(0, 101) <= target.CurrentSuccess)          //send successful       
            {
                player.BtcVallet.Deposit(target.BtcVallet.Balance);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Trasaction successful, you recieved {target.BtcVallet.Balance} BTC.");
            }
            else                                                            //send failed
            {
                ++player.CriminalityLevel;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Transaction failed, you have been discovered. This target is lost, find a new one");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Criminality level increased to {player.CriminalityLevel}");
            }
            target = null;        //after a successful or unsuccesful send, target is gone
        }

        private static void Hack(Player player, ref AbstractPerson target)          
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (target == null)
            {
                Console.WriteLine("If you wanna hack something, you should have that something first. Find a target");
                return;
            }

            int randomHack = RNGesus.RNG.Next(0, player.HackingSkill + 1);
            if (target.CurrentSuccess + randomHack > 100)                          //attack
            {
                target.CurrentSuccess = 100;
            }
            else
            {
                target.CurrentSuccess += randomHack;
            }

            target.CurrentSuccess -= target.CalculateDefence();                  //target's reaction

            Console.ForegroundColor = ConsoleColor.Green;
            switch (target.CurrentSuccess)
            {
                case < 0:                                                  //target detected the attack
                    ++player.CriminalityLevel;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Gotta do better next time, your pathetic attmept has been detected. And these are the people I have to work with...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Criminality level increased to {player.CriminalityLevel}");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("You have to find a new target, we're done here... Try to do better next time");
                    target = null;
                    break;
                case > 60:                                                    
                    if (target.BtcVallet != null)                        
                    {
                        Console.WriteLine($"Target's BTC vallet adress is: {target.BtcVallet.Adress}");
                        Console.WriteLine($"Targets password is: {target.BtcVallet.Password}");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Truly impressive, hackerman...");
                    }
                    else                                  
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Target doesn't own a BTC vallet, gotta find a new one.");
                        Console.WriteLine("You would't want to smash your head against a brick wall, would ya? We're done here");
                        target = null;
                    }
                    break;
                case > 30:
                    if (target.BtcVallet != null)
                    {
                        Console.WriteLine($"Target's BTC vallet adress is: {target.BtcVallet.Adress}");         
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Target doesn't own a BTC vallet, gotta find a new one, we're done here");
                        target = null;
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("If you want to know more about your prey, you gotta try harder than this.");
                    break;
            }
            if (target != null)
            {
                ++target.HackCounter;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Current hacking success on this target is: {target.CurrentSuccess}");
            }
        }

        
        private static bool Find(Player player, ref AbstractPerson target)    
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (!player.FindTarget())
            {
                if (target == null)           
                {
                    Console.WriteLine("No target, no money to find a new one... Thanks for wasting my time. Game over!");
                    return false;
                }
                Console.WriteLine("Looks like you gotta commit to the current target, you don't have money to a scout a new one");
                Console.WriteLine("I'm not a charity corp, alright? You either pay up, or you surrender, there is no other way out");
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            target = (RNGesus.RNG.Next(1, 11)) switch                       
            {
                1 => new EpicPerson(),
                < 5 => new RarePerson(),
                _ => new CommonPerson(),
            };
            Console.WriteLine("Found you a new target. " + target.ToString());
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Now do what you do best, hackerman");
            return true;
        }

        private static void Bribe(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            switch (player.DecreaseCriminalityLevel())
            {
                case 1:
                    if (player.CriminalityLevel % 2 == 0)
                        Console.WriteLine("Looks like you've been a good citizen. Or know the right people. Or even both? Whatever, who am I to judge, right?");
                    else
                        Console.WriteLine("You've helped an old lady to cross the road. A hero we didn't deserve");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Criminality level reduced to {player.CriminalityLevel}");
                    break;
                case -1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("If you wanna go under the radar, you gotta cover yourself with some money first.");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("And it seems you've got none, so get out there and earn some. Or get caught. I don't care.");
                    break;
                default:
                    Console.WriteLine("Seems like your crime register is as clear as day. A clean professional, or an underachiever? Show me");
                    break;
            }
        }


        private static void Learn(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (player.IncreaseHack())
            {
                Console.WriteLine($"Hacking skill increased to {player.HackingSkill}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Night City is not in Europe, even illegal education doesn't come for free here...");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not enough BTC to increase hacking skill, get some money first");
            }
        }


        private static bool IsWin(Player player)    
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (player.BtcVallet.Balance >= 5)
            {
                Console.WriteLine("Well, congratulations, welcome to the \"rich\" club! Grab me a drink later, you know, for all the *kind* help I gave ya...");
                return true;
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Gotta earn more if you wanna end it this early, stealing a few virtual cents won't cut it. Go on, impress me");
            return false;
        }


        private static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Available commands:");
            string[] commands = { "find", "hack", "send", "bribe", "learn", "info", "win", "surrender" };
            string[] guides = { 
                "spend 0.01BTC to find a new target to hack",
                "hack the previously found target person",
                "attempt to transfer money from target's BTC vallet to yours",
                "spend 0.05BTC to decrease criminality level by 1",
                "spend 0.005BTC to increase hacking skill by 1",
                "prints current hacking skill, criminality level and BTC vallet balance",
                "ends the game as a win if there is more than 5BTC in your BTC vallet",
                "ends the game as a loss"
            };
            
            for (int i = 0; i < commands.Length; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(commands[i]);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" -> ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(guides[i]);
            }
        }
    }
}
