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
        Texture2D m_�⺻_��;
        SoundEffectInstance ����;
        SoundEffect ����_Sound;

        Texture2D m_AppleAttack;
        Texture2D m_CheeseAttack;
        Texture2D m_MeatAttack;
        Texture2D m_AppleRemains;
        Texture2D m_CheeseRemains;
        Texture2D m_MeatRemains;
        Texture2D m_ClearImage;
        int GoStomach = 8800;

        #region ��������_�ĵ�
        //���ĺ� ������
        Texture2D[] m_ItemList_On = new Texture2D[6];
        Texture2D[] m_ItemList_Off = new Texture2D[6];
        Texture2D m_GameOver;

        Texture2D m_�⺻_��;
        const float gravity = 5;
        int Gulletupdate = 0;
        List<int> FixFoodType = new List<int>();
        List<int> ObstacleType = new List<int>();
        List<int> EatFoodType = new List<int>();

        int[] m_FoodTypeArr;

        SpriteFont m_Font;
        SpriteFont m_Font2;
        SpriteFont m_Font3;

        //ī��Ʈ�ٿ� ȿ���� 
        SoundEffect m_CountSound;
        Texture2D m_CountImage1;
        Texture2D m_CountImage2;
        Texture2D m_CountImage3;
        Texture2D m_CountImageGO;

        Texture2D m_GulletColorBackground;
        Texture2D[] m_ThroatMap = new Texture2D[12];

        //���ʽ�
        Texture2D m_BonusImage;

        //����
        Texture2D m_FairyImage;
        Vector2 m_FairyPosition = new Vector2(950, 200);
        Rectangle m_FairyRect;

        //��Ʈ ����
        Texture2D m_Life;


        //���ع�
        Texture2D m_Obstacle;

        //���ع� �ε����� �� ȿ��
        Texture2D m_EffectSprite;

        // ���� �ܿ��� �����۵��� ������ġ�Ͽ� ��Ƶδ� List (�ʺ��� ����)
        List<Vector2> m_FixedFoodPosition = new List<Vector2>();
        List<Rectangle> m_FixedFoodRect = new List<Rectangle>();
        List<bool> m_FixedFoodIsDrawble = new List<bool>();

        Vector2[] m_FixedObstaclePosition;
        Rectangle[] m_FixedObstacleRect;
        List<bool> m_FixedObstacleIsDrawble = new List<bool>();


        //���ĺ� ������ 
        List<bool> m_FixedItemIsDrawble = new List<bool>();
        Rectangle[] m_FixedItemRect;
        Vector2[] m_FixedItemPosition;

        //��Ͽ����� ���Ĺ� ������ �Ӹ� ���� ������� �� ����List
        List<Vector2> m_FoodStackOnMony = new List<Vector2>();

        //��Ͽ����� ��� ��Ʈ List
        List<bool> m_LifeList = new List<bool>();

        //countdown�Ҷ� ���� ���� ����
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
        #region �ĵ��� �Ѱ��� ����


        //���Ĺ��� ����
        int m_FoodNumber = 15;
        //���� ���Ĺ��� ����
        int[] m_DeadNumber;
        //int m_ObstacleNumber;
        //int m_�������Ĺ��ǰ���;

        #endregion
        public override void Init(ContentManager Content)
        {
            Wrong_Sound = Content.Load<SoundEffect>("�뷡/wrong");
            Wrong = Wrong_Sound.CreateInstance();
            Wrong.IsLooped = false;
            m_�⺻_�� = Content.Load<Texture2D>("�⺻��/�⺻��");

            ����_Sound = Content.Load<SoundEffect>("�뷡/����");
            ���� = ����_Sound.CreateInstance();
            ����.IsLooped = false;

            m_ClearImage = Content.Load<Texture2D>("CLEAR");

            #region �ĵ�_Load

            m_GameOver = Content.Load<Texture2D>("�ĵ�/gameover");
            m_ItemList_On[0] = Content.Load<Texture2D>("�ĵ�/G_on");
            m_ItemList_On[1] = Content.Load<Texture2D>("�ĵ�/U_on");
            m_ItemList_On[2] = Content.Load<Texture2D>("�ĵ�/L_on(1)");
            m_ItemList_On[3] = Content.Load<Texture2D>("�ĵ�/L_on(2)");
            m_ItemList_On[4] = Content.Load<Texture2D>("�ĵ�/E_on");
            m_ItemList_On[5] = Content.Load<Texture2D>("�ĵ�/T_on");

            m_ItemList_Off[0] = Content.Load<Texture2D>("�ĵ�/G_off");
            m_ItemList_Off[1] = Content.Load<Texture2D>("�ĵ�/U_off");
            m_ItemList_Off[2] = Content.Load<Texture2D>("�ĵ�/L_off");
            m_ItemList_Off[3] = Content.Load<Texture2D>("�ĵ�/L_off");
            m_ItemList_Off[4] = Content.Load<Texture2D>("�ĵ�/E_off");
            m_ItemList_Off[5] = Content.Load<Texture2D>("�ĵ�/T_off");

            m_CountSound = Content.Load<SoundEffect>("�ĵ�/countdown");
            m_�⺻_�� = Content.Load<Texture2D>("�⺻��/�⺻��");
            m_GulletColorBackground = Content.Load<Texture2D>("GulletColorBackground");

            m_AppleAttack = Content.Load<Texture2D>("FoodAttack");
            m_CheeseAttack = Content.Load<Texture2D>("CheeseAttack");
            m_MeatAttack = Content.Load<Texture2D>("MeatAttack");

            m_AppleRemains = Content.Load<Texture2D>("appleremain");
            m_CheeseRemains = Content.Load<Texture2D>("cheeseremain");
            m_MeatRemains = Content.Load<Texture2D>("meatremain");

            m_CountImage1 = Content.Load<Texture2D>("�ĵ�/count1");
            m_CountImage2 = Content.Load<Texture2D>("�ĵ�/count2");
            m_CountImage3 = Content.Load<Texture2D>("�ĵ�/count3");
            m_CountImageGO = Content.Load<Texture2D>("�ĵ�/GO");

            m_BonusImage = Content.Load<Texture2D>("BONUS");
            m_ThroatMap[0] = Content.Load<Texture2D>("�⺻��/�ĵ�1");
            m_ThroatMap[1] = Content.Load<Texture2D>("�⺻��/�ĵ�2");
            m_ThroatMap[2] = Content.Load<Texture2D>("�⺻��/�ĵ�3");
            m_ThroatMap[3] = Content.Load<Texture2D>("�⺻��/�ĵ�4");
            m_ThroatMap[4] = Content.Load<Texture2D>("�⺻��/�ĵ�5");
            m_ThroatMap[5] = Content.Load<Texture2D>("�⺻��/�ĵ�6");
            m_ThroatMap[6] = Content.Load<Texture2D>("�⺻��/�ĵ�7");
            m_ThroatMap[7] = Content.Load<Texture2D>("�⺻��/�ĵ�8");
            m_ThroatMap[8] = Content.Load<Texture2D>("�⺻��/�ĵ�9");
            m_ThroatMap[9] = Content.Load<Texture2D>("�⺻��/�ĵ�10");
            m_ThroatMap[10] = Content.Load<Texture2D>("�⺻��/�ĵ�11");
            m_ThroatMap[11] = Content.Load<Texture2D>("�⺻��/�ĵ�12");

            m_FairyImage = Content.Load<Texture2D>("��Ͼս�������Ʈ");


            //����
            m_DeadNumber = new int[m_FoodNumber];
            m_FoodTypeArr = new int[m_FoodNumber];


            //��Ʈ ����
            m_Life = Content.Load<Texture2D>("�ĵ�/heart");

            //��� ���� ��Ʈ �Ҿ�ֱ�
            //false�̸� ���� �ϳ� �پ���.
            for (int i = 0; i < 3; i++)
            {
                m_LifeList.Add(true);
            }

            m_Font = Content.Load<SpriteFont>("�ĵ�/myFont");
            m_Font2 = Content.Load<SpriteFont>("�ĵ�/myFont2");
            m_Font3 = Content.Load<SpriteFont>("�ĵ�/myFont3");
            m_Obstacle = Content.Load<Texture2D>("FoodAttack");
            m_EffectSprite = Content.Load<Texture2D>("�ĵ�/effect");
            //var music = Content.Load<Song>("�ĵ�/bgm");

            //MediaPlayer.Play(music);
            //MediaPlayer.IsRepeating = true;



            //�浹������Ʈ();
            //Allocate_Fixed_Obstacle();
            //Allocate_Fixed_Food();

            //���ĺ� ������ 
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
        #region Update_�ĵ�


        public override void Update(GameTime gameTime)
        {
            Mony_321ī��Ʈ�ٿ���߷�����();

            if (m_FrameCount++ < 2)
            {
                //ī��Ʈ�ٿ� ȿ����
                m_CountSound.Play();
            }

            if (Gulletupdate == 0)
            {
                for (int a = 0; a < m_FoodNumber; a++)
                {
                    //����
                    //���� ���Ĺ����� �� �׿�����  �ܿ������� �ĵ��� ������
                    //if (m_FoodList[a].m_IsDead)
                    FixFoodType.Add(m_FoodTypeArr[a]);
                    //������ �������� ���ع��� �ڴ´�.
                    //else
                    ObstacleType.Add(m_FoodTypeArr[a]);
                }
                �浹������Ʈ();
                Allocate_Fixed_Food();
                Allocate_Fixed_Obstacle();
                Gulletupdate++;
            }


            //Ű���� ���� �޼ҵ�
            if (m_FairyPosition.Y > 768)
            {
                Keyboard_Control();
            }

            //��� Rectangle
            m_FairyRect = new Rectangle((int)m_FairyPosition.X + 20, (int)m_FairyPosition.Y + 20, 40, 40);

            //���Ĺ� �Ծ����� ���θ� Ȯ�� ��, �Ծ��ٸ� ���� �ְ� ���Ĺ��� ������� �Ѵ�
            Eat_FoodItem();
            //��ֹ� �¿�
            ��ֹ��浹();
            //���ع� �浹üũ
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
        private void Mony_321ī��Ʈ�ٿ���߷�����()
        {
            if (m_MonyWaitTime++ > 1)
            {
                m_MonyWaitTime = 0;

                if (m_MonyFrameCounter++ >= 10)
                    m_MonyFrameCounter = 0;
            }

            //4�ʰ� ������ ��Ͽ����� �ڵ����� �������� -> �߷� ����
            if (m_FrameCount++ > 470)
            {
                if (m_FairyPosition.Y < 768)
                {
                    m_FairyPosition.X -= m_Speed / 2;
                    m_FairyPosition.Y += m_Speed;
                    count = 4;  //����Ʈ��
                    Update���WithTile(Direction.Down);
                }
                else
                {
                    m_FairyPosition.Y += m_Speed;
                    count = 4;  //����Ʈ��
                    Update���WithTile(Direction.Down);
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
                //���ع��� ����� ��
                if (m_FairyRect.Intersects(m_FixedObstacleRect[i]))
                {
                    m_FixedObstaclePosition[i] = Vector2.Zero;
                    m_TimeIndex = 1;
                    m_EffectCount2 = 0;
                    TotalScore -= 5;

                    //�ε����� ���� ����  
                    //���� 3���� ��
                    if (m_LifeList[2] == true)
                    {
                        m_LifeList[2] = false;
                    }
                    //���� 2�� Ȥ�� 1���� ��
                    if (m_LifeList[2] == false && m_LifeList[1] == true && m_LifeList[0] == true)
                    {
                        m_LifeList[1] = false;
                        ////���� 2��
                        //if (m_LifeList[1] == true)
                        //    m_LifeList[1] = false;
                        ////���� 1��
                        //else
                        //    m_LifeList[0] = false;
                    }
                    else
                    {
                        m_LifeList[0] = false;

                    }
                    Wrong.Play();

                    //���ع��� �ε����� �Ծ��� ���Ĺ��� �� ���س���. �Ӹ����� ���� ���Ĺ����� �ϴ� �����. -> List�ʱ�ȭ
                    //m_FoodStackOnMony.Clear();

                    m_FixedObstacleIsDrawble[i] = true;
                }
            }
        }

        private void Eat_FoodItem()
        {
            for (var i = 0; i < m_FixedFoodRect.Count; i++)
            {
                //���� fairy_rect�� random_food[i]�� �����Ѵٸ�
                if (m_FairyRect.Intersects(m_FixedFoodRect[i]))
                {
                    EatFoodType.Add(FixFoodType[i]);
                    //���Ĺ��� ���� ���̹Ƿ� ���Ĺ��� �׸��� �ʵ��� true���� �ش�.
                    m_IsStart = true;
                    m_EffectCount1 = 0;
                    m_FixedFoodIsDrawble[i] = true;
                    EatGulletFoodNumber++;
                    TotalScore += 10; ����.Play();
                    //���Ĺ� �Ծ��� �� List�� �ϳ��� �־��ش١ھ����� �� ��ǥ�� �Ⱦ��ϱ� �ƹ��ų� �־��ش�.
                    m_FoodStackOnMony.Add(new Vector2(m_FairyPosition.X, m_FairyPosition.Y));

                    m_Score += 10;


                    //20�� ������ ���� �ӵ� 1�� ����
                    if (m_Score % 20 == 0)
                    {
                        m_Speed++;
                        m_Bonus++;
                    }

                    //�� ó�� �������� ���� 10�� �߰��ϸ�ǹǷ�, Rectangle�� �ʱ�ȭ�ؼ� �ٽô� �̰Ͱ� Intersect�ϴ� ���� ������ ���ش�
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
                Update���WithTile(Direction.Left);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                m_FairyPosition.X += 6;
                Update���WithTile(Direction.Right);
            }
            //if (keyboardState.IsKeyDown(Keys.Down))
            //{
            //    m_FairyPosition.Y += 20;
            //    Update���WithTile(Direction.Down);
            //}

            if (m_FairyPosition.Y >= 8800)
            {
                //gostomach = 8800
                GoStomach += 3;

                ����� = 1;
            }

        }
        #endregion
        #region �ĵ��޼ҵ�
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


        #region Draw_�ĵ�
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //10��. 1 * 10
            //260 * 260
            var width = m_EffectSprite.Width / 10;
            var height = m_EffectSprite.Height;

            //GraphicsDevice.Clear(new Color(255, 238, 210));

            var topY = -m_FairyPosition.Y - 150;// +halfScreenHeight;

            //ī�޶� 
            var matrix = Matrix.CreateTranslation(new Vector3(-500, topY, 0)) * Matrix.CreateScale(m_CameraScale) * Matrix.CreateTranslation(500, 350, 0);

            //ī�޶� ����ϱ� ���� Begin�ڵ�
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, matrix);

            //spriteBatch.Draw(m_GulletColorBackground, Vector2.Zero, Color.White);
            DrawMapAndFood(spriteBatch);
            Draw_Fixed_Food(spriteBatch);
            //��Ȳ���� ���� ��Ͽ��� �׸���
            Draw_Mony(spriteBatch);

            //��Ʈ �׸���
            for (int i = 0; i < m_LifeList.Count; i++)
            {
                //true�϶��� �׸���
                if (m_LifeList[i] == true)
                    spriteBatch.Draw(m_Life, new Rectangle(50 * (i + 1), (int)m_FairyPosition.Y - 200, 50, 50), Color.White);

            }
            //���ĺ� ��ܹٿ� �׸���      
            for (int i = 0; i < 6; i++)
            {
                if (m_FixedItemIsDrawble[i])
                    spriteBatch.Draw(m_ItemList_On[i], new Rectangle(600 + (i * 100), (int)m_FairyPosition.Y - 200, 130, 130), Color.White);
                else
                    spriteBatch.Draw(m_ItemList_Off[i], new Rectangle(600 + (i * 100), (int)m_FairyPosition.Y - 200, 100, 100), Color.White);

            }


            //���Ƿ� ���ĺ� ������ �׸���
            Draw_Fixed_Item(spriteBatch);

            //���ع� �׸���
            Draw_Fixed_Obstacle(spriteBatch);
            //���¹� ���� ���� �׸���
            //spriteBatch.DrawString(m_Font, m_Score.ToString(), new Vector2(55, m_FairyPosition.Y - 230), new Color(73, 56, 47));
            //�������������();


            if (����� == 0)
                ��������ܿ����������(spriteBatch);

            //�߰�
            //spriteBatch.DrawString(m_Font, m_FairyPosition.Y + "", new Vector2(m_FairyPosition.X, m_FairyPosition.Y - 100), Color.Yellow);
            //�߰�
            //��� �����ϸ� �ڵ����� �� �Ѿ����.
            //if (m_FairyPosition.Y >= 8800)
            //{
            //    GoStomach += 3;
            //    if (m_TimingCount++ < 180)
            //    {
            //        ����� = 1;
            //else if (����� == 1)
            //{
            //    if (m_TimingCount++ < 180)
            //    {
            //        m_FairyPosition.Y = 8800;
            //        spriteBatch.Draw(m_ClearImage, new Rectangle(0, (int)m_FairyPosition.Y - 200, 1126, 768), Color.White);
            //        //����

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
            //���ʽ�, �� ȿ���ֱ�
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
        int ����� = 0;
        //int GoStomachTime = 0;

        private void ��������ܿ����������(SpriteBatch spriteBatch)
        {
            //RemainMotion();

            //��� �Ӹ����� ���� �ܿ����� ������� �׸���.(List�̿�)
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
            //30�� ������ �þ ������ ���ʽ�!ǥ�� 
            if (m_Score % 30 == 0 && m_Score != 0)
            {
                if (m_EffectCount1++ < 90)
                {
                    spriteBatch.Draw(m_BonusImage, new Rectangle((int)m_FairyPosition.X - 50, (int)m_FairyPosition.Y - 120, m_BonusImage.Width - 80, m_BonusImage.Height - 30), Color.White);
                    //spriteBatch.DrawString(m_Font, "+10", new Vector2(m_FairyPosition.X - 10, m_FairyPosition.Y - 30), Color.White);
                    //spriteBatch.DrawString(m_Font, "Bonus!", new Vector2(m_FairyPosition.X - 30, m_FairyPosition.Y - 65), Color.White);
                }
            }
            //���ع� �Ծ��� ������ ���̳ʽ� ���� ǥ��
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

        //��� �׸���
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
            //��
            spriteBatch.Draw(m_�⺻_��, new Rectangle(0, -768, 1366, 768), Color.White);

            //�ĵ��� �׸���
            for (int a = 0; a < 11; a++)
                spriteBatch.Draw(m_ThroatMap[a], new Rectangle(0, 768 * a, 1366, 768), Color.White);
            spriteBatch.Draw(m_ThroatMap[10], new Vector2(0, 768 * 11), new Rectangle(0, 0, 1366, 768), Color.White, 0.0f, new Vector2(0, 0), 0.0f, SpriteEffects.FlipHorizontally, 0);
            //for (int a = 10; a >= 0; a--)
            //    spriteBatch.Draw(m_ThroatMap[a], new Vector2(0, 768 * a), new Rectangle(0, 0, 1366, 768), Color.White, 0.0f, new Vector2(0, 0), 0.0f, SpriteEffects.FlipHorizontally, 0);
            //spriteBatch.Draw(m_RobinImage, new Vector2(m_RobinPosition.X, m_RobinPosition.Y + extraY), new Rectangle(column * width, row * height, width, height), Color.White, 0.0f, new Vector2(width / 2, height / 2), 0.5f, SpriteEffects.FlipHorizontally, 0);
            //��
            //spriteBatch.Draw(m_�⺻_��, new Rectangle(0, 768 * 12, 1366, 768), Color.White);

        }
        private void Draw_Fixed_Item(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 6; i++)
            {
                //���ع��� ����� ��
                if (m_FairyRect.Intersects(m_FixedItemRect[i]))
                {
                    m_FixedItemPosition[i] = Vector2.Zero;
                    m_FixedItemIsDrawble[i] = true;
                }


                ////�������� ���� �ʾ��� ���� �������� Draw.
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
                //��ֹ��� �ε����� �ʾ��� ������ ��ֹ��� Draw.
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
                //m_IsFoodRemove = false �̸� �������� ���� ���� ��. -> Draw�Ѵ�.
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
        #region �ĵ��浹

        int[] TileMapNumber = {//ù��° ��
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
                               //�ι�° ��
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
                               //3��° ��
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
                               //4��° ��
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
                               //5��° ��
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
                               //6��° ��
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
                               //7��° ��
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
                               //8��° ��
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
                               //9��° ��
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
                               //10��° ��
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
                               //11��° ��
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
                               //12��° �� 
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

        private void ��ֹ��浹()
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

        private void �浹������Ʈ()
        {
            for (var i = 0; i < TileMapNumber.Length; i++)
            {
                TileRect[i] = new Rectangle((i % TileMax_X) * 48, (i / TileMax_X) * 48, 48, 48);
            }
        }

        void Update���WithTile(Direction direction)
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
