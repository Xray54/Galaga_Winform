using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// 현재 위치한 좌표와 각도를 가지고 있는다.
    /// 만약 Parent를 설정하면
    /// </summary>
    public class Transform : Component
    {
        public Vec2D localPosition { get; set; }
        private Vec2D position_;
        public Vec2D position 
        {
            get
            {
                if (parent != null)
                {
                    return parent.transform.position + localPosition;
                }
                return position_;
            }
            set
            {
                Vec2D offset = position - value;
                position_ = value;
            }
        }
        public float Rotation { get; set; }
        private GameObject parent;
        public List<GameObject> ChildObjects { get; private set; } = new List<GameObject>();

        public Transform(GameObject gameObj) : base(gameObj)
        {
        }
        public bool HaveParent()
        {
            if (parent == null)
            {
                return false;
            }
            return true;
        }
        public void LookAt(Vec2D target)
        {
            float x = target.X - position.X;
            float y = target.Y - position.Y;
            Rotation = Convert.ToSingle(Math.Atan2(y, x) * 180 / Math.PI) + 90f;
        }
        public GameObject GetChild(int index)
        {
            return ChildObjects[index];
        }        
        public void SetChild(GameObject child)
        {
            ChildObjects.Add(child);
            child.transform.parent = this.gameObject;
        }
        public bool IsChildOf( Transform _parent)
        {
            return parent.transform == _parent;
        }
    }
}
