using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    /// <summary>
    /// 게임 매니저를 구현한다.
    /// </summary>
    class GameManager : Component
    {
        public GameManager(GameObject gameObject) : base(gameObject) { }

        #region 싱글톤
        /// <summary>
        /// 싱글톤을 구현한다.
        /// </summary>
        private static GameManager m_instance;
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<GameManager>();
                }
                return m_instance;
            }
        }
        #endregion

        /// <summary>
        /// 현재 스테이지 
        /// </summary>
        public int CurrentStage { get; private set; } = 0;

        /// <summary>
        /// 캐릭터가 총 발사한 총알 개수
        /// </summary>
        public int TotalFireCount { get; private set; } = 0;

        /// <summary>
        /// 플레이어의 점수
        /// </summary>
        private int score = 0;

        public void IncreaseFireCount(int amout)
        {
            TotalFireCount += amout;
        }
        public void IncreaseStage(int amout)
        {
            CurrentStage += amout;
            UIManager.Instance.StageLevelIncrease(amout);
        }
        public void IncreaseScore(int amout)
        {
            score += amout;
            UIManager.Instance.UpdateScoreText(score);
        }

    }
}
