using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicProg
{
    static class FormController
    {
        private static bool m_bFormMove = false;
        private static bool m_bFormResize = false;
        private static Point m_vec2ClickPos = new Point();

        public static void OnMove(MouseEventArgs e)
        {
            if (m_bFormMove)
            {
                if (Form.ActiveForm != null && Form.ActiveForm.WindowState == FormWindowState.Maximized)
                {
                    var backup = Form.ActiveForm.Size.Width;
                    Form.ActiveForm.WindowState = FormWindowState.Normal;
                    m_vec2ClickPos = new Point(Convert.ToInt32((double)((double)Form.ActiveForm.Size.Width / (double)backup) * m_vec2ClickPos.X), Convert.ToInt32((double)((double)Form.ActiveForm.Size.Width / (double)backup) * m_vec2ClickPos.Y));
                }

                if (Form.ActiveForm != null)
                    Form.ActiveForm.Location = new Point(Form.ActiveForm.Location.X + e.Location.X - m_vec2ClickPos.X,
                        Form.ActiveForm.Location.Y + e.Location.Y - m_vec2ClickPos.Y);
            }
        }

        public static void OnUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_bFormMove = false;
            }
        }

        public static void OnDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_bFormMove = true;
                m_vec2ClickPos = e.Location;
            }
        }

        public static void OnFormMouseMove(MouseEventArgs e)
        {
            if (Form.ActiveForm == null ||
                ((Math.Abs(e.Location.X - Form.ActiveForm.Size.Width) >= 5 ||
                  Math.Abs(e.Location.Y - Form.ActiveForm.Size.Height) >= 5) && !m_bFormResize)) return;
            Cursor.Current = Cursors.SizeNWSE;
            m_bFormResize = e.Button == MouseButtons.Left;
            if (m_bFormResize)
                Form.ActiveForm.Size = new Size(e.X > 360 ? e.X : 360, e.Y > 100 ? e.Y : 100);

        }
    }
}
