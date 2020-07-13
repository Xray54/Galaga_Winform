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
    class PlayerShipUI : GameObject
    {
        public PlayerShipUI()
        {
            Vec2D point = new Vec2D(100, 200);
            transform.position = point;

            SpriteComponent spriteComponent = new SpriteComponent(this);
            spriteComponent.Image = Resources.player_1;
            AddComponent(spriteComponent);
        }
    }
}
