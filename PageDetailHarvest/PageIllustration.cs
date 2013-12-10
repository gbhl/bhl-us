using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    internal class PageIllustration
    {
        private int _top = 0;

        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        private int _bottom = 0;

        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        private int _left = 0;

        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        private int _right = 0;

        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }
    }
}
