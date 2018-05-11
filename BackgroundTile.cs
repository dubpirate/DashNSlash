using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DashNSlash.MacOS
{
    public class BackgroundTile : Sprite
    {
        public BackgroundTile(String name, float ts, float x, float y)
            : base(name, ts, new Vector2 (x, y))
        {
            
        }

        public void Update()
        {
            rectangle.X -= 5;
        }

        public int GetX() { return rectangle.X; }
    }
}