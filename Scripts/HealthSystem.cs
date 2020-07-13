using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// HP시스템을 구현
    /// </summary>
    class HealthSystem : Component
    {
        /// <summary>
        /// 최대 HP를 설정
        /// RestoreHealth함수를 사용하면 이 수치 위로 올라가지 않는다.
        /// </summary>
        public int MaxHealth { get; set; } = 1;
        /// <summary>
        /// 현재 HP
        /// </summary>
        public int Health { get; set; } = -1;

        /// <summary>
        /// 죽으면 실행할 이벤트
        /// </summary>
        public event Action EventOnDead;

        /// <summary>
        /// 데미지를 입으면 실행할 이벤트
        /// 남은 HP를 매개변수로 전달한다.
        /// </summary>
        public event Action<int> EventGetDamage;
        public HealthSystem(GameObject gameObject) : base(gameObject) { }
        public override void Start()
        {
            if (Health == -1)
            {
                Health = MaxHealth;
            }
        }
        public void GetDamage(int damage)
        {
            Health -= damage;
            EventGetDamage?.Invoke(Health);
            if (Health <= 0)
            {
                EventOnDead?.Invoke();
            }
        }
        public void RestoreHealth(int restoredAmount)
        {
            Health += restoredAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}
