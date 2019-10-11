using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public InGameObjects InGameObjects;

    public static Game game;
    public static bool isPaused = false;
    public static bool isGameEnd = false;
    public static bool isUIEnabled;

    public static int currentInning;
    public static bool isBottom;
    public static Team currentAttack;
    public static Team currentDefend;
    public static Pitcher currentPitcher;
    public static Pitcher otherPitcher;
    public static Batter currentBatter;
    public static List<Batter> homeBattingOrder;
    public static List<Batter> awayBattingOrder;
    public static int homeCurrentBattersIndex;
    public static int awayCurrentBattersIndex;

    //public static Queue<Batter> runnerInBases;
    public static Batter[] runnerInBases = { null, null, null, null };
    public static bool[] stealingAttempts = { false, false, false, false };
    public static int strikeCount;
    public static int ballCount;
    public static int outCount;

    public void Start()
    {
        Debug.Log("GAME STARTED");
        InitializeGame();

        //UI initialization.
        InGameObjects.PlayerUIApply.SetPlayers();
        InGameObjects.boardPanel.Initialize();
        InGameObjects.scorePanel.gameObject.SetActive(false);
        InGameObjects.inningPanel.gameObject.SetActive(false);
        InGameObjects.resultPanel.SetActive(false);
        InGameObjects.fieldPanel.SetActive(false);

        StartCoroutine(TurnDelayed(false));  //THIS SHOULD BE CHANGED TO FALSE AT RELEASE.
    }

    IEnumerator TurnDelayed(bool isDebug = false)
    {
        while (!isGameEnd)
        {
            if(!isPaused)
            {
                Turn();
            }
            yield return new WaitForSeconds(isDebug ? 0.001f : SpeedPanel.speedValue[(int)SpeedPanel.spd]);
        }
    }

    /// <summary>
    /// Initializes a game.
    /// </summary>
    public static void InitializeGame(bool isUIEnable = true)
    {
        //Toggles UI on or off.
        isUIEnabled = isUIEnable;

        //Resets isPaused value to false.
        isPaused = false;

        //Resets isGameEnd value to false.
        isGameEnd = false;

        //Sets the inning.
        currentInning = 1;
        isBottom = false;
        currentAttack = game.away;
        currentDefend = game.home;

        //Sets batting orders.
        homeBattingOrder = game.home.battingOrder.d;
        homeCurrentBattersIndex = 0;
        awayBattingOrder = game.away.battingOrder.d;
        awayCurrentBattersIndex = 0;

        //Sets a pitcher.
        currentPitcher = game.homeStarterPitcher;
        otherPitcher = game.awayStarterPitcher;
        strikeCount = 0;
        ballCount = 0;
        outCount = 0;

        //Initializes batter and pitcher set.
        game.homeBatterSet = new HashSet<Batter>();
        game.awayBatterSet = new HashSet<Batter>();
        game.homePitcherSet = new HashSet<Pitcher>();
        game.awayPitcherSet = new HashSet<Pitcher>();

        //Sets pitcher set.
        game.homePitcherSet.Add(currentPitcher);
        game.awayPitcherSet.Add(otherPitcher);

        //Brings the batter to plate.
        AtPlate.AdvanceBatterToPlate();
    }

    /// <summary>
    /// A single turn.
    /// </summary>
    public void Turn()
    {
        if(isUIEnabled)
        {
            //InningPanel
            InGameObjects.inningPanel.UpdateLayout();

            //OutPanel
            InGameObjects.outPanelLayout.ClearLayout();
            InGameObjects.outPanelLayout.UpdateLayout();

            //BasePanel
            InGameObjects.basePanel.UpdateLayout();
            InGameObjects.basePanel.UpdateStealing();

            //ScorePanel
            InGameObjects.scorePanel.UpdateLayout();

            //BoardPanel
            InGameObjects.boardPanel.UpdateLayout();

            //Field_Condition
            InGameObjects.PlayerUIApply.SetPlayers(true);
        }

        //Initializes stealingAttempts array to false.
        for (int i = 0; i < 4; ++i)
        {
            stealingAttempts[i] = false;
        }
        //First, determine whether runners in bases attempt to steal base or not.
        for (int i = 1; i <= 3; ++i)
        {
            bool isBaseStealing = BaseRunning.BaseStealDetermine(runnerInBases[i], true);
            if (isBaseStealing)
            {
                Debug.Log("BASE " + i + "TRYING TO STEAL");
                //If a runner is going to steal base, change stealingAttempt bool to true.
                stealingAttempts[i] = true;
            }
        }

        if(isUIEnabled)
        {
            //This is a stealingAttempts UI update.
            InGameObjects.basePanel.UpdateStealing();
        }

        //Then, a pitcher determines whether pick off the ball or not.
        bool isPickedOff = PickingOff.PickOffDetermine(out int whichBase, true);
        if (isPickedOff)
        {
            Debug.Log("PICK OFF");
            //If the pitcher picks off the ball, a pickoff function starts and control goes to ball in play.
            PickingOff.PickOff(-1, true);
            BallInPlay(BallInPlayMode.PICKOFF, true, whichBase);
            return;
        }
        else
        {
            //If not picked off, pitcher pitches, and sets wildpitch and hitbypitch bool variables.
            Pitching.Pitch(out bool isWildPitch, out bool isHitByPitch, true);
            if (isWildPitch)
            {
                Debug.Log("WILD PITCHED");
                //If a pitcher wild pitched, control goes to ball in play.
                PitchedWild.WildPitch(currentPitcher);
                BallInPlay(BallInPlayMode.WILD_PITCH);
                return;
            }
            else
            {
                //If not wild pitched, determine wheter a batter swung or not.
                bool isSwung = AtPlate.SwingDetermine(true);
                if (isHitByPitch)
                {
                    //If a batter got a hit-by-pitch ball, advances runner by 1 base, and the turn ends.
                    AtPlate.AddBall(true);
                    return;
                }
                else
                {
                    //If not hit-by-pitch ball, check if a batter swung.
                    if (isSwung)
                    {
                        Debug.Log("He Swings!");
                        //If swung, determine whether a swing hit or not.
                        bool isHit = Hitting.HitDetermine(true);
                        if (isHit)
                        {
                            //If hit, control goes to ball in play.
                            BallInPlay(BallInPlayMode.NORMAL);
                            return;
                        }
                        else
                        {
                            //If not hit, adds a strike.
                            AtPlate.AddStrike();
                            return;
                        }
                    }
                    else
                    {
                        //If not swung, determine whether a ball is strike or ball.
                        bool isInStrike = Pitching.InStrikeZoneDetermine(true);
                        if (isInStrike)
                        {
                            //If a ball is in strikezone, adds a strike.
                            AtPlate.AddStrike();
                            return;
                        }
                        else
                        {
                            //If not in strikezone, adds a ball.
                            AtPlate.AddBall();
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
                //Field Play Required.
            }
            else if (mode == BallInPlayMode.PICKOFF)
            {
                //Field Play Required.
            }
            else if (mode == BallInPlayMode.WILD_PITCH)
            {
                //Field Play Required.
            }
        }
    }

    /// <summary>
    /// Conducts field play.
    /// </summary>
    /// <param name="isRandom"></param>
    public void FieldPlay(bool isRandom = true)
    {
        if (isRandom)
        {
            float random = UnityEngine.Random.Range(0f, 10f);

            //FlyOut
            if (0 <= random && random <= 3.5f)
            {
                AtPlate.AddOut(AtPlate.Out.FLY_BALL);
            }
            //GroundBall
            else if (3.5f <= random && random <= 7f)
            {
                AtPlate.AddOut(AtPlate.Out.GROUND_BALL);
            }
            //Hit
            else if (7f <= random && random <= 10f)
            {
                random = UnityEngine.Random.Range(0, 10);
                if (0 <= random && random <= 4)
                {
                    Hitting.AddHit(Hitting.Hit.SINGLE);
                }
                else if (5 <= random && random <= 7)
                {
                    Hitting.AddHit(Hitting.Hit.DOUBLE);
                }
                else if (8 <= random && random <= 8)
                {
                    Hitting.AddHit(Hitting.Hit.TRIPLE);
                }
                else if (9 <= random && random <= 9)
                {
                    Hitting.AddHit(Hitting.Hit.HOME_RUN);
                }
            }
        }
    }
}
