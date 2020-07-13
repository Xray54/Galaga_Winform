using Engine;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    class WayPointManager : GameObject
    {
        public WayPointManager()
        {
            AddComponent(new WayPoints(this));
        }
    }
}
