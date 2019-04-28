using System.Windows.Forms;

namespace GraphicProg
{

    static class ConnectController
    {
        private static Data m_cStartConnectData;
        private static MouseButtons m_eLastMouseButton;
        private static Data m_cLastEntered;
        private static Connector m_cTempConnector;

        public static Data.EType m_eDrawType { private set; get; } = Data.EType.UNIVERSAL | Data.EType.INPUT | Data.EType.OUTPUT;

        private static void StartConnect(Data starter)
        {
            m_cStartConnectData = starter;
            m_eDrawType = starter.m_nType & Data.EType.UNIVERSAL | ((starter.m_nType & Data.EType.INPUT) != 0 ? Data.EType.OUTPUT : Data.EType.INPUT);
            MainForm.m_cActiveNodeController.m_BluePanel.Refresh();
            m_cTempConnector?.RemoveSelf();
            m_cTempConnector = new Connector(starter);
        }

        public static void SetEntered(Data data)
        {
            m_cLastEntered = data;
        }

        private static void FinishConnect(Data finisher)
        {
            var input = (m_cStartConnectData?.m_nType & Data.EType.INPUT) != 0 ? m_cStartConnectData : finisher;

            if (finisher != null && m_cStartConnectData != null && finisher != m_cStartConnectData)
            {
                if (m_cStartConnectData != finisher && m_cStartConnectData.GetParentNode() != finisher.GetParentNode() && (m_cStartConnectData.m_nType & finisher.m_nType) != 0)
                {
                    m_cTempConnector.FinishConnect(finisher);
                    input.m_cConnector = m_cTempConnector;
                    m_cTempConnector = null;
                }
            }

            m_eDrawType = Data.EType.UNIVERSAL | Data.EType.INPUT | Data.EType.OUTPUT;
            m_cStartConnectData = null;
            m_cTempConnector?.RemoveSelf();
            m_cTempConnector = null;
            MainForm.m_cActiveNodeController.m_BluePanel.Refresh();
        }

        public static void CheckState()
        {
            if (!m_cTempConnector?.m_bFinishedConnector ?? (m_eDrawType != (Data.EType.UNIVERSAL | Data.EType.INPUT | Data.EType.OUTPUT) && MainForm.MouseButtons != MouseButtons.Left))
            {
                m_eDrawType = Data.EType.UNIVERSAL | Data.EType.INPUT | Data.EType.OUTPUT;
                MainForm.m_cActiveNodeController.m_BluePanel.Refresh();
                m_cTempConnector = null;
                m_cStartConnectData = null;
            }
        }

        public static void Connect(Data sender, MouseEventArgs args)
        {
            if (sender.GetParentNode().m_iNodeID == -1) return;

            if (m_cStartConnectData == null && args.Button == MouseButtons.Left)
                StartConnect(sender);
            if (args.Button != MouseButtons.Left && m_eLastMouseButton == MouseButtons.Left)
                FinishConnect(m_cLastEntered);
            m_eLastMouseButton = args.Button;
            MainForm.m_cActiveNodeController.Check();
        }
    }
}
