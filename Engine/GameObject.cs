using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace Engine
{
    /// <summary>
    /// 게임의 모든 오브젝트들은 GameObject를 상속받아야함
    /// </summary>
    public class GameObject
    {        
        public string Tag { get; set; }
        public string Name { get; set; } = "GameObject";
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 오브젝트가 가지고 있는 컴포넌트 리스트
        /// </summary>
        public List<Component> Components { get; private set; }

        /// <summary>
        /// 트렌스 폼과 콜라이더는 컴퍼넌트에 안들어가고 따로 빠짐
        /// </summary>
        public Transform transform { get; private set; }
        public BoxCollider collider { get; private set; }
        public GameObject()
        {
            transform = new Transform(this);
            Components = new List<Component>();
        }

        /// <summary>
        /// 오브젝트의 스타트는 모든 컴포넌트들의 Start를 실행시킨다.
        /// </summary>
        public void Start()
        {
            foreach (var component in Components)
            {
                if (component.Enabled)
                {
                    component.Start();
                }
            }
        }        
        /// <summary>
        /// 모든 컴포넌트의 Update를 실행
        /// </summary>
        public void Update()
        {
            foreach (var component in Components)
            {
                if (component.Enabled)
                {
                    component.Update();
                }
            }
        }

        /// <summary>
        /// 오브젝트에 컴포넌트를 추가
        /// </summary>
        /// <param name="component">추가할 컴포넌트</param>
        public void AddComponent(Component component)
        {
            // 콜라이더인지만 확인후 따로 빼냄
            if (IsCollider(component))
            {
                // 중복삽입 확인
                if(collider != null)
                {
                    Debug.WriteLine("콜라이더를 2개 넣었습니다. (AddComponent)");
                    throw new System.Exception("콜라이더를 2개 넣었습니다. (AddComponent)");
                }
                collider = component as BoxCollider;
                return;
            }

            Components.Add(component);
        }
        /// <summary>
        /// ICollider 를 상속받았는지  확인
        /// </summary>
        /// <param name="component">검사할 컴포넌트</param>
        /// <returns>ICollider상속 받으면 true</returns>
        bool IsCollider(Component component)
        {
            return component as ICollider != null;
        }
        /// <summary>
        /// 삽입된 컴포넌트들 중에 처음 찾은 컴포넌트를 반환
        /// 못 찾으면 null 반환
        /// </summary>
        /// <typeparam name="T">찾을 컴포넌트 타입</typeparam>
        /// <returns>찾은 첫번째의 컴포넌트</returns>
        public T GetComponent<T>() where T: Component
        {
            T ret = null;
            foreach (var component in Components)
            {
                ret = component as T;
                if (ret != null)
                    break;                  
            }
            return ret;
        }

        /// <summary>
        /// 오브젝트를 생성하는 함수 
        /// 오브젝트를 생성하고 현재씬의 오브젝트자료구조 맨 뒤에 삽입한다.
        /// </summary>
        /// <typeparam name="T">생성할 오브젝트 타입</typeparam>
        /// <returns></returns>
        public static T Instantiate<T>() where T : GameObject , new()
        {
            T MakedObject = new T();
            SceneManager.GetActiveScene().AddObject(MakedObject);
            MakedObject.Start();
            return MakedObject;
        }

        /// <summary>
        /// 오브젝트를 삭제해주는 함수
        /// </summary>
        /// <param name="obj">삭제할 오브젝트</param>
        /// <param name="t">삭제 지연할 시간</param>
        public static void Destroy(GameObject obj, float delayTime = 0f)
        {
            SceneManager.GetActiveScene().DeleteObject(obj, delayTime);
        }

        /// <summary>
        /// 오브젝트 이름으로 오브젝트를 찾는 함수
        /// 못찾으면 null 반환
        /// </summary>
        /// <param name="name">찾을 이름</param>
        /// <returns>찾은 아이템</returns>
        public static GameObject Find(string name)
        {
            List<GameObject> objects = SceneManager.GetActiveScene().GetGameObjects();

            foreach (var item in objects)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 모든 오브젝트를 순회하여 입력한 타입의 컴포넌트를 가지고 있는지 확인하여 
        /// 찾은 첫번째 컴포넌트를 반환해주는 함수
        /// 못찾으면 null 반환
        /// </summary>
        /// <typeparam name="T">찾을 컴포넌트</typeparam>
        /// <returns>찾은 컴포넌트</returns>
        public static T FindObjectOfType<T>() where T : Component
        {

            List<GameObject> objects = SceneManager.GetActiveScene().GetGameObjects();

            foreach (var obj in objects)
            {
                T ret =  obj.GetComponent<T>();
                if (ret != null)
                    return ret;
            }
            return null;
        }

        /// <summary>
        /// 현재 게임중의 모든 오브젝트들을 반환한다.
        /// 리스트로 반환
        /// </summary>
        /// <returns>모든 오브젝트 리스트</returns>
        public static ref readonly List<GameObject> GetGameObjects()
        {
            return ref SceneManager.GetActiveScene().GetGameObjects();
        }

        /// <summary>
        /// 모든 자식 오브젝트들을 순회하여 해당 컴포넌트를 찾는다
        /// 못찾으면 null 반환
        /// </summary>
        /// <typeparam name="T">탐색할 컴포넌트</typeparam>
        /// <returns>찾은 컴포넌트</returns>
        public T GetComponentInChildren<T>() where T :Component
        {
            foreach (var item in transform.ChildObjects)
            {
                T findingComponent = item.GetComponent<T>();
                if (findingComponent != null)
                {
                    return findingComponent;
                }
            }
            return null;
        }

    }
}
