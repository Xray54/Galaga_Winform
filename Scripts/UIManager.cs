using Engine;
using Galaga.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Scripts
{
    /// <summary>
    /// UI들을 관리한다.
    /// </summary>
    public class UIManager : Component
    {
        public UIManager(GameObject gameObject) : base(gameObject) { }

        #region 싱글톤
        private static UIManager m_instance;
        public static UIManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<UIManager>();
                }
                return m_instance;
            }
        } 
        #endregion

        /// <summary>
        /// 남은 목숨 갯수
        /// </summary>
        public int LifeCount { get => playerShips.Count; }
        /// <summary>
        /// 남은 목숨을 표시해주는 게임 오브젝트 리스트
        /// </summary>
        List<PlayerShipUI> playerShips = new List<PlayerShipUI>();

        /// <summary>
        /// playerShips의 마지막을 요소의 좌표를 반환해준다.
        /// </summary>
        public Vec2D? GetLastShipLoaction { get { return playerShips.Last()?.transform.position; } }
        public Vec2D lifeShipsStartPoint;

        public TextComponet scoreText;
        public GameObject gameOverObject;


        public GameObject stageLevelObejct;
        public List<GameObject> stageLevelObjects = new List<GameObject>();

        public override void Start()
        {
            stageLevelObjects.Add(stageLevelObejct);
        }
        public void EnableGameOver()
        {
            gameOverObject.Enabled = true;
        }
        public void UpdateScoreText(int score)
        {
            scoreText.Text = score.ToString();
        }
        public void LifeIncrease(int amount = 1)
        {             
            for (int i = 0; i < amount; i++)
            {
                PlayerShipUI ship = GameObject.Instantiate<PlayerShipUI>();

                Vec2D bulletPoint = lifeShipsStartPoint;
                bulletPoint.X += 16 * playerShips.Count;
                ship.transform.position = bulletPoint;
                playerShips.Add(ship);
            }
        }
        public void LifeDecrease(int amount = 1)
        {
            if (LifeCount <= 0)
            {
                return;
            }
            for (int i = 0; i < amount; i++)
            {
                PlayerShipUI lastShip = playerShips[playerShips.Count - 1];
                GameObject.Destroy(lastShip, 0);
                playerShips.Remove(lastShip);
            }
        }
        public void StageLevelIncrease(int amout)
        {
            for (int i = 0; i < amout; i++)
            {

                Vec2D nextPos = stageLevelObjects[stageLevelObjects.Count-1].transform.position;
                nextPos.X -= 8;
                StageLevel level = GameObject.Instantiate<StageLevel>();
                level.transform.position = nextPos;
                stageLevelObjects.Add(level);
            }
        }
    }
}