using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using Galaga.Objects;
using Galaga.Scripts;

namespace Galaga
{
    /// <summary>
    /// 사용자 씬을 생성
    /// 마지막에 집어넣으면 맨 위에, 마지막에 출력
    /// 오브젝트를 추가한 순서대로 출력한다.
    /// 프리팹에 있는 수치들을 개인별로 수정할때는 여기서 수정한다.
    /// </summary>
    public class Galaga : Scene
    {
        public override void MakeObjects()
        {
            Size windowSize = WinformRender.Instance.NowForm.Size;
            Size gameSize = new Size(224, 304);
            Vec2D playerStartPoint = new Vec2D(gameSize.Width / 2, gameSize.Height - 32);


            // 백그라운드 오브젝트 생성
            BackGround backGround = new BackGround();
            backGround.Name = "BackGround";
            Vec2D point = new Vec2D(224 / 2, 320 / 2);
            backGround.transform.position = point;
            AddObject(backGround);

            backGround = new BackGround();
            point = new Vec2D(224 / 2, 320 / 2 - 320 + 8);
            backGround.transform.position = point;
            AddObject(backGround);

            backGround = new BackGround();
            point = new Vec2D(224 / 2, 320 / 2 - 640 + 16);
            backGround.transform.position = point;
            AddObject(backGround);

            Size BackgroundSize = backGround.GetComponent<SpriteComponent>().Image.Size;

            // 플레이어 생성
            Player player = new Player();
            point = playerStartPoint;
            player.transform.position = point;
            InputComponent input = player.GetComponent<InputComponent>();
            if (input != null)
            {
                input.MaxWidth = gameSize.Width;
            }
            TargetScrolling targetScrolling = player.GetComponent<TargetScrolling>();
            if (targetScrolling != null)
            {
                targetScrolling.Destination = playerStartPoint;
            }
            AddObject(player);


            // 게임 중앙에 모이기 위한 위치 설정
            CompanyHeadquarter company = new CompanyHeadquarter();
            point = new Vec2D(224 / 2, 16 * 5);
            company.transform.position = point;
            AddObject(company);

            GameObject unitPosition;
            Vec2D localPoint;

            int totalLine = 6;
            int[] enemyCountAtLine = new int[] { 4, 4, 8, 8, 10, 10 };

            // 여러개의 빈 오브젝트를 생성해서 위의 company의 자식으로 설정
            // 부모만 움직이면 자식도 움직이기 설정
            for (int t = 0; t < totalLine; t++)
            {
                for (int i = 0; i < enemyCountAtLine[t]; i++)
                {
                    unitPosition = new GameObject();
                    company.transform.SetChild(unitPosition);
                    localPoint = new Vec2D(16 * i - (enemyCountAtLine[t] / 2 - 1) * 16 - 8, 16 * t - 40);
                    unitPosition.transform.localPosition = localPoint;
                    AddObject(unitPosition);
                }
            }

            EnemySpawner enemySpawner = new EnemySpawner();
            AddObject(enemySpawner);

            PlayerLifeManager playerLifeManager = new PlayerLifeManager();
            LifeManager lifeManager = playerLifeManager.GetComponent<LifeManager>();
            if (lifeManager != null)
            {
                lifeManager.playerhealth = player.GetComponent<HealthSystem>();
                lifeManager.player = player;
            }
            AddObject(playerLifeManager);

            Text score = new Text();
            score.transform.position = new Vec2D(gameSize.Width / 10*4, 0);
            TextComponet textComponet = score.GetComponent<TextComponet>();
            AddObject(score);

            Text gameOver = new Text();
            gameOver.transform.position = new Vec2D(gameSize.Width / 10*3, gameSize.Height/2);
            TextComponet textComponet2 = gameOver.GetComponent<TextComponet>();
            textComponet2.Text = "GameOver";
            gameOver.Enabled = false;
            AddObject(gameOver);

            StageLevel stageLevel = new StageLevel();
            Vec2D vec2D = new Vec2D();
            vec2D.X = gameSize.Width;
            vec2D.Y = gameSize.Height - 8;
            stageLevel.transform.position = vec2D;
            stageLevel.Enabled = false;
            AddObject(stageLevel);

            UIManagerObject uIManagerObject = new UIManagerObject();
            UIManager uIManager = uIManagerObject.GetComponent<UIManager>();
            if (textComponet != null && uIManager != null)
            {
                uIManager.scoreText = textComponet;
                uIManager.gameOverObject = gameOver;
                uIManager.stageLevelObejct = stageLevel;
            }
            AddObject(uIManagerObject);


            WayPointManager wayPointManager = new WayPointManager();
            WayPoints wayPoints = wayPointManager.GetComponent<WayPoints>();
            wayPoints.GameSize = new Vec2D(gameSize.Width,gameSize.Height);
            AddObject(wayPointManager);

            GameManagerObject gameManagerObject = new GameManagerObject();
            AddObject(gameManagerObject);

        }
    }
}
