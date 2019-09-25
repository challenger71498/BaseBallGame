using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BatterStatistics : PlayerStatistics
{
    public BatterStatistics()
        : base()
    {
        ;
    }

    public void AddStat(int g, int ab, int h, int sin, int dbl, int trp, int hr, int gs, int itphr, int rbi, int pa, int bb, int hbp, int ibb, int k, int sb, int cs, int fc, int di, int r, int gdp, int lob, int sf, int sh, int tb, int tob, int xbh)
    {
        SetStat(pa, PS.PA);
        SetStat(ab, PS.AB);
        SetStat(h, PS.H_BAT);
        SetStat(sin, PS.SIN);
        SetStat(dbl, PS.DBL);
        SetStat(trp, PS.TRP);
        SetStat(hr, PS.HR_BAT);
        SetStat(gs, PS.GS_BAT);
        SetStat(itphr, PS.ITPHR);
        SetStat(fc, PS.FC);
        SetStat(di, PS.DI);
        SetStat(sf, PS.SF);
        SetStat(sh, PS.SH);
        SetStat(r, PS.R);
        SetStat(rbi, PS.RBI);
        SetStat(bb, PS.BB_BAT);
        SetStat(ibb, PS.IBB_BAT);
        SetStat(hbp, PS.HBP);
        SetStat(k, PS.K_BAT);
        SetStat(sb, PS.SB);
        SetStat(cs, PS.CS);
        SetStat(gdp, PS.GDP);
        SetStat(lob, PS.LOB);
        SetStat(sf, PS.SF);
        SetStat(sh, PS.SH);
        SetStat(tb, PS.TB);
        SetStat(tob, PS.TOB);
        SetStat(xbh, PS.XBH);
        
        //Calc complex stats and set it.
    }
}
