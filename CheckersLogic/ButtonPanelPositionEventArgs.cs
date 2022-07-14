using System;
using System.Drawing;

namespace CheckersLogic
{
    public delegate void MovementEffectEventHandler(ButtonPanelPositionEventArgs e);

    public class ButtonPanelPositionEventArgs : EventArgs
    {
        private Point m_PanelPosition;

        public ButtonPanelPositionEventArgs(Point i_PanelPosition)
        {
            m_PanelPosition = i_PanelPosition;
        }

        public Point PanelPosition 
        { 
            get { return m_PanelPosition; } 
        }
    }
}
