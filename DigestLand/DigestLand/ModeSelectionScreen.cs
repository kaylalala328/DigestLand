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
    class ModeSelectionScreen : IScreen
    {
        Texture2D m_Background;

        //TutorialButton, ClickedTutorialButton
        Button m_TutorialButton;
        //ChallengeModeButton, ClickedChallengeModeButton, LockedChallengeModeButton
        Button m_ChallengeButton;
        //BackButton, ClickedBackButton
        Button m_BackButton;
        Song music;


        public override void Init(ContentManager Content)
        {
            //은비노래

            music = Content.Load<Song>("노래/셀렉트모드");
            m_Background = Content.Load<Texture2D>("ScreenImage/ModeSelctionState");

            m_TutorialButton = new Button();
            m_TutorialButton.Init(Content, new Vector2(313, 323), "Button/TutorialButton", "Button/ClickedTutorialButton");
            m_TutorialButton.UserEvent = OnClickTutorialButton;

            m_ChallengeButton = new Button();
            m_ChallengeButton.Init(Content, new Vector2(772, 323), "Button/ChallengeModeButton", "Button/ClickedChallengeModeButton");
            m_ChallengeButton.UserEvent = OnClickChallengeModeButton;

            m_BackButton = new Button();
            m_BackButton.Init(Content, new Vector2(62, 50), "Button/BackButton", "Button/ClickedBackButton");
            m_BackButton.UserEvent = OnClickBackButton;
        }

        private void OnClickTutorialButton()
        {
            m_ScreenManager.SelectScreen(Mode.TutorialStoryScreen);
            MediaPlayer.Stop();
        }

        private void OnClickChallengeModeButton()
        {
            m_ScreenManager.SelectScreen(Mode.ChallengeModeScreen);
            MediaPlayer.Stop();
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
            m_TutorialButton.Update(gameTime);
            m_ChallengeButton.Update(gameTime);
            m_BackButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_Background, Vector2.Zero, Color.White);

            m_TutorialButton.Draw(gameTime, spriteBatch);
            m_ChallengeButton.Draw(gameTime, spriteBatch);
            m_BackButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}