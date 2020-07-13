using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    /// <summary>
    /// 베지어 곡선에 사용할 좌표들을 저장한다.
    /// </summary>
    class WayPoints : Component
    {
        public enum ShapeTypeTable
        {
            kDownTurn6,
            kDownTurnJ,
            kCircleUpRight,
            kCircleUpLeft,
            kSwingLeftDown,
            kSwingRightDown
        }
        public WayPoints(GameObject gameObject) : base(gameObject) { }

        #region 싱글톤
        private static WayPoints m_instance;
        public static WayPoints Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<WayPoints>();
                }
                return m_instance;
            }
        } 
        #endregion

        public Vec2D GameSize { get; set; }




        /// <summary>
        /// 시작위치를 넣어주면 모양에 맞게 다음 좌표들을 반환해준다.
        /// </summary>
        /// <param name="start">시작할 위치</param>
        /// <param name="shape">웨이포인트 형태</param>
        /// <returns>생성한 웨이포인트들</returns>
        public List<Vec2D> GetWayPoint(Vec2D start, ShapeTypeTable shape)
        {
            List<Vec2D> waypoints = new List<Vec2D>();
            waypoints.Add(start);
            switch (shape)
            {
                case ShapeTypeTable.kDownTurn6:
                    waypoints.Add(new Vec2D(start.X - 32, start.Y + 180));
                    waypoints.Add(new Vec2D(start.X - 96, start.Y + 180));
                    waypoints.Add(new Vec2D(start.X - 96, start.Y + 220));
                    waypoints.Add(new Vec2D(start.X - 32, start.Y + 220));
                    waypoints.Add(new Vec2D(start.X - 16, start.Y + 200));
                    break;
                case ShapeTypeTable.kDownTurnJ:
                    waypoints.Add(new Vec2D(start.X + 32, start.Y + 180));
                    waypoints.Add(new Vec2D(start.X + 96, start.Y + 180));
                    waypoints.Add(new Vec2D(start.X + 96, start.Y + 220));
                    waypoints.Add(new Vec2D(start.X + 32, start.Y + 220));
                    waypoints.Add(new Vec2D(start.X + 16, start.Y + 200));
                    break;
                case ShapeTypeTable.kCircleUpRight:
                    waypoints.Add(new Vec2D(start.X + GameSize.X / 2, start.Y));
                    waypoints.Add(new Vec2D(start.X + GameSize.X / 4 * 3, start.Y - GameSize.Y/3*2));
                    waypoints.Add(new Vec2D(start.X                 , start.Y - GameSize.Y / 3 * 2));
                    waypoints.Add(new Vec2D(start.X                 , start.Y - GameSize.Y / 6 )); 
                    waypoints.Add(new Vec2D(start.X + GameSize.X / 2, start.Y));
                    waypoints.Add(new Vec2D(start.X + GameSize.X / 2, start.Y - GameSize.Y / 3 ));

                    break;
                case ShapeTypeTable.kCircleUpLeft:
                    waypoints.Add(new Vec2D(start.X - GameSize.X / 2, start.Y));
                    waypoints.Add(new Vec2D(start.X - GameSize.X / 4 * 3, start.Y - GameSize.Y / 3 * 2));
                    waypoints.Add(new Vec2D(start.X, start.Y - GameSize.Y / 3 * 2));
                    waypoints.Add(new Vec2D(start.X, start.Y - GameSize.Y / 6));
                    waypoints.Add(new Vec2D(start.X - GameSize.X / 2, start.Y));
                    waypoints.Add(new Vec2D(start.X - GameSize.X / 2, start.Y - GameSize.Y / 3));
                    break;
                case ShapeTypeTable.kSwingLeftDown:
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 5, GameSize.Y / 10 * 4));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 4, GameSize.Y / 10 * 6));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 1, GameSize.Y / 10 * 4));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 5, 0));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 8, GameSize.Y / 10 * 6)); 
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 9, GameSize.Y / 10 * 12));
                    break;
                case ShapeTypeTable.kSwingRightDown:
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 5, GameSize.Y / 10 * 4));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 6, GameSize.Y / 10 * 6));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 9, GameSize.Y / 10 * 4));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 5, 0));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 2, GameSize.Y / 10 * 6));
                    waypoints.Add(new Vec2D(GameSize.X / 10 * 1, GameSize.Y / 10 * 12));
                    break;
                default:
                    break;
            }
            return waypoints;
        }
    }
}
