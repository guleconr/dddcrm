using System;
using System.Collections.Generic;
using System.Text;


public static class NumberExtensions
{
    [System.Diagnostics.DebuggerStepThrough]
    public static Int32? ToIntNullable(this object o)
    {
        Int32? sonuc = null;
        try
        {
            if (o != null && o != DBNull.Value)
                sonuc = Convert.ToInt32(o);
        }
        catch { }
        return sonuc;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static Int32 ToInt(this object o)
    {
        Int32 sonuc = 0;
        try
        {
            if (o != null && o != DBNull.Value)
                sonuc = Convert.ToInt32(o);
        }
        catch { }
        return sonuc;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static long? ToLongNullable(this object o)
    {
        long? sonuc = null;
        try
        {
            if (o != null && o != DBNull.Value)
                sonuc = Convert.ToInt64(o);
        }
        catch { }
        return sonuc;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static long ToLong(this object o)
    {
        long sonuc = 0;
        try
        {
            if (o != null && o != DBNull.Value)
                sonuc = Convert.ToInt64(o);
        }
        catch { }
        return sonuc;
    }
}

