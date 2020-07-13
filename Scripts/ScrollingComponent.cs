using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace Galaga
{
    /// <summary>
    /// 지정된 방향으로 오브젝트를 계속 이동시켜주는 컴포넌트
    /// </summary>
    public class ScrollingComponent : Component
    {
        public ScrollingComponent(GameObject gameObject) : base(gameObject) { }
        /// <summary>
        /// 1초에 이동할 수치
        /// </summary>
        public float Speed { get; set; } = 50f;
        /// <summary>
        /// X,Y 로 이동할 유무
        /// </summary>
        public bool ScrollX { get; set; } = false;
        public bool ScrollY { get; set; } = false;
        /// <summary>
        /// 반대방향으로 이동할지 설정 여부
        /// 기본은 좌표에 속도를 +한다
        /// </summary>
        public bool XAddNegative { get; set; } = false;
        public bool YAddNegative { get; set; } = false;
        public override void Update()
        {
            Vec2D nextPoint = gameObject.transform.position;

            if(ScrollX)
            {
                if (XAddNegative)
                {
                    nextPoint.X -= Speed * GameEngine.Instance.DeltaTime;
                }
                else
                {
                    nextPoint.X += Speed * GameEngine.Instance.DeltaTime;
                }
            }

            if (ScrollY)
            {
                if (YAddNegative)
                {
                    nextPoint.Y -= Speed * GameEngine.Instance.DeltaTime;
                }
                else
                {
                    nextPoint.Y += Speed * GameEngine.Instance.DeltaTime;
                }
            }

            gameObject.transform.position = nextPoint;
        }
    }
}