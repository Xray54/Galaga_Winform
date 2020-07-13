using Engine;
using Galaga.Objects;
using Galaga.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Galaga.Scripts
{
    public class Slot
    {
        public Transform transform;
        public GameObject gameObject;
    }
    /// <summary>
    /// 적 AI를 구현한다.
    /// 적을 스폰할 준비과 되면 
    /// 적을 스폰하고 전부 스폰이 완료되면
    /// 공격을 실행한다.
    /// 남아있는 적이 없으면 다시 적을 스폰한다.
    /// </summary>
    class CompanyCommander:Component
    {
        public CompanyCommander(GameObject gameObject) : base(gameObject) { }

        public Slot[][] ShipSlots = new Slot[6][]
        {
            new Slot[4],
            new Slot[4],
            new Slot[8],
            new Slot[8],
            new Slot[10],
            new Slot[10]
        };
        List<GameObject> aliveUnits = new List<GameObject>();
        List<GameObject> attackingUnits = new List<GameObject>();


        /// <summary>
        /// 공격을 실행하고 다음 공격 실행까지 시간 간격
        /// </summary>
        public float AttackTimeInterval { get; set; } = 5f;
        private float LastAttackTime;

        Spawner spawner;
        Random random = new Random();
        bool AttackReady = false;
        bool spawning = false;

        /// <summary>
        /// 자식 오브젝트를 가져와 저장한다. ShipSlots에 저장한다.
        /// </summary>
        public override void Start()
        {
            int slotCount = 0;
            for (int i = 0; i < ShipSlots.Length; i++)
            {
                for (int j = 0; j < ShipSlots[i].Length; j++)
                {
                    GameObject child = gameObject.transform.GetChild(slotCount++);
                    if (child != null)
                    {
                        ShipSlots[i][j] = new Slot();
                        ShipSlots[i][j].transform = child.transform;
                    }
                }
            }
            spawner = GameObject.FindObjectOfType<Spawner>();
            if (spawner == null)
            {
                Debug.WriteLine("CompanyCommander::Start  :  Spawner 컴퍼넌트가 없습니다.");
            }
            LastAttackTime = -AttackTimeInterval;

            spawnInfos.Add(new List<SpawnInfo>());
            spawnInfos[spawnInfos.Count - 1].Add(spawn01);
            spawnInfos[spawnInfos.Count - 1].Add(spawn02);

            spawnInfos.Add(new List<SpawnInfo>());
            spawnInfos[spawnInfos.Count - 1].Add(spawn03);
            spawnInfos[spawnInfos.Count - 1].Add(spawn04);

            spawnInfos.Add(new List<SpawnInfo>());
            spawnInfos[spawnInfos.Count - 1].Add(spawn05);

            spawnInfos.Add(new List<SpawnInfo>());
            spawnInfos[spawnInfos.Count - 1].Add(spawn06);

            spawnInfos.Add(new List<SpawnInfo>());
            spawnInfos[spawnInfos.Count - 1].Add(spawn07);

            spawnInfosBoss.Add(new List<SpawnInfo>());
            spawnInfosBoss[spawnInfosBoss.Count - 1].Add(spawnBoss);

        }
        List<List<SpawnInfo>> spawnInfos = new List<List<SpawnInfo>>();
        List<List<SpawnInfo>> spawnInfosBoss = new List<List<SpawnInfo>>();

        #region 스폰 할 적 저장

        public struct SpawnInfo
        {
            public EnemyType enemyType;
            public StartPoint dir;
            public WayPoints.ShapeTypeTable pattern;
            public int count;
            public float firstDelay;
            public float intaval;
        }
        public enum EnemyType
        {
            kTypeGoei,
            kTypeZako,
            kTypeBoss,
            kTypeBigBoss
        }

        SpawnInfo spawn01 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeZako,
            dir = StartPoint.LeftTop,
            pattern = WayPoints.ShapeTypeTable.kDownTurn6,
            count = 4,
            intaval = 0.5f
        };
        SpawnInfo spawn02 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeGoei,
            dir = StartPoint.LeftTop,
            pattern = WayPoints.ShapeTypeTable.kDownTurnJ,
            count = 4,
            intaval = 0.5f
        };        
        SpawnInfo spawn03 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeBoss,
            dir = StartPoint.SideLeftBottom,
            pattern = WayPoints.ShapeTypeTable.kCircleUpRight,
            count = 4,
            intaval = 0.5f
        };
        SpawnInfo spawn04 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeGoei,
            dir = StartPoint.SideLeftBottom,
            pattern = WayPoints.ShapeTypeTable.kCircleUpRight,
            count = 4,
            firstDelay = 0.25f,
            intaval = 0.5f
        };
        SpawnInfo spawn05 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeGoei,
            dir = StartPoint.SideRightBottom,
            pattern = WayPoints.ShapeTypeTable.kCircleUpLeft,
            count = 8,
            intaval = 0.5f
        };
        SpawnInfo spawn06 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeZako,
            dir = StartPoint.RightTop,
            pattern = WayPoints.ShapeTypeTable.kDownTurn6,
            count = 8,
            intaval = 0.5f
        };
        SpawnInfo spawn07 = new SpawnInfo
        {
            enemyType = EnemyType.kTypeZako,
            dir = StartPoint.LeftTop,
            pattern = WayPoints.ShapeTypeTable.kDownTurnJ,
            count = 8,
            intaval = 0.5f
        };

        SpawnInfo spawnBoss = new SpawnInfo
        {
            enemyType = EnemyType.kTypeBigBoss,
            dir = StartPoint.LeftTop,
            pattern = WayPoints.ShapeTypeTable.kDownTurnJ,
            count = 1,
            intaval = 0.5f
        };
    #endregion

    public override void Update()
        {
            if (!spawning && aliveUnits.Count == 0)
            {
                GameManager.Instance.IncreaseStage(1);
                spawning = true;
                AttackReady = false;


                // 2스테이지 마다 보스 출현
                if (GameManager.Instance.CurrentStage % 2 == 0)
                {
                    SpawnEnemy(spawnInfosBoss, 1);
                }
                else
                {
                    SpawnEnemy(spawnInfos, 1);
                }
            }

            if (!spawning && 
                AttackReady == true && 
                aliveUnits.Count != 1 &&
                LastAttackTime + AttackTimeInterval <= GameEngine.Instance.Time)
            {
                LastAttackTime = GameEngine.Instance.Time;
                AttackReady = false;
                Attack();
            }
        }

        /// <summary>
        /// 공격을 실행한다.
        /// 공격을 실행할 갯수를 정하고
        /// 공격 형태를 랜덤으로 정하여 이동경로를 정해준다.
        /// </summary>
        public async void Attack()
        {
            int maxAttackUnit = 2;
            if (GameManager.Instance.CurrentStage < random.Next(100))
            {
                maxAttackUnit += random.Next(1, 3);
            }
            if (maxAttackUnit > aliveUnits.Count)
            {
                maxAttackUnit = aliveUnits.Count;
            }

            int addAttackUnitCount = 0;
            for (int i = 0; i < aliveUnits.Count; i++)
            {
                bool alreadyAttacking = false;
                foreach (var item in attackingUnits)
                {
                    if (aliveUnits[i] == item)
                        alreadyAttacking = true;
                }
                if (alreadyAttacking == false && addAttackUnitCount < maxAttackUnit)
                {
                    addAttackUnitCount++;
                    attackingUnits.Add(aliveUnits[i]);
                    BezierCurveMove move = aliveUnits[i].GetComponent<BezierCurveMove>();


                    WayPoints.ShapeTypeTable attackType = WayPoints.ShapeTypeTable.kSwingLeftDown;
                    StartPoint reattackPosition = StartPoint.LeftTop;

                    switch (random.Next(2))
                    {
                        case 0:
                            attackType = WayPoints.ShapeTypeTable.kSwingLeftDown;
                            break;
                        case 1:
                            attackType = WayPoints.ShapeTypeTable.kSwingRightDown;
                            break;
                    }


                    switch (random.Next(2))
                    {
                        case 0:
                            reattackPosition = StartPoint.LeftTop;
                            break;
                        case 1:
                            reattackPosition = StartPoint.RightTop;
                            break;
                    }


                    move.WayPoints = WayPoints.Instance.GetWayPoint(aliveUnits[i].transform.position, attackType);
                    move.CurrentProgress = 0;
                    move.Destination = null;

                    Action handler = null;
                    handler = () =>
                    {
                        move.EventMoveDone -= handler;
                        move.WayPoints = WayPoints.Instance.GetWayPoint(Spawner.Instance.GetLocation(reattackPosition), attackType);
                        move.CurrentProgress = 0;

                        move.EventMoveDone += ()=> move.CurrentProgress = 0;
                    };
                    move.EventMoveDone += handler;



                    await Task.Delay(500);
                }
            }
            AttackReady = true;
        }

        /// <summary>
        /// 매개변수로 전달된 적 생성 정보로 적을 생성한다.
        /// </summary>
        /// <param name="spawnInfo">생성할 적 정보</param>
        public async Task<bool> SpawnEnemy(SpawnInfo spawnInfo)
        {
            await Task.Delay(Convert.ToInt32(spawnInfo.firstDelay * 1000));

            for (int i = 0; i < spawnInfo.count; i++)
            {
                Slot slot = null;
                int score = 100;
                switch (spawnInfo.enemyType)
                {
                    case EnemyType.kTypeZako:
                        slot = GetEmptySlot(spawnInfo.enemyType);
                        slot.gameObject = spawner.SpawnEnemy<Enemy01>(spawnInfo.dir, spawnInfo.pattern, slot.transform);
                        score = 100;
                        break;

                    case EnemyType.kTypeGoei:
                        slot = GetEmptySlot(spawnInfo.enemyType);
                        slot.gameObject = spawner.SpawnEnemy<Enemy02>(spawnInfo.dir, spawnInfo.pattern, slot.transform);
                        score = 150;
                        break;

                    case EnemyType.kTypeBoss:
                        slot = GetEmptySlot(spawnInfo.enemyType);
                        slot.gameObject = spawner.SpawnEnemy<Enemy03>(spawnInfo.dir, spawnInfo.pattern, slot.transform);
                        score = 300;
                        break;

                    case EnemyType.kTypeBigBoss:
                        slot = GetEmptySlot(spawnInfo.enemyType);
                        slot.gameObject = spawner.SpawnEnemy<BigBoss>(spawnInfo.dir, spawnInfo.pattern, slot.transform);
                        score = 2000;
                        break;

                    default:
                        break;
                }
                if (slot != null)
                {
                    SetupEnemy(slot, score);
                    aliveUnits.Add(slot.gameObject);
                }
                await Task.Delay(Convert.ToInt32(spawnInfo.intaval * 1000));
            }
            return true;
        }
        /// <summary>
        /// 주어진 적 생성 자료구조로 적을 생성한다.
        /// 각 생성마다 3초씩 쉰다.
        /// </summary>
        /// <param name="spawnInfos">적 생성 정보</param>
        /// <param name="firstDleay">맨 처음 실행하기 전에 딜레이할 시간</param>
        public async void SpawnEnemy(List<List<SpawnInfo>> spawnInfos, float firstDleay)
        {
            await Task.Delay(Convert.ToInt32(firstDleay * 1000));
            for (int j = 0; j < spawnInfos.Count; j++)
            {
                for (int i = 0; i < spawnInfos[j].Count; i++)
                {
                    if (i != spawnInfos[j].Count-1)
                    {
                        SpawnEnemy(spawnInfos[j][i]);
                    }
                    else
                    {
                        await Task.Run(() => SpawnEnemy(spawnInfos[j][i]));
                    }
                }
                await Task.Delay(3000);
            }
            spawning = false;
            // 공격 준비 설정
            AttackReady = true;
            // 공격할 유닛 랜덤으로 선택하기 위해 셔플
            Shuffle(aliveUnits);
        }


        /// <summary>
        /// slot에 설정된 적 유닛의 로직을 설정한다.
        /// 각 컴포넌트에 이벤트를 추가한다.
        /// </summary>
        /// <param name="slot">설정할 슬롯</param>
        /// <param name="score">죽으면 추가할 점수</param>
        private void SetupEnemy(Slot slot , int score)
        {
            BezierCurveMove move = slot.gameObject.GetComponent<BezierCurveMove>();
            HealthSystem health = slot.gameObject.GetComponent<HealthSystem>();
            DamageSystem damageSystem = slot.gameObject.GetComponent<DamageSystem>();

            //damageSystem.EventGiveDamage += () => health.GetDamage(2);

            // 한번만 작동하게 하는 이벤트 삽입
            // 자기 자신에게 데미지 주기
            Action selfDamage = null;
            selfDamage = () =>
            {
                damageSystem.EventGiveDamage -= selfDamage;
                health.GetDamage(2);
            };
            damageSystem.EventGiveDamage += selfDamage;

            // 죽으면 애니메이션 재생하게 하기
            health.EventOnDead += () =>
            {
                slot.gameObject.collider.Enabled = false;
                move.Enabled = false;
                AnimationSprite animation = slot.gameObject.GetComponent<AnimationSprite>();
                animation.ImageCurrentIndex = 0;
                animation.ImageChangeInterval = 0.1f;
                animation.ImageList.Clear();
                animation.ImageList.Add(Resources.enemy_die_01);
                animation.ImageList.Add(Resources.enemy_die_02);
                animation.ImageList.Add(Resources.enemy_die_03);
                animation.ImageList.Add(Resources.enemy_die_04);
                animation.ImageList.Add(Resources.enemy_die_05);
                // 재생 다하면 오브젝트 삭제
                animation.EventPrintedAllImage += () => GameObject.Destroy(slot.gameObject, 0);
                animation.EventPrintedAllImage += () => slot.gameObject = null;
            };
            // 점수 증가
            health.EventOnDead += () => GameManager.Instance.IncreaseScore(score);
            health.EventOnDead += () => aliveUnits.Remove(slot.gameObject);

            // 보스몬스터일경우 남은 HP가 1이면 애니메이션 스프라이트 변경
            if (slot.gameObject as Enemy03 != null)
            {
                health.EventGetDamage += (leftHP) =>
                {
                    if (leftHP == 1)
                    {
                        AnimationSprite animationSprite = slot.gameObject.GetComponent<AnimationSprite>();
                        if (animationSprite != null)
                        {
                            animationSprite.ImageList.Clear(); 
                            animationSprite.ImageList.Add( Resources.Enemy_03_03_01);
                            animationSprite.ImageList.Add( Resources.Enemy_03_03_01);
                        }
                    }
                    else if(leftHP == 2)
                    {
                        AnimationSprite animationSprite = slot.gameObject.GetComponent<AnimationSprite>();
                        if (animationSprite != null)
                        {
                            animationSprite.ImageList.Clear();
                            animationSprite.ImageList.Add(Resources.Enemy_03_02_01);
                            animationSprite.ImageList.Add(Resources.Enemy_03_02_01);
                        }
                    }
                };
            }
        }



        /// <summary>
        /// 적에 따라 2,3라인과, 4,5라인을 번갈아 선택해주기 위한 변수 저장
        /// </summary>
        private int line23SelctTime = 0;
        private int line45SelctTime = 0;

        /// <summary>
        /// 적 타입에 맞는 비어있는 슬롯을 선택해준다
        /// 슬롯에 gameobject가 null인지 확인하여 반환한다.
        /// 비어있는 슬롯이 없으면 null를 반환한다.
        /// </summary>
        /// <param name="type">적 타입</param>
        /// <returns>비어있는 슬록</returns>
        public Slot GetEmptySlot(EnemyType type)
        {
            int line = 0;

            switch (type)
            {
                case EnemyType.kTypeGoei:
                    line = (line45SelctTime++ % 2 == 0) ? 2 : 3;
                    break;
                case EnemyType.kTypeZako:
                    line = (line23SelctTime++ % 2 == 0) ? 4 : 5;
                    break;
                case EnemyType.kTypeBoss:
                    line = 1;
                    break;
                default:
                    break;
            }

            for (int i = ShipSlots[line].Length / 2 , left = i-1; i < ShipSlots[line].Length; i++ , left--)
            {
                if (ShipSlots[line][i].gameObject == null)
                {
                    return ShipSlots[line][i];
                }
                else if (ShipSlots[line][left].gameObject == null)
                {
                    return ShipSlots[line][left];
                }
            }

            return null;
        }

        /// <summary>
        /// 주어진 리스트를 섞어준다.
        /// </summary>
        /// <typeparam name="T">리스트에 들어간 타입</typeparam>
        /// <param name="list">섞어줄 리스트</param>
        public void Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int j = random.Next(i + 1);
                T temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }
    }
}
