using Microsoft.Xna.Framework;

namespace DashNSlash.MacOS
{
    public class Enemy : Sprite
    {
        public Enemy(float tileSize, GraphicsDeviceManager graphics, float yPos)
			: base("ShadowLeft", tileSize, new Vector2(graphics.GraphicsDevice.Viewport.Width, yPos))
        {
            
        }

        public void Update()
        {
            rectangle.X -= 5;
        }

        public int GetX()
        {
            return rectangle.X;
        }

        public double GetTop()
        {
            return rectangle.Top;
        }

        public double GetBottom()
        {
            return rectangle.Bottom;
        }
    }
}
