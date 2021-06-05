using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Flappy1;
        Vector2 FlappyPos = new Vector2(960, 540);

        Texture2D pixel; 
        Rectangle PipeUp = new Rectangle(1820, -440, 100, 800);
        Rectangle PipeDown = new Rectangle(1820, 700, 100, 800);

        Vector2 snabbhet;
        int score = 0;
        Random r = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Flappy1 = Content.Load<Texture2D>("Flappy1");
            pixel = Content.Load<Texture2D>("pixel");
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            //Hur rören rör sig, Rör sig snabbare för varje rör som man klarar av
            PipeDown.X -= 4 + score;
            PipeUp.X -= 4 + score;

            //Detta gör så att ju längre man flyger nedåt desto snabbare går det
            FlappyPos += snabbhet;
            snabbhet.Y += 0.25f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //När man trycker på mellanslag så hoppar man upp 3f och sen så flyger man uppåt med 3f
                FlappyPos.Y -= 3;
                snabbhet.Y = -3f;
            }

            Rectangle playerRect = new Rectangle((int)FlappyPos.X, (int)FlappyPos.Y, Flappy1.Width, Flappy1.Height);

            //Om flyger för långt upp eller ner så stängs spelet av. Om man träffar något av rören så stängs spelet av.
            if (playerRect.Intersects(PipeDown))
                Exit();

            if (playerRect.Intersects(PipeUp))
                Exit();

            if (playerRect.Y >= Window.ClientBounds.Height - playerRect.Height)
                Exit();

            if (playerRect.Y <= 0)
                Exit();
            //När rören har gått hela vägen till andra sidan så börjar de om med en ny position baserat på det slumpade talet och så går de snabbare
            if (PipeDown.X <= 0)
            {
                int number = r.Next(-800, 0);
                PipeUp.Y = number;
                PipeDown.Y = number + 1080;
                PipeUp.X = 1820;
                PipeDown.X = 1820;
                score++;
            }
            //trycker man på escape så stängs spelet av
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Flappy1, FlappyPos, Color.White);
            spriteBatch.Draw(pixel, PipeDown, Color.White);
            spriteBatch.Draw(pixel, PipeUp, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
