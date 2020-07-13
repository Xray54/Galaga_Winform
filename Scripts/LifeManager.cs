using Engine;
using Galaga.Properties;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaga.Scripts
{
    /// <summary>
    /// 플레이어의 목숨과
    /// 각종 컴포넌트들의 연결을 구현한다.
    /// </summary>
    class LifeManager:Component
    {
        /// <summary>
        /// 현재 남은 목숨
        /// </summary>
        public int CurrentLife { get; private set; } = 0;
        public int StartLife { get; set; } = 3;

        /// <summary>
        /// 목숨이 0이되면 실행할 이벤트
        /// </summary>
        public event Action EventNoMoreLife;

        /// <summary>
        /// 컴포넌트 이벤트 추가를 위한 
        /// </summary>
        public HealthSystem playerhealth;
        public GameObject player;
        public LifeManager(GameObject gameObject) : base(gameObject) { }

        public override void Start()
        {
            // 시작할때 지정해준 목숨만큼 증가시킨다.
            IncreaseLife(StartLife);

            // 플레이어가 죽으면 실행할 이벤트를 추가
            // 죽으면 깜박이는 애니메이션등을 여기서 구현한다.
            // 플레이어가 죽으면 죽은 애니메이션 출력하고
            // 현재 라이프가 몇개 있는지 확인하여
            // 게임오버일지 재배치할지 결정한다.

            playerhealth.EventOnDead += () =>
            {
                // 죽는 애니메이션 추가와 실행
                AnimationSprite animation = player.GetComponent<AnimationSprite>();
                SpriteComponent sprite = player.GetComponent<SpriteComponent>();
                animation.ImageCurrentIndex = 0;
                animation.ImageList.Clear();
                animation.ImageList.Add(Resources.player_die_01);
                animation.ImageList.Add(Resources.player_die_02);
                animation.ImageList.Add(Resources.player_die_03);
                animation.ImageList.Add(Resources.player_die_04);

                animation.Enabled = true;


                // 상호작용 컴포넌트들을 작동중시키기고 시작위치로 다시 움기기 위한 컴포넌트만 작동시킨뒤
                // 목적지에 도착하면 다시 모든 컴포넌트를 동작시킨다.
                player.collider.Enabled = false;
                BulletShooter bulletShooter = player.GetComponent<BulletShooter>();
                bulletShooter.Enabled = false;
                InputComponent input = player.GetComponent<InputComponent>();
                input.Enabled = false;

                Action handler = null;


                // 죽은 애니메이션 모두 완료후 다음 한번만 작동하는 이벤트를 추가시킨다.
                handler = () =>
                {
                    // 부활하는 동안 깜박이는 애니매이션 추가
                    animation.EventPrintedAllImage -= handler;
                    sprite.Image = Resources.player_1;
                    animation.ImageList.Clear();
                    animation.ImageList.Add(Resources.player_1);
                    animation.ImageList.Add(Resources.player_1_02);

                    // 남은 라이프 갯수 확인하여 게임오버할지 판별
                    if (CurrentLife > 0)
                    {
                        player.transform.position = (Vec2D)UIManager.Instance.GetLastShipLoaction;
                        ReduceLife(1);

                        TargetScrolling scrolling = player.GetComponent<TargetScrolling>();
                        if (scrolling != null)
                        {
                            scrolling.Enabled = true;

                            // 부활 하여 시작위치까지 이동하면 상호작용 컴포넌트 작동시작 및 애니메이션 중단
                            scrolling.OnReachDestination += async () =>
                            {
                                player.transform.Rotation = 0;
                                scrolling.Enabled = false;
                                input.Enabled = true;

                                await Task.Delay(1000);
                                player.collider.Enabled = true;
                                bulletShooter.Enabled = true;
                                animation.Enabled = false;
                                sprite.Image = Resources.player_1;
                            };
                        }
                    }
                    // 남은 목숨이 0이라면
                    else
                    {
                        // 이벤트 있으면 실행시키고
                        EventNoMoreLife?.Invoke();
                        // 게임오버 메시지 출력하기
                        UIManager.Instance.EnableGameOver();
                        GameObject.Destroy(player);
                    }
                };
                animation.EventPrintedAllImage += handler;
            };
        }

        public void ReduceLife(int amount)
        {
            CurrentLife -= amount;
            UIManager.Instance.LifeDecrease(amount);
        }
        public void IncreaseLife(int amount)
        {
            CurrentLife += amount;
            UIManager.Instance.LifeIncrease(amount);
        }
    }
}
