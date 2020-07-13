using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace Galaga
{
    /// <summary>
    /// 키보드가 A,D 눌리면 해당 방향으로 속도만큼 이동한다.
    /// 최대 이동 제한 좌표가 있다.
    /// </summary>
    public class InputComponent : Component
    {
        public InputComponent(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// 최대 이동할수 있는 좌표
        /// </summary>
        public int MaxWidth { get; set; }
        /// <summary>
        /// 1초에 움직일 속도
        /// </summary>
        public float MoveSpeed { get; set; } = 100f;
        private int imageWidthHlaf;

        public override void Start()
        {
            SpriteComponent sprite = gameObject.GetComponent<SpriteComponent>();
            if(sprite != null)
            {
                imageWidthHlaf = sprite.Image.Width/2;
            }
        }
        public override void Update()
        {
            if(InputWinform.Instance.GetKeyDown(Keys.A))
            {
                Vec2D point = gameObject.transform.position;

                point.X -= MoveSpeed * GameEngine.Instance.DeltaTime;
                
                if(point.X- imageWidthHlaf < 0)
                {
                    point.X = imageWidthHlaf;
                }

                gameObject.transform.position = point; 

            }

            if(InputWinform.Instance.GetKeyDown(Keys.D))
            {
                Vec2D point = gameObject.transform.position;

                point.X += MoveSpeed * GameEngine.Instance.DeltaTime;

                if (point.X + imageWidthHlaf > MaxWidth)
                {
                    point.X = MaxWidth - imageWidthHlaf;
                }

                gameObject.transform.position = point; 
            }
        }
    }
}