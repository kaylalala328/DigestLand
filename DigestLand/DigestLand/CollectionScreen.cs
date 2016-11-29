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
    class CollectionScreen : IScreen
    {
        Texture2D m_Background;
        Button m_BackButton;
        Song music;
        public override void Init(ContentManager Content)
        {//은비노래
            music = Content.Load<Song>("노래/스티커판");

            m_Background = Content.Load<Texture2D>("ScreenImage/CollectionState");

            m_BackButton = new Button();
            m_BackButton.Init(Content, new Vector2(62, 50), "Button/BackButton", "Button/ClickedBackButton");
            m_BackButton.UserEvent = OnClickBackButton;
        }

        private void OnClickBackButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
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
            m_BackButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_Background, Vector2.Zero, Color.White);
            m_BackButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
