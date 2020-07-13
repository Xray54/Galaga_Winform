using Engine;
using Galaga.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Galaga.Scripts;

namespace Galaga
{
    /// <summary>
    /// 플레이어 총알 프리팹
    /// </summary>
    class PlayerBullet : GameObject
    {
        public PlayerBullet()
        {
            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.PlayerBullet;
            AddComponent(spriteComponent);

            BoxCollider boxCollider = new BoxCollider(this);
            boxCollider.Size = spriteComponent.Image.Size;
            AddComponent(boxCollider);

            ScrollingComponent scrolling = new ScrollingComponent(this);
            scrolling.Speed = 200f;
            scrolling.ScrollY = true;
            scrolling.YAddNegative = true;
            AddComponent(scrolling);

            LimitLocationDelete limit = new LimitLocationDelete(this);
            AddComponent(limit);

            DamageSystem damageSystem = new DamageSystem(this);
            damageSystem.HitAbleTag = "Enemy";
            AddComponent(damageSystem);

        }
    }
}
