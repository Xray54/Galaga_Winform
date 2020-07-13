using Engine;
using Galaga.Properties;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    class BigBoss : GameObject
    {
        public BigBoss()
        {
            Vec2D point = new Vec2D(-50, -50);
            transform.position = point;

            Tag = "Enemy";

            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.Enemy_04_01_01;
            AddComponent(spriteComponent);

            AnimationSprite animationSprite = new AnimationSprite(this);
            animationSprite.ImageList.Add(Resources.Enemy_04_01_01);
            AddComponent(animationSprite);

            BoxCollider boxCollider = new BoxCollider(this);
            Size boxSize = spriteComponent.Image.Size;
            boxSize.Width -= 5;
            boxSize.Height -= 5;
            boxCollider.Size = boxSize;
            AddComponent(boxCollider);

            HealthSystem healthSystem = new HealthSystem(this);
            healthSystem.Health = 20;
            AddComponent(healthSystem);

            DamageSystem damageSystem = new DamageSystem(this);
            damageSystem.HitAbleTag = "Player";
            AddComponent(damageSystem);

            BezierCurveMove bezierCurveMove = new BezierCurveMove(this);
            AddComponent(bezierCurveMove);

            EnemyBulletShooter enemyBulletShooter = new EnemyBulletShooter(this);
            enemyBulletShooter.ShootMinRotation = 0;
            enemyBulletShooter.ShootMaxRotation = 0;
            enemyBulletShooter.FireRate = 0.25f;
            enemyBulletShooter.FireProbability = 75;
            AddComponent(enemyBulletShooter);
        }
    }
}
