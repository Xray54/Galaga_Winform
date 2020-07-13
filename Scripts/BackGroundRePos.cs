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
    /// 배경화면을 특정 위치까지 내려가면 위로 되돌려줌
    /// </summary>
    public class BackGroundRePos : Component
    {
        float spriteHeight;
        public BackGroundRePos(GameObject gameObject) : base(gameObject) { }
        public override void Start()
        {
            SpriteComponent sprite = gameObject.GetComponent<SpriteComponent>();
            if(sprite == null)
            {
                Debug.WriteLine("BackGroundRePos 함수에서 로딩중에 Sprite컴포넌트가 없습니다.");
                return;
            }

            spriteHeight = sprite.Image.Height;
        }
        public override void Update()
        {
            if(gameObject.transform.position.Y - spriteHeight/2 >= spriteHeight)
            {
                Vec2D point = gameObject.transform.position;
                point.Y -= spriteHeight*2;
                gameObject.transform.position = point;
            }
        }
    }
}