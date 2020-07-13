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
    /// 플레이어 목숨 관리 매니저
    /// </summary>
    class PlayerLifeManager:GameObject
    {
        public PlayerLifeManager()
        {
            LifeManager lifeManager = new LifeManager(this);
            AddComponent(lifeManager);
        }
    }
}
