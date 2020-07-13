using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 출력할 이미지를 가지고 있는다
    /// 랜더는 이 컴포넌트가 있는지 확인후 있으면 Image를 출력한다.
    /// </summary>
    public class SpriteComponent : Component
    {
        public SpriteComponent(GameObject gameObj) : base(gameObj) { }

        public Image Image { get; set; }
    }
}
