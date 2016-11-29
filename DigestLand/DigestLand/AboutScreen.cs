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
    class AboutScreen : IScreen
    {
        Button m_BackButton;

        public override void Init(ContentManager Content)
        {
            m_BackButton = new Button();
            m_BackButton.Init(Content, new Vector2(62, 50), "Button/BackButton", "Button/ClickedBackButton");
            m_BackButton.UserEvent = OnClickBackButton;
        }

        private void OnClickBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        public override void Update(GameTime gameTime)
        {
            m_BackButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_BackButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}