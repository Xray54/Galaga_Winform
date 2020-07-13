using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.GameEngine
{
    class GameObject
    {

        
        public Point point { get; set; }
        public float Roation { get; set; }
        public Object Tag { get; set; }


        private SpriteComponent spriteComponent;

        public GameObject(SpriteComponent sprite)
        {
            spriteComponent = sprite;
        }
        public virtual void Update()
        {
            spriteComponent.Update();
        }
    }
}
