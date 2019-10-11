using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class sabermetrics
{
    //예상승률
    public static double WinRate(int W, int L)
    {
        return (Math.Pow(W, 2) / (Math.Pow(W, 2) + Math.Pow(L, 2)));
    }

    //출루율
    public static double OBP(int H, int BB, int HBP, int PA, int SF)
    {
        return (H + BB + HBP) / (PA + BB + HBP + SF);
    }

    //장타율
    public static double SLG(int _1B, int _2B, int _3B, int HR, int PA)
    {
        return ((_1B + 2 * _2B + 3 * _3B + 4 * HR) / PA);
    }

    //OPS
    public static double OPS(double OBP, double SLG)
    {
        return (OBP + SLG);
    }

    //GPA
    public static double GPA(double OBP, double SLG)
    {
        return (1.8 * OBP + SLG);
    }

    //wOBA
    public static double wOBA(int NIBB, int IBB, int HBP, int RBOE, int _1B, int _2B, int _3B, int HR, int PA)
    {
        return ((0.72 * NIBB + 0.75 * HBP + 0.90 * _1B + 0.92 * RBOE + 1.24 * _2B + 1.56 * _3B + 1.95 * HR) / (PA - IBB));
    }

    //wRAA
    public static double wRAA(double wOBA, double averwOBA, double Scale, int PA)
    {
        return ((wOBA - averwOBA) / (Scale * PA));
    }

    //선구안(ISO라 표현)
    public static double ISO(double ZSp, double OSp)
    {
        return (ZSp - 2 * OSp);
    }

    //ERA 평균자책점
    public static double ERA(int ER, int IP)
    {
        return (ER / IP * 9);
    }

    //RA/9 평균실점
    public static double RA_9(int RA, int IP)
    {
        return (RA / IP * 9);
    }

    //ERC ERA보완 투수스탯
    public static double ERC(int H, int HR, int BB, int IBB, int BFP,int HBP, int IP)
    {
        double PTB = (0.89 * (1.255 * (H - HR) + 4 * HR) + 0.56 * (BB + HBP - IBB));
        double ERC = (((H + BB + HBP) * PTB) / (BFP * IP));
        return (ERC >= 2.24 ?ERC:(((((H+BB+HBP)*PTB)/(BFP*IP))*9)*0.75));
    }

    //FIP 수비무관 자책점
    //constant의 평균값은 3.2
    public static double FIP(int HR, int BB, int HBP, int IP, int constant,int K)
    {
        return ((13 * HR) + (3 * (BB + HBP)) - 2 * K) / IP + constant;
    }

    //QERA
    //Kp는 K%
    public static double QREA(double Kp, double BBp, double GBp)
    {
        return Math.Pow((2.69 - 3.4 * Kp + 3.88 * BBp - 0.66 * GBp), 2);
    }

    //xFIP 투수개인의 능력 이외의 요인을 최대한 배제
    //constant는 FIP와 동일
    public static double xFIP(int FB, int HR, double FBp, int HBP, int constant, int K, int lgHR,int IP, int BB)
    {
        return ((13 * (FB * lgHR / FBp)) + (3 * (BB + HBP)) - (2 * K) / IP) + constant;
    }
    
}
