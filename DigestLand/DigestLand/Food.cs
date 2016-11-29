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
    public class Food
    {
        public Vector2 m_FoodPosition;
        public bool m_IsOpen = true;

        public int m_MapWaitTime = 0;
        public int m_MapMotionIndex = 0;

        public int m_FoodWaitTime = 0;
        public int m_FoodMotionIndex = 0;

        public int m_DeadCountTime = 0;

        public int m_Gravity = 20;

        public int m_DeadCount;

        public bool m_IsFoodAttack;
        public bool m_IsDead;
        public bool m_PlusScore;
        public bool m_MinusScore;
        public bool m_IsDropDown = false;

        public int m_FoodType;

        KeyboardState m_CurrentKeyboardState;

        public Food(int DeadCount, int FoodType)
        {
            m_FoodPosition = new Vector2(100, 440);
            m_DeadCount = DeadCount;
            m_FoodType = FoodType;
        }

        public void UpdateFood(int FoodSpeed, int FoodCounter, int[] AttackArea)
        {
            if (m_FoodWaitTime++ > FoodCounter)
            {
                m_FoodWaitTime = 0;

                if (m_FoodMotionIndex++ >= 2)
                    m_FoodMotionIndex = 0;

                if (m_FoodPosition.X > AttackArea[1])
                    m_IsDropDown = true;

                //음식물이 잔여물이 됐을 때
                if (m_IsDead)
                {
                    //목구멍으로 넘어갈 수 있는 범위
                    if (m_IsDropDown)
                    {
                        if (m_FoodPosition.X < 1150)
                        {
                            m_FoodPosition.X += 20;
                            m_FoodPosition.Y += 20;
                        }
                        else if (m_FoodPosition.X >= 1150 && m_FoodPosition.X <= 1250)
                        {
                            m_FoodPosition.Y += 30;
                        }
                        else if (m_FoodPosition.X > 1250)
                        {
                            m_FoodPosition.X -= 15;
                            m_FoodPosition.Y += 20;
                        }
                    }
                    else
                        if (!m_IsOpen)
                            m_FoodPosition.X += 20;
                }
                else
                {
                    if (m_FoodPosition.X > AttackArea[1] && m_FoodPosition.X < 1000)
                    {
                        m_FoodPosition.X += 20;
                        m_FoodPosition.Y += 20;
                        //m_FoodMotionIndex = 1;
                    }
                    else if (m_FoodPosition.X >= 1000)
                    {
                        m_FoodPosition.X += 15;
                        m_FoodPosition.Y += 30;
                    }
                    else
                    {
                        m_FoodPosition.X += FoodSpeed;


                    }
                }
            }
        }

        public void UpdateIsFoodAttack(int MapCounter, int[] AttackArea)
        {
            m_CurrentKeyboardState = Keyboard.GetState();

            if (m_CurrentKeyboardState.IsKeyDown(Keys.Space) && m_IsOpen)
                m_IsOpen = false;

            if (!m_IsOpen)
            {
                if (m_MapWaitTime++ > MapCounter)
                {
                    m_MapWaitTime = 0;

                    if (m_FoodPosition.X >= AttackArea[0] && m_FoodPosition.X <= AttackArea[1])
                        m_IsFoodAttack = true;

                    if (m_MapMotionIndex++ > 4)
                        m_MapMotionIndex = 0;
                }
                if (m_MapMotionIndex == 5)
                    m_IsOpen = true;
            }
            else
                m_IsFoodAttack = false;
        }

        public void UpdateDeadCount()
        {
            if (m_DeadCountTime++ > 60 && m_IsFoodAttack)
            {
                m_DeadCountTime = 0;
                --m_DeadCount;
                if (m_DeadCount == 0)
                {
                    m_IsDead = true;
                    m_PlusScore = m_IsDead;
                    m_MinusScore = !m_IsDead;
                }
            }
        }
    }
}