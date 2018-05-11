using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DashNSlash.MacOS
{
    public class Label : Sprite
    {
        public Label(float tileSize) : base("score", tileSize, new Vector2(0, 0))
        {
        }
    }
}
