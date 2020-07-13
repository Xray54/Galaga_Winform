using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// 스페이스를 누르면 총알을 발사한다
    /// </summary>
    public class BulletShooter : Component
    {
        public BulletShooter(GameObject gameObject) : base(gameObject) { }
        /// <summary>
        /// 발사 쿨타임을 정한다
        /// </summary>
        public float FireCoolTime { get; set; } = 0.2f;
        float lastFireTime;

        public override void Update()
        {
             if (InputWinform.Instance.GetKeyDown(Keys.Space))
            {
                if(lastFireTime + FireCoolTime < GameEngine.Instance.Time)
                {
                    lastFireTime = GameEngine.Instance.Time;
                    FireBullet();
                }
            }
        }

        /// <summary>
        /// 총알을 생성하고 좌표를 위로 조금 올려준다.
        /// </summary>
        void FireBullet()
        {
            PlayerBullet bullet = GameObject.Instantiate<PlayerBullet>();

            Vec2D bulletPoint = gameObject.transform.position;
            bulletPoint.Y -= 8;
            bullet.transform.position = bulletPoint;

            DamageSystem damageSystem = bullet.GetComponent<DamageSystem>();
            damageSystem.EventGiveDamage += () => GameObject.Destroy(bullet);

            GameManager.Instance.IncreaseFireCount(1);
        }
    }
}