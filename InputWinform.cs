using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Single;

namespace Galaga
{
    /// <summary>
    /// 윈폼에서 사용자가 입력한 키보드를 저장한다.
    /// </summary>
    public class InputWinform: Singleton<InputWinform>
    {
        public KeyEventArgs lastKeyEvent { get; private set; }

        private List<Keys> downKeys;

        public InputWinform()
        {
            downKeys = new List<Keys>();
        }
        public void SetKeyDown(KeyEventArgs e)
        {
            lastKeyEvent = e;

            foreach (var key in downKeys)
            {
                if (key == e.KeyCode)
                    return;
            }

            downKeys.Add(e.KeyCode);
        }
               
        public void SetKeyUp(KeyEventArgs e)
        {
            lastKeyEvent = e;

            foreach (var key in downKeys)
            {
                if (key == e.KeyCode)
                {
                    downKeys.Remove(e.KeyCode);
                    return;
                }
            }
        }

        public bool GetKeyDown(Keys keys)
        {
            foreach (var key in downKeys)
            {
                if (key == keys)
                    return true;
            }
            return false;
        }
        
    }
}
