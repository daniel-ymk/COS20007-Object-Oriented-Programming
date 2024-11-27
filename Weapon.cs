using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CleanHouse
{
    public enum WeaponType
    {
        TANK,       // High-damage, slower rate of fire
        MACHINE_GUN // Lower damage, rapid rate of fire
    }

    public class Weapon : IdentifiableObject
    {
        public WeaponType Type { get; private set; }
        public int Damage { get; private set; }
        public float FireRate { get; private set; } // Time between shots (in seconds)
        public float Range { get; private set; } // Maximum range for projectiles

        private List<Projectile> _projectiles;
        private DateTime _lastFireTime;

        public Weapon(string[] idents, WeaponType type) : base(idents)
        {
            Type = type;
            _projectiles = new List<Projectile>();
            _lastFireTime = DateTime.Now;

            // Set weapon-specific properties
            switch (type)
            {
                case WeaponType.TANK:
                    Damage = 50; // High damage
                    FireRate = 3.0f; // Slower fire rate (1.5 seconds)
                    Range = 800; // Maximum range for tank weapon
                    break;

                case WeaponType.MACHINE_GUN:
                    Damage = 10; // Lower damage
                    FireRate = 0.2f; // Rapid fire rate (0.2 seconds)
                    Range = 300; // Maximum range for machine gun
                    break;
            }
        }

        public void Fire(Direction direction, float x, float y,Tank FiredBy)
        {
            DateTime currentTime = DateTime.Now;

            // Check if the weapon is reloaded (enough time has passed since the last shot)
            if ((currentTime - _lastFireTime).TotalSeconds >= FireRate)
            {
                // Create a projectile with the weapon's range
                Projectile newProjectile = new Projectile(
                    new string[] { "projectile" },
                    x, y,
                    direction,
                    Damage, // Damage
                    Type == WeaponType.TANK ? Color.Blue : Color.Green, // Color
                    Type, // Weapon type
                    Range, // Weapon's range
                    FiredBy
                );

                _projectiles.Add(newProjectile);
                _lastFireTime = currentTime; // Update the last fire time
                Console.WriteLine($"Firing projectile from position ({x}, {y}) with damage {Damage}.");
            }
            else
            {
                // Weapon is still reloading
                Console.WriteLine("Reloading! Cannot fire yet.");
            }
        }
        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
        }

        //for the bots to fire only when reloaded
        public bool IsReloaded()
        {
            // Check if enough time has passed since the last fire
            return (DateTime.Now - _lastFireTime).TotalSeconds >= FireRate;
        }
    }
}
