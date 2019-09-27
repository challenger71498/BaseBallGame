using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static Game game;
    public static bool isGameEnd = false;

    public static int currentInning;
    public static bool isBottom;
    public static Team currentAttack;
    public static Team currentDefend;
    public static Pitcher currentPitcher;
    public static Pitcher savedPitcher;
    public static Batter currentBatter;
    public static List<KeyValuePair<int, Batter>> homeBattingOrder;
    public static List<KeyValuePair<int, Batter>> awayBattingOrder;
    public static int homeCurrentBattersIndex;
    public static int awayCurrentBattersIndex;

    public static Batter[] runnerInBases = { null, null, null, null };
    public static bool[] stealingAttempts = { false, false, false, false };
    public static int strikeCount;
    public static int ballCount;
    public static int outCount;
    
    /// <summary>
    /// Initializes a game.
    /// </summary>
    public void InitializeGame()
    {
        //Setting the inning.
        currentInning = 1;
        isBottom = false;
        currentAttack = game.away;
        currentDefend = game.home;

        //Setting batting orders.
        homeBattingOrder = game.home.battingOrder.d;
        homeCurrentBattersIndex = 1;
        awayBattingOrder = game.away.battingOrder.d;
        awayCurrentBattersIndex = 1;

        //Setting a pitcher.
        currentPitcher = game.awayStarterPitcher;
        strikeCount = 0;
        ballCount = 0;
        outCount = 0;

        //Bring the batter to plate.
        AdvanceBatterToPlate();
    }

    /// <summary>
    /// A single turn.
    /// </summary>
    public void Turn()
    {
        //Initializes stealingAttempts array to false.
        for (int i = 0; i < 4; ++i)
        {
            stealingAttempts[i] = false;
        }
        //First, determine whether runners in bases attempt to steal base or not.
        for (int i = 1; i <= 3; ++i)
        {
            bool isBaseStealing = BaseStealDetermine(runnerInBases[i], true);
            if (isBaseStealing)
            {
                //If a runner is going to steal base, change stealingAttempt bool to true.
                stealingAttempts[i] = true;
            }
        }

        //Then, a pitcher determines whether pick off the ball or not.
        bool isPickedOff = PickOffDetermine(out int whichBase, true);
        if (isPickedOff)
        {
            //If the pitcher picks off the ball, a pickoff function starts and control goes to ball in play.
            PickOff(-1, true);
            BallInPlay(BallInPlayMode.PICKOFF, true, whichBase);
            return;
        }
        else
        {
            //If not picked off, pitcher pitches, and sets wildpitch and hitbypitch bool variables.
            Pitch(out bool isWildPitch, out bool isHitByPitch, true);
            if (isWildPitch)
            {
                //If a pitcher wild pitched, control goes to ball in play.
                BallInPlay(BallInPlayMode.WILD_PITCH);
                return;
            }
            else
            {
                //If not wild pitched, determine wheter a batter swung or not.
                bool isSwung = SwingDetermine(true);
                if (isHitByPitch)
                {
                    //If a batter got a hit-by-pitch ball, advances runner by 1 base, and the turn ends.
                    AdvanceRunner(1);
                    return;
                }
                else
                {
                    //If not hit-by-pitch ball, check if a batter swung.
                    if (isSwung)
                    {
                        //If swung, determine whether a swing hit or not.
                        bool isHit = HitDetermine(true);
                        if (isHit)
                        {
                            //If hit, control goes to ball in play.
                            BallInPlay(BallInPlayMode.NORMAL);
                            return;
                        }
                        else
                        {
                            //If not hit, adds a strike.
                            AddStrike();
                            return;
                        }
                    }
                    else
                    {
                        //If not swung, determine whether a ball is strike or ball.
                        bool isInStrike = InStrikeZoneDetermine(true);
                        if (isInStrike)
                        {
                            //If a ball is in strikezone, adds a strike.
                            AddStrike();
                            return;
                        }
                        else
                        {
                            //If not in strikezone, adds a ball.
                            AddBall();
                            return;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Mode of ball-in-play.
    /// </summary>
    public enum BallInPlayMode
    {
        NORMAL, PICKOFF, WILD_PITCH
    }
    /// <summary>
    /// Move script control to ball in play situation.
    /// </summary>
    public void BallInPlay(BallInPlayMode mode, bool isRandom = true, int whichBase = -1)
    {
        if (isRandom)
        {
            FieldPlay();
        }
        else
        {
            if (mode == BallInPlayMode.NORMAL)
            {

            }
            else if (mode == BallInPlayMode.PICKOFF)
            {

            }
            else if (mode == BallInPlayMode.WILD_PITCH)
            {

            }
        }
    }

    /// <summary>
    /// Conducts field play.
    /// </summary>
    /// <param name="isRandom"></param>
    public void FieldPlay(bool isRandom = true)
    {
        if(isRandom)
        {
            int random = UnityEngine.Random.Range(0, 10);

            //FlyOut
            if (random == 0)
            {
                AddOut(Out.FLY_BALL);
            }
            //GroundBall
            else if (random == 1)
            {
                AddOut(Out.GROUND_BALL);
            }
            //Hit
            else if (random == 2)
            {
                random = UnityEngine.Random.Range(0, 3);
                if(random == 0)
                {
                    AddHit(Hit.SINGLE);
                }
                else if (random == 1)
                {
                    AddHit(Hit.DOUBLE);
                }
                else if (random == 2)
                {
                    AddHit(Hit.TRIPLE);
                }
                else if (random == 3)
                {
                    AddHit(Hit.HOME_RUN);
                }
            }
        }
    }

    /// <summary>
    /// Advances runners specified amount of.
    /// </summary>
    /// <param name="amount"></param>
    public void AdvanceRunner(int amount, bool includeBatter = true)
    {
        void Advance(int runnerBase)
        {
            if(runnerBase == 3)
            {
                RunnerToHomePlate(runnerInBases[3]);
            }

            if(runnerInBases[runnerBase + 1] != null)
            {
                Advance(runnerBase + 1);
            }
            runnerInBases[runnerBase + 1] = runnerInBases[runnerBase];
            runnerInBases[runnerBase] = null;
        }

        if(includeBatter)
        {
            runnerInBases[0] = currentBatter;
            for (int i = 0; i < amount; ++i)
            {
                Advance(amount);
            }
        }
        else
        {
            for (int i = 1; i <= amount; ++i)
            {
                Advance(amount);
            }
        }

        
    }

    public void RunnerToHomePlate(Batter batter)
    {
        runnerInBases[3] = null;
        //Add score by 1;
        if(currentAttack == game.home)
        {
            game.homeScoreBoard.AddRun(1);
        }
        else if (currentAttack == game.away)
        {
            game.awayScoreBoard.AddRun(1);
        }
        else
        {
            throw new System.NullReferenceException("Error: There is no appropriate team matching. Check currentAttack and Game instance again.");
        }

        //And other statistic stuffs.
        bool isEarnedRun = true;
        if(isEarnedRun)
        {
            //Earned Run
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.ER);
        }
        //Runs Batted In
        currentBatter.stats.SetStat(1, PlayerStatistics.PS.RBI);
        //Run
        batter.stats.SetStat(1, PlayerStatistics.PS.R);
    }

    /// <summary>
    /// Adds strike count.
    /// </summary>
    public void AddStrike()
    {
        if (strikeCount < 2)
        {
            strikeCount++;
        }
        else
        {
            //Stirkeout.
            AddOut(Out.STRIKEOUT);
        }
    }

    /// <summary>
    /// Adds ball count.
    /// </summary>
    public void AddBall(bool isHBP = false, bool isIBB = false)
    {
        if (ballCount < 3)
        {
            ballCount++;
        }
        else
        {
            AdvanceRunner(1);
            if(isHBP)
            {
                currentPitcher.stats.SetStat(1, PlayerStatistics.PS.HB);
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.HBP);
            }
            if(isIBB)
            {
                currentPitcher.stats.SetStat(1, PlayerStatistics.PS.IBB_PIT);
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.IBB_BAT);
            }
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.BB_BAT);
            ClearCount();
        }
    }

    public enum Out
    {
        STRIKEOUT, FLY_BALL, GROUND_BALL
    }

    /// <summary>
    /// Adds out count.
    /// </summary>
    public void AddOut(Out outs)
    {
        //Statistics.
        currentPitcher.stats.SetStat(0.1f, PlayerStatistics.PS.IP);

        //Determine which out happened, and apply appropriate stats.
        //Strikout
        if(outs == Out.STRIKEOUT)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.K_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.K_BAT);
        }
        else if(outs == Out.GROUND_BALL)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.GB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.GB_BAT);
        }
        else if(outs == Out.FLY_BALL)
        {
            currentPitcher.stats.SetStat(1, PlayerStatistics.PS.FB_PIT);
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.FB_BAT);
        }
        
        //Advance Batter.
        if (outCount < 2)
        {
            outCount++;
            if(currentAttack == game.home)
            {
                AdvanceBattingOrder(ref homeCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
            else if(currentAttack == game.away)
            {
                AdvanceBattingOrder(ref awayCurrentBattersIndex);
                AdvanceBatterToPlate();
            }
        }
        else
        {
            AdvanceInning();
        }
    }

    public enum Hit
    {
        SINGLE, DOUBLE, TRIPLE, HOME_RUN
    }

    /// <summary>
    /// Conduct behaviours when a batter hit a ball in a common way.
    /// </summary>
    /// <param name="hit"></param>
    public void AddHit(Hit hit, bool isITPHR = false)
    {
        if(hit == Hit.SINGLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.SIN);
            AdvanceRunner(1);
        }
        else if (hit == Hit.DOUBLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.DBL);
            AdvanceRunner(2);
        }
        else if (hit == Hit.TRIPLE)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.TRP);
            AdvanceRunner(3);
        }
        else if (hit == Hit.HOME_RUN)
        {
            currentBatter.stats.SetStat(1, PlayerStatistics.PS.HR_BAT);
            if(isITPHR)
            {
                currentBatter.stats.SetStat(1, PlayerStatistics.PS.ITPHR);
            }
            AdvanceRunner(4);
        }
    }

    /// <summary>
    /// Clears strike and ball count.
    /// </summary>
    public void ClearCount()
    {
        strikeCount = 0;
        ballCount = 0;
    }

    /// <summary>
    /// Advances inning.
    /// </summary>
    public void AdvanceInning()
    {
        //Innings pitched
        currentPitcher.stats.SetStat(0.8f, PlayerStatistics.PS.IP);

        //If the game satisfies the end condition, end game.
        if((game.homeScoreBoard.R != game.awayScoreBoard.R) || isGameEnd)
        {
            EndGame();
            return;
        }

        //Switch side.
        currentPitcher = savedPitcher;

        if (currentAttack == game.home)
        {
            currentAttack = game.away;
            currentDefend = game.home;
            
            currentBatter = awayBattingOrder[awayCurrentBattersIndex].Value;
        }
        else if (currentAttack == game.away)
        {
            currentAttack = game.home;
            currentDefend = game.away;

            currentBatter = homeBattingOrder[homeCurrentBattersIndex].Value;
        }
        else
        {
            throw new System.NullReferenceException("Error: There is no appropriate team matching. Check currentAttack and Game instance again.");
        }

        outCount = 0;
        ClearCount();
    }

    /// <summary>
    /// Advance batting order by 1.
    /// </summary>
    /// <param name="index"></param>
    public void AdvanceBattingOrder(ref int index)
    {
        index++;
        index %= 9;
    }

    /// <summary>
    /// Place batter at plate.
    /// </summary>
    public void AdvanceBatterToPlate()
    {
        if(currentAttack == game.home)
        {
            runnerInBases[0] = homeBattingOrder[homeCurrentBattersIndex].Value;
            currentBatter = runnerInBases[0];
        }
        else if (currentAttack == game.away)
        {
            runnerInBases[0] = awayBattingOrder[awayCurrentBattersIndex].Value;
            currentBatter = runnerInBases[0];
        }
        else
        {
            throw new System.Exception("There is no currentAttack team matching any home or away team. Check currentAttack variable to fix.");
        }
        
        currentBatter.stats.SetStat(1, PlayerStatistics.PS.PA);
        currentPitcher.stats.SetStat(1, PlayerStatistics.PS.BF);
    }

    public void EndGame()
    {
        game.isPlayed = true;
        //Show a summary tab after finishes a game.
    }
        

    /// <summary>
    /// Determines whether attempt to steal base or not.
    /// </summary>
    /// <param name="batter"></param>
    /// <returns></returns>
    public bool BaseStealDetermine(Batter batter, bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether pickoff or not.
    /// </summary>
    /// <returns></returns>
    public bool PickOffDetermine(out int whichBase, bool isRandom = false)
    {
        if (isRandom)
        {
            for (int i = 1; i <= 3; ++i)
            {
                if (runnerInBases[i] != null)
                {
                    whichBase = i;
                    return UnityEngine.Random.Range(0, 1) < 0.5f;
                }
            }
            whichBase = -1;
            return false;
        }
        else
        {
            whichBase = -1;
            return false;
        }
    }

    /// <summary>
    /// Throws pickoff ball.
    /// </summary>
    public void PickOff(int whichBase, bool isRandom = false)
    {
        if (isRandom)
        {

        }
    }

    /// <summary>
    /// Throws pitch. Returns whether the pitch is wild or not.
    /// </summary>
    /// <returns></returns>
    public void Pitch(out bool wildPitch, out bool hitByPitch, bool isRandom = false)
    {
        if (isRandom)
        {
            float randomFloat = UnityEngine.Random.Range(0, 1);
            if (randomFloat < 0.3f)
            {
                wildPitch = true;
                hitByPitch = false;
            }
            else if (0.3f <= randomFloat && randomFloat < 0.6f)
            {
                wildPitch = false;
                hitByPitch = true;
            }
            else
            {
                wildPitch = false;
                hitByPitch = false;
            }
        }
        else
        {
            wildPitch = false;
            hitByPitch = false;
        }
    }

    /// <summary>
    /// Determines whether swing or not.
    /// </summary>
    /// <returns></returns>
    public bool SwingDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Determines whether hit or not.
    /// </summary>
    /// <returns></returns>
    public bool HitDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Determines wheter a ball is in strike zone or not.
    /// </summary>
    /// <returns></returns>
    public bool InStrikeZoneDetermine(bool isRandom = false)
    {
        if (isRandom)
        {
            return UnityEngine.Random.Range(0, 1) < 0.5f;
        }
        else
        {
            return true;
        }
    }
}
