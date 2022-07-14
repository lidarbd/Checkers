namespace CheckersLogic
{
    public class Cell
    {
        private eSign m_CellContent;

        public eSign CellContent
        {
            get
            {
                return m_CellContent;
            }

            set
            {
                m_CellContent = value;
            }
        }
    }
}
