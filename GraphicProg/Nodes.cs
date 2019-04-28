using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GraphicProg
{
    public class Data
    {
        [Flags]
        public enum EType
        {
            INT = 1 << 0,
            FLOAT = 1 << 1,
            BOOL = 1 << 2,
            STRING = 1 << 3,
            UNIVERSAL = INT | FLOAT | BOOL | STRING,
            NUMERIC = INT | FLOAT,
            INPUT = 1 << 4,
            OUTPUT = 1 << 5
        }

        protected object m_Value;
        public EType m_nType { private set; get; }
        private ControlNode m_cParentNode;
        public Panel m_BackPanel { private set; get; }

        public Connector m_cConnector;

        public ControlNode GetParentNode()
        {
            return m_cParentNode;
        }

        public object GetValue()
        {
            if ((m_nType & EType.INPUT) != 0 && m_cConnector != null)
               return m_cConnector.Execute()?.GetValue();

            switch (m_nType)
            {
                case EType.INT:
                    return (int) m_Value;
                case EType.NUMERIC:
                case EType.FLOAT:
                    return (float) m_Value;
                case EType.BOOL:
                    return (bool) m_Value;
                case EType.STRING:
                    return (string) m_Value;
                default:
                    return m_Value;
            }
        }

        public void SetValue(object val)
        {
            switch (m_nType)
            {
                case EType.INT:
                    m_Value = (int) val;
                    break;
                case EType.NUMERIC:
                case EType.FLOAT:
                    m_Value = (float) val;
                    break;
                case EType.BOOL:
                    m_Value = (bool) val;
                    break;
                case EType.STRING:
                    m_Value = (string) val;
                    break;
                default:
                    m_Value = val;
                    break;
            }
        }

        public bool ValidValue() => m_Value != null;

        public Data(EType type, object val, ControlNode parent)
        {
            m_nType = type;
            SetValue(val);
            m_cParentNode = parent;

            m_BackPanel = new Panel()
            {
                Size = new Size(20,20),
                BackColor = Color.Transparent
            };
            m_BackPanel.Paint += (sender, args) => DrawSelf(args);

            m_BackPanel.MouseEnter += (sender, args) => ConnectController.SetEntered(this);
            m_BackPanel.MouseLeave += (sender, args) => ConnectController.SetEntered(null);
            m_BackPanel.MouseUp += (sender, args) => ConnectController.Connect(this, args);
            m_BackPanel.MouseMove += (sender, args) => ConnectController.Connect(this, args);
        }

        private void DrawSelf(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            paintEventArgs.Graphics.Clear(Color.AliceBlue);

            if ((ConnectController.m_eDrawType & (EType.INPUT | EType.OUTPUT) & m_nType) != 0 && (((ConnectController.m_eDrawType & EType.UNIVERSAL) & m_nType) != 0))
            {
                Color backColor = Color.Transparent;

                switch (m_nType & EType.UNIVERSAL)
                {
                    case EType.INT:
                        backColor = Color.Blue;
                        break;
                    case EType.FLOAT:
                        backColor = Color.Orange;
                        break;
                    case EType.BOOL:
                        backColor = Color.GreenYellow;
                        break;
                    case EType.STRING:
                        backColor = Color.Red;
                        break;
                    case EType.UNIVERSAL:
                        backColor = Color.Yellow;
                        break;
                    case EType.NUMERIC:
                        backColor = Color.Purple;
                        break;
                }

                paintEventArgs.Graphics.FillEllipse(new SolidBrush(backColor), 2, 2, 15, 15);
            }
        }

        public void RemoveSelf()
        {
            m_cConnector?.RemoveSelf();
            m_cConnector = null;
            m_cParentNode = null;
            m_Value = null;
        }
    }

    public class DataCollection
    {
        private Data[] m_arrData;
        public int m_nCount {private set; get; }

        public void RemoveSelf()
        {
            foreach (var v in m_arrData)
            {
                v.RemoveSelf();
            }

            m_arrData = null;
        }

        public DataCollection Copy(ControlNode controlNode)
        {
            DataCollection copyCollection = new DataCollection(m_nCount);
            for (int i = 0; i < m_nCount; ++i)
                copyCollection[i] = new Data(this[i].m_nType ,this[i].GetValue(), controlNode);
            return copyCollection;
        }

        public Data this[int num]
        {
            get
            {
                return num >= m_nCount ? null : m_arrData[num];
            }
            set
            {
                if (num < m_nCount) m_arrData[num] = value;
            }
        }

        public DataCollection(int count)
        {
            m_nCount = count;
            m_arrData = new Data[count];
        }
    }

    public abstract class ControlNode
    {
        public int m_iNodeID { private set; get; }

        protected DataCollection m_colInData;
        protected DataCollection m_colOutData;

        public Panel m_BackPanel { private set; get; }

        public abstract void Execute();

        public abstract ControlNode Copy(Point posPoint, int id = -1);

        private void MainDraw(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            paintEventArgs.Graphics.Clear(m_BackPanel.Parent.BackColor);
            Control control = m_BackPanel;
            int radius = 10;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(radius / 2, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius - 2, 0, radius, radius, 270, 90);
                path.AddLine(control.Width - 2, radius, control.Width - 2, control.Height - radius);
                path.AddArc(control.Width - radius - 2, control.Height - radius - 2, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height - 2, radius, control.Height - 2);
                path.AddArc(0, control.Height - radius - 2, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);

                using (SolidBrush brush = new SolidBrush(control.BackColor))
                {
                    paintEventArgs.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.DodgerBlue, 3))
                {
                    paintEventArgs.Graphics.DrawPath(pen, path);
                }
            }

            AddDraws(paintEventArgs);
        }

        protected void AddDraws(PaintEventArgs paintEventArgs)
        {
           
        }

        protected ControlNode(ControlNode toCopy, Point posPoint, int id) : this(new Data.EType[0], new Data.EType[0], Point.Empty, "", -1)
        {
            m_colInData = toCopy.m_colInData.Copy(this);
            m_colOutData = toCopy.m_colOutData.Copy(this);

            m_iNodeID = id;

            m_BackPanel = new Panel()
            {
                BackColor = Color.AliceBlue,
                Controls = { new Label() { Text = toCopy.m_BackPanel.Name, Location = new Point(5, 5), ForeColor = Color.DodgerBlue } },
                Location = posPoint,
                Size = new Size(250, 100 + 30 * (Math.Max(m_colInData.m_nCount, m_colOutData.m_nCount) - 1))
            };
            m_BackPanel.Paint += (sender, paintEventArgs) => { MainDraw(paintEventArgs); };

            m_BackPanel.MouseDown += (sender, args) => {
                DragAndDropController.DragNode(this, args);
                m_BackPanel.Focus();
            };
            m_BackPanel.MouseUp += (sender, args) => DragAndDropController.FreeNode(args);
            m_BackPanel.MouseMove += (sender, args) => DragAndDropController.MoveNode(args);

            var closeButton = new Button()
            {
                Size = new Size(40, 20),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance =
                {
                    CheckedBackColor = Color.Red,
                    MouseDownBackColor = Color.Red,
                    MouseOverBackColor = Color.Red
                },
                Location = new Point(200, 10),
                Text = @"❌",
                Font = new Font(FontFamily.GenericMonospace, 6),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red
            };
            m_BackPanel.Controls.Add(closeButton);

            closeButton.MouseEnter += (sender, args) => closeButton.ForeColor = Color.AliceBlue;
            closeButton.MouseLeave += (sender, args) => closeButton.ForeColor = Color.Red;
            closeButton.Click += (sender, args) => MainForm.m_cActiveNodeController.RemoveNode(this);

            for (int i = 0; i < m_colInData.m_nCount; ++i)
            {
                m_colInData[i].m_BackPanel.Location = new Point(15, 70 + 30 * i);
                m_BackPanel.Controls.Add(m_colInData[i].m_BackPanel);
            }

            for (int i = 0; i < m_colOutData.m_nCount; ++i)
            {
                m_colOutData[i].m_BackPanel.Location = new Point(250 - 35, 70 + 30 * i);
                m_BackPanel.Controls.Add(m_colOutData[i].m_BackPanel);
            }

        }

        protected ControlNode(Data.EType[] inTypes, Data.EType[] outTypes, Point posPoint, string szName, int id)
        {
            
            m_iNodeID = id;
            m_colInData = new DataCollection(inTypes.Length);
            m_colOutData = new DataCollection(outTypes.Length);

            m_BackPanel = new Panel()
            {
                Name = szName,
                BackColor = Color.AliceBlue,
                Controls = { new Label() {Text = szName, Location = new Point(5, 5), ForeColor = Color.DodgerBlue } },
                Location = posPoint,
                Size = new Size(250, 100 + 30 * (Math.Max(m_colInData.m_nCount, m_colOutData.m_nCount) - 1))
            };
            m_BackPanel.Paint += (sender, paintEventArgs) => { MainDraw(paintEventArgs); };

            m_BackPanel.MouseDown += (sender, args) => DragAndDropController.DragNode(this, args);
            m_BackPanel.MouseUp += (sender, args) => DragAndDropController.FreeNode(args);
            m_BackPanel.MouseMove += (sender, args) => DragAndDropController.MoveNode(args);

            #region DataInit
            for (var i = 0; i < inTypes.Length; ++i)
            {
                switch (inTypes[i])
                {
                    case Data.EType.INT:
                        m_colInData[i] = new Data(Data.EType.INT | Data.EType.INPUT, 0, this);
                        break;
                    case Data.EType.NUMERIC:
                        m_colInData[i] = new Data(Data.EType.NUMERIC | Data.EType.INPUT, .0f, this);
                        break;
                    case Data.EType.FLOAT:
                        m_colInData[i] = new Data(Data.EType.FLOAT | Data.EType.INPUT, .0f, this);
                        break;
                    case Data.EType.BOOL:
                        m_colInData[i] = new Data(Data.EType.BOOL | Data.EType.INPUT, false, this);
                        break;
                    case Data.EType.STRING:
                        m_colInData[i] = new Data(Data.EType.STRING | Data.EType.INPUT, "", this);
                        break;
                    default:
                        m_colInData[i] = new Data(Data.EType.UNIVERSAL | Data.EType.INPUT, "", this);
                        break;
                }
            }

            for (var i = 0; i < outTypes.Length; ++i)
            {
                switch (outTypes[i])
                {
                    case Data.EType.INT:
                        m_colOutData[i] = new Data(Data.EType.INT | Data.EType.OUTPUT, 0, this);
                        break;
                    case Data.EType.NUMERIC:
                        m_colOutData[i] = new Data(Data.EType.NUMERIC | Data.EType.OUTPUT, .0f, this);
                        break;
                    case Data.EType.FLOAT:
                        m_colOutData[i] = new Data(Data.EType.FLOAT | Data.EType.OUTPUT, .0f, this);
                        break;
                    case Data.EType.BOOL:
                        m_colOutData[i] = new Data(Data.EType.BOOL | Data.EType.OUTPUT, false, this);
                        break;
                    case Data.EType.STRING:
                        m_colOutData[i] = new Data(Data.EType.STRING | Data.EType.OUTPUT, "", this);
                        break;
                    default:
                        m_colOutData[i] = new Data(Data.EType.UNIVERSAL | Data.EType.OUTPUT, "", this);
                        break;
                }
            }
            #endregion

            for (int i = 0; i < m_colInData.m_nCount; ++i)
            {
                m_colInData[i].m_BackPanel.Location = new Point(15, 70 + 30 * i);
                m_BackPanel.Controls.Add(m_colInData[i].m_BackPanel);
            }

            for (int i = 0; i < m_colOutData.m_nCount; ++i)
            {
                m_colOutData[i].m_BackPanel.Location = new Point(250 - 35, 70 + 30 * i);
                m_BackPanel.Controls.Add(m_colOutData[i].m_BackPanel);
            }
        }

        public void RemoveSelf()
        {
            m_colInData.RemoveSelf();
            m_colOutData.RemoveSelf();

            m_colInData = null;
            m_colOutData = null;

            m_BackPanel = null;
        }
    }

    class MathNodeSum : ControlNode
    {
        public MathNodeSum(Point posPoint, int id) : base(new [] {Data.EType.NUMERIC, Data.EType.NUMERIC}, new[] { Data.EType.NUMERIC }, posPoint, "Sum", id)
        {
        }

        private MathNodeSum(MathNodeSum toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
        }

        public override void Execute()
        {
            m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) + Convert.ToSingle(m_colInData[1].GetValue()));
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new MathNodeSum(this, posPoint, id);
        }
    }

    class MathNodeSub : ControlNode
    {
        public MathNodeSub(Point posPoint, int id) : base(new[] { Data.EType.NUMERIC, Data.EType.NUMERIC }, new[] { Data.EType.NUMERIC }, posPoint, "Sub", id)
        {
        }

        private MathNodeSub(MathNodeSub toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new MathNodeSub(this, posPoint, id);
        }

        public override void Execute()
        {
            m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) - Convert.ToSingle(m_colInData[1].GetValue()));
        }
    }

    class MathNodeMult : ControlNode
    {
        public MathNodeMult(Point posPoint, int id) : base(new[] { Data.EType.NUMERIC, Data.EType.NUMERIC }, new[] { Data.EType.NUMERIC }, posPoint, "Mult", id)
        {
        }

        private MathNodeMult(MathNodeMult toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new MathNodeMult(this, posPoint, id);
        }

        public override void Execute()
        {
            m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) * Convert.ToSingle(m_colInData[1].GetValue()));
        }
    }

    class MathNodeDiv : ControlNode
    {
        public MathNodeDiv(Point posPoint, int id) : base(new[] { Data.EType.NUMERIC, Data.EType.NUMERIC }, new[] { Data.EType.NUMERIC }, posPoint, "Div", id)
        {
        }

        private MathNodeDiv(MathNodeDiv toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new MathNodeDiv(this, posPoint, id);
        }

        public override void Execute()
        {
            m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) / Convert.ToSingle(m_colInData[1].GetValue()));
        }
    }

    class ConstantNode : ControlNode
    {
        private TextBox m_Textbox;
        public ConstantNode(ControlNode toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
            m_Textbox = new TextBox();
            m_Textbox.Location = new Point(m_BackPanel.Size.Width / 2 - m_Textbox.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_Textbox.Size.Height / 2);

            m_Textbox.TextChanged += (sender, args) =>
            {
                if (m_Textbox.Text != "" && m_Textbox.Text[m_Textbox.Text.Length - 1] != ',')
                {
                    try
                    {
                        m_colOutData[0].SetValue(Convert.ToSingle(m_Textbox.Text));
                    }
                    catch
                    {
                        m_Textbox.Text = m_Textbox.Text.Remove(m_Textbox.Text.Length - 1);

                        m_Textbox.SelectionStart = m_Textbox.Text.Length;

                        m_colOutData[0].SetValue(m_Textbox.Text != "" ? Convert.ToSingle(m_Textbox.Text) : 0);
                    }
                }
                else
                    m_colOutData[0].SetValue(0);
            };

            m_BackPanel.Controls.Add(m_Textbox);
        }

        public ConstantNode(Point posPoint, int id) : base(new Data.EType[0], new[] { Data.EType.NUMERIC }, posPoint, "Constant", id)
        {
            m_Textbox = new TextBox();
            m_Textbox.Location = new Point(m_BackPanel.Size.Width / 2 - m_Textbox.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_Textbox.Size.Height / 2);

            m_Textbox.TextChanged += (sender, args) => m_Textbox.Text = @"0";

            m_BackPanel.Controls.Add(m_Textbox);
        }

        public override void Execute()
        {
            
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new ConstantNode(this, posPoint, id);
        }
    }

    class ValueView : ControlNode
    {
        private TextBox m_Textbox;
        private Timer m_Timer;

        public ValueView(ControlNode toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
            m_colInData[0].SetValue(0);
            m_Timer = new Timer {Interval = 100};
            m_Timer.Tick += (sender, args) => Execute();
            m_Timer.Start();

            m_Textbox = new TextBox();
            m_Textbox.Location = new Point(m_BackPanel.Size.Width / 2 - m_Textbox.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_Textbox.Size.Height / 2);

            m_BackPanel.Controls.Add(m_Textbox);
        }

        public ValueView(Point posPoint, int id) : base(new []{Data.EType.NUMERIC}, new Data.EType[0], posPoint, "ValueView", id)
        {
            m_Textbox = new TextBox();
            m_Textbox.Location = new Point(m_BackPanel.Size.Width / 2 - m_Textbox.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_Textbox.Size.Height / 2);
            m_BackPanel.Controls.Add(m_Textbox);
        }

        public override void Execute()
        {
            m_Textbox.Text = m_colInData?[0]?.GetValue()?.ToString();

            if (m_colInData != null) return;
            m_Textbox = null;
            m_Timer.Stop();
            m_Timer = null;
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new ValueView(this, posPoint, id);
        }
    }

    class CompareNode : ControlNode
    {
        private readonly ComboBox m_CompareSelector;

        public CompareNode(ControlNode toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
            m_CompareSelector = new ComboBox()
            {
                FlatStyle = FlatStyle.Flat,
                Items = { "=", "≠", ">", "<", ">=", "<=" }
            };
            m_CompareSelector.Location = new Point(m_BackPanel.Size.Width / 2 - m_CompareSelector.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_CompareSelector.Size.Height / 2);

            m_BackPanel.Controls.Add(m_CompareSelector);
        }

        public CompareNode(Point posPoint, int id) : base(new []{Data.EType.NUMERIC, Data.EType.NUMERIC}, new []{Data.EType.BOOL}, posPoint, "Compare", id)
        {
            m_CompareSelector = new ComboBox()
            {
                FlatStyle = FlatStyle.Flat,
                Items = { "=", "≠", ">", "<", ">=", "<=" }
            };
            m_CompareSelector.Location = new Point(m_BackPanel.Size.Width / 2 - m_CompareSelector.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_CompareSelector.Size.Height / 2);

            m_BackPanel.Controls.Add(m_CompareSelector);
        }

        public override void Execute()
        {
            
            switch (m_CompareSelector.SelectedIndex)
            {
                case 0:
                    m_colOutData[0].SetValue(Math.Abs(Convert.ToSingle(m_colInData[0].GetValue()) - Convert.ToSingle(m_colInData[1].GetValue())) < .00001);
                    break;
                case 1:
                    m_colOutData[0].SetValue(Math.Abs(Convert.ToSingle(m_colInData[0].GetValue()) - Convert.ToSingle(m_colInData[1].GetValue())) > .00001);
                    break;
                case 2:
                    m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) > Convert.ToSingle(m_colInData[1].GetValue()));
                    break;
                case 3:
                    m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) < Convert.ToSingle(m_colInData[1].GetValue()));
                    break;
                case 4:
                    m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) >= Convert.ToSingle(m_colInData[1].GetValue()));
                    break;
                case 5:
                    m_colOutData[0].SetValue(Convert.ToSingle(m_colInData[0].GetValue()) <= Convert.ToSingle(m_colInData[1].GetValue()));
                    break;
            }
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new CompareNode(this, posPoint, id);
        }
    }

    class BoolView : ControlNode
    {
        private Panel m_ShowPanel;
        private Timer m_Timer;

        public BoolView(ControlNode toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
            m_ShowPanel = new Panel()
            {
                BackColor = Color.Red,
                Size = new Size(50,40)
            };
            m_ShowPanel.Location = new Point(m_BackPanel.Size.Width / 2 - m_ShowPanel.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_ShowPanel.Size.Height / 2);
            m_Timer = new Timer { Interval = 100 };
            m_Timer.Tick += (sender, args) => Execute();
            m_Timer.Start();

            m_BackPanel.Controls.Add(m_ShowPanel);
        }

        public BoolView(Point posPoint, int id) : base(new []{Data.EType.BOOL}, new Data.EType[0], posPoint, "BoolView", id)
        {
            m_ShowPanel = new Panel()
            {
                BackColor = Color.Red,
                Size = new Size(50, 40)
            };
            m_ShowPanel.Location = new Point(m_BackPanel.Size.Width / 2 - m_ShowPanel.Size.Width / 2,
                m_BackPanel.Size.Height / 2 - m_ShowPanel.Size.Height / 2);

            m_BackPanel.Controls.Add(m_ShowPanel);
        }

        public override void Execute()
        {
            m_ShowPanel.BackColor = Convert.ToBoolean(m_colInData?[0]?.GetValue()) ? Color.Green : Color.Red;

            if (m_colInData != null) return;
            m_ShowPanel = null;
            m_Timer.Stop();
            m_Timer = null;
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new BoolView(this, posPoint, id);
        }
    }

    class CustomNode : ControlNode
    {
        private Panel m_WorkPanel;
        private NodeController m_CNodeController;
        private Button m_ExitButton;
        private Button m_CreateLogicButton;

        public CustomNode(CustomNode toCopy, Point posPoint, int id) : base(toCopy, posPoint, id)
        {
        }

        public CustomNode(Point posPoint, int id) : base(new Data.EType[0], new Data.EType[0], posPoint, "Custom Node", id)
        {
        }

        public override void Execute()
        {
            
        }

        public override ControlNode Copy(Point posPoint, int id = -1)
        {
            return new CustomNode(this, posPoint, id);
        }
    }
}
