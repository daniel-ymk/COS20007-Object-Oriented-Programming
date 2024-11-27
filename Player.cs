using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace CleanHouse
{
    public class Player : Tank
    {
        private List<Tank> _allTanks; // Reference to all tanks for collision handling
        private List<Wall> _allWalls;
        public Player(string[] identifiers, float x, float y, Weapon weapon, List<Tank> allTanks, List<Wall> allWalls)
            : base(identifiers, x, y, weapon, allTanks, allWalls)
        {
            _allTanks = allTanks; // Initialize the reference to all tanks
            Speed = 0.08f;
            _allWalls = allWalls;
        }



        // Handles weapon switching based on key input
        public void HandleWeaponSwitch()
        {
            if (SplashKit.KeyTyped(KeyCode.Num1Key)) // Press '1' for Cannon
            {
                Weapon = new Weapon(["Cannon"], WeaponType.TANK);
                Console.WriteLine("Switched to Tank Cannon!");
            }
            else if (SplashKit.KeyTyped(KeyCode.Num2Key)) // Press '2' for Machine Gun
            {
                Weapon = new Weapon(["MachineGun"],WeaponType.MACHINE_GUN);
                Console.WriteLine("Switched to Machine Gun!");
            }
        }



        // Override the Draw method to display player-specific information (e.g., score)
        public override void Draw()
        {
            base.Draw();

            int tankSize = 30; // Size of the tank (adjust as needed)
            SplashKit.DrawCircle(Color.Green, X + tankSize / 2, Y + tankSize / 2, 20);

            // Display the player's health and armor in the top-left corner
            SplashKit.DrawText($"Health: {Health}", Color.Black, "Arial", 16, 40, 40);
            SplashKit.DrawText($"Armor: {Armor}", Color.Black, "Arial", 16, 150, 40);
        }
        // Updates the player's state
        public override void Update()
        {
            // Handle movement input with collision checks
            if (SplashKit.KeyDown(KeyCode.WKey) && CanMove(Direction.UP, _allTanks, _allWalls))
            {
                Move(Direction.UP, _allTanks);
            }
            if (SplashKit.KeyDown(KeyCode.SKey) && CanMove(Direction.DOWN, _allTanks, _allWalls))
            {
                Move(Direction.DOWN, _allTanks);
            }
            if (SplashKit.KeyDown(KeyCode.AKey) && CanMove(Direction.LEFT, _allTanks, _allWalls))
            {
                Move(Direction.LEFT, _allTanks);
            }
            if (SplashKit.KeyDown(KeyCode.DKey) && CanMove(Direction.RIGHT, _allTanks, _allWalls))
            {
                Move(Direction.RIGHT, _allTanks);
            }
            // Handle firing
            if (SplashKit.KeyTyped(KeyCode.SpaceKey)) Fire();

            // Handle weapon switching
            HandleWeaponSwitch();

            // Update projectiles and other tank logic
            base.Update();
        }
    }
}