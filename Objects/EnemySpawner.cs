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
    /// 적군 생성과 위치를 지정해주기 위한 프리팹
    /// </summary>
    class EnemySpawner : GameObject
    {
        public EnemySpawner()
        {
            Components.Add(new Spawner(this));
        }
    }
}
