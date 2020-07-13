using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// 메인 게임 루프 
    /// </summary>
    public class GameEngine : Single.Singleton<GameEngine>
    {

        /// <summary>
        /// 게임 시작한 Tick
        /// </summary>
        int startTick = 0;
        /// <summary>
        /// 마지막 프레임 Tick
        /// </summary>
        int lastFrameTick = 0;
        public float FPS
        {
            get
            {
                return 1 / DeltaTime;
            }
        }
        /// <summary>
        /// 전 프레임이 실행된 시간을 반환한다.
        /// </summary>
        public float DeltaTime { get; private set; }
        /// <summary>
        /// 현재 게임이 실행된 시간을 반환한다.
        /// </summary>
        public float Time
        {
            get
            {
                return (Environment.TickCount - startTick) / 1000f;
            }
        }

        /// <summary>
        /// 게임이 시작한 틱 가지고 있기
        /// </summary>
        public GameEngine()
        {
            startTick = Environment.TickCount;
        }

        public event Action UpdateFrame;

        /// <summary>
        /// 메인 게임 루프
        /// 매프레임마다 DeletaTime를 계산고
        /// UpdateFrame에 등록된 이벤트를 실행한다.
        /// </summary>
        public async void Run()
        {
            startTick = Environment.TickCount;
            lastFrameTick = startTick;

            while (UpdateFrame == null)
            {
                Debug.WriteLine("프레임 이벤트에 함수가 없습니다.");
                return;
            }

            while (true)
            {
                await Task.Delay(1);

                int currentTick = Environment.TickCount;

                // delta time 실행
                DeltaTime = (currentTick - lastFrameTick) / 1000f;

                // update tick
                lastFrameTick = currentTick;

                UpdateFrame();
            }
        }
    }
}
