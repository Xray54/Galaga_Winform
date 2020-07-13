using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// HelathSystem과 연계되는 시스템
    /// 충돌이 된것이 있으면 설정된 데미지만큼 HelathSystem에 데미지를 준다.
    /// </summary>
    class DamageSystem : Component
    {
        /// <summary>
        /// 데미지를 줄 태그
        /// </summary>
        public string HitAbleTag { get; set; }
        /// <summary>
        /// 데미지를 줄 수치
        /// </summary>
        public int Damage { get; protected set; } = 1;
        /// <summary>
        /// 데미지를 주면 발생할 이벤트
        /// </summary>
        public event Action EventGiveDamage;
        public DamageSystem(GameObject gameObject) : base(gameObject) { }
        public override void Update()
        {
            if (gameObject.collider != null)
            {
                foreach (var other in gameObject.collider.ContactObects)
                {
                    if (other.Tag == HitAbleTag)
                    {
                        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
                        if (healthSystem != null)
                        {
                            healthSystem.GetDamage(Damage);
                            EventGiveDamage?.Invoke();
                        }
                    }
                }                
            }
        }
    }
}
