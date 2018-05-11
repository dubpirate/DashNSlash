using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DashNSlash.MacOS
{
    public class BackgroundSpawner
    {
		const string wall = "Wall";
		const string floor = "Floor";
		float tileSize;
        List<List<BackgroundTile>> layers;

        public BackgroundSpawner(float tileSize)
        {
			this.tileSize = tileSize;
        }

        public void InitialiseBackground()
        {
			layers = new List<List<BackgroundTile>>();
			List<BackgroundTile> nextLayer = new List<BackgroundTile>();
            float layerXPosition;
            float layerYPosition = 0;
            
            // Drawing the wall tiles.
            for (var layer = 0; layer < 5; layer++)
            {
                for (var tile = 0; tile < 10; tile++)
                {
					layerXPosition = tileSize * tile;
					nextLayer.Add(new BackgroundTile(wall, tileSize, layerXPosition, layerYPosition));
                }

				layerYPosition += tileSize;
				layerXPosition = 0;
                layers.Add(nextLayer);
				nextLayer = new List<BackgroundTile>();
            }

            // Drawing the floor tiles.
            for (var tile = 0; tile < 10; tile++)
            {
				layerXPosition = tileSize * tile;
				nextLayer.Add(new BackgroundTile(floor, tileSize, layerXPosition, tileSize * 5));
            }
            layers.Add(nextLayer);
        }
        
        public void LoadContent(ContentManager content)
        {
			foreach (var layer in layers)
            {
                foreach (var tile in layer)
                {
                    tile.LoadContent(content);
                }
            }
        }

		float NextTileX()
		{
			List<BackgroundTile> layer = layers[0];
			BackgroundTile endTile = layer[layer.Count - 1];
			float nextX = endTile.GetX() + tileSize;
			return nextX;
		}

        public void UpdateSpawnRate(ContentManager content)
        {
			// Check if the tiles at the end needs to be removed
			if (layers[0][0].GetX() < -tileSize * 2)
            {
				List<BackgroundTile> currentLayer;
                BackgroundTile newTile;
				float nextX = NextTileX();

				for (var layer = 0; layer < layers.Count - 1; layer++)
                {
					newTile = new BackgroundTile(wall, tileSize, nextX, tileSize * layer);               
					newTile.LoadContent(content);
					currentLayer = layers[layer];
					currentLayer.Add(newTile);
					currentLayer.RemoveAt(0);
				}

				newTile = new BackgroundTile(floor, tileSize, nextX, tileSize * (layers.Count - 1));
				newTile.LoadContent(content);
				currentLayer = layers[layers.Count - 1];            
                currentLayer.Add(newTile);
				currentLayer.RemoveAt(0);
            }
                
            UpdateTiles();
        }

        public void UpdateTiles()
        {
			foreach (var layer in layers)
            {
                foreach (var tile in layer)
                {
                    tile.Update();
                }
            }
        }
        
        public void DrawTiles(SpriteBatch spriteBatch)
        {
			foreach (var layer in layers)
            {
                foreach (var tile in layer)
                {
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}