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
    class TutorialMouthScreen : IScreen
    {
        Button m_OptionButton;

        #region 변수선언_입
        Texture2D m_AppleIdle;
        Texture2D m_AppleAttack;
        Texture2D m_AppleRemains;

        Texture2D m_CheeseIdle;
        Texture2D m_CheeseAttack;
        Texture2D m_CheeseRemains;

        Texture2D m_MeatIdle;
        Texture2D m_MeatAttack;
        Texture2D m_MeatRemains;

        Texture2D m_MouthColorBackground;
        Texture2D m_MouthBackground;
        Texture2D m_Tongue;
        Texture2D m_Mouth;

        SpriteFont m_MyFont;

        List<Food> m_FoodList = new List<Food>();

        List<bool> m_IsAllDropDownList = new List<bool>();

        int[] AttackArea = new int[2] { 335, 750 };

        int FoodSpeed = 15;
        int MapCounter = 2;
        int FoodCounter = 5;

        //효과음
        SoundEffectInstance soundInstance;
        SoundEffect m_EatSound;
        //효과음   
        SoundEffectInstance soundInstance2;
        SoundEffect m_EatSound2;
        #endregion

        //음식물의 개수
        int m_FoodNumber = 15;
        Song music;

        public override void Init(ContentManager Content)
        {
            //은비노래
            music = Content.Load<Song>("노래/튜토리얼실행");

            m_OptionButton = new Button();
            //Pause Button으로 바꿔야될듯
            m_OptionButton.Init(Content, new Vector2(10, 10), "Button/OptionButton", "Button/ClickedOptionButton");
            m_OptionButton.UserEvent = OnClickOptionButton;

            m_MouthColorBackground = Content.Load<Texture2D>("MouthColorBackground");
            m_MouthBackground = Content.Load<Texture2D>("입/MouthBackground");
            m_Tongue = Content.Load<Texture2D>("입/Tongue");
            m_Mouth = Content.Load<Texture2D>("입/Mouth");

            m_AppleIdle = Content.Load<Texture2D>("입/FoodIdle");
            m_AppleAttack = Content.Load<Texture2D>("FoodAttack");
            m_AppleRemains = Content.Load<Texture2D>("appleremain");

            m_CheeseIdle = Content.Load<Texture2D>("CheeseIdle");
            m_CheeseAttack = Content.Load<Texture2D>("CheeseAttack");
            m_CheeseRemains = Content.Load<Texture2D>("cheeseremain");

            m_MeatIdle = Content.Load<Texture2D>("MeatIdle");
            m_MeatAttack = Content.Load<Texture2D>("MeatAttack");
            m_MeatRemains = Content.Load<Texture2D>("meatremain");

            m_MyFont = Content.Load<SpriteFont>("myFont");

            m_EatSound = Content.Load<SoundEffect>("입/입_아그작");
            soundInstance = m_EatSound.CreateInstance();
            soundInstance.IsLooped = false;

            m_EatSound2 = Content.Load<SoundEffect>("입/입_슝");
            soundInstance2 = m_EatSound2.CreateInstance();
            soundInstance2.IsLooped = false;

            m_FoodList.Add(new Food(2, 0));

            for (int i = 0; i < m_FoodNumber; i++)
                m_IsAllDropDownList.Add(false);

            m_PauseRect = new Rectangle(10, 10, 70, 70);

            //튜토리얼 순서별로 체크하는 변수
            for (var i = 0; i < 3; i++)
                m_Tutorial[i] = false;

            m_PauseBackground = Content.Load<Texture2D>("PauseBackground");
            m_TutorialKeyboard = Content.Load<Texture2D>("Button/Keyboard");
            m_ClickCursor = Content.Load<Texture2D>("PressCursor");
            m_MonyFront = Content.Load<Texture2D>("모니앞스프라이트");
            m_TutorialStep1 = Content.Load<Texture2D>("MouthTutorial1");
            m_TutorialStep2 = Content.Load<Texture2D>("MouthTutorial2");
            m_TutorialStep3 = Content.Load<Texture2D>("MouthTutorial3");
        }

        private void OnClickOptionButton()
        {
            m_ScreenManager.SelectScreen(Mode.OptionAtTutorialScreen);
            // MediaPlayer.Stop();
        }

        bool m_IsPaused = false;
        bool[] m_Tutorial = new bool[3];

        Rectangle m_PauseRect;

        Texture2D m_PauseBackground;
        Texture2D m_TutorialKeyboard;
        Texture2D m_ClickCursor;
        Texture2D m_MonyFront;
        Texture2D m_TutorialStep1;
        Texture2D m_TutorialStep2;
        Texture2D m_TutorialStep3;

        int m_CursorMotionIndex, m_CursorMotionWaitTime;
        int m_KeyboardMotionIndex, m_KeyboardMotionWaitTime;

        int musicupdate = 0;
        public override void Update(GameTime gameTime)
        {
            if (musicupdate++ < 2)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
            m_OptionButton.Update(gameTime);
            //첫 번째 튜토리얼
            if (m_FoodList[0].m_FoodPosition.X >= 300 && m_Tutorial[0] == false)
            {
                m_IsPaused = true;
            }

            //두 번째 튜토리얼
            else if (m_FoodList[0].m_IsDead == true && m_FoodList[0].m_IsOpen && m_Tutorial[0] == true && m_Tutorial[1] == false)
            {
                m_IsPaused = true;
            }

            //세 번째 튜토리얼
            else if (m_IsAllDropDownList.Contains(false) == false && m_Tutorial[1] == true && m_Tutorial[2] == false)
            {
                m_IsPaused = true;
            }

            if (!m_IsPaused)
            {
                base.Update(gameTime);

                UpdateFoodList(FoodSpeed, MapCounter, FoodCounter, AttackArea, m_FoodList);

                AddFood();
            }

            else
            {
                var KeyboardState = Keyboard.GetState();

                if (KeyboardState.IsKeyDown(Keys.Space))
                {
                    m_IsPaused = false;
                    if (m_Tutorial[0] == false)
                        m_Tutorial[0] = true;
                    else if (m_Tutorial[0] == true && m_Tutorial[1] == false)
                        m_Tutorial[1] = true;
                    else if (m_Tutorial[1] == true && m_Tutorial[2] == false)
                        m_Tutorial[2] = true;
                }

                CursorMotion();
                KeyboardMotion();
            }

            if (m_FoodList.Count == m_FoodNumber && m_Tutorial[2])
            {
                if (m_FoodList[m_FoodNumber - 1].m_FoodPosition.Y >= 1400)
                    m_ScreenManager.SelectScreen(Mode.TutorialStepSelectionScreen);
            }
        }

        private void KeyboardMotion()
        {
            if (m_KeyboardMotionWaitTime++ > 13)
            {
                m_KeyboardMotionWaitTime = 0;
                if (m_KeyboardMotionIndex++ >= 1)
                    m_KeyboardMotionIndex = 0;
            }
        }

        private void CursorMotion()
        {
            if (m_CursorMotionWaitTime++ > 7)
            {
                m_CursorMotionWaitTime = 0;
                if (m_CursorMotionIndex++ >= 3)
                    m_CursorMotionIndex = 0;
            }
        }

        int m_MonyFrontWaitTime;
        int m_MonyFrontMotionIndex;

        private void MonyMotion()
        {
            if (m_MonyFrontWaitTime++ > 10)
            {
                m_MonyFrontWaitTime = 0;
                if (m_MonyFrontMotionIndex++ >= 2)
                    m_MonyFrontMotionIndex = 0;
            }
        }

        private void AddFood()
        {
            var Random = new Random(DateTime.Now.Second);
            //음식물 추가
            if (m_FoodList.Count < m_FoodNumber)
                //450좌표를 지날 때 음식물 추가로 나오게 한다
                if (m_FoodList[m_FoodList.Count - 1].m_FoodPosition.X > 450)
                    m_FoodList.Add(new Food(2, Random.Next(0, 3)));
        }

        #region Update_입

        public enum FoodType { Apple = 0, Cheese, Meat }

        private void UpdateFoodList(int FoodSpeed, int MapCounter, int FoodCounter, int[] AttackArea, List<Food> FoodList)
        {
            for (var i = 0; i < m_FoodList.Count; i++)
            {
                FoodList[i].UpdateIsFoodAttack(MapCounter, AttackArea);
                FoodList[i].UpdateFood(FoodSpeed, FoodCounter, AttackArea);
                FoodList[i].UpdateDeadCount();

                //if (FoodList[i].IsDropDown)
                //    soundInstance2.Play();
            }
        }
        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            MonyMotion();

            spriteBatch.Begin();

            spriteBatch.Draw(m_MouthColorBackground, Vector2.Zero, Color.White);
            //갈색배경
            spriteBatch.Draw(m_MouthBackground, new Vector2(-120, 0), Color.White);
            DrawTongue(spriteBatch);
            DrawFood(spriteBatch);
            DrawMouth(spriteBatch);

            if (m_IsPaused)
            {
                if (m_Tutorial[0] == false)
                {
                    spriteBatch.Draw(m_TutorialStep1, Vector2.Zero, Color.White);
                    spriteBatch.Draw(m_MonyFront,
                                         new Rectangle(250, 768 / 2 - m_MonyFront.Height / 2, 200, 200),
                                         new Rectangle(200 * m_MonyFrontMotionIndex, 0, 200, 200),
                                         Color.White);
                }
                else if (m_Tutorial[0] == true && m_Tutorial[1] == false)
                {
                    spriteBatch.Draw(m_TutorialStep2, Vector2.Zero, Color.White);
                    spriteBatch.Draw(m_MonyFront,
                                         new Rectangle(250, 768 / 2 - m_MonyFront.Height / 2, 200, 200),
                                         new Rectangle(200 * m_MonyFrontMotionIndex, 0, 200, 200),
                                         Color.White);
                }
                else if (m_Tutorial[1] == true && m_Tutorial[2] == false)
                {
                    spriteBatch.Draw(m_TutorialStep3, Vector2.Zero, Color.White);
                    spriteBatch.Draw(m_MonyFront,
                                         new Rectangle(250, 768 / 2 - m_MonyFront.Height / 2, 200, 200),
                                         new Rectangle(200 * m_MonyFrontMotionIndex, 0, 200, 200),
                                         Color.White);
                }
                spriteBatch.Draw(m_TutorialKeyboard, new Rectangle(630, 480, 515, 90), new Rectangle(515 * m_KeyboardMotionIndex, 0, m_TutorialKeyboard.Width / 2, m_TutorialKeyboard.Height), Color.White);
                spriteBatch.Draw(m_ClickCursor, new Rectangle(890, 530, m_ClickCursor.Width / 4 * 2, m_ClickCursor.Height * 2), new Rectangle(27 * m_CursorMotionIndex, 0, m_ClickCursor.Width / 4, m_ClickCursor.Height), Color.White);
            }
            m_OptionButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }

        #region Draw_입

        private void DrawMouth(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_FoodList.Count; i++)
            {
                if (m_FoodList[i].m_IsDropDown)
                    m_IsAllDropDownList[i] = true;
            }
            for (var i = 0; i < m_FoodList.Count; i++)
            {
                if (m_FoodList[i].m_IsOpen)
                    spriteBatch.Draw(m_Mouth, Vector2.Zero, new Rectangle(0, 0, 1366, 768), Color.White);
                else
                    spriteBatch.Draw(m_Mouth, Vector2.Zero, new Rectangle(0, 768 * m_FoodList[i].m_MapMotionIndex, 1366, 768), Color.White);
            }
        }

        private void DrawFood(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < m_FoodList.Count; i++)
            {
                //입이 닫힐 때 음식물이 공격 범위에 있으면 m_IsFoodAttack = true;
                if (m_FoodList[i].m_IsFoodAttack)
                {
                    //음식물이 음식조각으로 변했을 때
                    if (m_FoodList[i].m_IsDead)
                    {
                        switch (m_FoodList[i].m_FoodType)
                        {
                            case 0:
                                spriteBatch.Draw(m_AppleRemains,
                                                 new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 40, 96, 96),
                                                 new Rectangle(m_AppleRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                 Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(m_CheeseRemains,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 40, 96, 96),
                                                     new Rectangle(m_CheeseRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                     Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(m_MeatRemains,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 40, 96, 96),
                                                     new Rectangle(m_MeatRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                     Color.White);
                                break;
                        }
                    }
                    else
                    {
                        switch (m_FoodList[i].m_FoodType)
                        {
                            case 0:
                                spriteBatch.Draw(m_AppleAttack, new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 90, 96, 96), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(m_CheeseAttack, new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 90, 96, 96), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(m_MeatAttack, new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y - 90, 96, 96), Color.White);
                                break;
                        }
                    }
                    soundInstance.Play();
                }
                //입이 열려있는 상태
                else
                {
                    //입이 열려있는 상태에서 음식물이 죽어있을 때
                    if (m_FoodList[i].m_IsDead)
                    {
                        switch (m_FoodList[i].m_FoodType)
                        {
                            case 0:
                                spriteBatch.Draw(m_AppleRemains,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                     new Rectangle(m_AppleRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                     Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(m_CheeseRemains,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                     new Rectangle(m_CheeseRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                     Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(m_MeatRemains,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                     new Rectangle(m_MeatRemains.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                     Color.White);
                                break;
                        }
                    }
                    else
                    {
                        switch (m_FoodList[i].m_FoodType)
                        {
                            case 0:
                                spriteBatch.Draw(m_AppleIdle,
                                                     new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                     new Rectangle(m_AppleIdle.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_AppleIdle.Width / 3, m_AppleIdle.Height),
                                                     Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(m_CheeseIdle, new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                 new Rectangle(m_CheeseIdle.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_CheeseIdle.Width / 3, m_CheeseIdle.Height),
                                                 Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(m_MeatIdle, new Rectangle((int)m_FoodList[i].m_FoodPosition.X, (int)m_FoodList[i].m_FoodPosition.Y, 96, 96),
                                                 new Rectangle(m_MeatIdle.Width / 3 * m_FoodList[i].m_FoodMotionIndex, 0, m_MeatIdle.Width / 3, m_MeatIdle.Height),
                                                 Color.White);
                                break;
                        }
                    }
                }
            }
        }

        private void DrawTongue(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < m_FoodList.Count; i++)
            {
                if (m_FoodList[i].m_IsOpen)
                    spriteBatch.Draw(m_Tongue, new Vector2(-120, 0), new Rectangle(0, 0, 1366 - 240, 768), Color.White);
                else
                    spriteBatch.Draw(m_Tongue, new Vector2(-120, 0), new Rectangle(0, 768 * m_FoodList[i].m_MapMotionIndex, 1366 - 240, 768), Color.White);
            }
        }
        #endregion
    }
}