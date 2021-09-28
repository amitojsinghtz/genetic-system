using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Helpers
{
    public class TempColEqComparer : IEqualityComparer<TestTempCol>
    {
        public bool Equals(TestTempCol Col1, TestTempCol Col2)
        {
            if (Col1 == null && Col2 == null)
                return true;
            else if (Col1 == null || Col2 == null)
                return false;
            else if (Col1.ID == Col2.ID)
                return true;
            else
                return false;
        }

        public int GetHashCode(TestTempCol obj)
        {
            int hcode = obj.ID;
            return hcode;
        }
    }

    public class TestTempEqComparer : IEqualityComparer<TestTemp>
    {
        public bool Equals(TestTemp testTemp1, TestTemp tesTemp2)
        {
            if (testTemp1 == null && tesTemp2 == null)
                return true;
            else if (testTemp1 == null || tesTemp2 == null)
                return false;
            else if (testTemp1.ID == tesTemp2.ID)
                return true;
            else
                return false;
        }

        public int GetHashCode(TestTemp obj)
        {
            int hcode = obj.ID;
            return hcode;
        }
    }
}
