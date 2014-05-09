using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorprendente
{
    class SInteraction
    {
        #region Variables

        private static bool bb = false;

        #endregion

        #region Functions

        public static bool getBB()
        {
            return bb;
        }

        public static void setBB(bool b)
        {
            bb = b;
        }

        #endregion

    }
}
