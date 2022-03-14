using DontOpenTheChest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DontOpenTheChest
{
    public enum GameState
    {
        Default,
        Opened
    }
    public class DontOpenTheChest : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        GameState state;
        State_Default defaultLoop;
        State_Opened openedLoop;

        MouseState currentMouseState;
        MouseState priorMouseState;

        public DontOpenTheChest()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            defaultLoop = new State_Default();
            openedLoop = new State_Opened();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("ComicSans");

            defaultLoop.LoadContent(Content);
            openedLoop.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (state == GameState.Default)
            {
                currentMouseState = Mouse.GetState();
                int currentDistance = (int)Math.Sqrt(Math.Pow(400 - currentMouseState.X, 2) + Math.Pow(400 - currentMouseState.Y, 2));
                defaultLoop.Update(gameTime, currentDistance);
                if (currentDistance < 21 && currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released)
                {
                    state = GameState.Opened;
                }
            }
            else
            {
                openedLoop.Update(gameTime);
            }
            
            
            priorMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (state == GameState.Default)
            {
                defaultLoop.Draw(_spriteBatch, _spriteFont);
            }
            else
            {
                openedLoop.Draw(_spriteBatch, _spriteFont);
            }
            base.Draw(gameTime);
        }
    }
}
