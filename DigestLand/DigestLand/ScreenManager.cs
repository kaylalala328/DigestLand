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
    public enum Mode
    {
        TitleScreen,
        ModeSelectionScreen, AboutScreen, CollectionScreen,
        OptionAtTitleScreen, OptionAtTutorialScreen, OptionAtChallengeScreen,
        ChallengeModeScreen,
        TutorialStoryScreen, TutorialStepSelectionScreen,
        TutorialMouthScreen, TutorialGulletScreen, TutorialStomachScreen, TutorialDuodenumScreen, TutorialSmallIntestineScreen, TutorialLargeIntestineScreen,
        UserFeedBackScreen
    }

    class ScreenManager
    {
        Dictionary<Mode, IScreen> m_DicScreen = new Dictionary<Mode, IScreen>();

        IScreen m_ActiveScreen = null;

        public void AddScreen(Mode mode, IScreen screen, ContentManager Content)
        {
            screen.Init(Content);
            m_DicScreen.Add(mode, screen);
        }

        public IScreen GetScreen(Mode mode)
        {
            return m_DicScreen[mode];
        }

        public void Update(GameTime gameTime)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (m_ActiveScreen != null)
                m_ActiveScreen.Draw(gameTime, spriteBatch);
        }

        public void SelectScreen(Mode mode)
        {
            m_ActiveScreen = m_DicScreen[mode];
        }
    }
}