using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HISSystem.Helper
{
    public enum ActionButton
    {
        Add = 1,
        Update = 2,
        Delete = 3,
        View = 4,
        History = 5,
        Index = 6,
    }
    public enum Page
    {
        Employee = 1,
        Patient = 2,
        Lookup = 3,
        AccessControl = 4,
        Dashboard = 5,
        Bed = 6,
        AccessPermisson = 8,
        FollowUp = 10,
        SystemLog = 9,
    }
}
