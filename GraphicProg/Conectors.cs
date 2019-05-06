using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GraphicProg
{
    public partial class Connector : UserControl
    {
        public Data m_cInput { private set; get; }
        private Data m_cOutput;
        private Timer m_TickGenerator;
        public bool m_bFinishedConnector { private set; get; }

        public Connector(Data data)
        {
            InitializeComponent();

            m_bFinishedConnector = false;

            if ((data.m_nType & Data.EType.INPUT) != 0)
                m_cInput = data;
            else
                m_cOutput = data;

            MainForm.m_cActiveNodeController.m_BluePanel.Controls.Add(this);

            m_TickGenerator = new Timer { Interval = 1000 / 20 };
            m_TickGenerator.Tick += (sender, args) => Update();
            m_TickGenerator.Start();

            Update();
        }

        public void FinishConnect(Data data)
        {
            if ((data.m_nType & Data.EType.INPUT) != 0)
                m_cInput = data;
            else
                m_cOutput = data;

            m_bFinishedConnector = true;
        }

        public Connector(Data input, Data output)
        {
            InitializeComponent();

            m_bFinishedConnector = true;

            m_cInput = input;
            m_cOutput = output;

            MainForm.m_cActiveNodeController.m_BluePanel.Controls.Add(this);

            m_TickGenerator = new Timer {Interval = 1000 / 60};
            m_TickGenerator.Tick += (sender, args) => Update();
            m_TickGenerator.Start();

            Update();
        }

        public new void Update()
        {
            if ((m_cInput == null || m_cOutput == null || !m_cInput.ValidValue() || !m_cOutput.ValidValue()) &&
                m_bFinishedConnector)
            {
                RemoveSelf();
                return;
            }

            if (Form.ActiveForm != null)
            {
                var input = m_cInput == null
                    ? new Point(
                        MousePosition.X - MainForm.m_cActiveNodeController.m_BluePanel.Location.X -
                        Form.ActiveForm.Location.X - 70,
                        MousePosition.Y - MainForm.m_cActiveNodeController.m_BluePanel.Location.Y -
                        Form.ActiveForm.Location.Y - 40)
                    : new Point(m_cInput.m_BackPanel.Parent.Location.X + m_cInput.m_BackPanel.Location.X + 7,
                        m_cInput.m_BackPanel.Parent.Location.Y + m_cInput.m_BackPanel.Location.Y + 7);
                var output = m_cOutput == null
                    ? new Point(
                        MousePosition.X - MainForm.m_cActiveNodeController.m_BluePanel.Location.X -
                        Form.ActiveForm.Location.X - 70,
                        MousePosition.Y - MainForm.m_cActiveNodeController.m_BluePanel.Location.Y -
                        Form.ActiveForm.Location.Y - 40)
                    : new Point(m_cOutput.m_BackPanel.Parent.Location.X + m_cOutput.m_BackPanel.Location.X + 7,
                        m_cOutput.m_BackPanel.Parent.Location.Y + m_cOutput.m_BackPanel.Location.Y + 7);

                var deltaPosition = new Point(output.X - input.X, output.Y - input.Y);

                var startPosition = deltaPosition.X > 0 ? input : output;
                var endPosition = deltaPosition.X > 0 ? output : input;

                var upperDelta = deltaPosition.X > 0 ? deltaPosition.Y > 0 ? 0 : 5 : deltaPosition.Y > 0 ? 5 : 0;
                var lowerDelta = deltaPosition.X > 0 ? deltaPosition.Y > 0 ? 5 : 0 : deltaPosition.Y > 0 ? 0 : 5;
                var delta = Math.Abs(deltaPosition.X / 5) > 10 ? Math.Abs(deltaPosition.X / 5) : 10;
                using (var graphicsPath = new GraphicsPath())
                {
                    graphicsPath.AddPolygon(new[]
                    {
                        new PointF(startPosition.X, startPosition.Y),
                        new PointF(startPosition.X + delta - upperDelta, startPosition.Y),
                        new PointF(endPosition.X - delta - upperDelta, endPosition.Y),
                        new PointF(endPosition.X, endPosition.Y), new PointF(endPosition.X, endPosition.Y + 5),
                        new PointF(endPosition.X - delta - lowerDelta, endPosition.Y + 5),
                        new PointF(startPosition.X + delta - lowerDelta, startPosition.Y + 5),
                        new PointF(startPosition.X, startPosition.Y + 5)
                    });

                    Region = new Region(graphicsPath);
                }

                BringToFront();

                if (!m_bFinishedConnector && MouseButtons != MouseButtons.Left)
                    RemoveSelf();

            }
        }

        public Data Execute()
        {
            m_cOutput?.GetParentNode()?.Execute();
            return m_cOutput;
        }

        public void RemoveSelf()
        {
            if (m_cInput != null)
                m_cInput.m_cConnector = null;
            m_TickGenerator?.Stop();
            m_TickGenerator = null;
            m_cOutput = null;
            m_cInput = null;
            MainForm.m_cActiveNodeController.m_BluePanel.Controls.Remove(this);
        }
    }
}
