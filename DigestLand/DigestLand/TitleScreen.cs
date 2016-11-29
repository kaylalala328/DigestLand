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
    class TitleScreen : IScreen
    {
        Texture2D m_TitleBackground1;
        Texture2D m_TitleBackground2;
        Texture2D m_Cloud;

        Button m_StartButton;
        Button m_OptionButton;
        Button m_AboutButton;
        Button m_CollectionButton;
        Song music;

        public override void Init(ContentManager Content)
        {
            //은비노래
            music = Content.Load<Song>("노래/타이틀후보2");

            m_TitleBackground1 = Content.Load<Texture2D>("TITLE1");
            m_TitleBackground2 = Content.Load<Texture2D>("TITLE2");
            m_Cloud = Content.Load<Texture2D>("cloud");

            m_StartButton = new Button();
            m_StartButton.Init(Content, new Vector2(40, 600), "Button/StartButton", "Button/ClickedStartButton");
            m_StartButton.UserEvent = OnClickStartButton;

            m_OptionButton = new Button();
            m_OptionButton.Init(Content, new Vector2(1220, 625), "Button/OptionButton", "Button/ClickedOptionButton");
            m_OptionButton.UserEvent = OnClickOptionButton;

            m_AboutButton = new Button();
            m_AboutButton.Init(Content, new Vector2(1100, 625), "Button/CreaterButton", "Button/ClickedCreaterButton");
            m_AboutButton.UserEvent = OnClickCreaterButton;

            m_CollectionButton = new Button();
            m_CollectionButton.Init(Content, new Vector2(980, 635), "Button/CollectionButton", "Button/ClickedCollectionButton");
            m_CollectionButton.UserEvent = OnClickCollectionButton;
        }

        private void OnClickStartButton()
        {
            //MediaPlayer.Stop();
            m_ScreenManager.SelectScreen(Mode.ModeSelectionScreen);

        }

        private void OnClickOptionButton()
        {
            m_ScreenManager.SelectScreen(Mode.OptionAtTitleScreen);
        }

        private void OnClickCreaterButton()
        {
            m_ScreenManager.SelectScreen(Mode.AboutScreen);
        }

        private void OnClickCollectionButton()
        {
            m_ScreenManager.SelectScreen(Mode.CollectionScreen);
        }

        Vector2 m_CloudPosition = new Vector2(10, 80);

        int musicupdate = 0;
        public override void Update(GameTime gameTime)
        {
            if (musicupdate++ < 2)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
            var CloudSpeed = 0.5f;

            m_StartButton.Update(gameTime);
            m_OptionButton.Update(gameTime);
            m_AboutButton.Update(gameTime);
            m_CollectionButton.Update(gameTime);

            m_CloudPosition.X += CloudSpeed;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_TitleBackground1, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(m_Cloud, m_CloudPosition, Color.White);
            spriteBatch.Draw(m_TitleBackground2, new Vector2(0, 0), Color.White);

            m_StartButton.Draw(gameTime, spriteBatch);
            m_OptionButton.Draw(gameTime, spriteBatch);
            m_AboutButton.Draw(gameTime, spriteBatch);
            m_CollectionButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}