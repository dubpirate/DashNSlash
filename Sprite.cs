using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DashNSlash.MacOS
{
    public abstract class Sprite
    {
        protected String name;
		protected Color[] spriteData;
        protected Texture2D sprite;
        protected Rectangle rectangle;
        protected float tileSize;

        protected Sprite(String n, float ts, Vector2 coords)
        {
			// Initialise base sprite details. 
            name = n;
            tileSize = ts;
			rectangle = new Rectangle((int)coords.X, (int)coords.Y, (int)ts, (int)ts);
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(name);
        }

        
		public virtual void Update(GraphicsDeviceManager graphics, GameTime gt, ContentManager content) 
		{
			// pass
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, rectangle, Color.White);
        }

        public String Name { get; protected set; }

        public Rectangle GetRectangle { get; }
    }
}