using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 씬을 불러오고 저장한다.
    /// </summary>
    class SceneManager
    {
        /// <summary>
        /// 현재 씬을 저장한다.
        /// </summary>
        public static Scene CurrentSecne { get; private set; }

        public static Scene GetActiveScene()
        {
            return CurrentSecne;
        }
        /// <summary>
        /// 씬을 생성하고 현재 실행중인 씬에 저장한다.
        /// </summary>
        /// <typeparam name="T">생성할 씬 클래스</typeparam>
        public static void LoadScene<T>() where T : Scene,new()
        {
            CurrentSecne = new T();
            CurrentSecne.Init();
        }
    }
}
