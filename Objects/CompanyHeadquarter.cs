using Engine;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga
{
    class CompanyHeadquarter : GameObject
    {
        /// <summary>
        /// 게임 가운데 좌우로 움직이기 위한 빈 프리팹
        /// </summary>
        public CompanyHeadquarter()
        {
            AddComponent(new ShakeMove(this));
            AddComponent(new CompanyCommander(this));
        }
    }
}
