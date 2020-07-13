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
    /// 적군 총알 프리팹
    /// </summary>
    class EnemyBullet : GameObject
    {
        public EnemyBullet()
        {
            transform.Rotation = 180;

            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.EnemyBullet;
            AddComponent(spriteComponent);

            BoxCollider boxCollider = new BoxCollider(this);
            Size boxSize = new Size();
            boxSize.Width = 3;
            boxSize.Height = 8;
            boxCollider.Size = boxSize;
            AddComponent(boxCollider);

            TargetScrolling scrolling = new TargetScrolling(this);
            scrolling.Speed = 150f;
            var component = GameObject.FindObjectOfType<BulletShooter>();
            if (component != null)
            {
                scrolling.Destination = component.gameObject.transform.position;
            }
            AddComponent(scrolling);

            LimitLocationDelete limit = new LimitLocationDelete(this);
            AddComponent(limit);

            DamageSystem damageSystem = new DamageSystem(this);
            damageSystem.HitAbleTag = "Player";
            AddComponent(damageSystem);

        }
    }
}
