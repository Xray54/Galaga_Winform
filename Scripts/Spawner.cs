using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    public enum StartPoint
    {
        LeftTop,
        RightTop,
        SideLeftBottom,
        SideRightBottom,
        BottomLeft,
        BottomRight
    }
    /// <summary>
    /// 몬스터를 소환하고 
    /// </summary>
    class Spawner : Component
    {
        public Spawner(GameObject gameObject) : base(gameObject) { }


        #region 싱글톤
        private static Spawner m_instance;
        public static Spawner Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<Spawner>();
                }
                return m_instance;
            }
        }
        #endregion

        /// <summary>
        /// 몬스터를 생성한다.
        /// </summary>
        /// <typeparam name="T">생성할 몬스터 타입</typeparam>
        /// <param name="spawnPoint">생서할 위치</param>
        /// <param name="pattern">베지어 곡선에 넣을 패턴</param>
        /// <param name="destination">베지어 곡선에 넣을 도착지점</param>
        /// <returns>생성한 오브젝트</returns>
        public T SpawnEnemy<T>(StartPoint spawnPoint, WayPoints.ShapeTypeTable pattern,  Transform destination) where T : GameObject, new()
        {
            T enemy = GameObject.Instantiate<T>();

            BezierCurveMove moveType = enemy.GetComponent<BezierCurveMove>();
            if (moveType != null)
            {
                moveType.WayPoints = WayPoints.Instance.GetWayPoint(GetLocation(spawnPoint), pattern);
                moveType.Destination = destination;
            }
            return enemy;
        }

        /// <summary>
        /// StartPoint에 지정된 좌표를 반환해준다.
        /// </summary>
        /// <param name="dir">불러올 좌표 위치</param>
        /// <returns>해당좌표</returns>
        public Vec2D GetLocation(StartPoint dir)
        {
            switch (dir)
            {
                case StartPoint.LeftTop:
                    return new Vec2D(112 - 16, 0);
                case StartPoint.RightTop:
                    return new Vec2D(112 + 16, 0);
                case StartPoint.SideLeftBottom:
                    return new Vec2D(0, 240);
                case StartPoint.SideRightBottom:
                    return new Vec2D(224, 240);
                case StartPoint.BottomLeft:
                    return new Vec2D(112 - 32, 320);
                case StartPoint.BottomRight:
                    return new Vec2D(112 + 32, 320);
            }
            return new Vec2D(0, 0);
        }
    }
}
