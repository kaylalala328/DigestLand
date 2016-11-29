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
    class ChallengeModeScreen : IScreen
    {
        #region ��������

        int TotalScore = 0;
        //BackButton, ClickedBackButton
        Button m_OptionButton;

        Texture2D m_ClearImage;

        #region ��������_ī�޶�
        //�⺻ ��Ƴ��� ��
        Texture2D m_�⺻_��;       //��
        Texture2D m_�⺻_�ĵ�1;    //�Խĵ�����
        Texture2D m_�⺻_�ĵ�2;    //�ĵ�����
        Texture2D m_�⺻_�ĵ�3;
        Texture2D m_�⺻_�ĵ�4;
        Texture2D m_�⺻_�ĵ�5;
        Texture2D m_�⺻_�ĵ�6;
        Texture2D m_�⺻_�ĵ�7;
        Texture2D m_�⺻_�ĵ�8;
        Texture2D m_�⺻_�ĵ�9;
        Texture2D m_�⺻_�ĵ�10;
        Texture2D m_�⺻_�ĵ�11;
        Texture2D m_�⺻_�ĵ�12;
        Texture2D m_�⺻_��;
        Texture2D m_�⺻_��������;
        Texture2D m_�⺻_����;
        Texture2D m_EntireMap;      //��ü��
        Texture2D m_EntireMapBackGround;

        //��ü�ʿ��� ������ Rectangle List
        List<Rectangle> m_MapRectangleList = new List<Rectangle>();

        Texture2D m_Camera;
        Vector2 m_CameraPosition;

        int m_MouseX = 0;
        int m_MouseY = 0;

        enum MapDirection { ��, �ĵ�, ��, ��������, ����, ����, None };
        MapDirection m_CurrentMapPosition = MapDirection.��;
        MapDirection m_PreviousMapPosition = MapDirection.None;

        Rectangle[] m_MapRectangleArray = new Rectangle[5];
        #endregion

        #region ��������_��
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

        int FoodSpeed = 25;
        int MapCounter = 2;
        int FoodCounter = 5;

        int[] m_FoodTypeArr;

        //ȿ����
        SoundEffectInstance soundInstance;
        SoundEffect m_EatSound;
        //ȿ����   
        SoundEffectInstance soundInstance2;
        SoundEffect m_EatSound2;
        #endregion

        #region ��������_�ĵ�

        //�ű�� ���� Ǯ��
        //int[] m_FoodTypeArr;

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

        int ObstacleNumber;
        int ObstacleIntex;

        int FoodNumber;
        int FoodIndex;
        int EatGulletFoodNumber = 0;
        #endregion

        #region ��������_��
        //�̹��� ����
        Texture2D mVirus;
        Texture2D StomachMap;
        Texture2D Swater1;
        Texture2D Swater2;
        Texture2D MonyElf;
        Texture2D Fire;
        Texture2D ��Ͽ�;
        Texture2D ��Ϻ�;

        //������ ó�� �������� ��ġ
        Vector2[] FoodDropPosition = new Vector2[10];
        //���̷��� ������
        Vector2[] VirusPosition = new Vector2[10];
        //�Ѿ� ������
        Vector2 FireBall;
        //��Ͽ��� ������
        Vector2 m_MonyPosition = new Vector2(250, 80);
        #region �浹��
        int[] StomachTileMapNumber = { 1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                                       1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                                       1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
                                       1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,
                                       1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,   
                                       1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,   
                                       1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,   
                                       1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,   
                                       1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,    
                                       1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,    
                                       1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1
                                     };
        #endregion
        Rectangle m_MonyRect = new Rectangle();
        Rectangle[] DieFoodKindRect = new Rectangle[15];
        Rectangle[] VirusRect = new Rectangle[10];
        Rectangle[] StomachTileRect = new Rectangle[24 * 16];
        Rectangle[] DropFoodRect = new Rectangle[10];
        //�Ѿ˻���
        enum FireBallDirection { shot, end };
        FireBallDirection FireBall_Direction = FireBallDirection.end;
        //�浹����
        enum StomachDirection { Left, Right, Up, Down };
        //��Ͽ�������-�Ѿ�
        enum MonyState { ATTACKED, IDLE }
        MonyState m_MonyState = MonyState.IDLE;
        //��Ͽ�������-����
        enum MonyDirection { LEFT, RIGHT, Front };
        MonyDirection m_MonyDirection = MonyDirection.RIGHT;
        //���Ļ��� 
        int[] FoodState = new int[10];      // 1 : �������°�  2: �����  3:���� ��ȯ
        //���� ���Ѱ� ���� ���ϱ�
        int[] DieFoodKind = new int[3 * 15];
        //���� ����
        int StomachFoodNumber;
        //���̷�������
        int[] VirusState = new int[10];     //4: ������ 5:�������°�
        //����Ʈ Ÿ��
        int m_WaitTime;
        //�׿����ϴ� ���� ��
        int KillVirusNumber;
        //���� ������
        int VirusFrameCounter = 0;
        //���� ����ƮŸ��
        int VirusWaitTime = 0;
        //���� ���� ����ƮŸ��
        int VirusKillWaitTime = 0;
        //�Ѿ� ����ƮŸ��
        int FireBallWaitTime = 0;
        int shot = 0;
        int ball = 0;
        int[] randomy = new int[10];
        const int StomachTileMax_X = 24;
        const int StomachTileMax_Y = 16;
        int mstate = 0;
        //�߷�
        const float gravity = 5;

        #endregion

        #region ��������_��������
        //Texture2D m_FairyImage;
        Texture2D �ܹ���Nutrient, ź��ȭ��Nutrient, ����Nutrient;
        Texture2D �ܹ���, ź��ȭ��, ����, �����ѹ�;
        Texture2D m_��������;
        Texture2D m_����������;

        Texture2D m_DuodenumColorBackground;
        Texture2D m_BackGroundColor;
        Texture2D m_������;
        Texture2D m_������;
        Texture2D m_���ڱ���;
        Texture2D WaterImage;

        List<String> m_FatString = new List<string>();
        List<String> m_ProteinString = new List<string>();

        ////�ܹ����� ���� ����Ʈ
        Vector2[] FatPosition = new Vector2[15];
        Rectangle[] FatRect = new Rectangle[15];

        int[] FatState;

        Rectangle m_��Rect;
        Rectangle m_����Rect;

        int m_Time;
        int HowManyMonsters;

        Random m_Random = new Random();

        enum MDirection { ����, ��, None };
        MDirection m_diretion = MDirection.None;

        enum WaterDirection { ���ھ�, ������, None };
        WaterDirection water_direction = WaterDirection.None;
        #endregion

        #region �ĵ��� �Ѱ��� ����


        //���Ĺ��� ����
        int m_FoodNumber = 15;
        //���� ���Ĺ��� ����
        int[] m_DeadNumber;
        int m_ObstacleNumber;
        int m_�������Ĺ��ǰ���;

        #endregion

        #region ��������_����
        Texture2D m_SmallIntestineColorBackground;
        Texture2D m_SmallIntestineBackground;
        Texture2D m_SmallIntestineForeground;

        Texture2D m_Nutrient;

        Texture2D m_Villus;
        Texture2D m_Villus2;
        Texture2D m_FeedVirus;
        Texture2D m_FeedVirus2;

        int m_�������忡���Ѿ�¿���Ұ���;
        Vector2[] m_NutrientPosition;

        //�巡���� �� ����Ұ� ���õƴ��� Ȯ���ϴ� ����
        bool[] m_IsSelected;
        //����� ���� - ���п� �Կ��� ��
        bool[] m_IsFeeded;
        //���� ���� - ������ ������� ��
        bool[] m_IsAbsorb;
        int ����m_Speed = 15;

        Vector2[] m_VirusPosition;
        Rectangle[] m_VirusCollisionRect;

        int ����WaitTime;
        #endregion

        MouseState m_CurrentMouseState, m_PreviousMouseState;
        KeyboardState keyboardState;
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

        Song music;
        SoundEffectInstance Wrong;
        SoundEffect Wrong_Sound;
        SoundEffectInstance ����;
        SoundEffect ����_Sound;

        SoundEffectInstance VirusKillMusic;
        SoundEffect Virus_Sound;
        SoundEffectInstance ȣ����;
        SoundEffect ȣ����_Sound;

        SoundEffectInstance �Ǳ�;
        SoundEffect �Ǳ�_Sound;


        public override void Init(ContentManager Content)
        {//����뷡
            music = Content.Load<Song>("�뷡/������");

            Wrong_Sound = Content.Load<SoundEffect>("�뷡/wrong");
            Wrong = Wrong_Sound.CreateInstance();
            Wrong.IsLooped = false;

            ����_Sound = Content.Load<SoundEffect>("�뷡/����");
            ���� = ����_Sound.CreateInstance();
            ����.IsLooped = false;

            Virus_Sound = Content.Load<SoundEffect>("�뷡/����������");
            VirusKillMusic = Virus_Sound.CreateInstance();
            VirusKillMusic.IsLooped = false;


            ȣ����_Sound = Content.Load<SoundEffect>("�뷡/ȣ����");
            ȣ���� = ȣ����_Sound.CreateInstance();
            ȣ����.IsLooped = false;

            �Ǳ�_Sound = Content.Load<SoundEffect>("�뷡/���Ǳ�");
            �Ǳ� = �Ǳ�_Sound.CreateInstance();
            �Ǳ�.IsLooped = false;


            m_OptionButton = new Button();
            //Pause button���� �ٲ�ߵɵ�
            m_OptionButton.Init(Content, new Vector2(10, 10), "Button/OptionButton", "Button/ClickedOptionButton");
            m_OptionButton.UserEvent = OnClickOptionButton;

            m_ClearImage = Content.Load<Texture2D>("CLEAR");

            #region ī�޶�_Load
            m_�⺻_�� = Content.Load<Texture2D>("�⺻��/�⺻��");
            m_�⺻_�ĵ�1 = Content.Load<Texture2D>("�⺻��/�ĵ�1");
            m_�⺻_�ĵ�2 = Content.Load<Texture2D>("�⺻��/�ĵ�2");
            m_�⺻_�ĵ�3 = Content.Load<Texture2D>("�⺻��/�ĵ�3");
            m_�⺻_�ĵ�4 = Content.Load<Texture2D>("�⺻��/�ĵ�4");
            m_�⺻_�ĵ�5 = Content.Load<Texture2D>("�⺻��/�ĵ�5");
            m_�⺻_�ĵ�6 = Content.Load<Texture2D>("�⺻��/�ĵ�6");
            m_�⺻_�ĵ�7 = Content.Load<Texture2D>("�⺻��/�ĵ�7");
            m_�⺻_�ĵ�8 = Content.Load<Texture2D>("�⺻��/�ĵ�8");
            m_�⺻_�ĵ�9 = Content.Load<Texture2D>("�⺻��/�ĵ�9");
            m_�⺻_�ĵ�10 = Content.Load<Texture2D>("�⺻��/�ĵ�10");
            m_�⺻_�ĵ�11 = Content.Load<Texture2D>("�⺻��/�ĵ�11");
            m_�⺻_�ĵ�12 = Content.Load<Texture2D>("�⺻��/�ĵ�12");
            m_�⺻_�� = Content.Load<Texture2D>("�⺻��/�⺻��");
            m_�⺻_�������� = Content.Load<Texture2D>("�⺻��/�⺻��������");
            m_�⺻_���� = Content.Load<Texture2D>("�⺻��/�⺻����");
            m_EntireMap = Content.Load<Texture2D>("��ü��");
            m_EntireMapBackGround = Content.Load<Texture2D>("��ü�ʹ��");
            m_Camera = Content.Load<Texture2D>("ī�޶�");
            m_CameraPosition = new Vector2(1026, 0);

            m_MapRectangleArray[0] = new Rectangle(1126, 0, 240, 184);
            m_MapRectangleArray[1] = new Rectangle(1126, 184, 240, 98);
            m_MapRectangleArray[2] = new Rectangle(1126, 184 + 98, 240, 98);
            m_MapRectangleArray[3] = new Rectangle(1126, 184 + 98 + 98, 240, 80);
            m_MapRectangleArray[4] = new Rectangle(1126, 184 + 98 + 98 + 80, 240, 142);
            //m_MapRectangleArray[5] = new Rectangle(1126, 184 + 98 + 98 + 80 + 142, 240, 166);
            #endregion

            #region ��_Load
            m_MouthColorBackground = Content.Load<Texture2D>("MouthColorBackground");
            m_MouthBackground = Content.Load<Texture2D>("��/MouthBackground");
            m_Tongue = Content.Load<Texture2D>("��/Tongue");
            m_Mouth = Content.Load<Texture2D>("��/Mouth");

            m_AppleIdle = Content.Load<Texture2D>("��/FoodIdle");
            m_AppleAttack = Content.Load<Texture2D>("FoodAttack");
            m_AppleRemains = Content.Load<Texture2D>("appleremain");


            m_CheeseIdle = Content.Load<Texture2D>("CheeseIdle");
            m_CheeseAttack = Content.Load<Texture2D>("CheeseAttack");
            m_CheeseRemains = Content.Load<Texture2D>("cheeseremain");

            m_MeatIdle = Content.Load<Texture2D>("MeatIdle");
            m_MeatAttack = Content.Load<Texture2D>("MeatAttack");
            m_MeatRemains = Content.Load<Texture2D>("meatremain");

            m_MyFont = Content.Load<SpriteFont>("myFont");

            m_EatSound = Content.Load<SoundEffect>("��/��_�Ʊ���");
            soundInstance = m_EatSound.CreateInstance();
            soundInstance.IsLooped = false;

            m_EatSound2 = Content.Load<SoundEffect>("��/��_��");
            soundInstance2 = m_EatSound2.CreateInstance();
            soundInstance2.IsLooped = false;

            m_FoodList.Add(new Food(3, 0));

            for (int i = 0; i < m_FoodNumber; i++)
                m_IsAllDropDownList.Add(false);


            #endregion

            #region �ĵ�_Load
            m_CountSound = Content.Load<SoundEffect>("�ĵ�/countdown");

            m_GulletColorBackground = Content.Load<Texture2D>("GulletColorBackground");

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


            #endregion

            #region ��_Load
            //���Ĺ����

            �ܹ��� = Content.Load<Texture2D>("�ܹ���");
            ź��ȭ�� = Content.Load<Texture2D>("ź��ȭ��");
            ���� = Content.Load<Texture2D>("����");
            �����ѹ� = Content.Load<Texture2D>("�����ѹ�");

            //����
            StomachMap = Content.Load<Texture2D>("��");
            Swater1 = Content.Load<Texture2D>("����");
            Swater2 = Content.Load<Texture2D>("����2");

            //���̷���
            mVirus = Content.Load<Texture2D>("virus");

            //��Ͽ����̶� �Ѿ�
            MonyElf = Content.Load<Texture2D>("mony");
            ��Ͽ� = Content.Load<Texture2D>("��Ͽ���������Ʈ");
            Fire = Content.Load<Texture2D>("�Ѿ�");
            ��Ϻ� = Content.Load<Texture2D>("��");

            //�ʱⰪ

            #endregion

            #region ��������_Load
            WaterImage = Content.Load<Texture2D>("������");
            �ܹ���Nutrient = Content.Load<Texture2D>("�ܹ���n");
            ź��ȭ��Nutrient = Content.Load<Texture2D>("ź��ȭ��n");
            ����Nutrient = Content.Load<Texture2D>("����n");

            //m_Font = Content.Load<SpriteFont>("myFont");
            m_DuodenumColorBackground = Content.Load<Texture2D>("DuodenumColorBackground");

            m_�������� = Content.Load<Texture2D>("���������");
            m_���������� = Content.Load<Texture2D>("����������");
            m_������ = Content.Load<Texture2D>("������");
            m_������ = Content.Load<Texture2D>("������");
            m_���ڱ��� = Content.Load<Texture2D>("���ڱ���");
            m_BackGroundColor = Content.Load<Texture2D>("���");

            //��(80, 100) ���� 90 ���� 128
            //����(252, 172) ���� 120 ���� 80
            m_��Rect = new Rectangle(15, 70, 200 - 15, 200 - 70);
            m_����Rect = new Rectangle(530, 300, 1050 - 530, 450 - 300);

            //�ϴ� �ʱ�ȭ
            m_Time = m_Random.Next(120, 600);

            #endregion

            #region ����_Load
            m_SmallIntestineColorBackground = Content.Load<Texture2D>("SmallIntestineColorBackground");
            m_SmallIntestineBackground = Content.Load<Texture2D>("SmallIntestineBackground");
            m_SmallIntestineForeground = Content.Load<Texture2D>("SmallIntestineForeground");

            m_Nutrient = Content.Load<Texture2D>("Nutrient");

            m_Villus = Content.Load<Texture2D>("Villus");
            m_Villus2 = Content.Load<Texture2D>("Villus2");
            m_FeedVirus = Content.Load<Texture2D>("FeedVillus");
            m_FeedVirus2 = Content.Load<Texture2D>("FeedVillus2");
            m_IsAbsorb = new bool[17];

            //����� �ʱ�ȭ
            for (var i = 0; i < m_�������忡���Ѿ�¿���Ұ���; i++)
            {
                m_NutrientPosition[i] = new Vector2(1000, -30 - (150 * i));
                m_IsFeeded[i] = false;
                m_IsSelected[i] = false;
            }


            //���� �ʱ�ȭ
            for (var i = 0; i < 17; i++)
                m_IsAbsorb[i] = false;

            #region Virus
            m_VirusPosition = new Vector2[17];
            m_VirusCollisionRect = new Rectangle[17];

            m_VirusPosition[0] = new Vector2(830, 70);
            m_VirusPosition[1] = new Vector2(713, 200);//������
            m_VirusPosition[2] = new Vector2(616, 80);
            m_VirusPosition[3] = new Vector2(512, 205);//������
            m_VirusPosition[4] = new Vector2(930, 445);//���� �ڿ��ִ� ����
            m_VirusPosition[5] = new Vector2(346, 90);
            m_VirusPosition[6] = new Vector2(180, 400);//ȸ���ϱ�
            m_VirusPosition[7] = new Vector2(150, 503);//ȸ���ϱ�
            m_VirusPosition[8] = new Vector2(900, 33);
            m_VirusPosition[9] = new Vector2(240, 630);//������
            m_VirusPosition[10] = new Vector2(520, 470);
            m_VirusPosition[11] = new Vector2(784, 600);//������
            m_VirusPosition[12] = new Vector2(90, 220);//[7]�̶� ���� ���� ������
            m_VirusPosition[13] = new Vector2(360, 465);
            m_VirusPosition[14] = new Vector2(457, 650);//[9]�� ���� ���� ������
            m_VirusPosition[15] = new Vector2(715, 432);
            m_VirusPosition[16] = new Vector2(662, 605);//

            m_VirusCollisionRect[0] = new Rectangle(830, 70, 50, 90);
            m_VirusCollisionRect[1] = new Rectangle(719, 200, 45, 72);
            m_VirusCollisionRect[2] = new Rectangle(616, 80, 50, 90);
            m_VirusCollisionRect[3] = new Rectangle(513, 213, 48, 68);
            m_VirusCollisionRect[4] = new Rectangle(930, 445, 50, 90);
            m_VirusCollisionRect[5] = new Rectangle(346, 90, 50, 90);
            m_VirusCollisionRect[6] = new Rectangle(190, 362, 50, 42);
            m_VirusCollisionRect[7] = new Rectangle(90, 493, 49, 44);
            m_VirusCollisionRect[8] = new Rectangle(900, 33, 50, 90);
            m_VirusCollisionRect[9] = new Rectangle(247, 637, 36, 52);
            m_VirusCollisionRect[10] = new Rectangle(520, 470, 50, 90);
            m_VirusCollisionRect[11] = new Rectangle(790, 609, 40, 55);
            m_VirusCollisionRect[12] = new Rectangle(123, 219, 56, 49);
            m_VirusCollisionRect[13] = new Rectangle(360, 465, 50, 90);
            m_VirusCollisionRect[14] = new Rectangle(468, 657, 37, 54);
            m_VirusCollisionRect[15] = new Rectangle(715, 432, 50, 90);
            m_VirusCollisionRect[16] = new Rectangle(670, 612, 39, 58);
            #endregion
            #endregion
        }

        private void OnClickOptionButton()
        {
            m_ScreenManager.SelectScreen(Mode.OptionAtChallengeScreen);
            MediaPlayer.Stop();
        }
        int musicupdate = 0;
        public override void Update(GameTime gameTime)
        {
            m_OptionButton.Update(gameTime);
            if (musicupdate++ < 2)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.2f;

            }
            m_CurrentMouseState = Mouse.GetState();

            //Ư�� ��ġ�� �Ǿ��� �� for�� ����.
            for (int i = 0; i < m_MapRectangleArray.Count<Rectangle>(); i++)
            {
                //��ü�ʿ��� �ش��ϴ� ���� ������ �� enum���� Direction �Ҵ�
                if ((m_CurrentMouseState.LeftButton == ButtonState.Pressed) && (m_PreviousMouseState.LeftButton == ButtonState.Released) && m_MapRectangleArray[i].Contains(m_CurrentMouseState.X, m_CurrentMouseState.Y))
                {
                    switch (i)
                    {
                        case 0:
                            m_CurrentMapPosition = MapDirection.��;
                            break;
                        case 1:
                            m_CurrentMapPosition = MapDirection.�ĵ�;
                            break;
                        case 2:
                            m_CurrentMapPosition = MapDirection.��;
                            break;
                        case 3:
                            m_CurrentMapPosition = MapDirection.��������;
                            break;
                        case 4:
                            m_CurrentMapPosition = MapDirection.����;
                            break;
                        case 5:
                            m_CurrentMapPosition = MapDirection.����;
                            break;
                        default:
                            break;
                    }
                }
            }

            //ī�޶� �̵��� ���� ��(���� �� ��ġ�� ��ü�ʿ��� �̵��� ���� Ŭ���� �� ��ġ�� �ٸ���)
            if (m_PreviousMapPosition.ToString() != m_CurrentMapPosition.ToString())
                Camera_Update(gameTime);
            else
            {
                switch (m_CurrentMapPosition)
                {
                    case MapDirection.��:
                        MouthUpdate(gameTime);
                        break;
                    case MapDirection.�ĵ�:
                        GulletUpdate(gameTime);
                        break;
                    case MapDirection.��:
                        StomachUpdate(gameTime);
                        break;
                    case MapDirection.��������:
                        DuodenumUpdate(gameTime);
                        break;
                    case MapDirection.����:
                        SmallIntestineUpdate(gameTime);
                        break;
                }
            }
            m_PreviousMouseState = m_CurrentMouseState;

            base.Update(gameTime);
        }

        #region Update_ī�޶�
        protected void Camera_Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            var CameraSpeed = 32; ;  //16�� ���
            int[] MapPosition = new int[6] { 0, 768, 768 * 13, 768 * 14, 768 * 15, 768 * 16 };

            switch (m_CurrentMapPosition)
            {
                case MapDirection.��:
                    if (m_CameraPosition.Y > MapPosition[0])
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y > MapPosition[0])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }
                    else
                        m_PreviousMapPosition = MapDirection.��;
                    break;
                case MapDirection.�ĵ�:
                    //(�ĵ� �ؿ� �ִٰ�)������ �ĵ��� �ö�� ��-X
                    if (m_CameraPosition.Y > MapPosition[1] && m_PreviousMapPosition == MapDirection.��)
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y > MapPosition[1])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }
                    //(�ĵ� ���� �ִٰ�)�Կ��� �ĵ��� ������ ��- O
                    else if (m_CameraPosition.Y <= MapPosition[1])
                    {
                        if (m_CameraPosition.Y >= MapPosition[1] - 400)
                        {
                            m_CameraPosition.Y += CameraSpeed / 8;
                            if (m_CameraPosition.Y > MapPosition[1])
                                m_PreviousMapPosition = m_CurrentMapPosition;
                        }
                        else
                            m_CameraPosition.Y += CameraSpeed / 4;

                    }
                    break;
                case MapDirection.��:
                    //�� �ؿ� �ִٰ� ���� �ö�� ��
                    //mapPosition[2] = 768 * 13;
                    if (m_CameraPosition.Y > MapPosition[2] && m_PreviousMapPosition == MapDirection.��������)
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y > MapPosition[2])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }

                    //�ĵ� ������ ������ �ڿ����� ���� �Ѿ ���
                    else if (m_PreviousMapPosition == MapDirection.�ĵ�)
                    {
                        //ī�޶� ���� ��ġ : �ĵ� ������ ���� ��������
                        if (m_CameraPosition.Y >= 768 * 13)
                        {
                            m_CameraPosition.Y = 768 * 13;
                            m_CurrentMapPosition = MapDirection.��;
                            m_PreviousMapPosition = m_CurrentMapPosition;
                        }
                        else
                        {

                            if (m_CameraPosition.Y >= 768 * 13 - 400)
                                m_CameraPosition.Y += CameraSpeed / 8;
                            else if (m_CameraPosition.Y >= 768 * 13 - 100)
                                m_CameraPosition.Y += CameraSpeed / 16;
                            else
                                m_CameraPosition.Y += CameraSpeed / 4;

                        }
                    }

                    //�� ���� �ִٰ� ���� ������ �� - OK
                    else
                    {
                        if (m_CameraPosition.Y > MapPosition[2] - 400)
                        {
                            m_CameraPosition.Y += CameraSpeed / 8;
                            if (m_CameraPosition.Y > MapPosition[2])
                                m_PreviousMapPosition = m_CurrentMapPosition;
                        }
                        else
                            m_CameraPosition.Y += CameraSpeed;
                    }
                    break;

                case MapDirection.��������:
                    //�������� �Ʒ��� �ִٰ� ������������ �ö�� ��
                    if (m_CameraPosition.Y > MapPosition[3] && m_PreviousMapPosition == MapDirection.����)
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y == MapPosition[3])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }

                    //�������� ���� �ִٰ� ������������ ������ ��
                    else
                    {
                        if (m_CameraPosition.Y >= MapPosition[3])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                        else
                        {
                            if (m_CameraPosition.Y >= MapPosition[3] - 400)
                            {
                                m_CameraPosition.Y += CameraSpeed / 8;
                                if (m_CameraPosition.Y >= MapPosition[3] - 200)
                                    m_CameraPosition.Y += CameraSpeed / 16;
                            }
                            else
                                m_CameraPosition.Y += CameraSpeed;
                        }
                    }
                    break;
                case MapDirection.����:
                    //m_CameraPosition.Y += CameraSpeed;
                    //���� �Ʒ��� �ִٰ� �������� �ö�� ��
                    if (m_CameraPosition.Y > MapPosition[4] && (m_PreviousMapPosition == MapDirection.����))
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y == MapPosition[4])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }

                    //�������忡�� �������� ������ ��- O
                    else
                    {
                        if (m_CameraPosition.Y >= MapPosition[4])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                        else
                        {
                            if (m_CameraPosition.Y >= MapPosition[4] - 400)
                            {
                                m_CameraPosition.Y += CameraSpeed / 8;
                                if (m_CameraPosition.Y >= MapPosition[4] - 200)
                                    m_CameraPosition.Y += CameraSpeed / 16;
                            }
                            else
                                m_CameraPosition.Y += CameraSpeed;
                        }

                    }
                    break;
                case MapDirection.����:
                    //���� �Ʒ��� �ִٰ� �������� �ö�� ��
                    if (m_CameraPosition.Y > MapPosition[5] && (m_PreviousMapPosition == MapDirection.����))
                    {
                        m_CameraPosition.Y -= CameraSpeed;
                        if (m_CameraPosition.Y == MapPosition[5])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }

                    //���� ���� �ִٰ�(��, �ĵ�, ��, ��������, ���忡��) �������� ������ ��
                    else
                    {
                        m_CameraPosition.Y += CameraSpeed;
                        if (m_CameraPosition.Y == MapPosition[5])
                            m_PreviousMapPosition = m_CurrentMapPosition;
                    }
                    break;
            }
            base.Update(gameTime);
        }
        #endregion

        #region Update_��

        public enum FoodType { Apple = 0, Cheese, Meat }

        public void MouthUpdate(GameTime gameTime)
        {
            UpdateFoodList(FoodSpeed, MapCounter, FoodCounter, AttackArea, m_FoodList);

            var Random = new Random(DateTime.Now.Millisecond);
            //���Ĺ� �߰�
            if (m_FoodList.Count < m_FoodNumber)
            {
                //450��ǥ�� ���� �� ���Ĺ� �߰��� ������ �Ѵ�
                if (m_FoodList[m_FoodList.Count - 1].m_FoodPosition.X > 450)
                {
                    m_FoodTypeArr[m_FoodList.Count] = Random.Next(0, 3);
                    m_FoodList.Add(new Food(Random.Next(2, 5), m_FoodTypeArr[m_FoodList.Count]));
                }
            }
            base.Update(gameTime);
        }

        private void UpdateFoodList(int FoodSpeed, int MapCounter, int FoodCounter, int[] AttackArea, List<Food> FoodList)
        {
            for (var i = 0; i < m_FoodList.Count; i++)
            {
                FoodList[i].UpdateIsFoodAttack(MapCounter, AttackArea);
                FoodList[i].UpdateFood(FoodSpeed, FoodCounter, AttackArea);
                FoodList[i].UpdateDeadCount();

                //if (FoodList[i].IsDropDown)
                //    soundInstance2.Play();

                m_DeadNumber[i] = 0;

                if (FoodList[i].m_IsDead)
                    m_DeadNumber[i] = 1;
                if (FoodList[i].m_PlusScore)
                {
                    TotalScore += 10;
                    FoodList[i].m_PlusScore = false;
                }
                if (!FoodList[i].m_MinusScore && FoodList[i].m_FoodPosition.Y >= 700 && !FoodList[i].m_IsDead)
                {
                    TotalScore -= 5;
                    FoodList[i].m_MinusScore = true;
                    Wrong.Play();
                }
            }
            var total = 0;

            for (var i = 0; i < m_FoodNumber; i++)
                total += m_DeadNumber[i];
            m_ObstacleNumber = m_FoodNumber - total;
            m_�������Ĺ��ǰ��� = total;
            ObstacleNumber = m_ObstacleNumber;
            FoodNumber = m_�������Ĺ��ǰ���;
        }
        #endregion

        #region Update_�ĵ�
        int Gulletupdate = 0;
        List<int> FixFoodType = new List<int>();
        List<int> ObstacleType = new List<int>();
        List<int> EatFoodType = new List<int>();

        protected void GulletUpdate(GameTime gameTime)
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
                        m_LifeList[2] = false;
                    //���� 2�� Ȥ�� 1���� ��
                    if (m_LifeList[2] == false)
                    {
                        if (m_LifeList[1] == true)
                            m_LifeList[1] = false;
                        else
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
                GoStomach += 3;

                ����� = 1;
            }

        }
        #endregion

        #region Update_��
        int[] movecase = new int[10];
        //���̷��� ������ġ
        private void Random_Virus()
        {
            Random VirusFix = new Random();
            KillVirusNumber = VirusFix.Next(5, 8);

            for (int VirusNumber = 0; VirusNumber < KillVirusNumber; VirusNumber++)
            {
                var VirusFixValueX = VirusFix.Next(6, 11);
                var VirusFixValueY = VirusFix.Next(5, 9);
                movecase[VirusNumber] = VirusFix.Next(1, 4);
                VirusPosition[VirusNumber].X = VirusFixValueX * 60;
                VirusPosition[VirusNumber].Y = VirusFixValueY * 60;
                VirusRect[VirusNumber] = new Rectangle((int)VirusPosition[VirusNumber].X, (int)VirusPosition[VirusNumber].Y, 80, 80);
                Stomachupdate++;
            }
        }

        Vector2[] �����;
        int[] ����X����;
        int[] ����Y����;
        //���Ĺ� �ʱⰪ
        private void First_Food()
        {
            StomachFoodNumber = EatGulletFoodNumber;  //���Ĺ� ���� �����ֱ�

            ����� = new Vector2[StomachFoodNumber];
            Random FoodFix = new Random();
            ����X���� = new int[StomachFoodNumber];
            ����Y���� = new int[StomachFoodNumber];

            ////�� ���Ĵ� �ִ� 3�� ������ ��⸦ ������ ����!
            DieFoodKind[(0) * 3] = (FoodFix.Next(6, 12) - 5) / 2;
            DieFoodKind[(0) * 3 + 1] = (FoodFix.Next(6, 12) - 5) / 2;
            DieFoodKind[(0) * 3 + 2] = (FoodFix.Next(6, 12) - 5) / 2;

            for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
            {
                int FoodFixX = FoodFix.Next(2, 3);
                randomy[FoodCount] = FoodFix.Next(1, 5);
                FoodDropPosition[FoodCount].X = 220 + FoodFixX * 20;
                FoodDropPosition[FoodCount].Y = -FoodCount * FoodFixX * 50;
                FoodState[FoodCount] = 1;

                �����[FoodCount] = new Vector2((FoodFix.Next(9, 21) - 6) * 50, FoodFix.Next(4, 7) * 50);

                ����X����[FoodCount] = ((int)�����[FoodCount].X - (int)FoodDropPosition[FoodCount].X) / 10;
                ����Y����[FoodCount] = ((int)�����[FoodCount].Y - ((int)FoodDropPosition[FoodCount].Y + 150)) / 10;

                DieFoodKind[(FoodCount) * 3] = (FoodFix.Next(6, 12) - 5) / 2;
                DieFoodKind[(FoodCount) * 3 + 1] = (FoodFix.Next(6, 12) - 5) / 2;
                DieFoodKind[(FoodCount) * 3 + 2] = (FoodFix.Next(6, 12) - 5) / 2;
            }
        }

        private void DropFood()
        {
            Random Foodmove = new Random();

            for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
            {
                DropFoodRect[FoodCount] = new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64);
                int movecase = Foodmove.Next(-1, 1);
                if (m_WaitTime++ > 5)
                {
                    m_WaitTime = 0;
                    //DropFood �����϶��� ������
                    if (FoodState[FoodCount] == 1)
                    {
                        ////ó���� �߶������ٰ�
                        if (FoodDropPosition[FoodCount].Y < 150)
                        {
                            FoodDropPosition[FoodCount].Y += gravity;
                        }

                        else if (�����[FoodCount].Y > FoodDropPosition[FoodCount].Y)
                        {
                            FoodDropPosition[FoodCount].X += ����X����[FoodCount];
                            FoodDropPosition[FoodCount].Y += ����Y����[FoodCount];
                        }

                        else
                        {
                            FoodState[FoodCount] = 2;
                        }
                    }

                    else if (FoodState[FoodCount] == 2)
                    {

                        FoodDropPosition[FoodCount].Y = �����[FoodCount].Y + randomy[FoodCount] * 20 + 10 * movecase;

                    }
                    //ChageFood �����϶��� ������
                    else if (FoodState[FoodCount] == 3)
                    {
                        �Ǳ�.Play();
                        if (FoodDropPosition[FoodCount].Y > 500)
                            FoodDropPosition[FoodCount].Y = 500 + randomy[FoodCount] * 20 + 10 * movecase;

                        else
                            FoodDropPosition[FoodCount].Y += gravity * 3 * 3;
                    }
                }


                else if (FoodState[FoodCount] == 4)
                {
                    if (FoodDropPosition[FoodCount].X < 950) { FoodDropPosition[FoodCount].X += gravity; }
                    else if (FoodDropPosition[FoodCount].Y < 768)
                    {
                        FoodDropPosition[FoodCount].Y += gravity * 3;
                    }
                    else
                    {
                        //������ �������� �ڵ����� �������� Update ��Ŵ

                        IsStomachClear = true;

                        //3�� �ڿ� ���� ������ ������������ ������ �Ѵ�.
                        if (m_StomachTimingCount++ >= 180)
                        {
                            FoodDropPosition[FoodCount].Y = 768;

                            m_CurrentMapPosition = MapDirection.��������;
                        }
                    }
                }

            }
        }

        int m_StomachTimingCount = 0;

        double m_StartTime = 0;
        int time;
        private void endStomach(GameTime gameTime)
        {
            const double targetTime = 30;
            m_StartTime += gameTime.ElapsedGameTime.TotalSeconds;

            time = (int)(targetTime - m_StartTime);

            if (time == 0)
            {
                for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
                    FoodState[FoodCount] = 4;
            }

            else if (DieFoodAllKill == StomachFoodNumber && VirusAllKill == KillVirusNumber + 1)
            {
                for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
                    if (FoodDropPosition[FoodCount].Y > 470)
                        FoodState[FoodCount] = 4;
            }

            if (time == 29) { ȣ����.Play(); }
        }

        bool IsStomachClear = false;

        int TanDanGiNumber = 0;
        int VirusAllKill = 0;
        int DieFoodAllKill = 0;
        Rectangle[] StopFoodRect_z = new Rectangle[10];

        //���� ��ȯ
        private void ChangeFood()
        {
            for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
            {
                if (FoodState[FoodCount] == 2)
                {
                    DropFoodRect[FoodCount] = new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64);
                    StopFoodRect_z[FoodCount] = new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 10);
                    var keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        if (m_MonyRect.Intersects(StopFoodRect_z[FoodCount]))// && m_MonyPosition.Y <= FoodDropPosition[FoodCount].Y - 70 && m_MonyPosition.X >= FoodDropPosition[FoodCount].X - 20 && m_MonyPosition.X <= FoodDropPosition[FoodCount].X + 50)
                        {
                            TotalScore += 10; ����.Play();
                            m_MonyPosition.Y -= 10;
                            FoodState[FoodCount] = 3;
                            DieFoodAllKill++;
                            if (DieFoodKind[FoodCount * 3] != 0) { TanDanGiNumber++; }
                            if (DieFoodKind[FoodCount * 3 + 1] != 0) { TanDanGiNumber++; }
                            if (DieFoodKind[FoodCount * 3 + 2] != 0) { TanDanGiNumber++; }
                        }
                    }

                }
            }
        }

        //������ ���̷��� �����̴� ��
        private void MoveVirus()
        {
            Random move = new Random();
            if (m_WaitTime++ > 5)
            {
                m_WaitTime = 0;

                for (int VirusNumber = 0; VirusNumber < KillVirusNumber; VirusNumber++)
                {
                    if (movecase[VirusNumber] == 1)
                    {
                        VirusPosition[VirusNumber].X -= 10f;
                        VirusPosition[VirusNumber].Y += 2f;
                    }
                    if (movecase[VirusNumber] == 2)
                    {
                        VirusPosition[VirusNumber].X += 10f;
                        VirusPosition[VirusNumber].Y += 2f;
                    }
                    if (movecase[VirusNumber] == 3)
                    {
                        VirusPosition[VirusNumber].X += 10f;
                        VirusPosition[VirusNumber].Y -= 2f;
                    }
                    if (movecase[VirusNumber] == 4)
                    {
                        VirusPosition[VirusNumber].Y -= 10f;
                        VirusPosition[VirusNumber].X -= 2f;
                    }
                }
            }
        }

        //���̷��� ���̴°�
        private void KillVirus()
        {
            VirusRect = new Rectangle[KillVirusNumber];
            var mouse = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                ball = 0;
                shot = 1;
                switch (m_MonyDirection)
                {
                    case MonyDirection.LEFT:
                        FireBall = new Vector2(m_MonyPosition.X - 30, m_MonyPosition.Y + 50);
                        break;
                    case MonyDirection.RIGHT:
                        FireBall = new Vector2(m_MonyPosition.X + 80, m_MonyPosition.Y + 50);

                        break;
                }
            }

            if (shot == 1)
            {
                FireBall_Direction = FireBallDirection.shot;
                if (FireBallWaitTime++ > 3)
                {
                    FireBallWaitTime = 0;
                    if (ball++ < 5)
                    {
                        switch (m_MonyDirection)
                        {
                            case MonyDirection.LEFT:
                                FireBall.X = FireBall.X - 20 * ball;
                                break;
                            case MonyDirection.RIGHT:
                                FireBall.X = FireBall.X + 20 * ball;
                                break;
                        }
                    }
                    else
                    {
                        FireBall_Direction = FireBallDirection.end;
                        shot = 2;
                    }
                }
            }
            for (int VirusNumber = 0; VirusNumber < KillVirusNumber; VirusNumber++)
            {
                VirusRect[VirusNumber] = new Rectangle((int)VirusPosition[VirusNumber].X, (int)VirusPosition[VirusNumber].Y, 80, 80);
                Rectangle FireBallRectangle = new Rectangle((int)FireBall.X, (int)FireBall.Y, 25, 25);
                if (FireBall_Direction == FireBallDirection.shot)
                {
                    if (VirusRect[VirusNumber].Contains(FireBallRectangle))
                    {
                        shot = 2;

                        if (VirusKillWaitTime++ > 10)
                        {
                            FireBall_Direction = FireBallDirection.end;
                            TotalScore += 10;
                            VirusKillWaitTime = 0;
                            VirusState[VirusNumber] = 4;
                            VirusAllKill++;
                            VirusKillMusic.Play();
                        }
                    }
                }
            }
        }

        //Mony���� ������
        private void MonyMove()
        {
            m_MonyRect = new Rectangle((int)m_MonyPosition.X + 25, (int)m_MonyPosition.Y + 25, 50, 50);

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                m_MonyPosition.X -= gravity;
                m_MonyDirection = MonyDirection.LEFT;

                StomachUpdate���WithTile(StomachDirection.Left);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                m_MonyPosition.X += gravity;
                m_MonyDirection = MonyDirection.RIGHT;

                StomachUpdate���WithTile(StomachDirection.Right);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                m_MonyPosition.Y -= gravity;
                StomachUpdate���WithTile(StomachDirection.Up);
                m_MonyDirection = MonyDirection.Front;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                m_MonyPosition.Y += gravity;
                StomachUpdate���WithTile(StomachDirection.Down);
                m_MonyDirection = MonyDirection.Front;
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                m_MonyPosition = new Vector2(330, 500);
            }
        }

        //���� ������
        private void VirusFrame()
        {
            if (VirusWaitTime++ > 10)
            {
                VirusWaitTime = 0;

                if (VirusFrameCounter++ >= 3)
                    VirusFrameCounter = 0;
            }
        }

        int Stomachupdate = 0;
        protected void StomachUpdate(GameTime gameTime)
        {
            �����浹();
            �����浹();

            if (Stomachupdate == 0)
            {
                ���浹������Ʈ();
                Random_Virus();
                First_Food();
            }
            else if (Stomachupdate == KillVirusNumber)
            {
                MoveVirus();
            }
            endStomach(gameTime);
            VirusFrame();
            DropFood();
            KillVirus();
            ChangeFood();
            MonyMove();
            base.Update(gameTime);
        }

        #endregion

        #region ���浹

        private void ���浹������Ʈ()
        {
            for (var i = 0; i < StomachTileMapNumber.Length; i++)
                StomachTileRect[i] = new Rectangle((i % StomachTileMax_X) * 48, (i / StomachTileMax_X) * 48, 48, 48);
        }

        private void �����浹()
        {
            for (var TileIndex = 0; TileIndex < StomachTileMapNumber.Length; TileIndex++)
            {
                if (StomachTileMapNumber[TileIndex] == 1)
                {
                    for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
                    {
                        if (FoodState[FoodCount] == 1)
                        {
                            if (StomachTileRect[TileIndex].Intersects(DropFoodRect[FoodCount]))
                            {
                                if (StomachTileRect[TileIndex].X < DropFoodRect[FoodCount].X)
                                {
                                    FoodDropPosition[FoodCount].X += 50;
                                    FoodState[FoodCount] = 2;
                                }
                                else if (StomachTileRect[TileIndex].X >= DropFoodRect[FoodCount].X)
                                {
                                    FoodDropPosition[FoodCount].X -= 50;
                                    if (DropFoodRect[FoodCount].Y > 170)
                                        FoodState[FoodCount] = 2;
                                }
                            }
                        }

                        if (FoodState[FoodCount] == 2)
                        {
                            if (StomachTileRect[TileIndex].Intersects(DropFoodRect[FoodCount]))
                            {
                                if (StomachTileRect[TileIndex].X < DropFoodRect[FoodCount].X)
                                {
                                    DropFoodRect[FoodCount].X += 50;
                                    DropFoodRect[FoodCount].Y += 50;
                                }
                                else if (StomachTileRect[TileIndex].X >= DropFoodRect[FoodCount].X)
                                    FoodDropPosition[FoodCount].X -= 50;
                                else if (StomachTileRect[TileIndex].Y >= DropFoodRect[FoodCount].Y)
                                    FoodDropPosition[FoodCount].Y -= 50;
                                else if (StomachTileRect[TileIndex].Y < DropFoodRect[FoodCount].Y)
                                    FoodDropPosition[FoodCount].Y += 50;
                            }
                        }
                    }
                }
            }
        }

        void StomachUpdate���WithTile(StomachDirection direction)
        {
            if (m_MonyPosition.X < 0)
                m_MonyPosition.X = 0;
            if (m_MonyPosition.Y < 0)
                m_MonyPosition.Y = 0;

            for (var TileIndex = 0; TileIndex < StomachTileMapNumber.Length; TileIndex++)
            {
                if (StomachTileRect[TileIndex].Contains((int)m_MonyPosition.X, (int)m_MonyPosition.Y))
                {
                    if (StomachTileMapNumber[TileIndex] == 1)
                    {
                        m_MonyState = MonyState.ATTACKED;

                        if (direction == StomachDirection.Left)
                        {
                            m_MonyPosition.X = StomachTileRect[TileIndex].X + StomachTileRect[TileIndex].Width + 10;
                            TotalScore -= 5; Wrong.Play();
                        }
                        if (direction == StomachDirection.Right)
                        { m_MonyPosition.X = StomachTileRect[TileIndex].X - 10; TotalScore -= 5; Wrong.Play(); }
                        if (direction == StomachDirection.Up)
                        { m_MonyPosition.Y = StomachTileRect[TileIndex].Y + StomachTileRect[TileIndex].Height + 10; TotalScore -= 5; Wrong.Play(); }
                        if (direction == StomachDirection.Down)
                        { m_MonyPosition.Y = StomachTileRect[TileIndex].Y - 10; TotalScore -= 5; Wrong.Play(); }
                    }
                }
            }
        }

        int sad = 0;
        private void �����浹()
        {
            Random move = new Random();
            for (int VirusNumber = 0; VirusNumber < KillVirusNumber; VirusNumber++)
            {
                if (VirusRect[VirusNumber].Intersects(m_MonyRect))
                {
                    if (sad++ > 10)
                    {
                        TotalScore -= 5; Wrong.Play();
                        sad = 0;
                        m_MonyState = MonyState.ATTACKED;
                    }
                }

                if (VirusPosition[VirusNumber].X < 100) { movecase[VirusNumber] = 3; }
                if (VirusPosition[VirusNumber].X > 800) { movecase[VirusNumber] = 1; }
                if (VirusPosition[VirusNumber].Y < 150) { movecase[VirusNumber] = 2; }
                if (VirusPosition[VirusNumber].Y > 600) { movecase[VirusNumber] = 4; }
            }
        }
        #endregion

        int Duodenumupdate = 0;
        #region Update_��������
        protected void DuodenumUpdate(GameTime gameTime)
        {
            m_MouseX = Mouse.GetState().X;
            m_MouseY = Mouse.GetState().Y;


            if (Duodenumupdate == 0)
            {
                TanDanGi_First();
                Duodenumupdate++;
            }
            else
            {
                TanDanGi_Move();
                //random���� ���� ��or���ڰ� ���õǾ��� ��� ����Ǵ� �ڵ�
                Click_GanEja();
            }
            base.Update(gameTime);
        }

        int clickwaittmime = 0;
        int WaterWaitTime = 0;
        int WaterFrameCounter = 0;
        Rectangle ������ = new Rectangle(270, 330, 50, 40);
        Rectangle ���ھ� = new Rectangle(340, 430, 80, 30);
        int NutrientNumber = 0;

        int[] �����ѹ�;
        int[] �����ѹ�;
        private void Click_GanEja()
        {
            if (WaterWaitTime++ > 4)
            {
                WaterWaitTime = 0;

                if (WaterFrameCounter++ >= 6)
                    WaterFrameCounter = 0;
            }
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed) && m_��Rect.Contains(m_MouseX, m_MouseY) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                m_diretion = MDirection.��;
                water_direction = WaterDirection.������;
                for (int i = 0; i < HowManyMonsters; i++)
                {
                    if (FatRect[i].Intersects(������))
                    {
                        if (FatState[i] == 3)
                        {
                            TotalScore += 10;
                            FatState[i] = 4;
                            NutrientNumber++; ����.Play();
                        }
                        else
                        {
                            if (�����ѹ�[i] == 0)
                            {
                                TotalScore -= 5; Wrong.Play();
                                �����ѹ�[i] = 1;
                            }
                        }
                    }
                }
            }
            //���� �߸� �׿��ٸ� ���� ����
            else if ((Mouse.GetState().LeftButton == ButtonState.Pressed) && m_����Rect.Contains(m_MouseX, m_MouseY) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                m_diretion = MDirection.����;
                water_direction = WaterDirection.���ھ�;
                for (int i = 0; i < HowManyMonsters; i++)
                    if (FatRect[i].Intersects(���ھ�))
                    {
                        if (FatState[i] == 1 || FatState[i] == 2 || FatState[i] == 4)
                        {
                            ����.Play();
                            TotalScore += 10;
                            FatState[i] += 10;
                            NutrientNumber++;
                            ����Nutrient.Add(FatState[i]);
                        }
                        else if (FatState[i] == 3)
                        {
                            if (�����ѹ�[i] == 0)
                            {
                                TotalScore -= 5; Wrong.Play();
                                �����ѹ�[i] = 1;
                            }
                        }
                    }
            }
        }
        int FatWaittime = 0;
        Random randomvalue = new Random();
        int d_gravity = 10;
        int[] TanDanGiDrop;
        //ź������ �������� �޼ҵ�
        private void TanDanGi_Move()
        {
            if (FatWaittime++ > 3)
            {
                FatWaittime = 0;
                for (int i = 0; i < HowManyMonsters; i++)
                {
                    FatRect[i] = new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50);
                    m_FatString.Add("Unclick");
                    m_ProteinString.Add("Unclick");
                    var TanDanGi_Go = randomvalue.Next(2, 5);

                    if (FatPosition[i].Y < 0)
                        FatPosition[i].Y += d_gravity;
                    //ù��° �������� |
                    else if (FatPosition[i].Y < 120)
                    {
                        FatPosition[i].X = 900 + TanDanGi_Go * 5 - 1.5f;
                        FatPosition[i].Y += d_gravity;
                    }
                    //ù��° ���� -
                    else if (FatPosition[i].Y < 200 && FatPosition[i].X > 335)
                    {
                        FatPosition[i].X -= d_gravity;
                        FatPosition[i].Y = 130 + TanDanGi_Go * 5;
                    }
                    else if (FatPosition[i].Y < 550 && FatPosition[i].X > 300)
                    {
                        FatPosition[i].X = 300 + TanDanGi_Go * 5;
                        FatPosition[i].Y += d_gravity;
                    }
                    else if (FatPosition[i].Y < 600 && FatPosition[i].X < 950)
                    {
                        FatPosition[i].X += d_gravity;
                        FatPosition[i].Y = 550 + TanDanGi_Go * 5;
                    }
                    else if (FatPosition[i].Y < 768)
                    {
                        FatPosition[i].Y += d_gravity;
                    }

                    else if (FatPosition[i].Y >= 768)
                    {
                        FatPosition[i].Y = 800;
                        if (FatState[i] == 1 || FatState[i] == 2 || FatState[i] == 3 || FatState[i] == 4)
                        {
                            if (TanDanGiDrop[i] == 0)
                            {
                                TotalScore -= 5; Wrong.Play();
                                TanDanGiDrop[i] = 1;
                            }
                        }

                        ////����

                        if ((int)FatPosition[HowManyMonsters - 1].Y >= 768)
                        {
                            if (m_��������TimingCount++ >= 180)
                            {
                                //m_PreviousMapPosition = m_CurrentMapPosition;
                                m_CurrentMapPosition = MapDirection.����;
                            }
                            else
                                m_��������Clear�׷����� = true;
                        }

                    }
                }
            }
        }
        bool m_��������Clear�׷����� = false;
        int m_��������TimingCount = 0;
        List<int> TanDanGiState = new List<int>();
        //ź���� �ʱⰪ
        private void TanDanGi_First()
        {
            for (int i = 0; i < StomachFoodNumber * 3; i++)
            {
                if (DieFoodKind[i] != 0)
                {
                    TanDanGiState.Add(DieFoodKind[i]);
                }
            }
            HowManyMonsters = TanDanGiNumber;
            FatState = new int[HowManyMonsters];
            TanDanGiDrop = new int[HowManyMonsters];
            �����ѹ� = new int[HowManyMonsters];
            �����ѹ� = new int[HowManyMonsters];
            for (int b = 0; b < HowManyMonsters; b++)
            {
                �����ѹ�[b] = �����ѹ�[b] = 0;
                TanDanGiDrop[b] = 0;
                FatState[b] = TanDanGiState[b];
                FatPosition[b] = new Vector2(900, 0 - b * 100);
                FatRect[b] = new Rectangle((int)FatPosition[b].X, (int)FatPosition[b].Y, 50, 50);
            }

        }
        #endregion

        int SmallIntestineupdate = 0;

        #region Update_����
        int[] NutrientDrop;
        List<int> ����Nutrient = new List<int>();
        protected void SmallIntestineUpdate(GameTime gameTime)
        {
            if (SmallIntestineupdate == 0)
            {
                m_�������忡���Ѿ�¿���Ұ��� = ����Nutrient.Count;
                m_NutrientPosition = new Vector2[m_�������忡���Ѿ�¿���Ұ���];
                m_NutrientPosition = new Vector2[m_�������忡���Ѿ�¿���Ұ���];
                m_IsFeeded = new bool[m_�������忡���Ѿ�¿���Ұ���];
                m_IsSelected = new bool[m_�������忡���Ѿ�¿���Ұ���];
                NutrientDrop = new int[m_�������忡���Ѿ�¿���Ұ���];
                //����� �ʱ�ȭ
                for (var i = 0; i < m_�������忡���Ѿ�¿���Ұ���; i++)
                {
                    NutrientDrop[0] = 0;
                    m_NutrientPosition[i] = new Vector2(1000, -30 - (100 * i));
                    m_IsFeeded[i] = false;
                    m_IsSelected[i] = false;
                }
                SmallIntestineupdate++;
            }
            else
            {
                UpdateNutrientMove();
                DragNutrient();
            }
        }
        int VillusTime = 0;

        private void DragNutrient()
        {
            Rectangle[] NutrientRect = new Rectangle[m_�������忡���Ѿ�¿���Ұ���];

            m_CurrentMouseState = Mouse.GetState();
            for (var i = 0; i < m_�������忡���Ѿ�¿���Ұ���; i++)
            {
                NutrientRect[i] = new Rectangle((int)m_NutrientPosition[i].X, (int)m_NutrientPosition[i].Y, m_Nutrient.Width, m_Nutrient.Height);

                //����� �����ϴ� �κ�
                if (m_CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (m_IsSelected[i] == false)
                    {
                        if (NutrientRect[i].Contains(m_CurrentMouseState.X, m_CurrentMouseState.Y))
                            m_IsSelected[i] = true;
                    }
                    else
                    {
                        m_NutrientPosition[i].X += m_CurrentMouseState.X - m_PreviousMouseState.X;
                        m_NutrientPosition[i].Y += m_CurrentMouseState.Y - m_PreviousMouseState.Y;
                    }
                }

                //����Ҹ� �巡���ϴٰ� ���� ��
                else if (m_CurrentMouseState.LeftButton == ButtonState.Released && m_PreviousMouseState.LeftButton == ButtonState.Pressed)
                {
                    for (var k = 0; k < 17; k++)
                    {
                        if (m_IsAbsorb[k] == false && NutrientRect[i].Intersects(m_VirusCollisionRect[k]))
                        {
                            m_IsFeeded[i] = true;
                            m_IsAbsorb[k] = true;
                            TotalScore += 10;
                            ����.Play();
                        }
                        if (m_IsAbsorb[k])
                        {
                            if (VillusTime++ > 10)
                            {
                                VillusTime = 0;
                                m_IsAbsorb[k] = false;
                            }
                        }
                    }
                    if (m_IsFeeded[i] == true)
                        m_NutrientPosition[i] = Vector2.Zero;
                    m_IsSelected[i] = false;
                }

            }
            m_PreviousMouseState = m_CurrentMouseState;
        }

        private void UpdateNutrientMove()
        {
            var Random = new Random();
            var NutrientRandomMove = Random.Next(3, 7);

            if (����WaitTime++ > 10)
            {
                ����WaitTime = 0;
                for (var i = 0; i < m_�������忡���Ѿ�¿���Ұ���; i++)
                {
                    //����Ҹ� ���� ���п� ������ �ʾ��� ��
                    if (m_IsFeeded[i] == false && m_IsSelected[i] == false)
                    {
                        if (m_NutrientPosition[i].Y < 120)
                        {
                            m_NutrientPosition[i].X = 1000 + (NutrientRandomMove * 5);
                            m_NutrientPosition[i].Y += ����m_Speed * 2;
                        }
                        else if (m_NutrientPosition[i].X > 200 && m_NutrientPosition[i].Y < 250)
                        {
                            m_NutrientPosition[i].X -= ����m_Speed * 3;
                            m_NutrientPosition[i].Y = 150 + NutrientRandomMove * 5;
                        }
                        else if (m_NutrientPosition[i].X > 120 && m_NutrientPosition[i].Y < 600)
                        {
                            m_NutrientPosition[i].X = 120 + NutrientRandomMove * 5;
                            m_NutrientPosition[i].Y += ����m_Speed * 2;
                        }
                        else if (m_NutrientPosition[i].X < 920 && m_NutrientPosition[i].Y < 700)
                        {
                            m_NutrientPosition[i].X += ����m_Speed * 3;
                            m_NutrientPosition[i].Y = 600 + NutrientRandomMove * 5;
                        }
                        else if (m_NutrientPosition[i].X < 1000 && m_NutrientPosition[i].Y < 750)
                        {
                            m_NutrientPosition[i].X = 970 - NutrientRandomMove * 5;
                            m_NutrientPosition[i].Y += ����m_Speed * 2;
                        }

                        else if (m_NutrientPosition[i].Y > 768)
                        {
                            m_NutrientPosition[i].Y = 800;

                        }

                        if (m_NutrientPosition[i].Y == 800)
                        {
                            if (NutrientDrop[i] == 0)
                            {
                                TotalScore -= 5; Wrong.Play();
                                NutrientDrop[i] = 1;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (m_PreviousMapPosition.ToString() != m_CurrentMapPosition.ToString())
                Camera_Draw(gameTime, spriteBatch);
            else
            {
                //�⺻���� ���� �� �ڵ� ���� �� �׷����ƾ� ��.
                switch (m_CurrentMapPosition)
                {
                    case MapDirection.��:
                        DrawMouth(gameTime, spriteBatch);
                        break;
                    case MapDirection.�ĵ�:
                        DrawGullet(gameTime, spriteBatch);
                        break;
                    case MapDirection.��:
                        DrawStomach(gameTime, spriteBatch);
                        break;
                    case MapDirection.��������:
                        DuodenumDraw(gameTime, spriteBatch);
                        break;
                    case MapDirection.����:
                        SmallIntestineDraw(gameTime, spriteBatch);
                        break;
                }
            }

            spriteBatch.Begin();

            //spriteBatch.Draw(m_TitleBackground, new Vector2(0, 0), Color.White);

            m_OptionButton.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(m_EntireMapBackGround, new Rectangle((int)m_CameraPosition.X + 100, 0, 240, 768), Color.White);
            //��ü ��
            spriteBatch.Draw(m_EntireMap, new Rectangle((int)m_CameraPosition.X + 100, 68, 240, 700), Color.White);


            spriteBatch.DrawString(m_Font, "" + TotalScore, new Vector2(m_CameraPosition.X + 250, 10), new Color(127, 127, 127));

            //ī�޶� �׽�Ʈ
            //spriteBatch.DrawString(m_Font, m_CameraPosition.Y + "", new Vector2(m_CameraPosition.X, m_CameraPosition.Y + 100), Color.bl);
            //this.Window.Title = "X : " + m_CurrentMouseState.X + "  Y: " + m_CurrentMouseState.Y;

            //spriteBatch.DrawString(m_MyFont, m_CurrentMouseState.LeftButton.ToString() + ", " + m_PreviousMouseState.LeftButton.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }

        #region Draw_ī�޶�
        protected void Camera_Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(Color.White);

            var topY = -m_CameraPosition.Y - 350;// +halfScreenHeight;

            //ī�޶� 
            var matrix = Matrix.CreateTranslation(new Vector3(-500, topY, 0)) * Matrix.CreateScale(1.0f) * Matrix.CreateTranslation(500, 350, 0);

            //ī�޶� ����ϱ� ���� Begin�ڵ�
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, matrix);

            //�⺻������ �׸���
            //�⺻:��
            DrawTongue(spriteBatch);
            DrawFood(spriteBatch);
            DrawMouth(spriteBatch);
            spriteBatch.Draw(m_�⺻_��, new Rectangle(0, 0, 1366, 768), Color.White);

            //�⺻:�ĵ�
            spriteBatch.Draw(m_�⺻_�ĵ�1, new Rectangle(0, 768, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�2, new Rectangle(0, 768 * 2, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�3, new Rectangle(0, 768 * 3, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�4, new Rectangle(0, 768 * 4, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�5, new Rectangle(0, 768 * 5, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�6, new Rectangle(0, 768 * 6, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�7, new Rectangle(0, 768 * 7, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�8, new Rectangle(0, 768 * 8, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�9, new Rectangle(0, 768 * 9, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�10, new Rectangle(0, 768 * 10, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�11, new Rectangle(0, 768 * 11, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_�ĵ�12, new Rectangle(0, 768 * 12, 1366, 768), Color.White);

            //�⺻:��
            spriteBatch.Draw(m_�⺻_��, new Rectangle(0, 768 * 13, 1366, 768), Color.White);
            //�⺻:��������
            spriteBatch.Draw(m_�⺻_��������, new Rectangle(0, 768 * 14, 1366, 768), Color.White);
            //�⺻:����
            spriteBatch.Draw(m_�⺻_����, new Rectangle(0, 768 * 15, 1366, 768), Color.White);
            spriteBatch.Draw(m_�⺻_����, new Rectangle(0, 768 * 16, 1366, 768), Color.White);

            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }
        #endregion

        #region Draw_��
        protected void DrawMouth(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(new Color(215, 255, 255));
            spriteBatch.Begin();
            spriteBatch.Draw(m_MouthColorBackground, Vector2.Zero, Color.White);
            //�������
            spriteBatch.Draw(m_MouthBackground, new Vector2(-120, 0), Color.White);
            //�ĵ�
            spriteBatch.Draw(m_�⺻_�ĵ�1, new Rectangle(0, 768, 1366, 768), Color.White);
            //��(�ĵ�2)
            spriteBatch.Draw(m_�⺻_�ĵ�2, new Rectangle(0, 768 * 2, 1366, 768), Color.White);
            DrawTongue(spriteBatch);
            DrawFood(spriteBatch);
            DrawMouth(spriteBatch);

            for (int i = 0; i < m_FoodList.Count; i++)
            {
                //������ ���Ĺ� isdropdown �� true�� �� && ��� ���Ĺ����� ������ �� ����߷�����  ���� �ĵ��������� �Ѿ����.
                //�� Update�� ���� �ʴ´�.
                if (m_FoodList[i].m_IsDropDown)
                    m_IsAllDropDownList[i] = true;
            }

            //���Ĺ��� �� �Ѿ�� �ڵ����� �ĵ��� �Ѿ���� �Ѵ�.
            if (m_FoodList.Count == m_FoodNumber && m_FoodList[m_FoodNumber - 1].m_FoodPosition.Y > 768 && m_IsAllDropDownList.Contains(false) == false)
            //if (!m_IsAllDropDownList.Contains(false))
            {
                spriteBatch.Draw(m_ClearImage, new Rectangle(0, 0, 1126, 768), Color.White);
                //spriteBatch.DrawString(m_Font, "Clear!", new Vector2(600, 400), Color.White);

                //3�� �ڿ� ���� ������ �ĵ��� ������ �Ѵ�.
                if (m_MouthTimingCount++ >= 180)
                {
                    m_PreviousMapPosition = m_CurrentMapPosition;
                    m_CurrentMapPosition = MapDirection.�ĵ�;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }
        int m_MouthTimingCount = 0;

        private void DrawMouth(SpriteBatch spriteBatch)
        {
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
                //���� ���� �� ���Ĺ��� ���� ������ ������ m_IsFoodAttack = true;
                if (m_FoodList[i].m_IsFoodAttack)
                {
                    if (m_CurrentMapPosition == MapDirection.��)
                    {
                        //���Ĺ��� ������������ ������ ��
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
                }
                //���� �����ִ� ����
                else
                {
                    //���� �����ִ� ���¿��� ���Ĺ��� �׾����� ��
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

        int GoStomach = 8800;
        #region Draw_�ĵ�
        protected void DrawGullet(GameTime gameTime, SpriteBatch spriteBatch)
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
                    spriteBatch.Draw(m_Life, new Rectangle((int)m_FairyPosition.X + 50 * (i + 1), (int)m_FairyPosition.Y + 50, 50, 50), Color.White);

            }

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
            else if (����� == 1)
            {
                if (m_TimingCount++ < 180)
                {
                    m_FairyPosition.Y = 8800;
                    spriteBatch.Draw(m_ClearImage, new Rectangle(0, (int)m_FairyPosition.Y - 200, 1126, 768), Color.White);
                    //����

                    RemainMotion();

                    if (GoStomachTime++ > 10)
                    {
                        GoStomachTime = 0;

                        for (int i = 0; i < m_FoodStackOnMony.Count; i++)
                        {
                            switch (EatFoodType[i])
                            {
                                case 0:
                                    spriteBatch.Draw(m_AppleRemains,
                                                         new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
                                                         new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                         Color.White);
                                    break;
                                case 1:
                                    spriteBatch.Draw(m_CheeseRemains,
                                                         new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
                                                         new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                         Color.White);
                                    break;
                                case 2:
                                    spriteBatch.Draw(m_MeatRemains,
                                                         new Rectangle((int)m_FairyPosition.X, (int)GoStomach - 35 * (i + 1), m_FixedFoodSize, m_FixedFoodSize),
                                                         new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                         Color.White);
                                    break;
                            }
                        }
                    }
                }

                else
                {
                    m_CameraPosition.Y = 9500;
                    m_CurrentMapPosition = MapDirection.��;
                }
            }
            //���ʽ�, �� ȿ���ֱ�
            Effect_BonusAndOops(spriteBatch);

            CountDown(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }
        int m_TimingCount = 0;
        int m_RemainsMotionIndex;
        int m_RemainsWaitTime;
        int ����� = 0;
        int GoStomachTime = 0;

        private void ��������ܿ����������(SpriteBatch spriteBatch)
        {
            RemainMotion();

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

            for (int a = 0; a < 12; a++)
                spriteBatch.Draw(m_ThroatMap[a], new Rectangle(0, 768 * a, 1366, 768), Color.White);
            //��
            spriteBatch.Draw(m_�⺻_��, new Rectangle(0, 768 * 12, 1366, 768), Color.White);

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

        #region Draw_��
        protected void DrawStomach(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(Swater1, Vector2.Zero, Color.White);
            StomachDrawFood(spriteBatch);
            MonyDraw(spriteBatch);
            DrawVirus(spriteBatch);

            spriteBatch.Draw(Swater2, new Rectangle(0, 200, Swater2.Width, Swater2.Height - 200), new Color(160, 160, 160, 160));
            spriteBatch.Draw(StomachMap, Vector2.Zero, Color.White);
            spriteBatch.DrawString(m_Font3, "Time" + time, Vector2.Zero, Color.White);

            if (IsStomachClear)
            {
                spriteBatch.Draw(m_ClearImage, new Rectangle(0, 0, 1126, 768), Color.White);
                //spriteBatch.DrawString(m_Font, "Clear!", new Vector2(600, 400), Color.White);
                //3�� �ڿ� ���� ������ ������������ ������ �Ѵ�.

            }
            spriteBatch.End();
            base.Draw(gameTime, spriteBatch);
        }

        private void DrawVirus(SpriteBatch spriteBatch)
        {
            int alpa;
            for (int VirusNumber = 0; VirusNumber < KillVirusNumber; VirusNumber++)
            {
                if (VirusState[VirusNumber] == 4)
                {
                    for (alpa = 0; alpa < 5; alpa++)
                        if (VirusKillWaitTime++ > alpa * 2)
                            spriteBatch.Draw(mVirus, new Rectangle((int)VirusPosition[VirusNumber].X, (int)VirusPosition[VirusNumber].Y, 64, 64), new Rectangle(128 * VirusFrameCounter, 0, 128, 128), new Color(255, 255, 255, 50 * (5 - alpa)));
                    if (alpa == 5)
                        if (VirusKillWaitTime++ > alpa * 2)
                            VirusState[VirusNumber] = 5;
                }

                else if (VirusState[VirusNumber] == 5)
                    VirusPosition[VirusNumber] = new Vector2(0, 0);
                else
                    spriteBatch.Draw(mVirus, new Rectangle((int)VirusPosition[VirusNumber].X, (int)VirusPosition[VirusNumber].Y, 64, 64), new Rectangle(128 * VirusFrameCounter, 0, 128, 128), Color.White);
            }
        }

        int m_ź����WaitTime;
        int m_ź����MotionIndex;

        private void StomachDrawFood(SpriteBatch spriteBatch)
        {
            ź����Motion();
            RemainMotion();

            for (int FoodCount = 0; FoodCount < StomachFoodNumber; FoodCount++)
            {
                Random DieView = new Random();
                int DieFoodMove = DieView.Next(1, 3);
                if (FoodState[FoodCount] == 3 || FoodState[FoodCount] == 4)
                {
                    if (FoodDropPosition[FoodCount].Y < 470)
                    {//���
                        switch (EatFoodType[FoodCount])
                        {
                            case 0:
                                spriteBatch.Draw(m_AppleRemains,
                                                     new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                     new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                     Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(m_CheeseRemains,
                                                     new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                     new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                     Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(m_MeatRemains,
                                                     new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                     new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                     Color.White);
                                break;
                        }
                    }
                    else
                    {
                        for (int DieFoodNumber = FoodCount * 3; DieFoodNumber < FoodCount * 3 + 3; DieFoodNumber++)
                        {
                            switch (DieFoodKind[DieFoodNumber])
                            {
                                case 1:
                                    spriteBatch.Draw(�ܹ���,
                                                         new Rectangle((int)FoodDropPosition[FoodCount].X + 50 * (DieFoodNumber + (-1 - (3 * FoodCount))), (int)FoodDropPosition[FoodCount].Y, 50, 50),
                                                         new Rectangle(�ܹ���.Width / 2 * m_ź����MotionIndex, 0, �ܹ���.Width / 2, �ܹ���.Height),
                                                         Color.White);
                                    break;
                                case 2:
                                    spriteBatch.Draw(ź��ȭ��,
                                                         new Rectangle((int)FoodDropPosition[FoodCount].X + 50 * (DieFoodNumber + (-1 - (3 * FoodCount))), (int)FoodDropPosition[FoodCount].Y, 50, 50),
                                                         new Rectangle(ź��ȭ��.Width / 2 * m_ź����MotionIndex, 0, ź��ȭ��.Width / 2, ź��ȭ��.Height),
                                                         Color.White);
                                    break;
                                case 3:
                                    spriteBatch.Draw(����,
                                                         new Rectangle((int)FoodDropPosition[FoodCount].X + 50 * (DieFoodNumber + (-1 - (3 * FoodCount))), (int)FoodDropPosition[FoodCount].Y, 50, 50),
                                                         new Rectangle(����.Width / 2 * m_ź����MotionIndex, 0, ����.Width / 2, ����.Height),
                                                         Color.White);
                                    break;
                            }
                        }
                    }
                }
                else
                {//���
                    switch (EatFoodType[FoodCount])
                    {
                        case 0:
                            spriteBatch.Draw(m_AppleRemains,
                                                 new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                 new Rectangle(m_AppleRemains.Width / 3 * m_RemainsMotionIndex, 0, m_AppleRemains.Width / 3, m_AppleRemains.Height),
                                                 Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(m_CheeseRemains,
                                                 new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                 new Rectangle(m_CheeseRemains.Width / 3 * m_RemainsMotionIndex, 0, m_CheeseRemains.Width / 3, m_CheeseRemains.Height),
                                                 Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(m_MeatRemains,
                                                 new Rectangle((int)FoodDropPosition[FoodCount].X, (int)FoodDropPosition[FoodCount].Y, 64, 64),
                                                 new Rectangle(m_MeatRemains.Width / 3 * m_RemainsMotionIndex, 0, m_MeatRemains.Width / 3, m_MeatRemains.Height),
                                                 Color.White);
                            break;
                    }
                }
            }
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

        int m_SideFrameCounter = 0;
        int m_SideFrameWaitTIme = 0;
        int m_FrontFrameCounter = 0;

        private void MonyDraw(SpriteBatch spriteBatch)
        {
            MonyMotion();

            if (m_MonyState == MonyState.ATTACKED)
            {
                switch (m_MonyDirection)
                {
                    case MonyDirection.LEFT:
                        if (mstate++ > 10)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 30, (int)m_MonyPosition.Y - 15, 70, 70), new Color(255, 255, 255, 255));
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Yellow, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (mstate++ > 20)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 30, (int)m_MonyPosition.Y - 15, 70, 70), new Color(180, 180, 180, 180));
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Red, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (mstate++ > 30)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 30, (int)m_MonyPosition.Y - 15, 70, 70), new Color(130, 130, 130, 130));
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Yellow, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (mstate++ > 40)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 30, (int)m_MonyPosition.Y - 15, 70, 70), new Color(80, 80, 80, 80));
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Red, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (mstate++ > 50)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 30, (int)m_MonyPosition.Y - 15, 70, 70), new Color(0, 0, 0, 0));
                        m_MonyState = MonyState.IDLE;
                        break;
                    case MonyDirection.RIGHT:
                        if (mstate++ > 10)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), null, new Color(255, 255, 255, 255), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Yellow);
                        if (mstate++ > 20)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), null, new Color(180, 180, 180, 180), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Red);
                        if (mstate++ > 30)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), null, new Color(130, 130, 130, 130), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Yellow);
                        if (mstate++ > 40)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), null, new Color(80, 80, 80, 80), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.Red);
                        if (mstate++ > 50)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), null, new Color(0, 0, 0, 0), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        m_MonyState = MonyState.IDLE;
                        break;
                    case MonyDirection.Front:
                        if (mstate++ > 10)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), new Color(255, 255, 255, 255));
                        spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_FrontFrameCounter, 0, 200, 200), Color.Yellow);
                        if (mstate++ > 20)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), new Color(180, 180, 180, 180));
                        spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_FrontFrameCounter, 0, 200, 200), Color.Red);
                        if (mstate++ > 30)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), new Color(130, 130, 130, 130));
                        spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_FrontFrameCounter, 0, 200, 200), Color.Yellow);
                        if (mstate++ > 40)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), new Color(80, 80, 80, 80));
                        spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_FrontFrameCounter, 0, 200, 200), Color.Red);
                        if (mstate++ > 50)
                            spriteBatch.Draw(��Ϻ�, new Rectangle((int)m_MonyPosition.X + 10, (int)m_MonyPosition.Y - 15, 70, 70), new Color(0, 0, 0, 0));
                        m_MonyState = MonyState.IDLE;
                        break;
                }
            }

            else if (m_MonyState == MonyState.IDLE)
            {
                switch (m_MonyDirection)
                {
                    case MonyDirection.LEFT:
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        break;
                    case MonyDirection.RIGHT:
                        spriteBatch.Draw(��Ͽ�, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_SideFrameCounter, 0, 200, 200), Color.White);
                        break;
                    case MonyDirection.Front:
                        spriteBatch.Draw(m_FairyImage, new Rectangle((int)m_MonyPosition.X, (int)m_MonyPosition.Y, 100, 100), new Rectangle(200 * m_FrontFrameCounter, 0, 200, 200), Color.White);
                        break;
                }
            }

            if (FireBall_Direction == FireBallDirection.shot)
            {
                switch (m_MonyDirection)
                {
                    case MonyDirection.LEFT:
                        spriteBatch.Draw(Fire, new Rectangle((int)FireBall.X, (int)FireBall.Y, 40, 40), new Rectangle(32 * ball, 0, 32, 32), Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        break;
                    case MonyDirection.RIGHT:
                        spriteBatch.Draw(Fire, new Rectangle((int)FireBall.X, (int)FireBall.Y, 40, 40), new Rectangle(32 * ball, 0, 32, 32), Color.White);
                        break;
                }
            }
            else if (FireBall_Direction == FireBallDirection.end)
            {
                spriteBatch.Draw(Fire, new Rectangle((int)FireBall.X, (int)FireBall.Y, 0, 0), Color.White);
            }
        }

        private void MonyMotion()
        {
            if (m_SideFrameWaitTIme++ > 3)
            {
                m_SideFrameWaitTIme = 0;

                if (m_SideFrameCounter++ >= 5)
                    m_SideFrameCounter = 0;

                if (m_FrontFrameCounter++ >= 2)
                    m_FrontFrameCounter = 0;
            }
        }
        #endregion



        #region Draw_��������
        protected void DuodenumDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(new Color(252, 188, 144));

            spriteBatch.Begin();
            spriteBatch.Draw(m_DuodenumColorBackground, Vector2.Zero, Color.White);

            //����
            spriteBatch.Draw(m_BackGroundColor, new Rectangle(0, 0, 1126, 768), Color.White);
            //������
            spriteBatch.Draw(m_������, new Rectangle(0, 0, m_������.Width, m_������.Height), Color.White);

            ��or�����ϰ��Draw(spriteBatch);

            //�������� ���
            spriteBatch.Draw(m_��������, new Rectangle(0, 0, m_��������.Width, m_��������.Height), Color.White);
            spriteBatch.Draw(m_����������, new Rectangle(0, 0, m_��������.Width, m_��������.Height), Color.White);
            ���ھ�or������Draw(spriteBatch);
            //��ü �� ��
            //spriteBatch.Draw(m_BodyMap, new Rectangle(1140, 0, 240, 768), Color.White);

            Monster_���¿�����Draw(spriteBatch);

            if (m_��������Clear�׷�����)
                spriteBatch.Draw(m_ClearImage, new Rectangle(0, 0, 1126, 768), Color.White);
            //spriteBatch.DrawString(m_Font, "(" + m_MouseX + "," + m_MouseY + ")", new Vector2(0, 0), Color.Black);
            // spriteBatch.DrawString(m_Font, "Score :    " + d_Score.ToString(), new Vector2(500, 300), Color.Black);
            spriteBatch.End();


            base.Draw(gameTime, spriteBatch);
        }
        int wwaittime = 0;

        private void ���ھ�or������Draw(SpriteBatch spriteBatch)
        {
            if (water_direction == WaterDirection.������)
            {
                spriteBatch.Draw(WaterImage, new Rectangle(260, 250, 120, 80), new Rectangle(60 * WaterFrameCounter, 0, 60, 40), Color.Red, 0.7f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                if (wwaittime++ > 6)
                {
                    wwaittime = 0;
                    m_diretion = MDirection.None;
                    water_direction = WaterDirection.None;
                }
            }

            else if (water_direction == WaterDirection.���ھ�)
            {
                spriteBatch.Draw(WaterImage, new Rectangle(450 - 120, 400, 120, 80), new Rectangle(60 * WaterFrameCounter, 0, 60, 40), Color.Yellow);
                if (wwaittime++ > 6)
                {
                    wwaittime = 0;
                    water_direction = WaterDirection.None;
                }
            }
        }

        private void ��or�����ϰ��Draw(SpriteBatch spriteBatch)
        {
            if (m_diretion == MDirection.��)
            {
                spriteBatch.Draw(m_������, new Rectangle(0, 0, m_������.Width, m_������.Height), Color.White);
                //spriteBatch.Draw(WaterImage, new Vector2(230, 260), new Rectangle(0, 0, 60, 40), Color.White);
                if (clickwaittmime++ > 5)
                {
                    clickwaittmime = 0;
                    m_diretion = MDirection.None;
                    water_direction = WaterDirection.None;
                }
            }

            else if (m_diretion == MDirection.����)
            {
                spriteBatch.Draw(m_���ڱ���, new Rectangle(0, 0, m_���ڱ���.Width, m_���ڱ���.Height), Color.White);
                spriteBatch.Draw(WaterImage, new Vector2(450, 400), new Rectangle(60 * WaterFrameCounter, 40, 60, 40), Color.White);
                if (clickwaittmime++ > 5)
                {
                    clickwaittmime = 0;
                    m_diretion = MDirection.None;
                }
            }
        }

        int m_NutrientWaitTime;
        int m_NutrientMotionIndex;

        private void Monster_���¿�����Draw(SpriteBatch spriteBatch)
        {
            NutrientMotion();
            ź����Motion();

            for (int i = 0; i < HowManyMonsters; i++)
            {
                switch (FatState[i])
                {
                    case 1:
                        spriteBatch.Draw(�ܹ���,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���.Width / 2 * m_ź����MotionIndex, 0, �ܹ���.Width / 2, �ܹ���.Height),
                                             Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(ź��ȭ��,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���.Width / 2 * m_ź����MotionIndex, 0, �ܹ���.Width / 2, �ܹ���.Height),
                                             Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(����,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���.Width / 2 * m_ź����MotionIndex, 0, �ܹ���.Width / 2, �ܹ���.Height),
                                             Color.White);
                        break;
                    case 4:
                        spriteBatch.Draw(�����ѹ�,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���.Width / 2 * m_ź����MotionIndex, 0, �ܹ���.Width / 2, �ܹ���.Height),
                                             Color.White);
                        break;
                    case 11:
                        spriteBatch.Draw(�ܹ���Nutrient,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                        break;
                    case 12:
                        spriteBatch.Draw(ź��ȭ��Nutrient,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                        break;
                    case 14:
                        spriteBatch.Draw(����Nutrient,
                                             new Rectangle((int)FatPosition[i].X, (int)FatPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                        break;
                }
            }
        }

        private void ź����Motion()
        {
            if (m_ź����WaitTime++ > 10)
            {
                m_ź����WaitTime = 0;

                if (m_ź����MotionIndex++ >= 1)
                    m_ź����MotionIndex = 0;
            }
        }

        private void NutrientMotion()
        {
            if (m_NutrientWaitTime++ > 10)
            {
                m_NutrientWaitTime = 0;
                if (m_NutrientMotionIndex++ >= 3)
                    m_NutrientMotionIndex = 0;
            }
        }
        #endregion

        #region Draw_����
        protected void SmallIntestineDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_SmallIntestineColorBackground, Vector2.Zero, Color.White);
            spriteBatch.Draw(m_SmallIntestineBackground, Vector2.Zero, Color.White);

            DrawVillus(spriteBatch);

            for (var i = 0; i < m_�������忡���Ѿ�¿���Ұ���; i++)
            {
                if (m_IsFeeded[i] == false)
                {
                    if (����Nutrient[i] == 11)
                        spriteBatch.Draw(�ܹ���Nutrient,
                                             new Rectangle((int)m_NutrientPosition[i].X, (int)m_NutrientPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                    if (����Nutrient[i] == 12)
                        spriteBatch.Draw(ź��ȭ��Nutrient,
                                             new Rectangle((int)m_NutrientPosition[i].X, (int)m_NutrientPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                    if (����Nutrient[i] == 14)
                        spriteBatch.Draw(����Nutrient,
                                             new Rectangle((int)m_NutrientPosition[i].X, (int)m_NutrientPosition[i].Y, 50, 50),
                                             new Rectangle(�ܹ���Nutrient.Width / 4 * m_NutrientMotionIndex, 0, �ܹ���Nutrient.Width / 4, �ܹ���Nutrient.Height),
                                             Color.White);
                }
            }

            spriteBatch.Draw(m_SmallIntestineForeground, Vector2.Zero, Color.White);

            //spriteBatch.Draw(m_TotalMap, new Vector2(1126, 0), Color.White);

            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }

        private void DrawVillus(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < 17; i++)
            {
                //������ ������� �ʾ��� ��
                if (!m_IsAbsorb[i])
                {
                    switch (i)
                    {
                        case 1:
                        case 3:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 6:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, -1.5f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 7:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 2.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 9:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 11:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 12:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, -0.9f, Vector2.Zero, SpriteEffects.None, 0);
                            break;
                        case 14:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 16:
                            spriteBatch.Draw(m_Villus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        default:
                            spriteBatch.Draw(m_Villus2, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), Color.White);
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                        case 3:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 6:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, -1.5f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 7:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 2.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 9:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 11:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 12:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, -0.9f, Vector2.Zero, SpriteEffects.None, 0);
                            break;
                        case 14:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        case 16:
                            spriteBatch.Draw(m_FeedVirus, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                            break;
                        default:
                            spriteBatch.Draw(m_FeedVirus2, new Rectangle((int)m_VirusPosition[i].X, (int)m_VirusPosition[i].Y, 50, 90), Color.White);
                            break;
                    }
                }

                //spriteBatch.Draw(m_Rect, m_VillusCollisionRect[i], Color.White);
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
                               //4��° ��
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
                               //00��° ��
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
                               //00��° ��
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
