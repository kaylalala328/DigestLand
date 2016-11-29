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
    class OptionAtTutorialScreen : IScreen
    {
        Texture2D m_Background;

        Button m_XButton;
        Button m_SoundOnButton;
        Button m_SoundOffButton;
        Button m_BGMOnButton;
        Button m_BGMOffButton;
        Button m_HomeButton;
        Button m_ReplayButton;
        Button m_StepSelectionButton;

        public override void Init(ContentManager Content)
        {
            m_Background = Content.Load<Texture2D>("ScreenImage/OptionWindow");

            //챌린지, 타이틀에서 누른 옵션일 경우 삭제
            m_XButton = new Button();
            m_XButton.Init(Content, new Vector2(1140, 80), "Button/XButton", "Button/ClickedXButton");
            m_XButton.UserEvent = OnClickXButton;

            m_SoundOnButton = new Button();
            m_SoundOnButton.Init(Content, new Vector2(756, 274), "Button/OnButton", "Button/OnButton");
            m_SoundOnButton.UserEvent = OnClickSoundOnButton;

            m_SoundOffButton = new Button();
            m_SoundOffButton.Init(Content, new Vector2(756, 274), "Button/OnButton", "Button/OnButton");
            m_SoundOffButton.UserEvent = OnClickSoundOffButton;

            m_BGMOnButton = new Button();
            m_BGMOnButton.Init(Content, new Vector2(756, 380), "Button/OnButton", "Button/OffButton");
            m_BGMOnButton.UserEvent = OnClickBGMOnButton;

            m_BGMOffButton = new Button();
            m_BGMOffButton.Init(Content, new Vector2(756, 380), "Button/OnButton", "Button/OnButton");
            m_BGMOffButton.UserEvent = OnClickBGMOffButton;

            m_HomeButton = new Button();
            m_HomeButton.Init(Content, new Vector2(268, 520), "Button/HomeButton", "Button/ClickedHomeButton");
            m_HomeButton.UserEvent = OnClickHomeButton;

            m_ReplayButton = new Button();
            m_ReplayButton.Init(Content, new Vector2(617, 520), "Button/ReplayButton", "Button/ClickedReplayButton");
            m_ReplayButton.UserEvent = OnClickReplayButton;

            m_StepSelectionButton = new Button();
            m_StepSelectionButton.Init(Content, new Vector2(962, 520), "Button/StepSelectionButton", "Button/ClickedStepSelectionButton");
            m_StepSelectionButton.UserEvent = OnClickStepSelectionButton;
        }

        private void OnClickXButton()
        {
            m_ScreenManager.SelectScreen(Mode.ModeSelectionScreen);
        }

        private void OnClickSoundOnButton() { }

        private void OnClickSoundOffButton() { }

        private void OnClickBGMOnButton() { }

        private void OnClickBGMOffButton() { }

        private void OnClickHomeButton()
        {
            m_ScreenManager.SelectScreen(Mode.TitleScreen);
        }

        private void OnClickReplayButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialMouthScreen);
        }

        private void OnClickCollectionButton()
        {
            m_ScreenManager.SelectScreen(Mode.CollectionScreen);
        }

        private void OnClickStepSelectionButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialStepSelectionScreen);
        }

        public override void Update(GameTime gameTime)
        {
            m_XButton.Update(gameTime);
            m_SoundOnButton.Update(gameTime);
            m_SoundOffButton.Update(gameTime);
            m_BGMOnButton.Update(gameTime);
            m_BGMOffButton.Update(gameTime);
            m_HomeButton.Update(gameTime);
            m_ReplayButton.Update(gameTime);
            m_StepSelectionButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_Background, Vector2.Zero, Color.White);

            m_XButton.Draw(gameTime, spriteBatch);
            m_SoundOnButton.Draw(gameTime, spriteBatch);
            m_SoundOffButton.Draw(gameTime, spriteBatch);
            m_BGMOnButton.Draw(gameTime, spriteBatch);
            m_BGMOffButton.Draw(gameTime, spriteBatch);
            m_HomeButton.Draw(gameTime, spriteBatch);
            m_ReplayButton.Draw(gameTime, spriteBatch);
            m_StepSelectionButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}