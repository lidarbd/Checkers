using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UICheckers
{
    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Manager manager = new Manager();
        }
    }
}
