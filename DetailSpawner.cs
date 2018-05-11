using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace DashNSlash.MacOS
{
    public class DetailSpawner
    {
        double spawnNext;
        float tileSize;
		List<DetailTile> detailTiles;
		List<String> tileTextures;

        public DetailSpawner(float ts)
        {
            tileSize = ts;
            spawnNext = 0;
			detailTiles = new List<DetailTile>();
			tileTextures = new List<string>
            {
                "Rock",
                "Rubble1",
                "Rubble2",
                "Torch",
                "Moss",
                "Crack"
            };
        }

        public void InitialiseBackground(GraphicsDeviceManager graphics)
        {
            NewTile(graphics);
        }

		public void UpdateSpawnRate(GraphicsDeviceManager graphics, GameTime gameTime)
        {
			UpdateTiles();

			spawnNext -= gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnNext <= 0)
            {
                NextSpawn();
                NewTile(graphics);
            }
        }

		void UpdateTiles()
        {
            DetailTile toRemove = null;
            foreach (DetailTile tile in detailTiles)
            {
				tile.Update();
                if (tile.GetX() < 0 - tileSize)
                {
                    toRemove = tile;
                }
            }
            if (toRemove != null) detailTiles.Remove(toRemove);
        }

        void NextSpawn()
        {
            Random rnd = new Random();
            spawnNext = 0.5 * rnd.Next(1, 2);
        }

        public void NewTile(GraphicsDeviceManager graphics)
        {
            String nextTile = NextTile();
            DetailTile newTile;

            if (nextTile.Equals("Crack") || nextTile.Equals("Moss") || nextTile.Equals("Torch"))
			{
				newTile = new DetailTile(nextTile, tileSize, graphics, true);
			}
            else
			{
				newTile = new DetailTile(nextTile, tileSize, graphics);
			}

            detailTiles.Add(newTile);
        }

        String NextTile()
        {
            Random rnd = new Random();
            int texture = rnd.Next(0, tileTextures.Count - 1);
            return tileTextures[texture];
        }

		public void DrawTiles(SpriteBatch spriteBatch, ContentManager content)
        {
            foreach (DetailTile tile in detailTiles)
            {
				tile.LoadContent(content);
                tile.Draw(spriteBatch);
            }
        }
    }
}