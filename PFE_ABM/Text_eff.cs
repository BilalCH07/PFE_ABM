using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PFE_ABM
{
    public partial class Text_eff : Label
    {
        public Text_eff()
        {
            InitializeComponent();
            UseCompatibleTextRendering = true;
            this.BackColor = Color.White;
            timer1.Start();
        }
        int position, speed;
        public int Set_Speed { get { return speed; } set { speed = value; } }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform((float)position, 0);
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (position < -Width)
            {
                position = Width;
            }
            position -= speed;
            Invalidate();
        }
    }
}
