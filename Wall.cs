using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHouse
{
    public class Wall : GameObject
    {
        private int _width;
        private int _height;

        public Wall(string[] identifiers, float x, float y, int width, int height)
            : base(identifiers, x, y)
        {
            _width = width;
            _height = height;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public override void Draw()
        {
            // Draw the wall as a rectangle (adjust color and style as needed)
            SplashKit.FillRectangle(Color.Gray, X, Y, _width, _height);
        }

        public bool CollidesWithTank(Tank tank)
        {
            // Check if the tank collides with this wall
            return SplashKit.RectanglesIntersect(
                SplashKit.RectangleFrom(X, Y, _width, _height),
                SplashKit.RectangleFrom(tank.X, tank.Y, 30, 30) // Tank size
            );
        }

        public bool CollidesWithProjectile(Projectile projectile)
        {
            // Check if the projectile collides with this wall
            return SplashKit.RectanglesIntersect(
                SplashKit.RectangleFrom(X, Y, _width, _height),
                SplashKit.RectangleFrom(projectile.X, projectile.Y, 10, 10)
            );
        }
    }
}
