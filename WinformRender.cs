using Engine;
using Galaga.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaga
{
    /// <summary>
    /// 윈폼에서 게임오브젝트를 출력해준다
    /// </summary>
    class WinformRender : Single.Singleton<WinformRender>
    {
        /// <summary>
        /// 출력할 게임 오브젝트들 이 리스트는 씬과 공유한다.
        /// </summary>
        List<GameObject> gameObjects;
        public Form NowForm { get; private set; }
        /// <summary>
        /// 출력할 위폼을 정해준다
        /// </summary>
        /// <param name="form">출력할 위폼</param>
        public void Init(Form form)
        {
            NowForm = form;
            form.Paint += Render;
        }


        /// <summary>
        /// 게임 오브젝트를 설정한다.
        /// </summary>
        /// <param name="objs">출력할 게임 오브젝트 리스트</param>
        public void SetGameObject(in List<GameObject> objs)
        {
            gameObjects = objs;
        }



        /// <summary>
        /// 이미지를 회전시켜 출력시켜준다.
        /// </summary>
        /// <param name="img">회전시킬 이미지</param>
        /// <param name="rotationAngle">회전할 각도</param>
        /// <returns>회전시킨 이미지</returns>
        public static Image RotateImage(Image img, float rotationAngle)
        {
            // 빈 이미지를 생성
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            // 새로 생성한 이미지를 그리기 위한 그래픽 생성
            Graphics gfx = Graphics.FromImage(bmp);

            // 좌표계 원점을 이동
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            // 원점 중심으로 이미지를 회전시킨다.
            gfx.RotateTransform((rotationAngle % 360));

            // 원점 되돌리기
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            // 이미지 품질 변환
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // 이미지를 회전한 비트맨에 출력
            gfx.DrawImage(img, new Point(0,0));
           
            // 새로운 비트맵 그래픽을 해제
            gfx.Dispose();

            // 그린 이미지 반환
            return bmp;
        }

        /// <summary>
        /// 랜더링을한다.
        /// 이 함수는 윈포에 OnPaint 이벤트에 넣었다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Render(HDC hdc)
        {
            SpriteComponent spriteComponent = null;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject obj = gameObjects[i];
                if (obj.Enabled == false)
                    continue;

                TextComponet text = obj.GetComponent<TextComponet>();
                if (text != null)
                {
                    e.Graphics.DrawString(text.Text, text.Font, text.Brush, new PointF(obj.transform.position.X, obj.transform.position.Y));
                }

                spriteComponent = obj.GetComponent<SpriteComponent>();

                if (spriteComponent == null)
                    continue;
                

                float x = obj.transform.position.X;
                float y = obj.transform.position.Y;
                float angle = obj.transform.Rotation;


                Image img = RotateImage(spriteComponent.Image, angle);
                GraphicsUnit units = GraphicsUnit.Point;

                RectangleF rf = img.GetBounds(ref units);
                rf.X = x - rf.Width/2;
                rf.Y = y - rf.Height/2;
                e.Graphics.DrawImage(img, rf);
            }
        }
    }
}
