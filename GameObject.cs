using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHouse
{
    public abstract class GameObject : IdentifiableObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        protected GameObject(string[] identifiers, float x, float y) : base(identifiers)
        {
            
            X = x;
            Y = y;
        }

        public abstract void Draw();
    }
}
