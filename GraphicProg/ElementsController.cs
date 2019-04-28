using System.Drawing;
using System.Windows.Forms;

namespace GraphicProg
{
    class ElementsController
    {
        public int m_iOffset { private set; get; }
        private readonly Panel m_ElementsPanel;

        public ElementsController(Panel container)
        {
            m_ElementsPanel = container;
        }

        public void AddNode(ControlNode node)
        {
            node.m_BackPanel.Location = new Point(10, m_iOffset);
            m_iOffset += node.m_BackPanel.Size.Height + 10;
            m_ElementsPanel.Controls.Add(node.m_BackPanel);
        }

        public void RemoveNode(ControlNode node)
        {
            m_ElementsPanel.Controls.Remove(node.m_BackPanel);
            m_iOffset = 0;

            foreach (Control control in m_ElementsPanel.Controls)
            {
                control.Location = new Point(10, m_iOffset);
                m_iOffset += control.Size.Height + 10;
            }
        }
    }
}
