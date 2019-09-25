using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PitcherStatistics : PlayerStatistics
{
    public PitcherStatistics()
        : base()
    {
        ;
    }

    public void AddStat(int w, int l, int gs, int gf, int cg, int sho, int hld, int svo, int sv, int h, int er, int hr, int bb, int ibb, int hb, int k, int bk, int bf, int bs, int gidp, int gidpo, int gir, int ip, int ir, int ira, int pit, int qs, int wps, int wp)
    {
        SetStat(w, PS.W);
        SetStat(l, PS.L);
        SetStat(gs, PS.GS_PIT);
        SetStat(gf, PS.GF);
        SetStat(cg, PS.CG);
        SetStat(sho, PS.SHO);
        SetStat(hld, PS.HLD);
        SetStat(svo, PS.SVO);
        SetStat(sv, PS.SV);
        SetStat(h, PS.H_PIT);
        SetStat(er, PS.ER);
        SetStat(hr, PS.HR_PIT);
        SetStat(bb, PS.BB_PIT);
        SetStat(ibb, PS.IBB_PIT);
        SetStat(hb, PS.HB);
        SetStat(k, PS.K_PIT);
        SetStat(bk, PS.BK);
        SetStat(bf, PS.BF);
        SetStat(bs, PS.BS);
        SetStat(gidp, PS.GIDP);
        SetStat(gidpo, PS.GIDPO);
        SetStat(gir, PS.GIR);
        SetStat(ip, PS.IP);
        SetStat(ir, PS.IR);
        SetStat(ira, PS.IRA);
        SetStat(pit, PS.PIT);
        SetStat(qs, PS.QS);
        SetStat(wps, PS.WPS);
        SetStat(wp, PS.WP);

        //Calc complex stats and set it.
    }
}
