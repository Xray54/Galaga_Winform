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
    /// 플레이어 프리팹
    /// </summary>
    class Player :GameObject
    {
        public Player()
        {
            Vec2D point = new Vec2D(100, 200);
            transform.position = point;

            Tag = "Player";

            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.player_1;
            AddComponent(spriteComponent);

            AnimationSprite animationSprite = new AnimationSprite(this);
            animationSprite.ImageList.Add(Resources.player_die_01);
            animationSprite.ImageList.Add(Resources.player_die_02);
            animationSprite.ImageList.Add(Resources.player_die_03);
            animationSprite.ImageList.Add(Resources.player_die_04);
            animationSprite.Enabled = false;
            animationSprite.SpriteComponent = spriteComponent;
            animationSprite.ImageChangeInterval = 0.1f;

            AddComponent(animationSprite);

            BoxCollider boxCollider = new BoxCollider(this);
            Size boxSize = spriteComponent.Image.Size;
            boxSize.Width -= 5;
            boxSize.Height -= 5;
            boxCollider.Size = boxSize;
            AddComponent(boxCollider);

            HealthSystem healthSystem = new HealthSystem(this);
            healthSystem.MaxHealth = 1;
            AddComponent(healthSystem);

            AddComponent(new InputComponent(this));


            AddComponent(new BulletShooter(this));

            TargetScrolling scrolling = new TargetScrolling(this);
            scrolling.Speed = 100;
            scrolling.Enabled = false;
            AddComponent(scrolling);
        }
    }
}
