using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Flappy1;
        Texture2D pipe;
        Vector2 pipePos = new Vector2(1900, 600);
        Vector2 Flappy1Pos = new Vector2(960, 540);
        Vector2 snabbhet;
        float hopp = -1;
        bool play = true;
        //KOmentar
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            Flappy1 = Content.Load<Texture2D>("Flappy1");
            pipe = Content.Load<Texture2D>("pipe");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            while (play == true)
            {
                Flappy1Pos += snabbhet;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Flappy1Pos.Y -= 4f;
                    snabbhet.Y = -4f;
                    hopp -= 0.02f;
                }
                int i = 1;
                snabbhet.Y += 0.25f * i;

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }
                Rectangle playerRect = new Rectangle((int)Flappy1Pos.X, (int)Flappy1Pos.Y, Flappy1.Width, Flappy1.Height);
                Rectangle pipeRect = new Rectangle((int)pipePos.X, (int)pipePos.Y, pipe.Width, pipe.Height);
          
            if(playerRect.Intersects(pipeRect))
            {
                Flappy1Pos.X = 500;
                Flappy1Pos.Y = 500;
                play = false;
            }
            // TODO: Add your update logic here
            pipePos.X += hopp;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here.
                spriteBatch.Begin();
                spriteBatch.Draw(Flappy1, Flappy1Pos, Color.White);
                spriteBatch.Draw(pipe, pipePos, Color.White);
                spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
