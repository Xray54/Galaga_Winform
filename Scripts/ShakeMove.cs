using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    /// <summary>
    /// 시작하는 위치를 Start에서 받아온다
    /// 그 시작하는 위치 기준으로
    /// Transform의 position을 X,Y 선택하여 좌우로 움직인다.
    /// </summary>
    class ShakeMove : Component
    {
        public float Speed { get; set; } = 16f;

        /// <summary>
        /// X,Y축으로 움직일 것인가 선택
        /// </summary>
        public bool ShakeX { get; set; } = true;
        public bool ShakeY { get; set; } = false;
        public float MinOffsetX { get; set; } = 32;
        public float MaxOffsetX { get; set; } = 32;
        public float MinOffsetY { get; set; } = 32;
        public float MaxOffsetY { get; set; } = 32;
        public bool DirectionRight { get; set; } = true;
        public bool DirectionDown { get; set; } = true;

        /// <summary>
        /// 시작할 위치를 Start에서 받아서 이 좌표 중심으로 좌우로 움직임
        /// </summary>
        private Vec2D startPosition;
        public ShakeMove(GameObject gameObject) : base(gameObject)
        {

        }
        public override void Start()
        {
            startPosition = gameObject.transform.position;
        }
        public override void Update()
        {
            Vec2D nextPoint = gameObject.transform.position;

            if (ShakeX)
            {
                if (DirectionRight)
                {
                    nextPoint.X += Speed * GameEngine.Instance.DeltaTime;
                    if (startPosition.X + MaxOffsetX  <= gameObject.transform.position.X)
                    {
                        DirectionRight = false;
                    }
                }
                else
                {
                    nextPoint.X -= Speed * GameEngine.Instance.DeltaTime;
                    if (startPosition.X - MaxOffsetX >= gameObject.transform.position.X)
                    {
                        DirectionRight = true;
                    }
                }
            }

            if (ShakeY)
            {
                if (DirectionDown)
                {
                    nextPoint.Y += Speed * GameEngine.Instance.DeltaTime;
                    if (startPosition.Y + MaxOffsetY <= gameObject.transform.position.Y)
                    {
                        DirectionDown = false;
                    }
                }
                else
                {
                    nextPoint.Y -= Speed * GameEngine.Instance.DeltaTime;
                    if (startPosition.Y - MaxOffsetY >= gameObject.transform.position.Y)
                    {
                        DirectionDown = true;
                    }
                }
            }

            gameObject.transform.position = nextPoint;
        }
    }
}
