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
    public class ScrollingComponent : Component
    {
        float Speed = 50f;
        public bool ScrollX { get; set; } = false;
        public bool ScrollY { get; set; } = false;
        public bool XAddNegative { get; set; } = false;
        public bool YAddNegative { get; set; } = false;
        public ScrollingComponent(GameObject gameObject) : base(gameObject)
        {

        }
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