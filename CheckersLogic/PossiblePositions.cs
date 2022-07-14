using System.Drawing;

namespace CheckersLogic
{
    public class PossiblePositions
    {
        private Point m_SkippedToolPosition, m_PlayerNextPosition;

        public PossiblePositions()
        {
            m_SkippedToolPosition = new Point();
            m_PlayerNextPosition = new Point();
        }

        public Point SkippedToolPosition
        {
            get
            {
                return m_SkippedToolPosition;
            }

            set
            {
                m_SkippedToolPosition = value;
            }
        }

        public Point PlayerNextPosition
        {
            get
            {
                return m_PlayerNextPosition;
            }

            set
            {
                m_PlayerNextPosition = value;
            }
        }
    }
}
