using System;
using System.Drawing;

namespace CheckersLogic
{
    public delegate void MoveEventHandler(MovePositionEventArgs e);

    public class MovePositionEventArgs : EventArgs
    {
        private Point m_FromWhere;
        private Point m_ToWhere;

        public MovePositionEventArgs(Point i_FromWhere, Point i_ToWhere)
        {
            m_FromWhere = i_FromWhere;
            m_ToWhere = i_ToWhere;
        }

        public Point FromWhere 
        { 
            get { return m_FromWhere; } 
        }

        public Point ToWhere 
        { 
            get { return m_ToWhere; } 
        }
    }
}
