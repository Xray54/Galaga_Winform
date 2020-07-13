using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Galaga.Properties;
using System.Drawing;
using Engine;
using Galaga.Scripts;

namespace Galaga
{
    /// <summary>
    /// 백그라운드 프리팹
    /// 프리팹에는 생성자에서 삽입할 컴포넌트를 생성해서 삽입하면 된다.
    /// </summary>
    class BackGround : GameObject
    {
        public BackGround()
        {
            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.BackGroundStar2;
            AddComponent(spriteComponent);

            // 화면 내리는 효과를 주기 위한 컴포넌트
            ScrollingComponent scrolling = new ScrollingComponent(this);
            scrolling.ScrollY = true;                 
            scrolling.Speed = 150;                 
            AddComponent(scrolling);

            // 애니메이션 컴포넌트
            AnimationSprite animationSprite = new AnimationSprite(this);
            animationSprite.ImageList.Add(Resources.BackGroundStar1);
            animationSprite.ImageList.Add(Resources.BackGroundStar2);
            animationSprite.ImageList.Add(Resources.BackGroundStar3);
            animationSprite.ImageChangeInterval = 0.2f;
            AddComponent(animationSprite);

            AddComponent(new BackGroundRePos(this));
        }
    }
}

