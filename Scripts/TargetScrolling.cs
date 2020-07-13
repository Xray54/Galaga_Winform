using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace Galaga
{
    /// <summary>
    /// 목표된 지점까지 이동한다.
    /// 이동완료후에는 OnReachDestination 이벤트가 발생된다.
    /// </summary>
    public class TargetScrolling : Component
    {
        /// <summary>
        /// 이동할 스피드
        /// </summary>
        public float Speed { get; set; } = 50f;
        /// <summary>
        /// 이동할 좌표
        /// </summary>
        public Vec2D Destination { get; set; }
        /// <summary>
        /// 목표도달시 실행할 이벤트
        /// </summary>
        public event Action OnReachDestination;
        /// <summary>
        /// 이동할 방향벡터
        /// </summary>
        private Vec2D? direction;
        public TargetScrolling(GameObject gameObject) : base(gameObject) { }

        public override void Update()
        {
            if (!direction.HasValue)
            {
                GetDirection();
            }

            Vec2D nextPoint = gameObject.transform.position;

            nextPoint += direction.Value * Speed * GameEngine.Instance.DeltaTime;

            gameObject.transform.LookAt(nextPoint);

            gameObject.transform.position = nextPoint;

            if (OnReachDestination != null && gameObject.transform.position > Destination)
            {
                OnReachDestination?.Invoke();
            }
        }
        private void GetDirection()
        {
            direction = (Destination - gameObject.transform.position).normalized();
        }
    }
}