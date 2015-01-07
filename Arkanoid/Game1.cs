#region Using Statements
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

#endregion

namespace Arkanoid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D ballTexture2D;
        private Texture2D stickTexture2D;
        private Vector2 stick;
        private Vector2 ball;

        private bool space_pressed;

        enum Directions {Left,Right,Up,Down,LeftDown,LeftUp,RightDown,RightUp,Null }
        
        Directions direction;
        readonly private int ball_velocity=5;
        readonly private int stick_velocity = 3;

        public Game1()

        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1200;
            Content.RootDirectory = "Content";
            ball = new Vector2(50,50);
            stick = new Vector2(500,850);
            direction = Directions.RightDown;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture2D = Content.Load<Texture2D>(@"ball");
            stickTexture2D = Content.Load<Texture2D>(@"stick");


        }

      
        protected override void UnloadContent()
        {
        }

     
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && stick.X+stickTexture2D.Width<= Window.ClientBounds.Width)
                stick.X += stick_velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && stick.X >= 0)
                stick.X -= stick_velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                space_pressed = true;
         //7   stick.X = Mouse.GetState().X;
            switch (direction)
            {
                //case Directions.Left:
                //    ball.X -= velocity;
                //    break;
                //case Directions.Right:
                //    ball.X += velocity;
                //    break;
                //case Directions.Down:
                //    ball.Y += velocity;
                //    break;
                //case Directions.Up:
                //    ball.Y -= velocity;
                //    break;
                case Directions.LeftDown:
                    ball.X -= ball_velocity;
                    ball.Y += ball_velocity;
                    break;
                case Directions.LeftUp:
                    ball.X -= ball_velocity;
                    ball.Y -= ball_velocity;
                    break;
                case Directions.RightDown:
                    ball.X += ball_velocity;
                    ball.Y += ball_velocity;
                    break;
                case Directions.RightUp:
                    ball.X += ball_velocity;
                    ball.Y -= ball_velocity;
                    break;
                case Directions.Null:
               //     ballTexture2D.Dispose();

                    break;


            }

            
            
            if (ball.X == Window.ClientBounds.Width - ballTexture2D.Width  && direction == Directions.RightDown )
                    direction = Directions.LeftDown;
            if (ball.X == Window.ClientBounds.Width - ballTexture2D.Width && direction == Directions.RightUp)
                direction = Directions.LeftUp;
            if (ball.Y == Window.ClientBounds.Height - ballTexture2D.Height && direction == Directions.RightDown)
                //direction = Directions.RightUp;
                direction = Directions.Null;
            if (ball.Y == Window.ClientBounds.Height - ballTexture2D.Height && direction == Directions.LeftDown)
                //direction = Directions.LeftUp;
                direction = Directions.Null;
            if (ball.X == 0 && direction == Directions.LeftDown)
                direction = Directions.RightDown;
            if (ball.X == 0 && direction == Directions.LeftUp)
                direction = Directions.RightUp;
            if (ball.Y == 0 && direction == Directions.LeftUp)
                direction = Directions.LeftDown;
            if (ball.Y == 0 && direction == Directions.RightUp)
                direction = Directions.RightDown;

            
            if (direction == Directions.RightDown && ball.X >= stick.X && ball.X <= stick.X + stickTexture2D.Width &&
                ball.Y +ballTexture2D.Height-5 >= stick.Y)
                direction = Directions.RightUp;

            if (direction == Directions.LeftDown && ball.X >= stick.X && ball.X <= stick.X + stickTexture2D.Width &&
                ball.Y + ballTexture2D.Height-5 >= stick.Y)
                direction = Directions.LeftUp;
             
          
            Console.WriteLine("ball x" + ball.X+ " bally y" + ball.Y);
            Console.WriteLine("stick x" + stick.X + " stick y" + stick.Y);
            

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
          
            if (direction!=Directions.Null)
                spriteBatch.Draw(ballTexture2D,ball,Color.White);
            spriteBatch.Draw(stickTexture2D,stick,Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
