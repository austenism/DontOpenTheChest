using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace DontOpenTheChest.States
{
    public class State_Opened
    {
        private Texture2D chestTexture;
        Texture2D whiteOut;
        Texture2D beaconTexture;
        float beaconRotation = 0.0f;
        float alpha = 1.0f;
        int wait = 2;
       
        bool beaconGone = false;
        bool skipped = false;

        KeyboardState keyboard;

        bool PlayingAudio = false;
        Song beaconTOUCHED;

        private double TotalSeconds = 0.0;

        public void LoadContent(ContentManager content)
        {
            chestTexture = content.Load<Texture2D>("chest_open");
            whiteOut = content.Load<Texture2D>("WhiteOut");
            beaconTexture = content.Load<Texture2D>("beacon");
            beaconTOUCHED = content.Load<Song>("ANEWHANDTOUCHESTHEBEACON");
        }
        public void Update(GameTime gameTime)
        {
            TotalSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            if(TotalSeconds > wait && !PlayingAudio && !skipped)
            {
                PlayingAudio = true;
                MediaPlayer.Volume = 1.0f;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Play(beaconTOUCHED);
            }
            if(TotalSeconds > (27 + wait) && PlayingAudio)
            {
                beaconGone = true;
                MediaPlayer.Stop();
            }

            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                beaconGone = true;
                MediaPlayer.Stop();
            }

            if (alpha > 0.0f) 
                alpha -= 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;//give fading to alpha for whiteOut

            beaconRotation += (float)(2 * Math.PI) * 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (beaconRotation > (float)(2 * Math.PI))
                beaconRotation -= (float)(2 * Math.PI);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.Begin();
            //draw the chest
            spriteBatch.Draw(chestTexture,
                                   new Vector2(400, 385),
                                   null,
                                   Color.White,
                                   0,
                                   new Vector2(381, 406),
                                   0.5f,
                                   SpriteEffects.None,
                                   0);
            //draw the beacon
            if (!beaconGone && !skipped)
            {
                spriteBatch.Draw(beaconTexture,
                                       new Vector2(400, 285),
                                       null,
                                       Color.White,
                                       beaconRotation,
                                       new Vector2(175, 153),
                                       0.5f,
                                       SpriteEffects.None,
                                       0);
                spriteBatch.DrawString(spriteFont, "You fool, it's not a mimic at all! It's something much worse...",
                                        new Vector2(165, 50), Color.White, 0, new Vector2(0, 0),
                                        0.25f, SpriteEffects.None, 0);
                spriteBatch.DrawString(spriteFont, "Press ENTER to skip", Vector2.Zero, Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "There it goes... off to haunt another unfortunate chest",
                                        new Vector2(165, 50), Color.White, 0, new Vector2(0, 0),
                                        0.25f, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            if (alpha > 0.0f)
            {
                spriteBatch.Begin(blendState: BlendState.AlphaBlend);
                spriteBatch.Draw(whiteOut, new Vector2(0, 0), Color.White * alpha);
                spriteBatch.End();
            }
        }
    }
}
