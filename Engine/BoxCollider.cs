using Engine;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{

    /// <summary>
    /// 박스 콜라이더
    /// 모든 Enable된 콜라이더와 충돌 검사후 충돌된 오브젝트는 ContactObects안에 넣어줌
    /// 매 업데이트마다 ContactObects 갱신
    /// </summary>
    public class BoxCollider : Component, ICollider
    {
        /// <summary>
        /// 충돌한 오브젝트들을 저장하는 리스트
        /// </summary>
        public List<GameObject> ContactObects { get; private set; } = new List<GameObject>();
        public Vec2D Offset { get; set; }

        /// <summary>
        /// 히트 박스의 전체 크기
        /// 10이면 가운데 좌표부터 오른쪽 끝이 5이다
        /// </summary>
        public Size Size { get; set; }

        public float Left { get { return gameObject.transform.position.X + Offset.X - Size.Width / 2; } }
        public float Top { get { return gameObject.transform.position.Y + Offset.Y - Size.Height / 2; } }
        public float Right { get { return gameObject.transform.position.X + Offset.X + Size.Width / 2; } }
        public float Bottom { get { return gameObject.transform.position.Y + Offset.Y + Size.Height / 2; } }
        public Vec2D LeftTop {  get { return new Vec2D(Left, Top); } }
        public RectangleF Box { get { return new RectangleF(Left, Top, Size.Width, Size.Height); } }

        public BoxCollider(GameObject gameObject) : base(gameObject)
        {
        }
        public override void Update()
        {
            ContactObects.Clear();

            List<GameObject> gameObjects = GameObject.GetGameObjects();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject other = gameObjects[i];
                if (other.collider != null &&
                    other.collider.Enabled &&
                    IsContectAABB(Box, other.collider.Box))
                {
                    ContactObects.Add(other);
                }
            }
        }
        bool IsContectAABB(RectangleF a,RectangleF b)
        {
            return (a.Left <= b.Right && a.Right >= b.Left) &&
                   (a.Top <= b.Bottom && a.Bottom >= b.Top);
        }
    }
}