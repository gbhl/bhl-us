﻿using System.Collections.Generic;

namespace BHL.Export
{
    public interface IBHLExport
    {
        Dictionary<string, int> Stats();

        List<string> Errors();

        void Process();
    }
}
