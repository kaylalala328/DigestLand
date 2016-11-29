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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager m_ScreenManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1366;

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            m_ScreenManager = new ScreenManager();

            IScreen.m_ScreenManager = m_ScreenManager;

            m_ScreenManager.AddScreen(Mode.TitleScreen, new TitleScreen(), Content);
            m_ScreenManager.AddScreen(Mode.AboutScreen, new AboutScreen(), Content);
            m_ScreenManager.AddScreen(Mode.OptionAtTitleScreen, new OptionAtTitleScreen(), Content);
            m_ScreenManager.AddScreen(Mode.OptionAtTutorialScreen, new OptionAtTutorialScreen(), Content);
            m_ScreenManager.AddScreen(Mode.OptionAtChallengeScreen, new OptionAtChallengeScreen(), Content);
            m_ScreenManager.AddScreen(Mode.ModeSelectionScreen, new ModeSelectionScreen(), Content);
            m_ScreenManager.AddScreen(Mode.CollectionScreen, new CollectionScreen(), Content);
            m_ScreenManager.AddScreen(Mode.ChallengeModeScreen, new ChallengeModeScreen(), Content);
            m_ScreenManager.AddScreen(Mode.TutorialStoryScreen, new TutorialStoryScreen(), Content);
            m_ScreenManager.AddScreen(Mode.TutorialStepSelectionScreen, new TutorialStepSelectionScreen(), Content);
            m_ScreenManager.AddScreen(Mode.TutorialMouthScreen, new TutorialMouthScreen(), Content);
            m_ScreenManager.AddScreen(Mode.TutorialGulletScreen, new TutorialGulletScreen(), Content);
            //m_ScreenManager.AddScreen(Mode.TutorialStomachScreen, new TutorialStomachScreen(), Content);
            //m_ScreenManager.AddScreen(Mode.TutorialDuodenumScreen, new TutorialDuodenumScreen(), Content);
            //m_ScreenManager.AddScreen(Mode.TutorialSmallIntestineScreen, new TutorialSmallIntestineScreen(), Content);
            //m_ScreenManager.AddScreen(Mode.TutorialLargeIntestineScreen, new TutorialLargeIntestineScreen(), Content);
            //m_ScreenManager.AddScreen(Mode.UserFeedBackScreen, new UserFeedBackScreen(), Content);

            m_ScreenManager.SelectScreen(Mode.ChallengeModeScreen);

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            m_ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            m_ScreenManager.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}