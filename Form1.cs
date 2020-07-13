using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WinformRender.Instance.Init(this);

            SceneManager.LoadScene<Galaga>();

            GameEngine.Instance.Run();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            InputWinform.Instance.SetKeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            InputWinform.Instance.SetKeyUp(e);
        }
    }
}