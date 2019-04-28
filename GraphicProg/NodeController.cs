using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicProg
{
    public class NodeController
    {
        private static int m_iHighId;
        public Panel m_BluePanel { get; private set; }
        private Point m_vec2ClickPosition;
        private bool m_bAllowMoveBackPanel;
        private static Vector<int> m_arrRemoveId;
        private ControlNode m_cNodeToAdd;
        private List<ControlNode> m_listNode;

        public NodeController(Panel workPanel)
        {
            m_listNode = new List<ControlNode>();
            m_arrRemoveId = new Vector<int>();

            m_BluePanel = new Panel()
            {
                Size = new Size(4000, 4000),
                Location = new Point(-1000, -1000),
                BackColor = Color.DeepSkyBlue,
            };
            workPanel.Controls.Add(m_BluePanel);

            m_BluePanel.MouseDown += (sender, args) =>
            {
                m_bAllowMoveBackPanel = true;
                m_vec2ClickPosition = args.Location;
            };

            m_BluePanel.MouseUp += (sender, args) => m_bAllowMoveBackPanel = false;

            m_BluePanel.MouseMove += (sender, args) =>
            {
                if (m_bAllowMoveBackPanel && args.Button == MouseButtons.Left)
                {
                    m_BluePanel.Location = new Point(m_BluePanel.Location.X + args.X - m_vec2ClickPosition.X,
                        m_BluePanel.Location.Y + args.Y - m_vec2ClickPosition.Y);
                }

                if (m_cNodeToAdd != null)
                {
                    m_BluePanel.Controls.Add(m_cNodeToAdd.m_BackPanel);
                    m_cNodeToAdd.m_BackPanel.Location = args.Location;
                    m_cNodeToAdd = null;
                }

                ConnectController.CheckState();
            };
        }

        public static int GenerateNodeId()
        {
            if (m_arrRemoveId.Count == 0)
                return ++m_iHighId;

            var retValue = m_arrRemoveId.Front;
            m_arrRemoveId.RemoveFirst();
            return retValue;
        }

        public void RemoveNode(ControlNode node)
        {
            m_listNode.Remove(node);
            m_BluePanel.Controls.Remove(node.m_BackPanel);
            m_arrRemoveId.PushBack(node.m_iNodeID);
            node.RemoveSelf();
            Check();
        }

        public void AddNode(ControlNode node)
        {
            m_cNodeToAdd = node;
            m_listNode.Add(node);
        }

        public void Check()
        {
            foreach (Control control in m_BluePanel.Controls)
            {
                if (control is Connector connector && connector.m_cInput?.m_cConnector != control && connector.m_bFinishedConnector)
                    m_BluePanel.Controls.Remove(connector);
            }
        }

        public Panel CopyWorkPanel()
        {
            Panel returnPanel = new Panel()
            {
                Size = new Size(4000, 4000),
                Location = new Point(-1000, -1000),
                BackColor = Color.DeepSkyBlue,
            };



            return returnPanel;
        }
    }
}
