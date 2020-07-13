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
    /// 게임 매니저 프리팹
    /// </summary>
    class GameManagerObject : GameObject
    {
        public GameManagerObject()
        {
            AddComponent(new GameManager(this));
        }
    }
}
