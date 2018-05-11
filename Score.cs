using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DashNSlash.MacOS
{
    public class Score 
    {
        readonly Number numberline;
        readonly Label label;
        public Score(float tileSize)
        {
            numberline = new Number(0, tileSize);
            label = new Label(tileSize);
        }

        public void LoadContent(ContentManager content)
        {
            numberline.LoadContent(content);
            label.LoadContent(content);
        }


        public void UpdateScore(Player player, EnemySpawner enemySpawner, ContentManager content)
        {
			for (var i = 0; i < enemySpawner.CheckAttacks(player); i++)
            {
                numberline.AddOne(content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            numberline.DrawNumberline(spriteBatch);
            label.Draw(spriteBatch);
        }
    }
}
