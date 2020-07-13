using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Galaga.Scripts
{
    /// <summary>
    /// 스프라이트컴포넌트의 이미지를 바꾸어줌 정해준 시간만큼 바꾸어줌
    /// </summary>
    public class AnimationSprite :Component
    {
        public AnimationSprite(GameObject gameObj) : base(gameObj) { }
        /// <summary>
        /// 바꾸어줄 이미지 리스트
        /// </summary>
        public List<Image> ImageList { get; set; } = new List<Image>();
        /// <summary>
        /// 바꾸어주는 시간간격 1.0 =  1초
        /// </summary>
        public float ImageChangeInterval { get; set; } = 0.5f;
        /// <summary>
        /// 현재 출력하고 이미지 인덱스
        /// </summary>
        public int ImageCurrentIndex { get; set; }
        /// <summary>
        /// 모든 이미지를 순회하면 이벤트를 발생시킴
        /// </summary>
        public event Action EventPrintedAllImage;
        private float lastChangeTime { get; set; }

        public SpriteComponent SpriteComponent;

        public override void Start()
        {
            if(SpriteComponent == null)
            {
                SpriteComponent = gameObject.GetComponent<SpriteComponent>();
                if (SpriteComponent == null)
                {
                    Debug.WriteLine("Error: 스프라이트 컴퍼넌트가 없습니다. (AnimationSprite)");
                }
                return;
            }
        }
        public override void Update()
        {
            if(lastChangeTime + ImageChangeInterval < GameEngine.Instance.Time)
            {
                SpriteComponent.Image = ImageList[ImageCurrentIndex++];
                if (ImageCurrentIndex >= ImageList.Count)
                {
                    ImageCurrentIndex %= ImageList.Count;
                    EventPrintedAllImage?.Invoke();
                }
                lastChangeTime = GameEngine.Instance.Time;
            }
        }

    }
}
