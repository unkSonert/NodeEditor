using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicProg
{
    public partial class MainForm : Form
    {
        private NodeController m_cNodeController;
        public static NodeController m_cActiveNodeController { private set; get; }
        private ElementsController m_cElementController;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MouseEnterChangeColor(object sender, EventArgs e)
        {
            if (sender is Control control)
                control.ForeColor = Color.AliceBlue;
        }

        private void MouseLeaveChangeColor(object sender, EventArgs e)
        {
            if (sender is Control control)
                control.ForeColor = Color.FromArgb(110, 110, 110);
        }

        private void FormMove(object sender, MouseEventArgs e)
        {
            FormController.OnMove(e);
        }

        private void UpPanelUpMouse(object sender, MouseEventArgs e)
        {
            FormController.OnUp(e);
        }

        private void UpPanelDownMouse(object sender, MouseEventArgs e)
        {
            FormController.OnDown(e);
        }

        private void HideForm(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private new void Resize(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void OnResize(object sender, EventArgs e)
        {
            ResizeButton.Text = WindowState == FormWindowState.Maximized ? "🗗" : "🗖";
        }

        private void OnMouseMoveInForm(object sender, MouseEventArgs e)
        {
            FormController.OnFormMouseMove(e);
        }

        private void ResizeElPanel(object sender, EventArgs e)
        {
            ElPanel.Size = new Size(ElPanel.Size.Width == 60 ? 350 : 60, ElPanel.Size.Height);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            m_cNodeController = new NodeController(WorkPlace);
            m_cElementController = new ElementsController(ElementContainer);
            m_cActiveNodeController = m_cNodeController;

            m_cElementController.AddNode(new MathNodeSum(new Point(0, 0), -1));
            m_cElementController.AddNode(new MathNodeSub(new Point(0, 0), -1));
            m_cElementController.AddNode(new MathNodeMult(new Point(0, 0), -1));
            m_cElementController.AddNode(new MathNodeDiv(new Point(0, 0), -1));
            m_cElementController.AddNode(new ConstantNode(new Point(0, 0), -1));
            m_cElementController.AddNode(new ValueView(new Point(0, 0), -1));
            m_cElementController.AddNode(new BoolView(new Point(0, 0), -1));
            m_cElementController.AddNode(new CompareNode(new Point(0, 0), -1));

        }

        public void SetDefaultController()
        {
            WorkPlace.Controls.Remove(m_cActiveNodeController.m_BluePanel);
            m_cActiveNodeController = m_cNodeController;
            WorkPlace.Controls.Add(m_cActiveNodeController.m_BluePanel);
        }

        public void SetController(NodeController controller)
        {
            WorkPlace.Controls.Remove(m_cActiveNodeController.m_BluePanel);
            m_cActiveNodeController = controller;
            WorkPlace.Controls.Add(m_cActiveNodeController.m_BluePanel);
        }
    }
}
