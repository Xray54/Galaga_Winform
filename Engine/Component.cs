using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 컴포넌트 베이스 클래스
    /// 모든 컴포넌트들은 이 클래스를 상속받아야한다
    /// 그래고 모든 컴포넌트는 자기가 속해있는 게임 오브젝트를 가지고있는다
    /// 그러므로 생성자 매개변수에 자기가 속해있는 게임 오브젝트를 넘겨준다.
    /// </summary>
    public class Component
    {

        public GameObject gameObject = null;
        /// <summary>
        /// Enable꺼져있으면 Update가 실행이 안된다.
        /// </summary>
        public bool Enabled { get; set; } = true;
        public Component(GameObject baseObject)
        {
            gameObject = baseObject;
        }

        /// <summary>
        /// 컴포넌트들이 Start,Update 를 사용하고 싶으면 오버라이딩을 하면 된다.
        /// </summary>
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }
    }
}
