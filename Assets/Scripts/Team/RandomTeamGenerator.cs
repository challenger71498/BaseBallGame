using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeamGenerator
{
    public static List<string> cityNames = new List<string>()
    {
        "Seoul", "Incheon", "Busan", "Daejeon", "Daegu", "Ulsan", "Gwangju", "Saejong", "Suwon", "Cheongju"
    };

    public static List<string> teamNames = new List<string>()
    {
        "Bears", "Wyverns", "Giants", "Eagles", "Lions", "Dolphins", "Tigers", "Heroes", "Wings", "Mariners"
    };

    public static List<string> shortNames = new List<string>()
    {
        "SLB", "ICW", "BSG", "DJE", "DGL", "USD", "GJT", "SJH", "SWW", "CJG"
    };

    public static List<Color> teamColors = new List<Color>()
    {
        Colors.red, Colors.blue, Colors.green, Colors.pink, Colors.purple, Colors.skyblue, Colors.yellow, Colors.primaryDark, Colors.redDark, Colors.blueDark
    };

    static int teamCreated = 0;
    public static Team CreateTeam(int index = -1)
    {

        Team team = new Team();

        if(index == -1)
        {
            index = teamCreated;
        }

        team.teamData.SetData(TeamData.TP.CITY_NAME, cityNames[index]);
        team.teamData.SetData(TeamData.TP.TEAM_NAME, teamNames[index]);
        team.teamData.SetData(TeamData.TP.NAME, cityNames[index] + " " + teamNames[index]);
        team.teamData.SetData(TeamData.TP.SHORT_NAME, shortNames[index]);
        team.teamData.SetData(TeamData.TP.COLOR, teamColors[index]);

        team.players.d = RandomPlayerGenerator.CreateTeam();
        team.startingMembers.d = RandomPlayerGenerator.CreateStartingMember(team.players.d);
        team.battingOrder.d = RandomPlayerGenerator.CreateBattingOrder(team.players.d);
        team.startPitchOrder.d = RandomPlayerGenerator.CreateStartPitchingOrder(team.players.d);

        teamCreated++;

        return team;
    }


}
