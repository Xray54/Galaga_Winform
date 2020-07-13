using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Galaga.Scripts
{
    /// <summary>
    /// 적이 총알을 쏘는것을 구현한다.
    /// 일정 각도가 되고 FireRate가 되돌아 오고
    /// 특정 확률까지 만족하면 발사한다.
    /// </summary>
    class EnemyBulletShooter : Component
    {
        /// <summary>
        /// 최대각도
        /// </summary>
        public float ShootMaxRotation { get; set; }
        /// <summary>
        /// 최소각도
        /// </summary>
        public float ShootMinRotation { get; set; }

        /// <summary>
        /// 발사할 속도 1.0f = 1초
        /// </summary>
        public float FireRate { get; set; }
        /// <summary>
        /// 발사할 확률 100 = 100% 기본값 50
        /// 발사 실패시 다음 발사 시간까지 기달린다.
        /// </summary>
        public int FireProbability { get; set; } = 100;
        private float lastFireTime;

        private Random random = new Random();
        public EnemyBulletShooter(GameObject gameObject) : base(gameObject) { }
        public override void Start()
        {
            lastFireTime = -FireRate;
        }
        public override void Update()
        {
            if (gameObject.transform.Rotation >= ShootMinRotation &&
                gameObject.transform.Rotation <= ShootMaxRotation)
            {
                if (lastFireTime + FireRate <= GameEngine.Instance.Time)
                {
                    lastFireTime = GameEngine.Instance.Time;
                    if (FireProbability > random.Next(100))
                    {
                        FireBullet();
                    }
                }
            }
        }
        /// <summary>
        /// 총알을 발사하고
        /// 설정한다.
        /// </summary>
        void FireBullet()
        {
            EnemyBullet bullet = GameObject.Instantiate<EnemyBullet>();

            Vec2D bulletPoint = gameObject.transform.position;
            bulletPoint.Y += 8;
            bullet.transform.position = bulletPoint;

            DamageSystem damageSystem =  bullet.GetComponent<DamageSystem>();
            damageSystem.EventGiveDamage += () => GameObject.Destroy(bullet);
        }
    }
}
