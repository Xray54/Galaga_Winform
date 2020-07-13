using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// 베지어 커브를 구현
    /// 
    /// </summary>
    class BezierCurveMove :Component
    {
        public BezierCurveMove(GameObject gameObj) : base(gameObj) { }
        /// <summary>
        /// 경로를 지정해줌
        /// </summary>
        public List<Vec2D> WayPoints { get; set; } = new List<Vec2D>();
        /// <summary>
        /// 목적지를 지정해주면 경로까지 다 갔을경우 목적지를 계속 따라다닌다.
        /// </summary>
        public Transform Destination { get; set; }

        /// <summary>
        /// 1초당 진행속도 더해줄 속도를 정해준다.
        /// </summary>
        public float MoveSpeed { get; set; } = 0.4f;
        /// <summary>
        /// 베지어곡선 진행 정도 Max = 1
        /// </summary>
        public float CurrentProgress { get; set; } = 0f;

        /// <summary>
        /// 진행정도가 1이 되었을때 발생할 이벤트
        /// </summary>
        public event Action EventMoveDone;


        public override void Update()
        {
            if (WayPoints == null)
                return;

            // 진행이 완료되었으면 좌표를 계속 목적지에 설정한다
            if (CurrentProgress == 1)
            {
                if(Destination != null)
                    gameObject.transform.position = Destination.position;
                return;
            }

            CurrentProgress += MoveSpeed * GameEngine.Instance.DeltaTime;
            if (CurrentProgress >= 1)
            {
                CurrentProgress = 1;
            }
            Vec2D nextVec;
            if (Destination != null)
            {
                nextVec = GetPoint(WayPoints, Destination.position, CurrentProgress);
            }
            else
            {
                nextVec = GetPoint(WayPoints, CurrentProgress);
            }
            gameObject.transform.LookAt(nextVec);
            gameObject.transform.position = nextVec;
            if (CurrentProgress >= 1)
            {
                gameObject.transform.Rotation = 0;
                EventMoveDone?.Invoke();
                return;
            }
        }
        public void SetDestination(Transform destination)
        {
            Destination = destination;
        }

        public Vec2D GetPoint(in List<Vec2D> points, in Vec2D lastPoint, float progress)
        {
            if (progress > 1)
            {
                Debug.WriteLine($"(BezierCurveMove::GetPoint): progress 전달인자가 1이 넘습니다. progress:{progress}");
                return new Vec2D(0, 0);
            }
            float t = progress;

            points.Add(lastPoint);
            Vec2D[] pts = points.ToArray();
            points.RemoveAt(points.Count-1);

            int max_x = pts.Length - 1;
            for (int i = 0; i < max_x; i++)
            {
                int max_j = pts.Length - 1 - i;
                for (int j = 0; j < max_j; j++)
                {
                    pts[j] = pts[j] + t * (pts[j + 1] - pts[j]);
                }
            }
            return pts[0];
        }        
        public Vec2D GetPoint(in List<Vec2D> points, float progress)
        {
            if (points.Count == 0)
            {
                Debug.WriteLine(gameObject.ToString() );
                Debug.WriteLine(gameObject.transform.position.ToString() );
                Debug.WriteLine(WayPoints.Count);
            }

            if (progress > 1)
            {
                Debug.WriteLine($"(BezierCurveMove::GetPoint): progress 전달인자가 1이 넘습니다. progress:{progress}");
                return new Vec2D(0, 0);
            }
            float t = progress;

            Vec2D[] pts = points.ToArray();

            int max_x = pts.Length - 1;
            for (int i = 0; i < max_x; i++)
            {
                int max_j = pts.Length - 1 - i;
                for (int j = 0; j < max_j; j++)
                {
                    pts[j] = pts[j] + t * (pts[j + 1] - pts[j]);
                }
            }
            return pts[0];
        }
    }
}
