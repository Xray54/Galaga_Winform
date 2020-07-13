using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    /// <summary>
    /// 윈폼에서 사용하는 텍스트 컴퍼넌트
    /// </summary>
    public class TextComponet : Component
    {
        public TextComponet(GameObject gameObject) : base(gameObject) { }
        /// <summary>
        /// 출력할 이름
        /// </summary>
        public string Text { get; set; }
        public Font Font { get; set; } =  new Font("Arial", 16);
        public SolidBrush Brush { get; set; } = new SolidBrush(Color.White);
    }
}