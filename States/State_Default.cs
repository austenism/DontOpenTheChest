using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DontOpenTheChest.States
{
    public class State_Default
    {
        private Texture2D chestTexture;
        private Color chestColor = Color.White;

        Random ran = new Random();
        int shaking = 0;
        Vector2 offset;
        public int currentDistance;

        public void Initialize() 
        { 
            
        }
        public void LoadContent(ContentManager content)
        {
            chestTexture = content.Load<Texture2D>("chest_closed");
        }
        public void Update(GameTime gameTime, int currentDist)
        {
            //lets find the distance between the mouse and the forbidden lock handle
            currentDistance = currentDist;
            if(currentDistance > 200)
            {
                shaking = 0;
            }
            else if(currentDistance > 20)
            {
                shaking = 1;
            }
            else
            {
                shaking = 3;
            }
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (shaking > 0)
            {
                offset = new Vector2(ran.Next(-5 * shaking, 6 * shaking), ran.Next(-5 * shaking, 6 * shaking));
                spriteBatch.Begin(transformMatrix : Matrix.CreateTranslation(offset.X, offset.Y, 0));
            }
            else
            {
                spriteBatch.Begin();
            }
            spriteBatch.DrawString(spriteFont, 
                                    "         Don't open this 'chest'.\nIt's a risk that is not worth taking.\n              Could be a mimic.\nYou should just close the game now", 
                                    new Vector2(260, 50), Color.White, 0, new Vector2(0, 0),
                                    0.25f, SpriteEffects.None, 0);
            spriteBatch.End();

            spriteBatch.Begin();
            if (shaking > 1)
            {
                chestColor = Color.Red;
            }
            else 
            {
                chestColor = Color.White;
            }
            spriteBatch.Draw(chestTexture,
                                   new Vector2(400, 385),
                                   null,
                                   chestColor,
                                   0,
                                   new Vector2(342, 179),
                                   
                                   0.5f,
                                   SpriteEffects.None,
                                   0);
            spriteBatch.End();
        }
    }
}
