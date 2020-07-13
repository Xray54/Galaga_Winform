using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// 화면 밖에 나갔는지 확인하고 나갔으면 오브젝트를 삭제한다
    /// 총알이 화면 나가면 지우기 위한 컴포넌트
    /// </summary>
    class LimitLocationDelete : Component
    {
        public LimitLocationDelete(GameObject gameObj) : base(gameObj) { }
        public Size LimitSize { get; set; }
        private Size objectHlafSize;


        public override void Start()
        {
            GameObject background = GameObject.Find("BackGround");
            if (background == null)
            {
                Debug.WriteLine("BackGround가 없습니다. (LimitLocationDelete)");
                return;
            }
            LimitSize = background.GetComponent<SpriteComponent>().Image.Size;


            SpriteComponent sprite = gameObject.GetComponent<SpriteComponent>();
            if (sprite == null)
            {
                Debug.WriteLine("오브젝트 크기를 가져올 스트라이프가 없습니다. LimitLocationDelete()");
                return;
            }
            objectHlafSize = new Size(sprite.Image.Size.Width / 2, sprite.Image.Size.Height / 2);
        }

        public override void Update()
        {
            if (gameObject.transform.position.X - objectHlafSize.Width <= 0 || gameObject.transform.position.X + objectHlafSize.Width >= LimitSize.Width ||
                gameObject.transform.position.Y - objectHlafSize.Height <= 0 || gameObject.transform.position.Y + objectHlafSize.Height >= LimitSize.Height)
            {
                GameObject.Destroy(gameObject, 0);
            }
            
        }
    }
}
