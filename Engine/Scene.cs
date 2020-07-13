using Galaga;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// 엔진 메인 뼈대
    /// 씬의 모든 오브젝트들은 여기서 관리한다.
    /// 사용자는 씬을 상속은 클래스를 생성해 MakeObjects함수는 오버라이딩을 해주면된다.
    /// </summary>
    public class Scene 
    {
        /// <summary>
        /// 생성된 모든 오브젝트
        /// </summary>
        List<GameObject> gameObjects = new List<GameObject>();

        public Scene()
        {
        }

        /// <summary>
        /// 사용자가 정의한 오브젝트들을 만들고
        /// Render쪽에 씬의 오브젝트 리스트를 넘겨준다
        /// </summary>
        public void Init()
        {
            // 게임에서 사용할 오브젝트들 생성
            MakeObjects();

            // 출력할수 있게 씬의 리스트를 공유함
            WinformRender.Instance.SetGameObject(gameObjects);

            // 오브젝트들의 컴포넌트 Start실행
            StartObjects();

            // 메인루프에 씬의 업데이트 로직을 연결
            GameEngine.Instance.UpdateFrame += UpdateGame;
        }

        public void StopUpdate()
        {
            GameEngine.Instance.UpdateFrame -= UpdateGame;
        }

        /// <summary>
        /// 오브젝트들은 생성한다.
        /// 사용자는 씬을 상속받아 이것만 오버라이딩을 해주면 된다.
        /// </summary>
        public virtual void MakeObjects()
        {
            // 여기다가 오버라이드하여 사용자 오브젝터 생성
        }

        /// <summary>
        /// 모든 오브젝트들의 스타트를 호출한다.
        /// </summary>
        void StartObjects()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].Enabled)
                {
                    gameObjects[i].Start();
                }
            }
        }

        /// <summary>
        /// 게임 1프레임 처리 함수
        /// 모든 오브젝트들의 Update를 실행한다.
        /// 먼저 콜라이더컴포넌트를 가지고 있난 확인후 콜라이더 먼저 업데이트한다
        /// 다음에 모든 컴포넌트들의 Update를 실행한다.
        /// 마지막으로 랜더를 해준다.
        /// </summary>
        void UpdateGame()
        {
            // FPS측정용
            // Debug.WriteLine(GameEngine.Instance.FPS);


            // 먼저 콜라이더를 업데이트
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject item = gameObjects[i];
                if (!item.Enabled || item.collider == null)
                { 
                        continue;
                }
                if (item.collider.Enabled)
                {
                    item.collider.Update();
                }
            }

            // 모든 게임 오브젝트들을 업데이트
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].Enabled)
                {
                    gameObjects[i].Update();
                }
            }
            WinformRender.Instance.NowForm.Invalidate();
        }
        /// <summary>
        /// 씬에 오브젝트
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        /// <summary>
        /// delayTiem 뒤에 오브젝트 삭제
        /// </summary>
        /// <param name="gameObject">삭제할 오브젝트</param>
        /// <param name="delayTime">지연할 시간</param>
        public async void DeleteObject(GameObject gameObject , double delayTimeSec)
        {
            int delayTimeMili = Convert.ToInt32(delayTimeSec * 1000f);
            await Task.Delay(delayTimeMili);
            gameObjects.Remove(gameObject);
        }

        /// <summary>
        /// 현재 씬이 가지고 있는 모든 게임 오브젝트들을 자료구조 채로 반환
        /// </summary>
        /// <returns>모든 게임 오브젝트 List</returns>
        public ref readonly List<GameObject> GetGameObjects()
        {
            return ref gameObjects;
        }

    }
}
