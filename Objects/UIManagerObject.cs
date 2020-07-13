using Engine;
using Galaga.Properties;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    /// <summary>
    /// UI매니저 프리팹
    /// </summary>
    class UIManagerObject : GameObject
    {
        public UIManagerObject()
        {
            UIManager uIManager = new UIManager(this);
            uIManager.lifeShipsStartPoint = new Vec2D(8, 288);
            AddComponent(uIManager);
        }
    }
}
