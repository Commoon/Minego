using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minego
{
    public static class GlobalState
    {
        public static int Score1 = 0;
        public static int Score2 = 0;

        public static void Restart()
        {
            Score1 = 0;
            Score2 = 0;
        }
    }
}
    