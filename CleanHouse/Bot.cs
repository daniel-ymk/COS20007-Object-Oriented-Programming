using System;

namespace CleanHouse
{
    public class Bot : Tank
    {
        private Random _random; // For random movement
        private float _range;   // Detection range for engaging the player
        private Player _player; // Reference to the player
        private List<Tank> _allTanks; // Reference to all tanks for collision handling
        private List<Wall> _allWalls;


        public Bot(string[] identifiers, float x, float y, Weapon weapon, Player player, float range, List<Tank> allTanks, List<Wall> allWalls)
            : base(identifiers, x, y, weapon, allTanks, allWalls)
        {
            Speed = 0.04f;
            Health = 50;
            Armor = 50;
            _random = new Random();
            _player = player;
            _range = range;
            CurrentDirection = (Direction)_random.Next(0, 4); // Random initial direction
            _allTanks = allTanks; // Initialize the reference to all tanks
            _allWalls = allWalls;
        }


        // Method to patrol by moving in one direction until blocked, then changing direction
        public void Patrol()
        {
            if (CanMove(CurrentDirection,_allTanks, _allWalls))
            {
                Move(CurrentDirection, _allTanks); // Continue moving in the current direction
            }
            else
            {
                ChangeDirection(); // Change direction if movement is blocked
            }
        }

        // Method to change to a random new direction
        private void ChangeDirection()
        {
            Direction newDirection;
            do
            {
                newDirection = (Direction)_random.Next(0, 4); // Pick a random direction
            } while (newDirection == CurrentDirection); // Ensure it’s different from the current direction

            CurrentDirection = newDirection;
        }

        // Check if the player is within the detection range
        private bool PlayerInRange()
        {
            float dx = X - _player.X;
            float dy = Y - _player.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance <= _range;
        }

        // Aim at the player and fire
        public void EngagePlayer()
        {
            if (PlayerInRange() && Weapon.IsReloaded())
            {
                // Calculate the difference in X and Y between the bot and player
                float xDifference = _player.X - X;
                float yDifference = _player.Y - Y;

                // Move on the X-axis first if the player is farther along the X axis
                if (Math.Abs(xDifference) > Math.Abs(yDifference))
                {
                    // Player is to the left or right, move horizontally
                    if (xDifference > 0 && CanMove(Direction.RIGHT, _allTanks, _allWalls))
                    {
                        Move(Direction.RIGHT, _allTanks);  // Move right if the player is to the right
                        CurrentDirection = Direction.RIGHT; // Face right
                    }
                    else if (xDifference < 0 && CanMove(Direction.LEFT, _allTanks, _allWalls))
                    {
                        Move(Direction.LEFT, _allTanks);  // Move left if the player is to the left
                        CurrentDirection = Direction.LEFT; // Face left
                    }

                    // Fire only when the X positions are aligned
                }
                // Move on the Y-axis if the player is closer vertically
                else
                {
                    // Player is above or below, move vertically
                    if (yDifference > 0 && CanMove(Direction.DOWN, _allTanks, _allWalls))
                    {
                        Move(Direction.DOWN, _allTanks);  // Move down if the player is below
                        CurrentDirection = Direction.DOWN; // Face down
                    }
                    else if (yDifference < 0 && CanMove(Direction.UP, _allTanks, _allWalls))
                    {
                        Move(Direction.UP, _allTanks);  // Move up if the player is above
                        CurrentDirection = Direction.UP; // Face up
                    }

                    
            }

                // Fire only when the Y positions are aligned
                if (Math.Abs(yDifference) < 20 || Math.Abs(xDifference) < 20) // Close enough on the Y axis
                {
                    Fire();
                }
            }
        }

        // Update the bot's behavior
        public override void Update()
        {
            if (PlayerInRange())
            {
                EngagePlayer();
            }
            else
            {
                Patrol();
            }

            base.Update(); // Reuse Tank's projectile updates
        }

        public override void Draw()
        {
            base.Draw(); // Draw the bot
        }
    }
}
