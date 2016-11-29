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
    class TutorialGulletScreen : IScreen
    {
        SoundEffectInstance Wrong;
        SoundEffect Wrong_Sound;
        int TotalScore = 0;
        Texture2D m_기본_위;
        SoundEffectInstance 정답;
        SoundEffect 정답_Sound;

        Texture2D m_AppleAttack;
        Texture2D m_CheeseAttack;
        Texture2D m_MeatAttack;
        Texture2D m_AppleRemains;
        Texture2D m_CheeseRemains;
        Texture2D m_MeatRemains;
        Texture2D m_ClearImage;
        int GoStomach = 8800;

        #region 변수선언_식도
        //알파벳 아이템
        Texture2D[] m_ItemList_On = new Texture2D[6];
        Texture2D[] m_ItemList_Off = new Texture2D[6];
        Texture2D m_GameOver;

        Texture2D m_기본_입;
        const float gravity = 5;
        int Gulletupdate = 0;
        List<int> FixFoodType = new List<int>();
        List<int> ObstacleType = new List<int>();
        List<int> EatFoodType = new List<int>();

        int[] m_FoodTypeArr;

        SpriteFont m_Font;
        SpriteFont m_Font2;
        SpriteFont m_Font3;

        //카운트다운 효과음 
        SoundEffect m_CountSound;
        Texture2D m_CountImage1;
        Texture2D m_CountImage2;
        Texture2D m_CountImage3;
        Texture2D m_CountImageGO;

        Texture2D m_GulletColorBackground;
        Texture2D[] m_ThroatMap = new Texture2D[12];

        //보너스
        Texture2D m_BonusImage;

        //요정
        Texture2D m_FairyImage;
        Vector2 m_FairyPosition = new Vector2(950, 200);
        Rectangle m_FairyRect;

        //하트 생명
        Texture2D m_Life;


        //방해물
        Texture2D m_Obstacle;

        //방해물 부딪혔을 때 효과
        Texture2D m_EffectSprite;

        // 음식 잔여물 아이템들을 랜덤배치하여 담아두는 List (맵별로 각각)
        List<Vector2> m_FixedFoodPosition = new List<Vector2>();
        List<Rectangle> m_FixedFoodRect = new List<Rectangle>();
        List<bool> m_FixedFoodIsDrawble = new List<bool>();

        Vector2[] m_FixedObstaclePosition;
        Rectangle[] m_FixedObstacleRect;
        List<bool> m_FixedObstacleIsDrawble = new List<bool>();


        //알파벳 아이템 
        List<bool> m_FixedItemIsDrawble = new List<bool>();
        Rectangle[] m_FixedItemRect;
        Vector2[] m_FixedItemPosition;

        //모니요정이 음식물 먹으면 머리 위에 따라오게 할 음식List
        List<Vector2> m_FoodStackOnMony = new List<Vector2>();

        //모니요정의 목숨 하트 List
        List<bool> m_LifeList = new List<bool>();

        //countdown할때 쓰기 위한 변수
        int count = 4;

        float m_CameraScale = 1.0f;

        int m_Speed = 8;

        int m_Score = 0;

        int m_FrameCount = 0;

        int m_FixedFoodSize = 60;
        int m_FixedObstacleSize = 100;

        int m_TimeIndex = 0;
        int m_CollideNumber = 0;
        int m_Bonus = 0;

        bool m_IsStart = false;

        int m_MonyWaitTime = 0;
        int m_MonyFrameCounter = 0;

        int ObstacleNumber = 10;
        int ObstacleIntex;

        int FoodNumber = 15;
        int FoodIndex;
        int EatGulletFoodNumber = 0;
        #endregion
        #region 식도로 넘겨줄 변수


        //음식물의 개수
        int m_FoodNumber = 15;
        //죽은 음식물의 개수
        int[] m_DeadNumber;
        //int m_ObstacleNumber;
        //int m_죽은음식물의개수;

        #endregion
        public override void Init(ContentManager Content)
        {
            Wrong_Sound = Content.Load<SoundEffect>("노래/wrong");
            Wrong = Wrong_Sound.CreateInstance();
            Wrong.IsLooped = false;
            m_기본_위 = Content.Load<Texture2D>("기본맵/기본위");

            정답_Sound = Content.Load<SoundEffect>("노래/정답");
            정답 = 정답_Sound.CreateInstance();
            정답.IsLooped = false;

            m_ClearImage = Content.Load<Texture2D>("CLEAR");

            #region 식도_Load

            m_GameOver = Content.Load<Texture2D>("식도/gameover");
            m_ItemList_On[0] = Content.Load<Texture2D>("식도/G_on");
            m_ItemList_On[1] = Content.Load<Texture2D>("식도/U_on");
            m_ItemList_On[2] = Content.Load<Texture2D>("식도/L_on(1)");
            m_ItemList_On[3] = Content.Load<Texture2D>("식도/L_on(2)");
            m_ItemList_On[4] = Content.Load<Texture2D>("식도/E_on");
            m_ItemList_On[5] = Content.Load<Texture2D>("식도/T_on");

            m_ItemList_Off[0] = Content.Load<Texture2D>("식도/G_off");
            m_ItemList_Off[1] = Content.Load<Texture2D>("식도/U_off");
            m_ItemList_Off[2] = Content.Load<Texture2D>("식도/L_off");
            m_ItemList_Off[3] = Content.Load<Texture2D>("식도/L_off");
            m_ItemList_Off[4] = Content.Load<Texture2D>("식도/E_off");
            m_ItemList_Off[5] = Content.Load<Texture2D>("식도/T_off");

            m_CountSound = Content.Load<SoundEffect>("식도/countdown");
            m_기본_입 = Content.Load<Texture2D>("기본맵/기본입");
            m_GulletColorBackground = Content.Load<Texture2D>("GulletColorBackground");

            m_AppleAttack = Content.Load<Texture2D>("FoodAttack");
            m_CheeseAttack = Content.Load<Texture2D>("CheeseAttack");
            m_MeatAttack = Content.Load<Texture2D>("MeatAttack");

            m_AppleRemains = Content.Load<Texture2D>("appleremain");
            m_CheeseRemains = Content.Load<Texture2D>("cheeseremain");
            m_MeatRemains = Content.Load<Texture2D>("meatremain");

            m_CountImage1 = Content.Load<Texture2D>("식도/count1");
            m_CountImage2 = Content.Load<Texture2D>("식도/count2");
            m_CountImage3 = Content.Load<Texture2D>("식도/count3");
            m_CountImageGO = Content.Load<Texture2D>("식도/GO");

            m_BonusImage = Content.Load<Texture2D>("BONUS");
            m_ThroatMap[0] = Content.Load<Texture2D>("기본맵/식도1");
            m_ThroatMap[1] = Content.Load<Texture2D>("기본맵/식도2");
            m_ThroatMap[2] = Content.Load<Texture2D>("기본맵/식도3");
            m_ThroatMap[3] = Content.Load<Texture2D>("기본맵/식도4");
            m_ThroatMap[4] = Content.Load<Texture2D>("기본맵/식도5");
            m_ThroatMap[5] = Content.Load<Texture2D>("기본맵/식도6");
            m_ThroatMap[6] = Content.Load<Texture2D>("기본맵/식도7");
            m_ThroatMap[7] = Content.Load<Texture2D>("기본맵/식도8");
            m_ThroatMap[8] = Content.Load<Texture2D>("기본맵/식도9");
            m_ThroatMap[9] = Content.Load<Texture2D>("기본맵/식도10");
            m_ThroatMap[10] = Content.Load<Texture2D>("기본맵/식도11");
            m_ThroatMap[11] = Content.Load<Texture2D>("기본맵/식도12");

            m_FairyImage = Content.Load<Texture2D>("모니앞스프라이트");


            //수정
            m_DeadNumber = new int[m_FoodNumber];
            m_FoodTypeArr = new int[m_FoodNumber];


            //하트 생명
            m_Life = Content.Load<Texture2D>("식도/heart");

            //모니 생명 하트 불어넣기
            //false이면 생명 하나 줄어든다.
            for (int i = 0; i < 3; i++)
            {
                m_LifeList.Add(true);
            }

            m_Font = Content.Load<SpriteFont>("식도/myFont");
            m_Font2 = Content.Load<SpriteFont>("식도/myFont2");
            m_Font3 = Content.Load<SpriteFont>("식도/myFont3");
            m_Obstacle = Content.Load<Texture2D>("FoodAttack");
            m_EffectSprite = Content.Load<Texture2D>("식도/effect");
            //var music = Content.Load<Song>("식도/bgm");

            //MediaPlayer.Play(music);
            //MediaPlayer.IsRepeating = true;



            //충돌업데이트();
            //Allocate_Fixed_Obstacle();
            //Allocate_Fixed_Food();

            //알파벳 아이템 
            Allocate_FixedItem();

            #endregion
        }

        private void Allocate_FixedItem()
        {
            for (int i = 0; i < 6; i++)
                m_FixedItemIsDrawble.Add(false);


            m_FixedItemPosition = new Vector2[6];

            m_FixedItemPosition[0] = new Vector2(600, 800);
            m_FixedItemPosition[1] = new Vector2(600, 1300);
            m_FixedItemPosition[2] = new Vector2(600, 3000);
            m_FixedItemPosition[3] = new Vector2(500, 3900);
            m_FixedItemPosition[4] = new Vector2(400, 5500);
            m_FixedItemPosition[5] = new Vector2(600, 6200);

            m_FixedItemRect = new Rectangle[6];

            m_FixedItemRect[0] = new Rectangle((int)m_FixedItemPosition[0].X, (int)m_FixedItemPosition[0].Y, 100, 100);
            m_FixedItemRect[1] = new Rectangle((int)m_FixedItemPosition[1].X, (int)m_FixedItemPosition[1].Y, 100, 100);
            m_FixedItemRect[2] = new Rectangle((int)m_FixedItemPosition[2].X, (int)m_FixedItemPosition[2].Y, 100, 100);
            m_FixedItemRect[3] = new Rectangle((int)m_FixedItemPosition[3].X, (int)m_FixedItemPosition[3].Y, 100, 100);
            m_FixedItemRect[4] = new Rectangle((int)m_FixedItemPosition[4].X, (int)m_FixedItemPosition[4].Y, 100, 100);
            m_FixedItemRect[5] = new Rectangle((int)m_FixedItemPosition[5].X, (int)m_FixedItemPosition[5].Y, 100, 100);
        }
        #region Update_식도


        public override void Update(GameTime gameTime)
        {
            Mony_321카운트다운과중력적용();

            if (m_FrameCount++ < 2)
            {
                //카운트다운 효과음
                m_CountSound.Play();
            }

            if (Gulletupdate == 0)
            {
                for (int a = 0; a < m_FoodNumber; a++)
                {
                    //수정
                    //입의 음식물들을 잘 죽였으면  잔여물들이 식도에 박히고
                    //if (m_FoodList[a].m_IsDead)
                    FixFoodType.Add(m_FoodTypeArr[a]);
                    //죽이지 못했으면 방해물로 박는다.
                    //else
                    ObstacleType.Add(m_FoodTypeArr[a]);
                }
                충돌업데이트();
                Allocate_Fixed_Food();
                Allocate_Fixed_Obstacle();
                Gulletupdate++;
            }


            //키보드 조작 메소드
            if (m_FairyPosition.Y > 768)
            {
                Keyboard_Control();
            }

            //모니 Rectangle
            m_FairyRect = new Rectangle((int)m_FairyPosition.X + 20, (int)m_FairyPosition.Y + 20, 40, 40);

            //음식물 먹었는지 여부를 확인 후, 먹었다면 점수 주고 음식물이 사라지게 한다
            Eat_FoodItem();
            //장애물 좌우
            장애물충돌();
            //방해물 충돌체크
            Is_Obstacle_Check();

            base.Update(gameTime);
        }
        private void RemainMotion()
        {
            if (m_RemainsWaitTime++ > 10)
            {
                m_RemainsWaitTime = 0;
                if (m_RemainsMotionIndex++ >= 2)
                    m_RemainsMotionIndex = 0;
            }
        }
        private void Mony_321카운트다운과중력적용()
        {
            if (m_MonyWaitTime++ > 1)
            {
                m_MonyWaitTime = 0;

                if (m_MonyFrameCounter++ >= 10)
                    m_MonyFrameCounter = 0;
            }

            //4초가 지나면 모니요정이 자동으로 떨어진다 -> 중력 적용
            if (m_FrameCount++ > 470)
            {
                if (m_FairyPosition.Y < 768)
                {
                    m_FairyPosition.X -= m_Speed / 2;
                    m_FairyPosition.Y += m_Speed;
                    count = 4;  //디폴트값
                    Update모니WithTile(Direction.Down);
                }
                else
                {
                    m_FairyPosition.Y += m_Speed;
                    count = 4;  //디폴트값
                    Update모니WithTile(Direction.Down);
                }
            }
            else
            {
                if (m_FrameCount < 110)
                    count = 3;
                else if (m_FrameCount > 110 && m_FrameCount < 230)
                    count = 2;
                else if (m_FrameCount > 230 && m_FrameCount < 350)
                    count = 1;
                else if (m_FrameCount > 350 && m_FrameCount < 470)
                    count = 0;

                m_FairyPosition.Y += 0;
            }
        }


        private void Is_Obstacle_Check()
        {
            for (var i = 0; i < ObstacleNumber; i++)
            {
                //방해물과 닿았을 때
                if (m_FairyRect.Intersects(m_FixedObstacleRect[i]))
                {
                    m_FixedObstaclePosition[i] = Vector2.Zero;
                    m_TimeIndex = 1;
                    m_EffectCount2 = 0;
                    TotalScore -= 5;

                    //부딪히면 생명 깎임  
                    //생명 3개일 때
                    if (m_LifeList[2] == true)
                    {
                        m_LifeList[2] = false;
                    }
                    //생명 2개 혹은 1개일 때
                    if (m_LifeList[2] == false && m_LifeList[1] == true && m_LifeList[0] == true)
                    {
                        m_LifeList[1] = false;
                        ////생명 2개
                        //if (m_LifeList[1] == true)
                        //    m_LifeList[1] = false;
                        ////생명 1개
                        //else
                        //    m_LifeList[0] = false;
                    }
                    else
                    {
                        m_LifeList[0] = false;

                    }
                    Wrong.Play();

                    //방해물과 부딪히면 먹었던 음식물을 다 토해낸다. 머리위에 쌓인 음식물들을 싹다 지운다. -> List초기화
                    //m_FoodStackOnMony.Clear();

                    m_FixedObstacleIsDrawble[i] = true;
                }
            }
        }

        private void Eat_FoodItem()
        {
            for (var i = 0; i < m_FixedFoodRect.Count; i++)
            {
                //만약 fairy_rect가 random_food[i]를 포함한다면
                if (m_FairyRect.Intersects(m_FixedFoodRect[i]))
                {
                    EatFoodType.Add(FixFoodType[i]);
                    //음식물을 먹은 것이므로 음식물을 그리지 않도록 true값을 준다.
                    m_IsStart = true;
                    m_EffectCount1 = 0;
                    m_FixedFoodIsDrawble[i] = true;
                    EatGulletFoodNumber++;
                    TotalScore += 10; 정답.Play();
                    //음식물 먹었을 때 List에 하나씩 넣어준다★어차피 이 좌표는 안쓰니까 아무거나 넣어준다.
                    m_FoodStackOnMony.Add(new Vector2(m_FairyPosition.X, m_FairyPosition.Y));

                    m_Score += 10;


                    //20점 단위로 점점 속도 1씩 증가
                    if (m_Score % 20 == 0)
                    {
                        m_Speed++;
                        m_Bonus++;
                    }

                    //맨 처음 접촉했을 때만 10점 추가하면되므로, Rectangle을 초기화해서 다시는 이것과 Intersect하는 일이 없도록 해준다
                    m_FixedFoodRect[i] = new Rectangle();
                }
            }
        }

        private void Keyboard_Control()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                m_FairyPosition.X -= 6;
                Update모니WithTile(Direction.Left);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                m_FairyPosition.X += 6;
                Update모니WithTile(Direction.Right);
            }
            //if (keyboardState.IsKeyDown(Keys.Down))
            //{
            //    m_FairyPosition.Y += 20;
            //    Update모니WithTile(Direction.Down);
            //}

            if (m_FairyPosition.Y >= 8800)
            {
                //gostomach = 8800
                GoStomach += 3;

                힘들어 = 1;
            }

        }
        #endregion
        #region 식도메소드
        private void Allocate_Fixed_Obstacle()
        {
            m_FixedObstaclePosition = new Vector2[ObstacleNumber];
            m_FixedObstacleRect = new Rectangle[ObstacleNumber];


            Random Tile = new Random();
            for (int a = 0; a < ObstacleNumber; a++)
            {

                Obstaclemovecase[a] = Obstaclemove.Next(-1, 1);
                if (Obstaclemovecase[a] == 0)
                {
                    Obstaclemovecase[a] = 1;
                }
                ObstacleIntex = Tile.Next(100, 24 * 16 * 12);
                if (TileMapNumber[ObstacleIntex] == 7)
                {
                    m_FixedObstaclePosition[a] = (new Vector2((int)TileRect[ObstacleIntex].X, (int)TileRect[ObstacleIntex].Y));
                    m_FixedObstacleRect[a] = (new Rectangle((int)TileRect[ObstacleIntex].X + 20, (int)TileRect[ObstacleIntex].Y + 20, m_FixedObstacleSize - 40, m_FixedObstacleSize - 40));
                    m_FixedObstacleIsDrawble.Add(false);

                }
                else
                    a--;
            }
        }
        private void Allocate_Fixed_Food()
        {
            Random Tile = new Random(DateTime.Now.Millisecond);
            for (int b = 0; b < FoodNumber; b++)
            {
                FoodIndex = Tile.Next(100, 24 * 16 * 12);
                if (TileMapNumber[FoodIndex] == 0 || TileMapNumber[FoodIndex] == 5)
                {
                    m_FixedFoodPosition.Add(new Vector2((int)TileRect[FoodIndex].X, (int)TileRect[FoodIndex].Y));
                    m_FixedFoodRect.Add(new Rectangle((int)TileRect[FoodIndex].X + 20, (int)TileRect[FoodIndex].Y + 20, m_FixedFoodSize - 40, m_FixedFoodSize - 40));
                    m_FixedFoodIsDrawble.Add(false);
                }
                else
                    b--;
            }
        }
        #endregion


        #region Draw_식도
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //10개. 1 * 10
            //260 * 260
            var width = m_EffectSprite.Width / 10;
            var height = m_EffectSprite.Height;

            //GraphicsDevice.Clear(new Color(255, 238, 210));

            var topY = -m_FairyPosition.Y - 150;// +halfScreenHeight;

            //카메라 
            var matrix = Matrix.CreateTranslation(new Vector3(-500, topY, 0)) * Matrix.CreateScale(m_CameraScale) * Matrix.CreateTranslation(500, 350, 0);

            //카메라 사용하기 위한 Begin코드
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, matrix);

            //spriteBatch.Draw(m_GulletColorBackground, Vector2.Zero, Color.White);
            DrawMapAndFood(spriteBatch);
            Draw_Fixed_Food(spriteBatch);
            //상황별에 따라 모니요정 그리기
            Draw_Mony(spriteBatch);

            //하트 그리기
            for (int i = 0; i < m_LifeList.Count; i++)
            {
                //true일때만 그린다
                if (m_LifeList[i] == true)
                    spriteBatch.Draw(m_Life, new Rectangle(50 * (i + 1), (int)m_FairyPosition.Y - 200, 50, 50), Color.White);

            }
            //알파벳 상단바에 그리기      
            for (int i = 0; i < 6; i++)
            {
                if (m_FixedItemIsDrawble[i])
                    spriteBatch.Draw(m_ItemList_On[i], new Rectangle(600 + (i * 100), (int)m_FairyPosition.Y - 200, 130, 130), Color.White);
                else
                    spriteBatch.Draw(m_ItemList_Off[i], new Rectangle(600 + (i * 100), (int)m_FairyPosition.Y - 200, 100, 100), Color.White);

            }


            //임의로 알파벳 아이템 그리기
            Draw_Fixed_Item(spriteBatch);

            //방해물 그리기
            Draw_Fixed_Obstacle(spriteBatch);
            //상태바 위에 점수 그리기
            //spriteBatch.DrawString(m_Font, m_Score.ToString(), new Vector2(55, m_FairyPosition.Y - 230), new Color(73, 56, 47));
            //게임종료후통계();


            if (힘들어 == 0)
                모니위에잔여물따라오게(spriteBatch);

            //추가
            //spriteBatch.DrawString(m_Font, m_FairyPosition.Y + "", new Vector2(m_FairyPosition.X, m_FairyPosition.Y - 100), Color.Yellow);
            //추가
            //모두 수행하면 자동으로 위 넘어가도록.
            //if (m_FairyPosition.Y >= 8800)
            //{
            //    GoStomach += 3;
            //    if (m_TimingCount++ < 180)
            //    {
            //        힘들어 = 1;
            //else if (힘들어 == 1)
            //{
            //    if (m_TimingCount++ < 180)
            //    {
            //        m_FairyPosition.Y = 8800;
            //        spriteBatch.Draw(m_ClearImage, new Rectangle(0, (int)m_FairyPosition.Y - 200, 1126, 768), Color.White);
            //        //은비

            //        RemainMotion();

            //        if (GoStomachTime++ > 10)
            //        {
            //            GoStomachTime = 0;

            //            for (int i = 0; i < m_FoodStackOnMony.Count; i++)
            //            {
            //                switch (EatFoodType[i])
            //                {
            //                    case 0:
            //                        spriteBatch.Draw(m_AppleRemains,
            //                                             new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
            //                                             new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
            //                                             Color.White);
            //                        break;
            //                    case 1:
            //                        spriteBatch.Draw(m_CheeseRemains,
            //                                             new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
            //                                             new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
            //                                             Color.White);
            //                        break;
            //                    case 2:
            //                        spriteBatch.Draw(m_MeatRemains,
            //                                             new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
            //                                             new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
            //                                             Color.White);
            //                        break;
            //                }
            //            }
            //        }
            //    }

            //else
            //{
            //    m_CameraPosition.Y = 9500;
            //}
            //}
            //보너스, 웁스 효과주기
            Effect_BonusAndOops(spriteBatch);

            CountDown(spriteBatch);

            if (m_LifeList[0] == false)
                spriteBatch.Draw(m_GameOver, new Rectangle(0, (int)m_FairyPosition.Y, 1366, 400), Color.White);

            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }
        //int m_TimingCount = 0;
        int m_RemainsMotionIndex;
        int m_RemainsWaitTime;
        int 힘들어 = 0;
        //int GoStomachTime = 0;

        private void 모니위에잔여물따라오게(SpriteBatch spriteBatch)
        {
            //RemainMotion();

            //모니 머리위에 먹은 잔여물들 따라오게 그린다.(List이용)
            for (int i = 0; i < m_FoodStackOnMony.Count; i++)
            {
                switch (EatFoodType[i])
                {
                    case 0:
                        spriteBatch.Draw(m_AppleRemains,
                                             new Rectangle((int)m_FairyPosition.X + 30, (int)m_FairyPosition.Y - (i + 1) * 35, m_FixedFoodSize, m_FixedFoodSize),
                                             new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                             Color.White);
                        break;
                    case 1:
                        spriteBatch.Draw(m_CheeseRemains,
                                             new Rectangle((int)m_FairyPosition.X + 30, (int)m_FairyPosition.Y - (i + 1) * 35, m_FixedFoodSize, m_FixedFoodSize),
                                             new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                             Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(m_MeatRemains,
                                             new Rectangle((int)m_FairyPosition.X + 30, (int)m_FairyPosition.Y - (i + 1) * 35, m_FixedFoodSize, m_FixedFoodSize),
                                             new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                             Color.White);
                        break;
                }
            }
        }
        int m_EffectCount1 = 0;
        int m_EffectCount2 = 0;

        private void Effect_BonusAndOops(SpriteBatch spriteBatch)
        {
            //30점 단위씩 늘어날 때마다 보너스!표시 
            if (m_Score % 30 == 0 && m_Score != 0)
            {
                if (m_EffectCount1++ < 90)
                {
                    spriteBatch.Draw(m_BonusImage, new Rectangle((int)m_FairyPosition.X - 50, (int)m_FairyPosition.Y - 120, m_BonusImage.Width - 80, m_BonusImage.Height - 30), Color.White);
                    //spriteBatch.DrawString(m_Font, "+10", new Vector2(m_FairyPosition.X - 10, m_FairyPosition.Y - 30), Color.White);
                    //spriteBatch.DrawString(m_Font, "Bonus!", new Vector2(m_FairyPosition.X - 30, m_FairyPosition.Y - 65), Color.White);
                }
            }
            //방해물 먹었을 때마다 마이너스 점수 표시
            if (m_Score == 0 && m_IsStart == true)
            {
                if (m_EffectCount2++ < 90)
                {
                    spriteBatch.DrawString(m_Font, "-5", new Vector2(m_FairyPosition.X - 10, m_FairyPosition.Y - 30), Color.Black);
                    spriteBatch.DrawString(m_Font, "Oops!", new Vector2(m_FairyPosition.X - 30, m_FairyPosition.Y - 65), Color.Black);
                }
                spriteBatch.Draw(m_EffectSprite, new Vector2(m_FairyPosition.X, m_FairyPosition.Y), new Rectangle(260 * m_MonyFrameCounter, 100, 100, 100), Color.White);
            }
        }
        int m_MonyFrontFrameCounter = 0;
        int m_MonyFrontWaitTime = 0;

        //모니 그리기
        private void Draw_Mony(SpriteBatch spriteBatch)
        {
            MonyFrontMotion();

            if (m_TimeIndex == 0)
                spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_FairyPosition.X, (int)m_FairyPosition.Y, 90, 90), new Rectangle(200 * m_MonyFrontFrameCounter, 0, 200, 200), Color.White);
            else if (m_TimeIndex == 1)
            {
                //m_Speed = 0;
                spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_FairyPosition.X, (int)m_FairyPosition.Y, 90, 90), new Rectangle(200 * m_MonyFrontFrameCounter, 0, 200, 200), Color.Red);

                if (m_CollideNumber++ > 2)
                {
                    m_TimeIndex = 0;
                    m_CollideNumber = 0;
                    //m_Speed =1;
                }
            }
        }

        private void MonyFrontMotion()
        {
            if (m_MonyFrontWaitTime++ > 6)
            {
                m_MonyFrontWaitTime = 0;

                if (m_MonyFrontFrameCounter++ > 1)
                    m_MonyFrontFrameCounter = 0;
            }
        }


        private void CountDown(SpriteBatch spriteBatch)
        {
            switch (count)
            {
                case 3:
                    spriteBatch.Draw(m_CountImage3, new Rectangle(200, 0, m_CountImage3.Width, m_CountImage3.Height), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(m_CountImage2, new Rectangle(200, 0, m_CountImage2.Width, m_CountImage2.Height), Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(m_CountImage1, new Rectangle(200, 0, m_CountImage1.Width, m_CountImage1.Height), Color.White);
                    break;
                case 0:
                    spriteBatch.Draw(m_CountImageGO, new Rectangle(180, 100, m_CountImageGO.Width, m_CountImageGO.Height), Color.White);
                    break;
                default:
                    break;
            }
        }

        private void DrawMapAndFood(SpriteBatch spriteBatch)
        {
            //입
            spriteBatch.Draw(m_기본_입, new Rectangle(0, -768, 1366, 768), Color.White);

            //식도맵 그리기
            for (int a = 0; a < 11; a++)
                spriteBatch.Draw(m_ThroatMap[a], new Rectangle(0, 768 * a, 1366, 768), Color.White);
            spriteBatch.Draw(m_ThroatMap[10], new Vector2(0, 768 * 11), new Rectangle(0, 0, 1366, 768), Color.White, 0.0f, new Vector2(0, 0), 0.0f, SpriteEffects.FlipHorizontally, 0);
            //for (int a = 10; a >= 0; a--)
            //    spriteBatch.Draw(m_ThroatMap[a], new Vector2(0, 768 * a), new Rectangle(0, 0, 1366, 768), Color.White, 0.0f, new Vector2(0, 0), 0.0f, SpriteEffects.FlipHorizontally, 0);
            //spriteBatch.Draw(m_RobinImage, new Vector2(m_RobinPosition.X, m_RobinPosition.Y + extraY), new Rectangle(column * width, row * height, width, height), Color.White, 0.0f, new Vector2(width / 2, height / 2), 0.5f, SpriteEffects.FlipHorizontally, 0);
            //위
            //spriteBatch.Draw(m_기본_위, new Rectangle(0, 768 * 12, 1366, 768), Color.White);

        }
        private void Draw_Fixed_Item(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 6; i++)
            {
                //방해물과 닿았을 때
                if (m_FairyRect.Intersects(m_FixedItemRect[i]))
                {
                    m_FixedItemPosition[i] = Vector2.Zero;
                    m_FixedItemIsDrawble[i] = true;
                }


                ////아이템이 닿지 않았을 때에 아이템을 Draw.
                if (m_FixedItemIsDrawble[i] == false)
                {
                    spriteBatch.Draw(m_ItemList_On[i], new Rectangle((int)m_FixedItemPosition[i].X, (int)m_FixedItemPosition[i].Y, 100, 100), Color.White);
                }

            }
        }
        private void Draw_Fixed_Obstacle(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < ObstacleNumber; i++)
            {
                //장애물에 부딪히지 않았을 때에는 장애물을 Draw.
                if (m_FixedObstacleIsDrawble.Count == ObstacleNumber && m_FixedObstacleIsDrawble[i] == false)
                {
                    switch (ObstacleType[i])
                    {
                        case 0:
                            spriteBatch.Draw(m_AppleAttack, new Rectangle((int)m_FixedObstaclePosition[i].X, (int)m_FixedObstaclePosition[i].Y, m_FixedObstacleSize, m_FixedObstacleSize), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(m_CheeseAttack, new Rectangle((int)m_FixedObstaclePosition[i].X, (int)m_FixedObstaclePosition[i].Y, m_FixedObstacleSize, m_FixedObstacleSize), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(m_MeatAttack, new Rectangle((int)m_FixedObstaclePosition[i].X, (int)m_FixedObstaclePosition[i].Y, m_FixedObstacleSize, m_FixedObstacleSize), Color.White);
                            break;
                    }
                }
            }
        }

        private void Draw_Fixed_Food(SpriteBatch spriteBatch)
        {

            var leftX = -m_FairyPosition.X;// +halfScreenWidth;
            var topY = -m_FairyPosition.Y;// +halfScreenHeight;

            for (var i = 0; i < m_FixedFoodPosition.Count; i++)
            {
                //m_IsFoodRemove = false 이면 아이템을 먹지 않은 것. -> Draw한다.
                if (m_FixedFoodIsDrawble[i] == false)
                {
                    switch (FixFoodType[i])
                    {
                        case 0:
                            spriteBatch.Draw(m_AppleRemains,
                                                 new Rectangle((int)m_FixedFoodPosition[i].X, (int)m_FixedFoodPosition[i].Y, m_FixedFoodSize, m_FixedFoodSize),
                                                 new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                 Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(m_CheeseRemains,
                                                 new Rectangle((int)m_FixedFoodPosition[i].X, (int)m_FixedFoodPosition[i].Y, m_FixedFoodSize, m_FixedFoodSize),
                                                 new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                 Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(m_MeatRemains,
                                                 new Rectangle((int)m_FixedFoodPosition[i].X, (int)m_FixedFoodPosition[i].Y, m_FixedFoodSize, m_FixedFoodSize),
                                                 new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                 Color.White);
                            break;
                    }
                }
            }
        }

        #endregion
        #region 식도충돌

        int[] TileMapNumber = {//첫번째 줄
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//0
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,//47
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,//94
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,//443
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,//494
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,1,//239
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,2,1,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,2,2,1,//287
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,2,2,1,1,
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,2,2,1,1,1,//334
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,2,2,1,1,1,1,
                               //두번째 줄
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,1,1,1,1,//4
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,1,1,1,
                               1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1,1,1,1,//47
                               1,1,1,4,4,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4,4,1,1,1,1,1,//90
                               1,1,1,4,4,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,4,4,4,4,0,0,0,0,0,0,0,0,0,0,4,4,1,1,1,1,1,//143
                               1,1,1,2,4,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,//191
                               1,1,1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,2,4,4,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,//239
                               1,1,1,1,2,2,4,0,0,0,0,0,0,0,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,1,2,2,0,0,0,0,0,0,0,0,0,0,4,2,1,1,1,1,1,//287
                               1,1,1,1,1,1,2,2,0,0,0,0,0,7,0,0,0,4,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,2,2,0,0,0,0,0,0,0,0,4,1,1,1,1,1,1,//335
                               1,1,1,1,1,1,1,1,2,2,0,0,0,7,0,0,0,4,1,1,1,1,1,1,
                               //3번째 줄
                               1,1,1,1,1,1,1,1,1,1,0,0,0,7,0,0,0,2,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,2,0,0,7,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,0,0,7,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,0,0,7,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,0,0,0,0,7,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,0,0,0,0,0,7,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,0,0,0,0,0,0,7,0,0,0,4,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,0,0,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,1,1,1,2,2,0,0,0,0,7,0,0,0,4,4,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,2,2,0,0,7,5,5,5,5,5,5,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,2,2,2,2,5,5,5,5,5,5,1,1,1,1,1,
                               //4번째 줄
                               1,1,1,1,1,1,1,1,1,1,1,1,1,2,5,5,5,5,5,1,1,1,1,1,//5
                               1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,5,5,5,2,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,//47
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,1,1,1,1,1,1,//95
                               1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,//543
                               1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,5,2,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,5,5,1,1,1,1,1,1,1,//090
                               1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,//239
                               1,1,1,1,1,1,1,1,1,0,0,0,7,0,0,0,0,4,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,4,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,//287
                               1,1,1,1,1,1,1,4,4,0,0,0,7,0,0,0,0,0,0,0,1,1,1,1,
                               1,1,1,1,1,1,4,4,4,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,//335
                               1,1,1,1,1,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,1,1,1,1,
                               //5번째 줄
                               1,1,1,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,//0
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,//47
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,
                               1,1,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,//95
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,//143
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,
                               1,1,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,//191
                               1,1,4,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,
                               1,1,1,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,//239
                               1,1,1,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,2,1,1,1,
                               1,1,1,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,1,1,1,1,//287
                               1,1,1,4,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,0,1,1,1,1,
                               1,1,1,2,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,2,1,1,1,1,//335
                               1,1,1,1,4,4,4,4,4,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,
                               //6번째 줄
                               1,1,1,1,4,4,4,4,4,0,0,0,7,0,0,0,0,0,2,1,1,1,1,1,//0
                               1,1,1,1,2,2,4,4,4,0,0,0,7,0,0,0,0,2,2,1,1,1,1,1,
                               1,1,1,1,1,2,2,4,4,0,0,0,7,0,0,0,2,2,1,1,1,1,1,1,//47
                               1,1,1,1,1,1,1,2,4,0,0,0,0,0,0,2,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,2,2,0,0,0,0,0,2,2,1,1,1,1,1,1,1,1,//95
                               1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//043
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//090
                               1,1,1,1,1,1,1,1,1,2,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//239
                               1,1,1,1,1,1,1,1,1,1,5,5,5,2,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//287
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//335
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,
                               //7번째 줄
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//0
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//47
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//95
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,1,1,1,1,1,1,1,1,1,1,1,//043
                               1,1,1,1,1,1,1,1,1,1,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,1,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//090
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,//239
                               1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,//287
                               1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,//335
                               1,1,1,1,1,1,1,1,0,0,0,7,0,0,0,0,0,1,1,1,1,1,1,1,
                               //8번째 줄
                               1,1,1,1,0,1,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,//0
                               1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
                               1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,//47
                               1,1,1,1,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,1,//95
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//043
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//090
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//239
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//287
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//335
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               //9번째 줄
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,//0
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,4,1,1,1,
                               1,1,4,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,2,1,1,1,//47
                               1,1,2,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,1,1,1,1,
                               1,1,1,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,1,1,1,1,//95
                               1,1,1,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,1,1,1,1,
                               1,1,1,4,4,0,0,0,0,0,0,7,0,0,0,0,0,0,4,4,1,1,1,1,//043
                               1,1,1,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,1,1,1,
                               1,1,1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,2,2,1,1,1,1,1,//090
                               1,1,1,1,1,2,2,0,0,0,0,7,0,0,0,0,2,2,1,1,1,1,1,1,
                               1,1,1,1,1,1,2,2,0,0,0,7,0,0,0,0,1,1,1,1,1,1,1,1,//239
                               1,1,1,1,1,1,1,1,5,5,5,5,5,5,5,2,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,5,5,5,5,5,5,2,2,1,1,1,1,1,1,1,1,//287
                               1,1,1,1,1,1,1,1,5,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,2,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//335
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               //10번째 줄
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//0
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//47
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,1,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//95
                               1,1,1,1,1,1,1,1,5,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,1,1,5,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,//043
                               1,1,1,1,1,1,1,0,0,0,0,7,0,0,0,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,1,0,0,0,0,0,7,0,0,0,1,1,1,1,1,1,1,1,1,//090
                               1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,0,0,0,0,0,0,0,7,0,0,0,0,1,1,1,1,1,1,1,1,//239
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,1,1,1,1,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,0,7,0,0,0,0,0,1,1,1,1,1,1,1,//287
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,//335
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,
                               //11번째 줄
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,//0
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,//47
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,1,1,1,1,1,1,
                               1,1,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,2,1,1,1,1,1,1,//95
                               1,1,2,0,0,0,0,0,0,0,0,7,0,0,0,2,2,2,1,1,1,1,1,1,
                               1,1,2,2,0,0,0,0,0,0,0,7,0,0,2,2,1,1,1,1,1,1,1,1,//043
                               1,1,1,1,0,0,0,0,0,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,0,0,0,7,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1,1,//090
                               1,1,1,1,0,0,0,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,2,2,1,1,1,1,1,1,1,1,1,1,1,1,//239
                               1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//287
                               1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//335
                               1,1,1,2,0,0,0,0,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               //12번째 줄 
                               1,1,1,1,4,4,4,4,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//4
                               1,1,1,1,4,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,4,4,4,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//47
                               1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//94
                               1,1,1,1,2,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//443
                               1,1,1,1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//494
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//239
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//287
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                               1,1,1,1,1,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,//334
                               1,1,1,1,4,4,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                              };

        enum Direction { Left, Right, Up, Down };

        const int TileMax_X = 24;
        const int TileMax_Y = 16;
        Rectangle[] TileRect = new Rectangle[24 * 16 * 12];
        Random Obstaclemove = new Random();
        int[] Obstaclemovecase = new int[20];

        private void 장애물충돌()
        {
            for (int a = 0; a < ObstacleNumber; a++)
            {
                m_FixedObstaclePosition[a].X += gravity / 2 * Obstaclemovecase[a];
                m_FixedObstacleRect[a] = new Rectangle((int)m_FixedObstaclePosition[a].X + 20, (int)m_FixedObstaclePosition[a].Y + 20, m_FixedObstacleSize - 40, m_FixedObstacleSize - 40);
                for (var TileIndex = 0; TileIndex < TileMapNumber.Length; TileIndex++)
                {
                    if (TileRect[TileIndex].Intersects(m_FixedObstacleRect[a]))
                    {
                        if (TileMapNumber[TileIndex] == 1 || TileMapNumber[TileIndex] == 2)
                        {
                            Obstaclemovecase[a] = Obstaclemovecase[a] * -1;
                            m_FixedObstaclePosition[a].X += gravity / 2 * Obstaclemovecase[a];
                            m_FixedObstacleRect[a] = new Rectangle((int)m_FixedObstaclePosition[a].X + 20, (int)m_FixedObstaclePosition[a].Y + 20, m_FixedObstacleSize - 40, m_FixedObstacleSize - 40);

                        }
                    }
                }
            }
        }

        private void 충돌업데이트()
        {
            for (var i = 0; i < TileMapNumber.Length; i++)
            {
                TileRect[i] = new Rectangle((i % TileMax_X) * 48, (i / TileMax_X) * 48, 48, 48);
            }
        }

        void Update모니WithTile(Direction direction)
        {
            if (m_FairyPosition.X < 0)
                m_FairyPosition.X = 0;
            if (m_FairyPosition.Y < 0)
                m_FairyPosition.Y = 0;

            for (var TileIndex = 0; TileIndex < TileMapNumber.Length; TileIndex++)
            {
                if (TileRect[TileIndex].Intersects(m_FairyRect))
                {
                    if (TileMapNumber[TileIndex] == 1)
                    {
                        if (direction == Direction.Left)
                        {
                            m_FairyPosition.X += 30;
                            m_TimeIndex = 1;
                            TotalScore -= 5; Wrong.Play();
                        }
                        else if (direction == Direction.Right)
                        {
                            m_FairyPosition.X -= 30;
                            m_TimeIndex = 1;
                            TotalScore -= 5; Wrong.Play();
                        }
                    }
                    if (TileMapNumber[TileIndex] == 2)
                    {
                        if (direction == Direction.Down)
                        {
                            m_FairyPosition.Y -= 30;
                            m_TimeIndex = 1;
                            TotalScore -= 5; Wrong.Play();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
