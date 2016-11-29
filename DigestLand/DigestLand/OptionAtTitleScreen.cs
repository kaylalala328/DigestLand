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
    class OptionAtTitleScreen : IScreen
    {
        Texture2D m_Background;

        Button m_SoundOnButton;
        Button m_SoundOffButton;
        Button m_BGMOnButton;
        Button m_BGMOffButton;
        Button m_HomeButton;

        public override void Init(ContentManager Content)
        {
            m_Background = Content.Load<Texture2D>("ScreenImage/OptionWindow");

            m_SoundOnButton = new Button();
            m_SoundOnButton.Init(Content, new Vector2(756, 274), "Button/OnButton", "Button/OffButton");
            m_SoundOnButton.UserEvent = OnClickSoundOnButton;

            m_SoundOffButton = new Button();
            m_SoundOffButton.Init(Content, new Vector2(756, 274), "Button/OffButton", "Button/OnButton");
            m_SoundOffButton.UserEvent = OnClickSoundOffButton;

            m_BGMOnButton = new Button();
            m_BGMOnButton.Init(Content, new Vector2(756, 380), "Button/OnButton", "Button/OffButton");
            m_BGMOnButton.UserEvent = OnClickBGMOnButton;

            m_BGMOffButton = new Button();
            m_BGMOffButton.Init(Content, new Vector2(756, 380), "Button/OffButton", "Button/OnButton");
            m_BGMOffButton.UserEvent = OnClickBGMOffButton;

            m_HomeButton = new Button();
            m_HomeButton.Init(Content, new Vector2(268, 520), "Button/HomeButton", "Button/ClickedHomeButton");
            m_HomeButton.UserEvent = OnClickHomeButton;
        }

        private void OnClickBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        private void OnClickSoundOnButton() { }

        private void OnClickBGMOffButton() { }

        private void OnClickBGMOnButton() { }

        private void OnClickSoundOffButton() { }

        private void OnClickHomeButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        public override void Update(GameTime gameTime)
        {
            m_SoundOnButton.Update(gameTime);
            m_SoundOffButton.Update(gameTime);
            m_BGMOnButton.Update(gameTime);
            m_BGMOffButton.Update(gameTime);
            m_HomeButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_Background, Vector2.Zero, Color.White);

            m_SoundOnButton.Draw(gameTime, spriteBatch);
            m_SoundOffButton.Draw(gameTime, spriteBatch);
            m_BGMOnButton.Draw(gameTime, spriteBatch);
            m_BGMOffButton.Draw(gameTime, spriteBatch);
            m_HomeButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}