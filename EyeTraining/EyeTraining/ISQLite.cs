using System;
using System.Collections.Generic;
using System.Text;

namespace Eyefit
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
