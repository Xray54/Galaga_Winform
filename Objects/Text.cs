using Engine;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    /// <summary>
    /// 텍스트를 출력하기 위한 컴포넌트
    /// </summary>
    class Text : GameObject
    {
        public Text()
        {
            Vec2D point = new Vec2D(100, 200);
            transform.position = point;

            TextComponet textComponet = new TextComponet(this);
            textComponet.Text = "0";
            AddComponent(textComponet);
        }
    }
}
