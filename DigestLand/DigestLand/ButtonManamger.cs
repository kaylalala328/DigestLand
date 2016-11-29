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
    class Button
    {
        Texture2D m_Normal;
        Texture2D m_Press;

        bool m_IsPress;
        bool m_IsPressOnce;

        Vector2 m_Position;

        //은비 효과음
        SoundEffectInstance soundInstance;
        SoundEffect m_ButtonSound;

        public delegate void ClickEvent();

        public ClickEvent UserEvent;

        public void Init(ContentManager Content, Vector2 position, string normalTextureName, string pressTextureName)
        {
            m_Normal = Content.Load<Texture2D>(normalTextureName);
            m_Press = Content.Load<Texture2D>(pressTextureName);

            m_Position = position;

            m_ButtonSound = Content.Load<SoundEffect>("버튼음");

            soundInstance = m_ButtonSound.CreateInstance();
            soundInstance.IsLooped = false;
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            m_IsPress = false;

            var rectangle = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Normal.Width, m_Normal.Height);

            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    m_IsPressOnce = true;
                    m_IsPress = false;
                }
                else
                {
                    m_IsPress = true;

                }
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                if (m_IsPressOnce)
                {
                    if (rectangle.Contains(mouseState.X, mouseState.Y))
                    {
                        //버튼 음 추가
                        soundInstance.Play();
                        if (UserEvent != null)
                            UserEvent();
                    }
                }
                m_IsPressOnce = false;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_IsPress ? m_Press : m_Normal, m_Position, m_IsPress ? Color.White : Color.White);
        }
    }
}
