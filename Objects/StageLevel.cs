using Engine;
using Galaga.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    /// <summary>
    /// UI에 표시할 플레이어 목숨 프리팹
    /// </summary>
    class StageLevel : GameObject
    {
        public StageLevel()
        {
            Vec2D point = new Vec2D(-10, -10);
            transform.position = point;

            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.stage_level;
            AddComponent(spriteComponent);
        }
    }
}
