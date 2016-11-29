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
    class TutorialStepSelectionScreen : IScreen
    {
        Texture2D m_Background;
        Texture2D m_ColorBackground;

        Button m_XButton;

        Button m_MouthButton;
        Button m_GulletButton;
        Button m_StomachButton;
        Button m_DuodenumButton;
        Button m_SmallIntestineButton;
        Button m_LargeIntestineButton;
        Song music;

        public override void Init(ContentManager Content)
        {

            //은비노래
            music = Content.Load<Song>("노래/튜토리얼모드");


            m_Background = Content.Load<Texture2D>("ScreenImage/Tutorial");
            m_ColorBackground = Content.Load<Texture2D>("ScreenImage/TutorialColorBackground");

            m_XButton = new Button();
            m_XButton.Init(Content, new Vector2(1265, 5), "Button/XButton", "Button/ClickedXButton");
            m_XButton.UserEvent = OnClickXButton;

            m_MouthButton = new Button();
            m_MouthButton.Init(Content, new Vector2(192, 154), "Button/MouthTutorialButton", "Button/ClickedMouthTutorialButton");
            m_MouthButton.UserEvent = OnClickMouthButton;

            m_GulletButton = new Button();
            m_GulletButton.Init(Content, new Vector2(546, 156), "Button/GulletTutorialButton", "Button/ClickedGulletTutorialButton");
            m_GulletButton.UserEvent = OnClickGulletButton;

            m_StomachButton = new Button();
            m_StomachButton.Init(Content, new Vector2(900, 156), "Button/StomachTutorialButton", "Button/ClickedStomachTutorialButton");
            m_StomachButton.UserEvent = OnClickStomachButton;

            m_DuodenumButton = new Button();
            m_DuodenumButton.Init(Content, new Vector2(192, 430), "Button/DuodenumTutorialButton", "Button/ClickedDuodenumTutorialButton");
            m_DuodenumButton.UserEvent = OnClickDuodenumButton;

            m_SmallIntestineButton = new Button();
            m_SmallIntestineButton.Init(Content, new Vector2(546, 430), "Button/SmallIntestineTutorialButton", "Button/ClickedSmallIntestineTutorialButton");
            m_SmallIntestineButton.UserEvent = OnClickSmallIntestineButton;

            m_LargeIntestineButton = new Button();
            m_LargeIntestineButton.Init(Content, new Vector2(900, 430), "Button/LargeIntestineTutorialButton", "Button/ClickedLargeIntestineTutorialButton");
            m_LargeIntestineButton.UserEvent = OnClickLargeIntestineButton;
        }

        private void OnClickXButton()
        {
            m_ScreenManager.SelectScreen(Mode.ModeSelectionScreen);
            MediaPlayer.Stop();
        }

        private void OnClickMouthButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialMouthScreen);
            MediaPlayer.Stop();
        }

        private void OnClickGulletButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialGulletScreen);
        }

        private void OnClickStomachButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialStomachScreen);
        }

        private void OnClickDuodenumButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialDuodenumScreen);
        }

        private void OnClickSmallIntestineButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialSmallIntestineScreen);
        }

        private void OnClickLargeIntestineButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialLargeIntestineScreen);
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
            m_MouthButton.Update(gameTime);
            m_GulletButton.Update(gameTime);
            m_StomachButton.Update(gameTime);
            m_DuodenumButton.Update(gameTime);
            m_SmallIntestineButton.Update(gameTime);
            m_LargeIntestineButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_ColorBackground, Vector2.Zero, Color.White);
            spriteBatch.Draw(m_Background, Vector2.Zero, Color.White);

            m_XButton.Draw(gameTime, spriteBatch);
            m_MouthButton.Draw(gameTime, spriteBatch);
            m_GulletButton.Draw(gameTime, spriteBatch);
            m_StomachButton.Draw(gameTime, spriteBatch);
            m_DuodenumButton.Draw(gameTime, spriteBatch);
            m_SmallIntestineButton.Draw(gameTime, spriteBatch);
            m_LargeIntestineButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}