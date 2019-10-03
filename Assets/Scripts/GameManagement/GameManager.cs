using System;
using System.Linq;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI.Extensions;

public class GameManager : MonoBehaviour
{
    //Prefabs
    [Header("Prefabs")]
    public GameObject datePanel;
    public GameObject listPanel;
    public GameObject itemPanel;
    public GameObject playerPanel;
    public GameObject stat;
    public GameObject statSmall;
    public GameObject pitches;
    public GameObject graph;
    public GameObject statistics;
    public GameObject training;
    public GameObject effectPanel;
    public GameObject matchUpPanel;
    public GameObject teamPanel;
    public GameObject recentMatchPanel;

    [Header("Prefabs for schedule component")]
    public GameObject Schedule_text;

    //Right Panel GameObjects
    [Header("Right Panel Gameobjects")]
    public Image accentPanel;
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI titleText;
    public VerticalLayoutGroup contentLayout;
    public GameObject dropdown;
    public Button confirm;

    //Left Panel GameObjects
    [Header("Left Panel Gameobjects")]
    public TextMeshProUGUI dateText;
    public VerticalLayoutGroup listContentLayout;

    //Status Panel GameObjects
    [Header("Status Panel Gameobjects")]
    public HorizontalLayoutGroup statusLayout;

    //Menu Panels
    [Header("Menu Panels")]
    public GameObject playersPanel;

    //Player Panel GameObjects
    [Header("Player Panel Gameobjects")]
    public GameObject playerContent;
    public GameObject skillPanel;
    public GameObject statisticsPanel;
    public GameObject roastersPanel;
    public GameObject trainingPanel;
    public SortDropdown sortDropdown;
    public Filter filter;

    [Header("PlayerInfo Panel Gameobjects")]
    public TextMeshProUGUI PIP_playerName;
    public TextMeshProUGUI PIP_number;
    public Image PIP_AccentPanel;
    public Image PIP_AccentPanel2;
    public TextMeshProUGUI PIP_postion;
    public TextMeshProUGUI PIP_height;
    public TextMeshProUGUI PIP_weight;
    public TextMeshProUGUI PIP_age;
    public TextMeshProUGUI PIP_leftHanded;

    [Header("Diamond Panel Gameobjects")]
    public Image PIP_diamondBGPanel;
    public GameObject[] PIP_diamondBGs;
    public UIPolygon PIP_uiPolygon;
    public TextMeshProUGUI[] PIP_stats;
    public TextMeshProUGUI[] PIP_values;

    [Header("Overall Panel Gameobjects")]
    public Image PIP_overallPanel;
    public Image PIP_overallAccentPanel;
    public TextMeshProUGUI PIP_overall;

    [Header("PlayerStat Panel Gameobjects")]
    public GameObject PIP_statPanel;

    [Header("Statistics Panel GameObjects")]
    public TextMeshProUGUI SP_playerName;
    public TextMeshProUGUI SP_number;
    public Image SP_AccentPanel;
    public Image SP_AccentPanel2;
    public TextMeshProUGUI SP_position;
    public GameObject SP_content;
    public GameObject SP_graphContent;
    public GameObject SP_graphPanel;
    public GameObject SP_high;
    public GameObject SP_low;

    [Header("Roaster Panel GameObjects")]
    public GameObject RP_statsPanel;
    public GameObject RP_fieldViewPanel;
    public GameObject RP_middlePanel;
    public GameObject RP_pitchersPanel;

    [Header("Field View Panel GameObjects")]
    public Transform RP_FVP_diamond;

    [Header("Pitchers Panel GameObjects")]
    public GameObject RP_pitchersPanelContent;

    [Header("Middle Panel GameObjects")]
    public UIPolygon RP_diamond;
    public UIPolygon RP_diamondSecond;
    public TextMeshProUGUI[] RP_values;
    public TextMeshProUGUI[] RP_valueSeconds;
    public TextMeshProUGUI[] RP_titles;

    [Header("PlayerInfo Panel GameObjects")]
    public TextMeshProUGUI RP_nameFirst;
    public TextMeshProUGUI RP_nameSecond;
    public TextMeshProUGUI RP_numberFirst;
    public TextMeshProUGUI RP_numberSecond;
    public Image RP_accentPanel;
    public Image RP_accentPanel2;
    public TextMeshProUGUI RP_position;

    [Header("Overall Panel GameObjects")]
    public Image RP_overallBGFirst;
    public Image RP_overallBGSecond;
    public Image RP_overallAccentPanelFirst;
    public Image RP_overallAccentPanelSecond;
    public Image RP_overallPanelFirst;
    public Image RP_overallPanelSecond;
    public TextMeshProUGUI RP_overallFirst;
    public TextMeshProUGUI RP_overallSecond;

    [Header("Stat Panel GameObjects")]
    public GameObject RP_content;

    [Header("Training Panel GameObjects")]
    public TextMeshProUGUI TP_playerName;
    public TextMeshProUGUI TP_number;
    public Image TP_AccentPanel;
    public Image TP_AccentPanel2;
    public TextMeshProUGUI TP_position;
    public GameObject TP_content;
    public GameObject TP_effectContent;

    [Header("Description Panel GameObjects")]
    public TextMeshProUGUI TP_title;
    public TextMeshProUGUI TP_description;
    public Button TP_apply;
    public TextMeshProUGUI TP_applyText;

    //Animator
    [Header("Animator")]
    public Animator animator;

    //Notification
    [Header("Notifications")]
    public GameObject notificationPanel;

    //Transition
    [Header("Transition")]
    public GameObject transitionTextObject;

    //Hide In Inspector
    [HideInInspector] public Filter.Mode mode = Filter.Mode.ALL;
    [HideInInspector] public SortDropdown.SortMode sortMode = SortDropdown.SortMode.OVERALL;
    [HideInInspector] public GameObject recentClick;

    //counter
    int counter = -1;

    //Schedulebutton isClicked
    [HideInInspector] public static GameObject ClickedObject;

    //Check whether there's matchup today.
    [HideInInspector] public bool isMatchUpToday = false;

    //Which game
    [HideInInspector] public Game game;

    //DEBUG:: Check whether it already has been created sample data.
    public static bool isCreated = false;

    //
    //Awake Function
    //

    void Start()
    {
        if(!isCreated)
        {
            //RandomNameGenerator Initialization
            RandomNameGenerator.SetNameList();

            //League Initialization
            Values.league = new League(true);

            //MyTeam Initailization
            Values.myTeam = Values.league.teams[0].Value;
            //Values.myTeam = Values.league.teams[UnityEngine.Random.Range(0, Values.league.teams.d.Count)].Value;
            
            //MyTeam schedule Initialization
            Values.schedules = new Dictionary<int, Schedule>();
            Values.league.BuildGameSchedule();

            isCreated = true;
        }

        //DEBUG: sample data setup
        //Values.schedules = Values.sampleSchedules;
        ArrayToDict();
        //foreach(List<Schedule> ss in scheduleByDate.Values)
        //{
        //    foreach(Schedule s in ss)
        //    {
        //        Debug.Log(s.GetTitle());
        //    }
        //}

        //Values.myPlayers = RandomPlayerGenerator.CreateTeam();
        //Values.myStartingMembers = RandomPlayerGenerator.CreateStartingMember(Values.myTeam.players);

        //Set today's game.
        game = Values.league.FindGame(Values.date, Values.myTeam);

        //when loaded
        animator.SetBool("isLoaded", true);

        //Load data: schedules, counter
        //SaveData();
        //LoadData();

        //Inactivates menu panels
        playersPanel.SetActive(false);

        //Inactivates transition text.
        transitionTextObject.SetActive(false);

        //LeftPanel

        //DateText
        dateText.text = Values.date.ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US")) + ", " + Values.date.Year;

        //ScheduleList
        bool noSchedules = true;
        for (DateTime d = new DateTime(Values.date.Year, Values.date.Month, Values.date.Day); (d - Values.date).Days < 14; d = d.AddDays(1))
        {
            //Checks whether the schedule exists
            if (Values.scheduleByDate.ContainsKey(d))
            {
                noSchedules = false;
                DateInstantiate(new Date(d));

                foreach (Schedule s in Values.scheduleByDate[d])
                {
                    ListInstantiate(s);
                    if(s.GetType() == typeof(Schedule_MatchUp) && s.date == Values.date)
                    {
                        isMatchUpToday = true;
                    }
                }
            }
        }

        //Shows "No Schedules" if there's no schdules at all.
        if (noSchedules)
        {
            DateInstantiate(new Date("No Schedules"));
        }

        //RIghtPanel

        //Components initially.
        dropdown.gameObject.SetActive(false);
        confirm.gameObject.SetActive(false);
        accentPanel.color = Color.white;

        //PlayersPanel
        RefreshPlayerList(Filter.Mode.ALL, SortDropdown.SortMode.OVERALL);
        skillPanel.SetActive(false);
        statisticsPanel.SetActive(false);
        roastersPanel.SetActive(false);
        trainingPanel.SetActive(false);

        //StatisticsPanel-GraphPanel
        SP_graphPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    //
    //Refresh Functions
    //


    //Refreshes player list.
    public static StartingMemberFilter currentStartingMemberFilter = StartingMemberFilter.ALL;
    public static bool isModeNow = true;
    public static Player.Position currentPosition;
    public static Player.MetaPosition currentMetaPosition;

    public enum StartingMemberFilter
    {
        ALL, MEMBER_ONLY, MEMBER_EXCLUDED, SUB_ONLY, SUB_EXCLUDED
    }

    public int RefreshPlayerList(Filter.Mode mode, SortDropdown.SortMode sortMode, StartingMemberFilter startingMemberFilter = StartingMemberFilter.ALL, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = true;

        int instantiatedAmount = 0;

        //Removes children of player content.
        for (int i = 0; i < playerContent.transform.childCount; ++i)
        {
            Transform child = playerContent.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (startingMemberFilter == StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }

            if (mode == Filter.Mode.BATTERS && Values.myTeam.players[i].Value.GetType() == typeof(Pitcher))
            {
                continue;
            }
            else if (mode == Filter.Mode.PITCHERS && Values.myTeam.players[i].Value.GetType() == typeof(Batter))
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerInstantiate(Values.myTeam.players[i].Value, playerView);
        }

        return instantiatedAmount;
    }

    public int RefreshPlayerList(Filter.Mode mode, SortDropdown.SortMode sortMode, GameObject content, StartingMemberFilter startingMemberFilter = StartingMemberFilter.ALL, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = true;

        int instantiatedAmount = 0;

        //Removes children of player content.
        for (int i = 0; i < content.transform.childCount; ++i)
        {
            Transform child = content.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (startingMemberFilter == StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }

            if (mode == Filter.Mode.BATTERS && Values.myTeam.players[i].Value.GetType() == typeof(Pitcher))
            {
                continue;
            }
            else if (mode == Filter.Mode.PITCHERS && Values.myTeam.players[i].Value.GetType() == typeof(Batter))
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerInstantiate(Values.myTeam.players[i].Value, content, playerView);
        }

        return instantiatedAmount;
    }

    public int RefreshPlayerList(Player.Position position, SortDropdown.SortMode sortMode, StartingMemberFilter startingMemberFilter = StartingMemberFilter.ALL, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = false;
        currentPosition = position;

        int instantiatedAmount = 0;

        //Removes children of player content.
        for (int i = 0; i < playerContent.transform.childCount; ++i)
        {
            Transform child = playerContent.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (startingMemberFilter == StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }

            if (Values.myTeam.players[i].Value.playerData.GetData(PlayerData.PP.POSITION) != position)
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerInstantiate(Values.myTeam.players[i].Value, playerView);
        }

        return instantiatedAmount;
    }

    public int RefreshPlayerList(Player.Position position, SortDropdown.SortMode sortMode, GameObject content, StartingMemberFilter startingMemberFilter = StartingMemberFilter.ALL, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = false;
        currentPosition = position;

        int instantiatedAmount = 0;

        //Removes children of player content.
        for (int i = 0; i < content.transform.childCount; ++i)
        {
            Transform child = content.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (startingMemberFilter == StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }

            if (Values.myTeam.players[i].Value.playerData.GetData(PlayerData.PP.POSITION) != position)
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerInstantiate(Values.myTeam.players[i].Value, content, playerView);
        }

        return instantiatedAmount;
    }

    public int RefreshPlayerList(Player.MetaPosition metaPosition, SortDropdown.SortMode sortMode, StartingMemberFilter startingMemberFilter = StartingMemberFilter.ALL, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS)
    {
        currentStartingMemberFilter = startingMemberFilter;
        isModeNow = false;
        currentMetaPosition = metaPosition;

        int instantiatedAmount = 0;

        //Removes children of player content.
        for (int i = 0; i < playerContent.transform.childCount; ++i)
        {
            Transform child = playerContent.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        PlayerSort(Values.myTeam.players.d, sortMode);

        //Instantiates playerPanel to it.
        for (int i = 0; i < Values.myTeam.players.d.Count; ++i)
        {
            if (startingMemberFilter == StartingMemberFilter.MEMBER_EXCLUDED && Values.myTeam.players[i].Value.isStartingMember && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.MEMBER_ONLY && !Values.myTeam.players[i].Value.isStartingMember)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_ONLY && !Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }
            else if (startingMemberFilter == StartingMemberFilter.SUB_EXCLUDED && Values.myTeam.players[i].Value.isSubstitute)
            {
                continue;
            }

            if (Values.myTeam.players[i].Value.playerData.GetData(PlayerData.PP.META_POSITION) != metaPosition)
            {
                continue;
            }

            ++instantiatedAmount;
            PlayerInstantiate(Values.myTeam.players[i].Value, playerView);
        }

        return instantiatedAmount;
    }


    //
    //Sort Functions
    //


    public void PlayerSort(List<KeyValuePair<int, Player>> playerList, SortDropdown.SortMode sortMode)
    {
        if (sortMode == SortDropdown.SortMode.NAME)
        {
            playerList.Sort(PlayerCompareByName);
        }
        else if (sortMode == SortDropdown.SortMode.NUMBER)
        {
            playerList.Sort(PlayerCompareByNumber);
        }
        else if (sortMode == SortDropdown.SortMode.OVERALL)
        {
            playerList.Sort(PlayerCompareByOverall);
        }
        else if (sortMode == SortDropdown.SortMode.POSITION)
        {
            playerList.Sort(PlayerCompareByPosition);
        }

        if (!SortDropdown.isAscendingOrder)
        {
            playerList.Reverse();
        }
    }

    public int PlayerCompareByName(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var NameComparison = ((string)pair2.Value.playerData.GetData(PlayerData.PP.NAME)).CompareTo(pair1.Value.playerData.GetData(PlayerData.PP.NAME));
        if (NameComparison != 0)
        {
            return -NameComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }

    public int PlayerCompareByOverall(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var OverallComparison = pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        return OverallComparison;
    }

    public int PlayerCompareByPosition(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var PositionComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.POSITION)).CompareTo((int)(pair2.Value.playerData.GetData(PlayerData.PP.POSITION)));
        if (PositionComparison != 0)
        {
            return PositionComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }

    public int PlayerCompareByNumber(KeyValuePair<int, Player> pair1, KeyValuePair<int, Player> pair2)
    {
        var NumberComparison = ((int)pair1.Value.playerData.GetData(PlayerData.PP.NUMBER)).CompareTo(pair2.Value.playerData.GetData(PlayerData.PP.NUMBER));
        if (NumberComparison != 0)
        {
            return NumberComparison;
        }
        else
        {
            return pair2.Value.GetOverall().CompareTo(pair1.Value.GetOverall());
        }
    }


    //
    //Load-Save Functions
    //


    //Translate schedules array into scheduleByDate dict.
    void ArrayToDict()
    {
        Values.scheduleByDate.Clear();

        foreach (Schedule s in Values.schedules.Values)
        {
            DateTime now_date = s.GetDate();

            if (Values.scheduleByDate.ContainsKey(now_date)) //scheduleByDate에 이미 해당되는 날짜가 키값으로 존재하는 경우
            {
                Values.scheduleByDate[now_date].Add(s);
                continue;
            }
            else //해당 날짜가 키값으로 없는 경우
            {
                Values.scheduleByDate.Add(now_date, new List<Schedule>()); //키값을 생성하고, 그 날짜에 해당되는 빈 리스트 생성
                Values.scheduleByDate[now_date].Add(s); //sampleSchedules[i], 즉 Schedule이 직전에 만들었던 빈 리스트에 추가됨.
            }
        }
    }

    //Loads: schedules, counter
    public void LoadData()
    {
        //Local variable for serialization.
        SerializableDict<DateTime, List<Schedule>> scheduleByDateOutput = new SerializableDict<DateTime, List<Schedule>>();

        //Reads Binary files.
        string path = Application.persistentDataPath + "/";
        BinaryFormatter b = new BinaryFormatter();
        //Schedules
        FileStream f_data = new FileStream(path + "Data.bin", FileMode.Open);
        FileStream f_raw = new FileStream(path + "Raw.bin", FileMode.Open);
        FileStream f_counter = new FileStream(path + "Counter.bin", FileMode.Open);
        scheduleByDateOutput = (SerializableDict<DateTime, List<Schedule>>)b.Deserialize(f_data);
        Values.scheduleByDate = scheduleByDateOutput.d;
        Values.schedules = (Dictionary<int, Schedule>)b.Deserialize(f_raw);
        counter = (int)b.Deserialize(f_counter);
        f_data.Close();
        f_raw.Close();
        f_counter.Close();
        //Players
        FileStream f_myPlayers = new FileStream(path + "MyPlayers.bin", FileMode.Open);
        f_myPlayers.Close();
    }

    //Saves: schedules, counter
    public void SaveData()
    {
        //Local variable for serialization.
        SerializableDict<DateTime, List<Schedule>> scheduleByDateOutput = new SerializableDict<DateTime, List<Schedule>>();
        scheduleByDateOutput.d = DeepCopy(Values.scheduleByDate);

        //Writes binary files.
        string path = Application.persistentDataPath + "/";
        BinaryFormatter b = new BinaryFormatter();
        //Schedules
        FileStream f_data = new FileStream(path + "Data.bin", FileMode.Create, FileAccess.Write);
        FileStream f_raw = new FileStream(path + "Raw.bin", FileMode.Create, FileAccess.Write);
        FileStream f_counter = new FileStream(path + "Counter.bin", FileMode.Create, FileAccess.Write);
        b.Serialize(f_data, scheduleByDateOutput);
        b.Serialize(f_raw, Values.schedules);
        b.Serialize(f_counter, counter);
        f_data.Close();
        f_raw.Close();
        f_counter.Close();
        //Players
        FileStream f_myPlayers = new FileStream(path + "MyPlayers.bin", FileMode.Create, FileAccess.Write);
        b.Serialize(f_myPlayers, Values.myTeam.players);
        f_myPlayers.Close();

    }

    //Deepcopy for scheduleByDate
    Dictionary<DateTime, List<Schedule>> DeepCopy(Dictionary<DateTime, List<Schedule>> scheduleList)
    {
        Dictionary<DateTime, List<Schedule>> output = new Dictionary<DateTime, List<Schedule>>();
        foreach (KeyValuePair<DateTime, List<Schedule>> kv in scheduleList)
        {
            DateTime dateTime = new DateTime(kv.Key.Year, kv.Key.Month, kv.Key.Day);
            List<Schedule> ssOutput = new List<Schedule>();
            foreach (Schedule s in kv.Value)
            {
                ssOutput.Add(s);
            }
            output.Add(dateTime, ssOutput);
        }

        return output;
    }


    //
    //Instantiate Functions
    //


    //Instantiates date prefab.
    void DateInstantiate(Date date)
    {
        datePanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = date.output;
        Instantiate(datePanel, listContentLayout.transform);
    }

    //Instantiates list prefab.
    void ListInstantiate(Schedule schedule)
    {

        listPanel.transform.GetChild(0).GetComponent<Image>().color = Schedule.categoryColors[(int)schedule.GetCategories()];
        listPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = schedule.GetTitle();
        listPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = schedule.GetDescription();

        ScheduleButton scheduleButton = listPanel.GetComponent<ScheduleButton>();
        scheduleButton.index = schedule.GetIndex();

        scheduleButton.titleText = titleText;
        scheduleButton.categoryText = categoryText;
        scheduleButton.accentpanel = accentPanel;
        scheduleButton.contentLayout = contentLayout;
        scheduleButton.dropdown = dropdown;
        scheduleButton.confirm = confirm;

        Instantiate(listPanel, listContentLayout.transform);
    }

    //Instantiates playerPanel prefab.
    GameObject PlayerInstantiate(Player player, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS, List<PlayerData.PP> prefs = null, bool isPitchesShown = true)
    {
        GameObject playerObject = Instantiate(playerPanel, playerContent.transform);

        playerObject.transform.GetChild(0).GetComponent<Image>().fillAmount = (100 - player.playerData.GetData(PlayerData.PP.CONDITION)) / 100f;
        playerObject.transform.GetChild(1).GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)]; //ListAccentPanel
        playerObject.transform.GetChild(2).GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)]; //ListAccentPanel2
        playerObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Player.positionStringShort[(int)player.playerData.data.d[PlayerData.PP.POSITION]];   //Position
        TextMeshProUGUI nameText = playerObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nameText.text = player.playerData.GetData(PlayerData.PP.NAME);   //Name
        playerObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString(); //Number
        TextMeshProUGUI orderText = playerObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>(); //Batting/StartPitching order
        if (player.order == -1)
        {
            orderText.text = "";
        }
        else
        {
            orderText.text = OrdinalNumbers.AddOrdinal(player.order);
        }
        TextMeshProUGUI overallText = playerObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        overallText.text = Mathf.FloorToInt(player.GetOverall()).ToString();   //Overall
        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(player.GetOverall()) < Player.statRange[i])
            {
                overallText.color = Player.statColor[i];
                if (i == 0)
                {
                    overallText.alpha = Player.statAlpha[i];
                }
                break;
            }
        }
        Image image = playerObject.transform.GetChild(7).GetComponent<Image>();
        if (player.isStartingMember) image.color = Color.white;
        else image.color = Color.clear;
        if (player.isSubstitute) image.color = new Color(1, 1, 1, 0.5f);

        if (prefs == null)
        {
            foreach (KeyValuePair<string, float> stat in player.finalStats.d)
            {
                StatInstantiate(stat.Key, stat.Value, playerObject);
            }
            if (player.GetType() == typeof(Pitcher) && isPitchesShown)
            {
                PitchesInstantiate(((Pitcher)player).pitches.d, playerObject);
            }
        }
        else
        {

        }

        //For button script
        PlayerList playerList = playerObject.GetComponent<PlayerList>();
        playerList.player = player;
        playerObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            playerList.OnClick(this, playerView);
        });

        return playerObject;
    }

    GameObject PlayerInstantiate(Player player, GameObject content, PlayerList.PlayerView playerView = PlayerList.PlayerView.SKILLS_AND_STATISTICS, List<PlayerData.PP> prefs = null, bool isPitchesShown = true)
    {
        GameObject playerObject = Instantiate(playerPanel, content.transform);

        playerObject.transform.GetChild(0).GetComponent<Image>().fillAmount = (100 - player.playerData.GetData(PlayerData.PP.CONDITION)) / 100f;
        playerObject.transform.GetChild(1).GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)]; //ListAccentPanel
        playerObject.transform.GetChild(2).GetComponent<Image>().color = Player.positionColor[(int)player.playerData.GetData(PlayerData.PP.POSITION)]; //ListAccentPanel2
        playerObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = Player.positionStringShort[(int)player.playerData.data.d[PlayerData.PP.POSITION]];   //Position
        TextMeshProUGUI nameText = playerObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nameText.text = player.playerData.GetData(PlayerData.PP.NAME);   //Name
        playerObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = player.playerData.GetData(PlayerData.PP.NUMBER).ToString(); //Number
        TextMeshProUGUI orderText = playerObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>(); //Batting/StartPitching order
        if (player.order == -1)
        {
            orderText.text = "";
        }
        else
        {
            orderText.text = OrdinalNumbers.AddOrdinal(player.order);
        }
        TextMeshProUGUI overallText = playerObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        overallText.text = Mathf.FloorToInt(player.GetOverall()).ToString();   //Overall
        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(player.GetOverall()) < Player.statRange[i])
            {
                overallText.color = Player.statColor[i];
                if (i == 0)
                {
                    overallText.alpha = Player.statAlpha[i];
                }
                break;
            }
        }
        Image image = playerObject.transform.GetChild(7).GetComponent<Image>();
        if (player.isStartingMember) image.color = Color.white;
        else image.color = Color.clear;
        if (player.isSubstitute) image.color = new Color(1, 1, 1, 0.5f);


        if (prefs == null)
        {
            foreach (KeyValuePair<string, float> stat in player.finalStats.d)
            {
                StatInstantiate(stat.Key, stat.Value, playerObject);
            }
            if (player.GetType() == typeof(Pitcher) && isPitchesShown)
            {
                PitchesInstantiate(((Pitcher)player).pitches.d, playerObject);
            }
        }
        else
        {

        }

        //For button script
        PlayerList playerList = playerObject.GetComponent<PlayerList>();
        playerList.player = player;
        playerObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            playerList.OnClick(this, playerView);
        });

        return playerObject;
    }

    //Instantiates stat prefab.
    void StatInstantiate(string title, float value, GameObject playerObject)
    {
        GameObject statLayout = playerObject.transform.GetChild(5).gameObject;
        GameObject statObject = Instantiate(stat, statLayout.transform);
        TextMeshProUGUI titleText = statObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        titleText.text = title;    //Stat.Title
        TextMeshProUGUI valueText = statObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        valueText.text = Mathf.FloorToInt(value).ToString();    //Stat.Value
        for (int i = 0; i < Player.statRange.Count; ++i)
        {
            if (Mathf.FloorToInt(value) < Player.statRange[i])
            {
                if (i == 4)
                {
                    titleText.color = Player.statColor[i];
                }
                valueText.color = Player.statColor[i];
                valueText.alpha = Player.statAlpha[i];
                break;
            }
        }
    }

    //Instantiates pitches prefab.
    void PitchesInstantiate(Dictionary<Pitcher.Pitch, float> pitchesDictionary, GameObject playerObject)
    {
        GameObject statLayout = playerObject.transform.GetChild(5).gameObject;
        GameObject pitchesObject = Instantiate(pitches, statLayout.transform);
        string text = "";
        foreach (Pitcher.Pitch pitch in pitchesDictionary.Keys)
        {
            text += Pitcher.PitchStringShort[(int)pitch] + " ";
        }
        text.Remove(text.Length - 1);
        pitchesObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    public void StatisticsInstantiate(PlayerStatistics.PS stat, float player, float average, int rank)
    {
        GameObject statObject = Instantiate(statistics, SP_content.transform);
        StatisticPanel statisticPanel = statObject.GetComponent<StatisticPanel>();

        statisticPanel.stat = stat;

        statObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerStatistics.PSStringShort[(int)stat];  //title
        TextMeshProUGUI playerText = statObject.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();  //player
        TextMeshProUGUI averageText = statObject.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>(); //average
        statObject.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = rank.ToString(); //ranking

        if (player == (int)player)
        {
            playerText.text = Mathf.FloorToInt(player).ToString();
            averageText.text = Mathf.FloorToInt(average).ToString("F0");
        }
        else
        {
            playerText.text = player.ToString("F3");
            averageText.text = average.ToString("F3");
        }

        if(PlayerStatistics.lowerBetter.Contains(stat))
        {
            if (player > average * 1.1f)
            {
                playerText.color = Colors.red;
                playerText.alpha = 0.5f;
            }
            else if (player < average * 0.8f)
            {
                playerText.color = Colors.green;
            }
        }
        else
        {
            if (player < average * 0.9f)
            {
                playerText.color = Colors.red;
                playerText.alpha = 0.5f;
            }
            else if (player > average * 1.2f)
            {
                playerText.color = Colors.green;
            }
        }
    }

    //Instantiates graph prefab.
    public void GraphInstantiate(int year, float min, float max, float player, float average, int rank = 1)
    {
        void SetTop(RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        if (player == (int)player)
        {
            average = Mathf.RoundToInt(average);
        }

        GameObject graphObject = Instantiate(graph, SP_graphContent.transform);

        RectTransform averageRect = graphObject.transform.GetChild(0).GetComponent<RectTransform>();   //Average
        SetTop(averageRect, 350 * (1 - (average - min) / (max - min)) + 50);
        RectTransform playerRect = graphObject.transform.GetChild(1).GetComponent<RectTransform>();  //Player
        SetTop(playerRect, 350 * (1 - (player - min) / (max - min)) + 50);

        TextMeshProUGUI averageText = graphObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();  //AverageText
        SetTop(averageText.GetComponent<RectTransform>(), 350 * (1 - (average - min) / (max - min)) + 50);
        TextMeshProUGUI playerText = graphObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();  //PlayerText
        SetTop(playerText.GetComponent<RectTransform>(), 350 * (1 - (player - min) / (max - min)) + 50);

        if (player == (int)player)
        {
            playerText.text = Mathf.FloorToInt(player).ToString();
            averageText.text = Mathf.FloorToInt(average).ToString("F0");
        }
        else
        {
            playerText.text = player.ToString("F3");
            averageText.text = average.ToString("F3");
        }

        TextMeshProUGUI rankText = graphObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();  //RankText
        rankText.text = rank.ToString();
        TextMeshProUGUI dateText = graphObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();  //Date
        dateText.text = year.ToString();

        if (player > average)
        {
            averageRect.SetSiblingIndex(1);
        }
    }

    //Instantiates training prefab.
    public void TrainingInstantiate(Training trainingScript, Player player)
    {
        GameObject trainingObject = Instantiate(training, TP_content.transform);

        TrainPanel trainPanel = trainingObject.GetComponent<TrainPanel>();
        trainPanel.train = trainingScript.train;
        trainPanel.player = player;

        trainingObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Training.trainString[(int)trainingScript.GetTrain()];   //title
        TextMeshProUGUI skillsText = trainingObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();  //skills
        string skillsString = "";
        foreach(PlayerData.PP pref in trainingScript.modifier.Keys)
        {
            bool flag = false;
            List<string> split = skillsString.Split(' ').ToList();
            for(int i = 0; i < split.Count; ++i)
            {
                if (PlayerData.PPStringShort[(int)PlayerData.FindSerializablePP(pref)] == split[i])
                {
                    split[i+1] += "+";
                    flag = true;
                    break;
                }
            }
            
            if(flag)
            {
                skillsString = "";
                for (int i = 0; i < split.Count; ++i)
                {
                    skillsString += split[i] + " ";
                }
            }
            else
            {
                skillsString += PlayerData.PPStringShort[(int)PlayerData.FindSerializablePP(pref)] + " +  ";
            }   
        }
        skillsText.text = skillsString;
        Image currentlySelected = trainingObject.transform.GetChild(2).GetComponent<Image>();   //currentlySelected
        if(player.train != trainingScript.train)
        {
            currentlySelected.color = Color.clear;
        }
        else
        {
            TrainPanel.focusedObject = trainingObject;
            TrainPanel.markedObject = trainingObject;
            trainingObject.GetComponent<TrainPanel>().OnClick();
        }
    }

    //Instantiates effect prefab.
    public void EffectInstantiate(PlayerData.PP pref, int days)
    {
        GameObject effectObject = Instantiate(effectPanel, TP_effectContent.transform);
        effectObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)PlayerData.FindSerializablePP(pref)];
        effectObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerData.PPString[(int)pref];
        effectObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = days.ToString() + " Days";
    }
}
