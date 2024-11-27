using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CleanHouse
{

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public abstract class Tank : GameObject
    {
        private int _health;
        private float _speed;
        private int _armor;
        private Weapon _weapon;
        private Direction _cur_direction;
        private List<Tank> _allTanks; // Reference to all tanks for collision handling
        private List<Wall> _allWalls;


        public Tank(string[] identifiers, float x, float y, Weapon weapon,  List<Tank> allTanks, List<Wall> allWalls)
            : base(identifiers, x, y)
        {
            _health = 100;
            _speed = 0.05f;
            _armor = 100;
            _weapon = weapon;
            _cur_direction = Direction.UP;
            _allTanks = allTanks;
            _allWalls = allWalls;
        }

        

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        public Direction CurrentDirection
        {
            get { return _cur_direction; }
            set { _cur_direction = value; }
        }




        ///// TANK 
        ///////////////////////////////////////
        ///
        public bool CanMove(Direction direction, List<Tank> allTanks, List<Wall> allWalls)
        {
            float newX = X, newY = Y;
            switch (direction)
            {
                case Direction.UP: newY -= _speed; break;
                case Direction.DOWN: newY += _speed; break;
                case Direction.LEFT: newX -= _speed; break;
                case Direction.RIGHT: newX += _speed; break;
            }

            // Check boundaries
            if (newX <= 0 || newX >= 800 - 30 || newY <= 0 || newY >= 600 - 30)
                return false;

            // Temporarily update position for collision checks
            float originalX = X, originalY = Y;
            X = newX; Y = newY;
            bool collidesWithTank = allTanks.Any(t => t != this && CollidesWith(t));
            bool collidesWithWall = allWalls.Any(wall => wall.CollidesWithTank(this));

            X = originalX; Y = originalY; // Revert position

            return !(collidesWithTank || collidesWithWall);
        }

        public void Move(Direction direction, List<Tank> allTanks)
        {
            // Store current position
            float newX = X;
            float newY = Y;

            // Calculate the new position
            switch (direction)
            {
                case Direction.UP:
                    newY -= _speed;
                    break;
                case Direction.DOWN:
                    newY += _speed;
                    break;
                case Direction.LEFT:
                    newX -= _speed;
                    break;
                case Direction.RIGHT:
                    newX += _speed;
                    break;
            }

            // Temporarily move to new position for collision checking
            float originalX = X;
            float originalY = Y;
            X = newX;
            Y = newY;

            // Check if the tank collides with any other tanks
            bool collision = allTanks.Any(t => t != this && CollidesWith(t));

            // Revert the position if a collision occurs
            if (collision)
            {
                X = originalX;
                Y = originalY;
            }
            else
            {
                _cur_direction = direction; // Update direction if movement succeeds
            }
        }
        public override void Draw()
        {
            // Define the size of each square in the 3x3 grid (e.g., 20x20 pixels)
            int squareSize = 10;

            // Define the starting position (top-left corner) of the tank
            float startX = X;
            float startY = Y;


            switch (_cur_direction)
            {
                case Direction.UP:
                    // Draw the first row (white, black, white)
                    SplashKit.FillRectangle(Color.White, startX, startY, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + 2 * squareSize, startY, squareSize, squareSize); 

                    // Draw the second row (black, black, black)
                    SplashKit.FillRectangle(Color.Black, startX, startY + squareSize, squareSize, squareSize);
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY + squareSize, squareSize, squareSize); 

                    // Draw the third row (black, white, black)
                    SplashKit.FillRectangle(Color.Black, startX, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    break;

                case Direction.DOWN:
                    SplashKit.FillRectangle(Color.Black, startX, startY, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + squareSize, startY, squareSize, squareSize);
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.Black, startX, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY + squareSize, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.White, startX, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + 2 * squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    break;

                case Direction.LEFT:
                    SplashKit.FillRectangle(Color.White, startX, startY, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY, squareSize, squareSize);
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.Black, startX, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + 2 * squareSize, startY + squareSize, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.White, startX, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY + 2 * squareSize, squareSize, squareSize);
                    break;

                case Direction.RIGHT:
                    SplashKit.FillRectangle(Color.Black, startX, startY, squareSize, squareSize);
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + 2 * squareSize, startY, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.White, startX, startY + squareSize, squareSize, squareSize);
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + 2 * squareSize, startY + squareSize, squareSize, squareSize); 

                    SplashKit.FillRectangle(Color.Black, startX, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.Black, startX + squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    SplashKit.FillRectangle(Color.White, startX + 2 * squareSize, startY + 2 * squareSize, squareSize, squareSize); 
                    break;

            }



            // Choose the size of the circle based on the weapon type
            float weaponRadius = _weapon.Type == WeaponType.TANK ? 4 : 1;  // Example sizes for the two weapon types            // Draw the red circle on top of the tank
            SplashKit.FillCircle(Color.Red, X + 15, Y + 15, weaponRadius); // Adjust Y to place it above the tank
            foreach (var projectile in _weapon.Projectiles) // Access the list of projectiles
            {
                projectile.Draw(); // Draw each active projectile
            }

        }








        ///////////////////////////////
        // PROJECTILES
        public void Fire()
        {
            _weapon.Fire(_cur_direction, X, Y, this);
        }

        public void UpdateProjectiles()
        {
            foreach (var projectile in _weapon.Projectiles) // Access the list of projectiles
            {
                // Move each projectile
                projectile.Move(_allWalls);
                projectile.HandleCollision(_allTanks);

            }
            _weapon.Projectiles.RemoveAll(p => !p.IsActive); // Remove inactive projectiles

        }


        public bool CollidesWith(Tank otherTank)
        {
            return SplashKit.RectanglesIntersect(
                SplashKit.RectangleFrom(X, Y, 30, 30), // Current tank's bounding box (adjust size as needed)
                SplashKit.RectangleFrom(otherTank.X, otherTank.Y, 30, 30) // Other tank's bounding box
            );
        }

        public void DestroyTank()
        {
            // Set health to 0 to indicate the tank is destroyed
            _health = 0;
            // Remove the tank from the allTanks list
            _allTanks.Remove(this); // 'this' refers to the current instance of the tank
        }

        ///UPDATE////////////////////
        public virtual void Update()
        {
            UpdateProjectiles();
        }
    }
}

