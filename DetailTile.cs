
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DashNSlash.MacOS
{
    public class DetailTile : Sprite
    {
        public DetailTile(String name, float tileSize, GraphicsDeviceManager graphics)
			: this(name, tileSize, graphics, false)
        {

        }

        public DetailTile(String name, float tileSize, GraphicsDeviceManager graphics, bool floating)
			: base(name, tileSize, new Vector2(graphics.GraphicsDevice.Viewport.Width, 0))
        {
            if (floating)
			{
				rectangle.Y = (int)(graphics.GraphicsDevice.Viewport.Height - RandomHeight());
			}
            else
			{
				rectangle.Y = (int)(graphics.GraphicsDevice.Viewport.Height - tileSize);
			}
        }

        int RandomHeight()
        {
            Random rnd = new Random();
			return (int)tileSize * rnd.Next(1, 6);
        }
        
		public void Update()
        {
            rectangle.X -= 5;
        }

        public int GetX() { return rectangle.X; }

        public int GetY() { return rectangle.Y; }
    }
}
