using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CleanHouse
{
    public class Program
    {
        private static Player _playerTank;
        private static Weapon _playerWeapon;
        private static List<Bot> _bots;
        private static List<Tank> _allTanks;
        private static List<Wall> _allWalls;

        public static void Main(string[] args)
        {
            // Initialize the window for the tank game
            Window window = new Window("Tank Game", 800, 600);
            _allTanks = new List<Tank>();
            _bots = new List<Bot>();
            _allWalls = new List<Wall>();

            // Create a weapon for the player tank
            _playerWeapon = new Weapon(new string[] { "player_weapon" }, WeaponType.TANK);

            // Create the player tank
            _playerTank = new Player(
                new string[] { "PlayerTank" },
                400, 300, // Starting position (center of the window)
                _playerWeapon,
                _allTanks, // Pass allTanks to the player
                _allWalls
            );

            // Add the player tank to the allTanks list
            _allTanks.Add(_playerTank);

            // Create bots
            CreateBots();

            // Create walls once (outside the game loop)
            CreateWalls();



            // Main game loop
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                // Only update and draw tanks here
                for (int i = 0; i < _allTanks.Count; i++)
                {
                    var tank = _allTanks[i];
                    tank.Update(); // Update tank behavior
                    tank.Draw();           // Draw the tank
                }

                // Draw all walls (no need to create them again)
                foreach (Wall wall in _allWalls)
                {
                    wall.Draw();
                }

                SplashKit.RefreshScreen();



                if (_playerTank.Health <= 0)   //If the player dies
                {
                    // Draw the text with the loaded font
                    SplashKit.FillRectangle(Color.Black, 250, 250, 200, 100);
                    SplashKit.DrawText("Game Over!", Color.White, 300, 300); 
                    
                    SplashKit.RefreshScreen();
                    SplashKit.Delay(3000); // Wait for 3 seconds to show the message
                    break; // Exit the game loop
                }

                else if (_allTanks.Count == 1)
                {
                    // Draw the text with the loaded font
                    SplashKit.FillRectangle(Color.Black, 250, 250, 200, 100);
                    SplashKit.DrawText("You Win", Color.White, 300, 300);
                                        SplashKit.RefreshScreen();
                    SplashKit.Delay(3000); // Wait for 3 seconds to show the message
                    break; // Exit the game loop
                }
            } while (!window.CloseRequested);
        }

        private static void CreateBots()
        {

            var cannon = new Weapon(new string[] { "Cannon" }, WeaponType.TANK);
            var machine_gun = new Weapon(new string[] { "MachineGun" }, WeaponType.MACHINE_GUN);
            int detection_range = 200;
            // Bot 1: Tank weapon
            var bot1 = new Bot(
                new string[] { "Bot1" },
                100, 100, // Starting position
                cannon,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot1);
            _allTanks.Add(bot1);

            // Bot 2: Machine gun
            var bot2 = new Bot(
                new string[] { "Bot2" },
                600, 100, // Starting position
                machine_gun,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot2);
            _allTanks.Add(bot2);

            // Bot 3: Machine gun
            var bot3Weapon = new Weapon(new string[] { "MachineGun" }, WeaponType.MACHINE_GUN);
            var bot3 = new Bot(
                new string[] { "Bot3" },
                400, 500, // Starting position
                machine_gun,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot3);
            _allTanks.Add(bot3);



            // Bot 4: machine weapon
            var bot4 = new Bot(
                new string[] { "Bot4" },
                200, 400, // Starting position
                machine_gun,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot4);
            _allTanks.Add(bot4);

            // Bot 5: Machine gun
            var bot5 = new Bot(
                new string[] { "Bot5" },
                600, 400, // Starting position
                machine_gun,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot5);
            _allTanks.Add(bot5);

            // Bot 6: Tank weapon
            var bot6 = new Bot(
                new string[] { "Bot6" },
                700, 250, // Starting position
                cannon,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot6);
            _allTanks.Add(bot6);

            // Bot 7: Tank gun
            var bot7 = new Bot(
                new string[] { "Bot7" },
                100, 500, // Starting position
                cannon,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot7);
            _allTanks.Add(bot7);

            // Bot 8: Tank weapon
            var bot8 = new Bot(
                new string[] { "Bot8" },
                500, 100, // Starting position
                cannon,
                _playerTank, // Reference to the player
                detection_range,         // Detection range
                _allTanks,   // Pass allTanks for collision
                _allWalls
            );
            _bots.Add(bot8);
            _allTanks.Add(bot8);




        }

        private static void CreateWalls()
        {
            // Maze-like walls (adjusted to avoid touching the tanks)
            _allWalls.Add(new Wall(new string[] { "Wall1" }, 0, 0, 20, 600));   // Left vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall2" }, 780, 0, 20, 600)); // Right vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall3" }, 0, 0, 800, 20));   // Top horizontal wall
            _allWalls.Add(new Wall(new string[] { "Wall4" }, 0, 580, 800, 20)); // Bottom horizontal wall

            // Inner maze walls (adjusted positions to ensure no overlap with tanks)
            _allWalls.Add(new Wall(new string[] { "Wall5" }, 60, 60, 20, 200));  // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall6" }, 400, 100, 20, 200)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall7" }, 200, 250, 120, 20)); // Horizontal wall
            _allWalls.Add(new Wall(new string[] { "Wall8" }, 500, 200, 200, 20)); // Horizontal wall
            _allWalls.Add(new Wall(new string[] { "Wall9" }, 600, 150, 20, 200)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall10" }, 350, 350, 120, 20)); // Horizontal wall
            _allWalls.Add(new Wall(new string[] { "Wall11" }, 200, 500, 20, 100)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall12" }, 600, 500, 20, 100)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall13" }, 300, 500, 20, 100)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall14" }, 450, 100, 20, 100)); // Vertical wall
            _allWalls.Add(new Wall(new string[] { "Wall15" }, 700, 100, 20, 100)); // Vertical wall
        }
    }
}
