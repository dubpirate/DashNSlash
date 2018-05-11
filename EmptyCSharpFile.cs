using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace test.MacOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
		readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D man;
        Texture2D enemy;
        float tileSize = 128;
        double spawnrate = 5;
        double spawnnext = 0;
        float startY;
        float targetX, targetY;
        Vector2 scale;
        bool jump;
        bool goingLeft;
        bool goingRight;
        bool enemySpawned;

        Vector2 position;
        Vector2 velocity = new Vector2(0, -700);
        Vector2 accelloration = new Vector2(0, 25);

        Vector2 enemyVelocity = new Vector2(-40, 0);
        Vector2 enemyPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1000,
                PreferredBackBufferHeight = 750
            };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here
            startY = graphics.GraphicsDevice.Viewport.Height - tileSize;
            position = new Vector2(
                (graphics.GraphicsDevice.Viewport.Width / 2) - tileSize / 2,
                startY);

            enemyPosition = new Vector2(
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height / 2);

            targetY = tileSize;
            targetX = tileSize;
            man = this.Content.Load<Texture2D>("Manright");
            enemy = this.Content.Load<Texture2D>("ShadowLeft");
            scale = new Vector2(targetX / (float)man.Width, targetX / (float)man.Width);
        }

        protected void MovementControl(float dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                man = this.Content.Load<Texture2D>("ManHitright");
            }
            else
            {
                man = this.Content.Load<Texture2D>("Manright");
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.A) || (position.Y <= startY && goingLeft)) && position.X > 0)
            {
                position += new Vector2(-10, 0);
                goingLeft = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) || (position.Y <= startY && goingRight) && position.X < graphics.GraphicsDevice.Viewport.Width - tileSize)
            {
                position += new Vector2(10, 0);
                goingRight = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                jump = true;
            }

            if (jump)
            {
                Jump(dt);
            }

            if (position.Y >= startY)
            {
                jump = false;
                goingRight = false;
                goingLeft = false;
                velocity = new Vector2(velocity.X, -700);
            }

            if (position.X > graphics.GraphicsDevice.Viewport.Width - tileSize)
            {
                position.X = graphics.GraphicsDevice.Viewport.Width - tileSize;
            }
        }

        protected void SpawnControl(float dt)
        {
            if (spawnnext > spawnrate && !enemySpawned)
            {
                spawnnext = 0;
                enemySpawned = true;
            }
            else
            {
                spawnnext += dt;
            }
        }

        protected void UpdateEnemy(float dt)
        {
            enemyPosition.X -= 100 * dt;
            if (enemyPosition.X < 0 - tileSize)
            {
                enemySpawned = false;
                enemyPosition = new Vector2(
                    graphics.GraphicsDevice.Viewport.Width,
                    graphics.GraphicsDevice.Viewport.Height / 2);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MovementControl(dt);
            SpawnControl(dt);

            if (enemySpawned)
            {
                UpdateEnemy(dt);
            }

            base.Update(gameTime);
        }

        protected void Jump(float dt)
        {
            position += velocity * dt;
            velocity += accelloration;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            //TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(enemy, position: enemyPosition, scale: scale);
            spriteBatch.Draw(man, position: position, scale: scale);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
