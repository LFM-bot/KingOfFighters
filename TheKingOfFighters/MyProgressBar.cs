using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TheKingOfFighters
{
    public class MyProgressBar : ProgressBar
    {
        private Color _color;
        public MyProgressBar(Color color)
        {
            _color = color;
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        //重写OnPaint方法
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush brush = null;
            Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
            bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum))));
            brush = new SolidBrush(_color);//Color.FromArgb(198,47,47)
            e.Graphics.FillRectangle(brush, 0, 0, bounds.Width, bounds.Height);

        }
    }

}
