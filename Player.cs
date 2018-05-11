using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DashNSlash.MacOS
{
    public class Player : Sprite
    {
        Boolean jump;
        Boolean attackReset;
        double attacking;
        int velocity;

        public Player(float ts, GraphicsDeviceManager graphics) 
            : base("Manright", ts, new Vector2(ts / 2, graphics.GraphicsDevice.Viewport.Height-ts))
        {
            jump = false;
            attacking = 0;
            velocity = VelocityReset();
        }

        public override void Update(GraphicsDeviceManager graphics, GameTime gt, ContentManager content)
        {
            if (attackReset && Keyboard.GetState().IsKeyDown(Keys.D))
                attacking = 0.25;
            else
                attacking -= gt.ElapsedGameTime.TotalSeconds;
            
            SetState(content);

            attackReset = Keyboard.GetState().IsKeyUp(Keys.D) && attacking <= 0;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                rectangle.Y += 5;

            jump |= Keyboard.GetState().IsKeyDown(Keys.W);

            if (jump) 
                Jump(gt);
            
            CheckEdgeCases(graphics);
        }

        void SetState(ContentManager content)
        {
            if (attacking > 0)
                Attack(content);
            else
                Normal(content);
        }

        public int VelocityReset()
        {
            return -2200;
        }

        void Attack(ContentManager content)
        {
            name = "ManHitright";
            attackReset = false;
            LoadContent(content);
        }

        void Normal(ContentManager content) 
        {
            attacking = 0;
            name = "Manright";
            LoadContent(content);
        }

        void CheckEdgeCases(GraphicsDeviceManager graphics)
        {
			if (rectangle.X < 0) 
				rectangle.X = 0; 

            if (rectangle.Y < 0) 
				rectangle.Y = 0;

			if (rectangle.X > graphics.GraphicsDevice.Viewport.Width)
                rectangle.X = graphics.GraphicsDevice.Viewport.Width;

            if (rectangle.Y > graphics.GraphicsDevice.Viewport.Height - tileSize + 1)
            {
                rectangle.Y = (int)(graphics.GraphicsDevice.Viewport.Height - tileSize);
                jump = false; 
                velocity = VelocityReset(); 
            }
                
        }
        public bool IsAttacking(Enemy enemy)
        {
            double enemyTop = enemy.GetTop();
            double enemyBot = enemy.GetBottom();
            bool inRange = rectangle.Top < enemyBot - (tileSize/8) && rectangle.Bottom > enemyTop - (tileSize / 8);
            return (attacking > 0) && inRange;
        }
        void Jump(GameTime gt)
        {
            rectangle.Y += (int) (velocity * gt.ElapsedGameTime.TotalSeconds);
            velocity += 75;
        }

		public int Attacking { get; }
    }
}
