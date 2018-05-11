using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DashNSlash.MacOS
{
    /// <summary>
    /// This is the main type.
    /// 
    /// All art by bradley peary
    /// All code (not including default monogame stuff) by w torkington.
    /// </summary>
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        EnemySpawner enemySpawner;
        BackgroundSpawner backgroundSpawner;
        DetailSpawner detailSpawner;
        Score score;

        float tileSize;

        Double screenWidth = 1080;
        Double screenHeight = 810;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = (int) screenWidth,
                PreferredBackBufferHeight = (int) screenHeight
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            tileSize = (float) screenHeight / 6;

            backgroundSpawner = new BackgroundSpawner(tileSize);
            backgroundSpawner.InitialiseBackground();

            detailSpawner = new DetailSpawner(tileSize);
            detailSpawner.InitialiseBackground(graphics);

            enemySpawner = new EnemySpawner(tileSize);
            enemySpawner.InitialiseEnemies(graphics);

            score = new Score(tileSize);

            player = new Player(tileSize, graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundSpawner.LoadContent(Content);

            score.LoadContent(Content);

            player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

            backgroundSpawner.UpdateSpawnRate(Content);

            detailSpawner.UpdateSpawnRate(graphics, gameTime);

            enemySpawner.UpdateSpawnRate(graphics, gameTime);

            player.Update(graphics, gameTime, Content);

            score.UpdateScore(player, enemySpawner, Content);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

                backgroundSpawner.DrawTiles(spriteBatch);
                detailSpawner.DrawTiles(spriteBatch, Content);
                enemySpawner.DrawEnemies(spriteBatch, Content);
			    player.Draw(spriteBatch);
                score.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
