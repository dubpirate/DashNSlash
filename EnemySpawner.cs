using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace DashNSlash.MacOS
{
    public class EnemySpawner
    {
        double spawnNext;
        float tileSize;
		readonly List<Enemy> enemies = new List<Enemy>();

        public EnemySpawner(float tileSize)
        {
			this.tileSize = tileSize;
        }

        public void InitialiseEnemies(GraphicsDeviceManager graphics)
        {
            NewEnemy(graphics);
        }
        
        public void UpdateSpawnRate(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            UpdateEnemies();

			spawnNext -= gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnNext <= 0)
            {
                NextSpawn();
                NewEnemy(graphics);
            }
        }

        void UpdateEnemies()
        {
			List<Enemy> toRemove = new List<Enemy>();

            foreach (Enemy enemy in enemies)
            {
				if (enemy.GetRectangle.Right < 0)
				{
					toRemove.Add(enemy);
				}
				else
				{
					enemy.Update();
				}
            }

			enemies.RemoveAll(e => toRemove.Contains(e));
        }
        
        public int CheckAttacks(Player player)
        {
			List<Enemy> kills = new List<Enemy>();

			foreach (var enemy in enemies)
			{
				if (enemy.GetX() > 1.1 * tileSize)
				{
					break;
				}
				
				if (enemy.GetX() > 0.8 * tileSize && player.IsAttacking(enemy))
				{
					kills.Add(enemy);
                }
			}

			if (kills.Count > 0)
			{
				enemies.RemoveAll(e => kills.Contains(e));
                return kills.Count;
			}

			return 0;
		}

        void NextSpawn()
        {
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            spawnNext = rnd1.Next(0, 3) * rnd2.Next(0, 3);
        }
        
        public void NewEnemy(GraphicsDeviceManager graphics) 
        {
            int nextHeight = NextHeight((int) (graphics.GraphicsDevice.Viewport.Height - tileSize));
            enemies.Add(new Enemy(tileSize, graphics, nextHeight));
        }

        int NextHeight(int max)
        {
            Random rnd = new Random();
            return rnd.Next(1, max);
        }
        
        public void DrawEnemies(SpriteBatch spriteBatch, ContentManager content)
        {
            foreach (var enemy in enemies) 
            {
                enemy.LoadContent(content);
                enemy.Draw(spriteBatch);
            }
        }
    }
}