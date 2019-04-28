using System.Drawing;
using System.Windows.Forms;

namespace GraphicProg
{
    static class DragAndDropController
    {
        private static ControlNode m_cNode;
        private static bool m_bAllowMove;
        private static Point m_vec2ClickPosition;

        public static void DragNode(ControlNode node, MouseEventArgs args)
        {
            if (args.Button != MouseButtons.Left) return;

            if (node.m_iNodeID == -1)
            {
                CreateNode(node);
            }
            else
            {
                m_cNode = node;
                m_bAllowMove = true;
                m_vec2ClickPosition = args.Location;
                m_cNode.m_BackPanel.BringToFront();
            }
        }

        public static void MoveNode(MouseEventArgs args)
        {
            if (m_cNode != null && m_bAllowMove)
            {
                m_cNode.m_BackPanel.Location = new Point(m_cNode.m_BackPanel.Location.X + args.X - m_vec2ClickPosition.X, m_cNode.m_BackPanel.Location.Y + args.Y - m_vec2ClickPosition.Y);
            }
        }

        public static void FreeNode(MouseEventArgs args)
        {
            if (args.Button != MouseButtons.Left) return;

            if (!m_bAllowMove)
                MainForm.m_cActiveNodeController.AddNode(m_cNode);
            m_cNode = null;
            m_bAllowMove = false;
        }

        public static void CreateNode(ControlNode node)
        {
            m_cNode = node.Copy(Cursor.Position, NodeController.GenerateNodeId());
        }
    }
}
