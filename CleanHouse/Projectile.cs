using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System;

namespace CleanHouse
{
    public class Projectile : GameObject
    {
        public Tank FiredBy { get; set; }


        private float _speed;
        private Direction _direction;
        private int _damage;
        private Color _color;
        private bool _isActive; // Flag to track if the projectile is active
        private WeaponType _weaponType;


        private float _distanceTraveled;
        private float _maxRange; // Maximum range of the projectile

        // Constructor to initialize the projectile with position, direction, speed, and damage
        public Projectile(string[] identifiers, float x, float y, Direction direction, int damage, Color color, WeaponType weaponType, float maxRange, Tank firedBy)
    : base(identifiers, x, y)
        {
            _direction = direction;
            _speed = 0.3f;
            _damage = damage;
            _color = color;
            _isActive = true;
            _weaponType = weaponType;
            _maxRange = maxRange;
            _distanceTraveled = 0;
            FiredBy = firedBy;

        }



        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public Direction MoveDirection
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
        }

        public WeaponType WeaponType
        {
            get { return _weaponType; }
            set { _weaponType = value; }
        }
        // Method to move the projectile
        public void Move(List<Wall> walls)
        {
            if (!_isActive) return; // If the projectile is inactive, don't move it

            // Store the old position to calculate distance traveled
            float oldX = X;
            float oldY = Y;

            // Update position based on direction
            switch (_direction)
            {
                case Direction.UP:
                    Y -= _speed;
                    break;
                case Direction.DOWN:
                    Y += _speed;
                    break;
                case Direction.LEFT:
                    X -= _speed;
                    break;
                case Direction.RIGHT:
                    X += _speed;
                    break;
            }

            // Calculate the distance traveled in this frame
            _distanceTraveled += (float)Math.Sqrt((X - oldX) * (X - oldX) + (Y - oldY) * (Y - oldY));

            // Deactivate the projectile if it exceeds the range
            if (_distanceTraveled >= _maxRange)
            {
                _isActive = false;
                return;
            }

            // Check if the projectile is off the screen and deactivate it
            if (X < 0 || X > 800 || Y < 0 || Y > 600)
            {
                _isActive = false; // Deactivate the projectile when it goes off-screen
            }


            // Check collision with walls
            foreach (Wall wall in walls)
            {
                if (wall.CollidesWithProjectile(this))
                {
                    _isActive = false; // Deactivate the projectile upon hitting a wall
                    return;
                }
            }


        }


        public bool CheckCollision(GameObject other)
        {
            if (!_isActive) return false; // Don't check collisions if the projectile is inactive

            // Assuming 'other' is a Tank, check if the projectile's rectangle collides with the tank's rectangle
            if (other is Tank tank)
            {
                if (tank == FiredBy) return false;

                // Define the size of the projectile and the tank
                Rectangle projectileRect = SplashKit.RectangleFrom(X, Y, 10, 10); // Example dimensions for the projectile
                Rectangle tankRect = SplashKit.RectangleFrom(tank.X, tank.Y, 30, 30); // Assuming tank's dimensions are 50x50

                // Manually check if the rectangles intersect
                bool collisionX = projectileRect.X + projectileRect.Width > tankRect.X &&
                                  projectileRect.X < tankRect.X + tankRect.Width;

                bool collisionY = projectileRect.Y + projectileRect.Height > tankRect.Y &&
                                  projectileRect.Y < tankRect.Y + tankRect.Height;

                return collisionX && collisionY; // Return true if both X and Y conditions are true (rectangles intersect)
            }

            return false;
        }


        // Method to handle collision with another object (e.g., deal damage or destroy projectile)
        public void HandleCollision(List<Tank> allTanks)
        {
            // Loop through each tank in the allTanks list
            foreach (Tank tank in allTanks)
            {
                // Check if the projectile collides with the tank
                if (CheckCollision(tank))
                {
                    // Logic for handling collision (e.g., damage the tank)
                    if (tank.Armor > 0)
                    {
                        tank.Armor -= _damage;


                         if (tank.Armor < 0)
                        {
                            tank.Health = tank.Health + tank.Armor;  // if the armor is broken then further deduct from Health  
                            tank.Armor = 0;
                        }
                        Console.WriteLine($"Tank {tank} armor reduced to {tank.Armor}");
                    }

                    else
                    {
                        tank.Health -= _damage; // Deal damage to the tank
                        Console.WriteLine($"Tank {tank} health reduced to {tank.Health}");
                    }
                    

                    // If the tank's health reaches zero, destroy it
                    if (tank.Health <= 0)
                    {
                        Console.WriteLine($"Tank {tank} destroyed!");
                        tank.DestroyTank(); // Destroy the tank
                    }

                    // Deactivate the projectile after collision
                    _isActive = false;
                    break; // Stop checking after the first collision
                }
            }
        }


        // Draw the projectile
        public override void Draw()
        {
            if (!_isActive) return;

            // Differentiate bullets visually based on weapon type
            switch (_weaponType)
            {
                case WeaponType.TANK:
                    SplashKit.FillCircle(Color.Red, X+10, Y+10, 10);
                    //SplashKit.FillRectangle(_color, X, Y, 15, 15); // Larger bullet for tank
                    break;
                case WeaponType.MACHINE_GUN:
                    SplashKit.FillCircle(Color.Black, X +13, Y +  13, 4 );
                    //SplashKit.FillRectangle(_color, X, Y, 8, 8); // Smaller bullet for machine gun
                    break;
            }
        }


    }
}