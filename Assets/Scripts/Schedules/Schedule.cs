using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;  //For debug purpose.
using UnityEngine;
using TMPro;

[Serializable]
public class Schedule
{
    //static variables
    public static Color[] categoryColors = { Color.black, Colors.red, Colors.blue, Colors.green };
    public static string[] categoryStrings = { "Testing", "Training", "Matchup", "Transfer" };
    //public static Sprite defaultSprite;

    //Enum for categories
    public enum Categories
    {
        TESTING, TRAINING, MATCHUP, TRANSFER
    };

    public enum ScheduleCategory
    {
        TESTING, TESTING_NOSELECT, TESTING_WITHCOMPONENT,
        TRAINING_SUCCESS,
        MATCHUP
    }

    public static Dictionary<Categories, List<ScheduleCategory>> scheduleCategories = new Dictionary<Categories, List<ScheduleCategory>>()
    {
        {
            Categories.TESTING, new List<ScheduleCategory>()
            {
                ScheduleCategory.TESTING, ScheduleCategory.TESTING_NOSELECT, ScheduleCategory.TESTING_WITHCOMPONENT
            }
        },
        {
            Categories.TRAINING, new List<ScheduleCategory>()
            {
                ScheduleCategory.TRAINING_SUCCESS
            }
        },
        {
            Categories.MATCHUP, new List<ScheduleCategory>()
            {
                ScheduleCategory.MATCHUP
            }
        }
    };

    //member functions
    public Schedule(int _index, DateTime _date, string _title, Categories _category, ScheduleCategory _schedules, string _desc, bool hasComponent = false)
    {
        index = _index;
        date = _date;
        title = _title;
        category = _category;
        scheduleCategory = _schedules;
        description = _desc;
    }

    public Schedule(int _index, DateTime _date) {
        index = _index;
        date = _date;
    }

    public Schedule DeepCopy()
    {
        return new Schedule(index, date, title, category, scheduleCategory, description);
    }

    public int GetIndex() { return index; }
    public DateTime GetDate() { return date; }
    public string GetTitle() { return title; }
    public Categories GetCategories() { return category; }
    public string GetDescription() { return description; }

    public string[] GetItems()
    {
        if(items == null)
        {
            return new string[] { };
        }
        else
        {
            return items;
        }
    }

    //data members
    [HideInInspector] public int index;

    public DateTime date;

    public string title;
    public Categories category;
    public ScheduleCategory scheduleCategory;
    public string description;
    
    public string[] items;

    [HideInInspector] public bool isConfirmed = false;
    [HideInInspector] public bool hasComponent;

    public bool isSelectable = true;

    [HideInInspector] public int selectedItem = -1;
}

public static class Schedules
{
    /// <summary>
    /// Applies schedule components to main panel.
    /// </summary>
    public static void ApplyScheduleComponent(Schedule schedule)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Remove remaining existing objects.
        for(int i = 0; i < gameManager.contentLayout.transform.childCount; ++i)
        {
            UnityEngine.Object.Destroy(gameManager.contentLayout.transform.GetChild(i).gameObject);
        }

        //Instantiate objects case by case.
        Schedule.ScheduleCategory cat = schedule.scheduleCategory;
        switch (cat)
        {
            case Schedule.ScheduleCategory.TESTING_WITHCOMPONENT:
                {
                    GameObject textObject0 = UnityEngine.Object.Instantiate(gameManager.Schedule_text, gameManager.contentLayout.transform);
                    textObject0.GetComponent<TextMeshProUGUI>().text = "This is a test text!";
                    GameObject textObject1 = UnityEngine.Object.Instantiate(gameManager.Schedule_text, gameManager.contentLayout.transform);
                    textObject1.GetComponent<TextMeshProUGUI>().text = "This is a test text!" + "\nHere's another line!";
                }
                break;
            case Schedule.ScheduleCategory.MATCHUP:
                {
                    //Data structure for team object.
                    Schedule_MatchUp scheduleMatchUp = (Schedule_MatchUp)schedule;

                    //Get team.
                    Team home = scheduleMatchUp.game.home;
                    Team away = scheduleMatchUp.game.away;

                    //Sets basic information.
                    GameObject textObject0 = UnityEngine.Object.Instantiate(gameManager.Schedule_text, gameManager.contentLayout.transform);
                    textObject0.GetComponent<TextMeshProUGUI>().text = "You have a match with " + scheduleMatchUp.enemy.teamData.GetData(TeamData.TP.NAME) + ".";
                    GameObject matchupPanel = UnityEngine.Object.Instantiate(gameManager.matchUpPanel, gameManager.contentLayout.transform);
                    TeamObject teamPanel0 = new TeamObject(matchupPanel.transform.GetChild(0).gameObject);
                    teamPanel0.nameText.text = home.teamData.GetData(TeamData.TP.NAME);
                    teamPanel0.winLossText.text = home.teamStats.GetData(TeamStatistics.TS.WIN) + "W " + home.teamStats.GetData(TeamStatistics.TS.LOSS) + "L";
                    TeamObject teamPanel1 = new TeamObject(matchupPanel.transform.GetChild(1).gameObject);
                    teamPanel1.nameText.text = away.teamData.GetData(TeamData.TP.NAME);
                    teamPanel1.winLossText.text = home.teamStats.GetData(TeamStatistics.TS.WIN) + "W " + home.teamStats.GetData(TeamStatistics.TS.LOSS) + "L";

                    //Sets color to point out which side is our team.
                    if (Values.myTeam == scheduleMatchUp.game.home)
                    {
                        teamPanel0.homeAwayText.color = Colors.blue;
                    }
                    else if (Values.myTeam == scheduleMatchUp.game.away)
                    {
                        teamPanel1.homeAwayText.color = Colors.blue;
                    }

                    //Sets recentMatchPanel.
                    for(int i = 0; i < teamPanel0.recentMatchesContent.transform.childCount; ++i)
                    {
                        UnityEngine.Object.Destroy(teamPanel0.recentMatchesContent.transform.GetChild(i).gameObject);
                    }
                    for (int i = 0; i < teamPanel1.recentMatchesContent.transform.childCount; ++i)
                    {
                        UnityEngine.Object.Destroy(teamPanel1.recentMatchesContent.transform.GetChild(i).gameObject);
                    }

                    List<Game> recentGames0 = Values.league.RecentMatches(3, home, scheduleMatchUp.game.date.date);
                    List<Game> recentGames1 = Values.league.RecentMatches(3, away, scheduleMatchUp.game.date.date);
                    
                    for(int i = 0; i < recentGames0.Count; ++i)
                    {
                        GameObject recentMatchObject = UnityEngine.Object.Instantiate(gameManager.recentMatchPanel, teamPanel0.recentMatchesContent.transform);
                        RecentMatchObject matchObject = new RecentMatchObject(recentMatchObject);
                        matchObject.SetByGame(recentGames0[i], home);
                    }
                    for (int i = 0; i < recentGames1.Count; ++i)
                    {
                        GameObject recentMatchObject = UnityEngine.Object.Instantiate(gameManager.recentMatchPanel, teamPanel1.recentMatchesContent.transform);
                        RecentMatchObject matchObject = new RecentMatchObject(recentMatchObject);
                        matchObject.SetByGame(recentGames1[i], away);
                    }

                    //If there's no recent matches, notice that.
                    if(recentGames0.Count == 0)
                    {
                        RecentMatchObject obj = new RecentMatchObject(UnityEngine.Object.Instantiate(gameManager.recentMatchPanel, teamPanel0.recentMatchesContent.transform));
                        obj.SetNoGame();
                    }
                    if (recentGames1.Count == 0)
                    {
                        RecentMatchObject obj = new RecentMatchObject(UnityEngine.Object.Instantiate(gameManager.recentMatchPanel, teamPanel1.recentMatchesContent.transform));
                        obj.SetNoGame();
                    }


                    //Sets keyPlayer.
                    Player keyPlayer0 = home.GetKeyPlayer();
                    Player keyPlayer1 = away.GetKeyPlayer();

                    teamPanel0.keyPlayerObject.SetByPlayer(keyPlayer0);
                    teamPanel1.keyPlayerObject.SetByPlayer(keyPlayer1);
                }
                break;
            default:
                {
                    GameObject textObject0 = UnityEngine.Object.Instantiate(gameManager.Schedule_text, gameManager.contentLayout.transform);
                    textObject0.GetComponent<TextMeshProUGUI>().text = "There is nothing to show."; 
                }
                break;
        }
    }
}