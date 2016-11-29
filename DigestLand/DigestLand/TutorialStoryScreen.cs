using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DigestLand
{
    class TutorialStoryScreen : IScreen
    {
        Button m_XButton;
        Texture2D[] m_StoryImage = new Texture2D[14];
        Song music;
        public override void Init(ContentManager Content)
        {
            //은비노래
            music = Content.Load<Song>("노래/스토리모드");

            m_StoryImage[0] = Content.Load<Texture2D>("STORY1");
            m_StoryImage[1] = Content.Load<Texture2D>("STORY2");
            m_StoryImage[2] = Content.Load<Texture2D>("STORY5");
            m_StoryImage[3] = Content.Load<Texture2D>("STORY6");
            m_StoryImage[4] = Content.Load<Texture2D>("STORY7");
            m_StoryImage[5] = Content.Load<Texture2D>("STORY8");
            m_StoryImage[6] = Content.Load<Texture2D>("STORY9");
            m_StoryImage[7] = Content.Load<Texture2D>("STORY10");
            m_StoryImage[8] = Content.Load<Texture2D>("STORY11");
            m_StoryImage[9] = Content.Load<Texture2D>("STORY12");
            m_StoryImage[10] = Content.Load<Texture2D>("STORY13");
            m_StoryImage[11] = Content.Load<Texture2D>("STORY14");
            m_StoryImage[12] = Content.Load<Texture2D>("STORY15");
            m_StoryImage[13] = Content.Load<Texture2D>("STORY4");

            m_XButton = new Button();
            m_XButton.Init(Content, new Vector2(1265, 5), "Button/XButton", "Button/ClickedXButton");
            m_XButton.UserEvent = OnClickSkipButton;


        }

        private void OnClickSkipButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialStepSelectionScreen);
            MediaPlayer.Stop();
        }
        int musicupdate = 0;
        public override void Update(GameTime gameTime)
        {
            if (musicupdate++ < 2)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
            m_XButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_XButton.Draw(gameTime, spriteBatch);

            var keyboardState = Keyboard.GetState();

            spriteBatch.Draw(m_StoryImage[0], Vector2.Zero, Color.White);

            if (keyboardState.IsKeyDown(Keys.W))
                spriteBatch.Draw(m_StoryImage[1], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.E))
                spriteBatch.Draw(m_StoryImage[13], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.R))
                spriteBatch.Draw(m_StoryImage[2], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.A))
                spriteBatch.Draw(m_StoryImage[4], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.S))
                spriteBatch.Draw(m_StoryImage[5], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.D))
                spriteBatch.Draw(m_StoryImage[6], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.F))
                spriteBatch.Draw(m_StoryImage[7], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.Z))
                spriteBatch.Draw(m_StoryImage[8], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.X))
                spriteBatch.Draw(m_StoryImage[9], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.C))
                spriteBatch.Draw(m_StoryImage[10], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.V))
                spriteBatch.Draw(m_StoryImage[11], Vector2.Zero, Color.White);
            else if (keyboardState.IsKeyDown(Keys.B))
                spriteBatch.Draw(m_StoryImage[12], Vector2.Zero, Color.White);

            m_XButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}