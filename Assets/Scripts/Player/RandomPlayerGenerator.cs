using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerGenerator
{
    /// <summary>
    /// Creats a random player.
    /// </summary>
    /// <param name="isPitcher"></param>
    /// <param name="metaPosition"></param>
    /// <returns></returns>
    public static Player CreatePlayer(bool isPitcher = true, Player.MetaPosition metaPosition = Player.MetaPosition.STARTER_PITCHER)
    {
        PlayerData playerData = new PlayerData("", 0, 0, 0, metaPosition);
        PlayerStatistics stats = new PlayerStatistics(1);

        playerData.SetData(PlayerData.PP.NAME, RandomNameGenerator.MakeName());
        playerData.SetData(PlayerData.PP.NUMBER, UnityEngine.Random.Range(0, 100));
        playerData.SetData(PlayerData.PP.BIRTH_DATE, new DateTime(UnityEngine.Random.Range(1970, 2000), UnityEngine.Random.Range(1, 13), UnityEngine.Random.Range(1, 28)));
        playerData.SetData(PlayerData.PP.HEIGHT, UnityEngine.Random.Range(170f, 190f));
        playerData.SetData(PlayerData.PP.WEIGHT, UnityEngine.Random.Range(60f, 90f));
        playerData.SetData(PlayerData.PP.IS_LEFT_HANDED, RandomHanded());
        playerData.SetData(PlayerData.PP.CONDITION, UnityEngine.Random.Range(80f, 100f));

        foreach (PlayerData.PP metaPref in PlayerData.serializableDictPrefs.Keys.ToList())
        {
            if (metaPref == PlayerData.PP.PITCH) continue;
            foreach (PlayerData.PP pref in ((SerializableDictPP)playerData.data.d[metaPref]).d.Keys.ToList())
            {
                playerData.SetDictData(metaPref, UnityEngine.Random.Range(30f, 100f), pref);
            }
        }

        //training
        Training.Train train = (Training.Train)UnityEngine.Random.Range(0, Training.trainString.Count);

        //pitcher
        if (isPitcher)
        {
            //Sets pitches.
            float valueTemp = UnityEngine.Random.Range(30f, 100f);
            Dictionary<Pitcher.Pitch, float> pitches = new Dictionary<Pitcher.Pitch, float>
            {
                { Pitcher.Pitch.FOURSEAM, valueTemp }
            };

            playerData.data.d.Add(PlayerData.PP.PITCH, new SerializableDictPP());
            playerData.SetDictData(PlayerData.PP.PITCH, valueTemp, (PlayerData.PP)PlayerData.PPString.IndexOf(Pitcher.PitchString[(int)Pitcher.Pitch.FOURSEAM]));
            for (int i = 0; i < UnityEngine.Random.Range(1, 6); ++i)
            {
                Pitcher.Pitch pitch;
                do
                {
                    pitch = (Pitcher.Pitch)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Pitcher.Pitch)).Length);
                } while (pitches.ContainsKey(pitch));
                float value = UnityEngine.Random.Range(30f, 100f);
                pitches.Add(pitch, value);
                playerData.SetDictData(PlayerData.PP.PITCH, value, (PlayerData.PP)PlayerData.PPString.IndexOf(Pitcher.PitchString[(int)pitch]));
            }

            //Sets random pitcher stat data.
            for(int year = Values.date.Year - UnityEngine.Random.Range(1, 3); year <= Values.date.Year; ++year)
            {
                int gameTotal = UnityEngine.Random.Range(20, 33);
                for (int game = 0; game < gameTotal; ++game)
                {
                    DateTime date = new DateTime(year, game / 28 + 1, game % 28 + 1);
                    stats.SetStat(1, PlayerStatistics.PS.G, date);
                    foreach (PlayerStatistics.PS stat in PlayerStatistics.pitcherPS)
                    {
                        stats.SetStat(UnityEngine.Random.Range(1, 3), stat, date);
                    }
                }
            }
            
            Player.AddPlayerCreated();
            return new Pitcher(Player.GetPlayerCreated(), playerData, stats, train, pitches, playerData.GetData(PlayerData.PP.STRENGTH), playerData.GetData(PlayerData.PP.CONTROL), playerData.GetData(PlayerData.PP.CONSISTENCY), playerData.GetData(PlayerData.PP.INTELLECT));
        }
        //batter
        else
        {
            //playerData.SetData(PlayerData.PP.POSITION, (Player.Position)UnityEngine.Random.Range(0, 9));

            //Sets batter stat data.
            for (int year = Values.date.Year - UnityEngine.Random.Range(1, 3); year <= Values.date.Year; ++year)
            {
                int gameTotal = UnityEngine.Random.Range(20, 33);
                for (int game = 0; game < gameTotal; ++game)
                {
                    DateTime date = new DateTime(year, game / 28 + 1, game % 28 + 1);
                    stats.SetStat(1, PlayerStatistics.PS.G, date);
                    foreach (PlayerStatistics.PS stat in PlayerStatistics.batterPS)
                    {
                        stats.SetStat(UnityEngine.Random.Range(1, 3), stat, date);
                    }
                }
            }

            Player.AddPlayerCreated();
            return new Batter(Player.GetPlayerCreated(), playerData, stats, train, playerData.GetData(PlayerData.PP.STRENGTH), playerData.GetData(PlayerData.PP.CONTROL), playerData.GetData(PlayerData.PP.CONSISTENCY), playerData.GetData(PlayerData.PP.INTELLECT));
        }
    }

    /// <summary>
    /// Returns true(LeftHanded) or false(RightHanded).
    /// </summary>
    /// <returns></returns>
    static bool RandomHanded()
    {
        return UnityEngine.Random.Range(0, 2) == 1;
    }

    /// <summary>
    /// Creates a team.
    /// </summary>
    /// <returns></returns>
    public static List<KeyValuePair<int, Player>> CreateTeam()
    {
        List<KeyValuePair<int, Player>> myPlayers = new List<KeyValuePair<int, Player>>();

        for (int i = 0; i < 10; ++i)
        {
            myPlayers.Add(new KeyValuePair<int, Player>(myPlayers.Count, CreatePlayer(true, Player.MetaPosition.STARTER_PITCHER)));
        }
        for (int i = 0; i < 10; ++i)
        {
            myPlayers.Add(new KeyValuePair<int, Player>(myPlayers.Count, CreatePlayer(true, Player.MetaPosition.RELIEF_PITCHER)));
        }
        for (int i = 0; i < 8; ++i)
        {
            myPlayers.Add(new KeyValuePair<int, Player>(myPlayers.Count, CreatePlayer(false, Player.MetaPosition.IN_FIELD_PLAYER)));
        }
        for (int i = 0; i < 8; ++i)
        {
            myPlayers.Add(new KeyValuePair<int, Player>(myPlayers.Count, CreatePlayer(false, Player.MetaPosition.OUT_FIELD_PLAYER)));
        }
        for (int i = 0; i < 3; ++i)
        {
            myPlayers.Add(new KeyValuePair<int, Player>(myPlayers.Count, CreatePlayer(false, Player.MetaPosition.CATCHER)));
        }

        return myPlayers;
    }

    /// <summary>
    /// Creates starting members.
    /// </summary>
    /// <param name="myPlayers"></param>
    /// <returns></returns>
    public static List<KeyValuePair<Player.Position, Player>> CreateStartingMember(List<KeyValuePair<int, Player>> myPlayers)
    {
        List<KeyValuePair<Player.Position, Player>> myStartingMembers = new List<KeyValuePair<Player.Position, Player>>();

        //Adds a member to myStartingMembers.
        void AddMember(int amount, Player.MetaPosition metaPosition, Player.Position position, bool isSub = false)
        {
            int c = 0;
            foreach (KeyValuePair<int, Player> playerPair in myPlayers)
            {
                if (c == amount) break;
                if (playerPair.Value.playerData.GetData(PlayerData.PP.META_POSITION) == metaPosition && !playerPair.Value.isStartingMember)
                {
                    ++c;
                    playerPair.Value.isStartingMember = true;
                    playerPair.Value.isSubstitute = isSub;
                    playerPair.Value.playerData.SetData(PlayerData.PP.POSITION, position);
                    myStartingMembers.Add(new KeyValuePair<Player.Position, Player>(playerPair.Value.playerData.GetData(PlayerData.PP.POSITION), playerPair.Value));
                }
            }
        }
        
        //pitchers
        AddMember(5, Player.MetaPosition.STARTER_PITCHER, Player.Position.STARTER_PITCHER);
        AddMember(2, Player.MetaPosition.STARTER_PITCHER, Player.Position.LONG_RELIEF_PITCHER);
        AddMember(2, Player.MetaPosition.RELIEF_PITCHER, Player.Position.MIDDLE_RELIEF_PITCHER);
        AddMember(1, Player.MetaPosition.RELIEF_PITCHER, Player.Position.SETUP_MAN);
        AddMember(1, Player.MetaPosition.RELIEF_PITCHER, Player.Position.CLOSER_PITCHER);

        //batters
        AddMember(1, Player.MetaPosition.CATCHER, Player.Position.CATCHER);
        AddMember(1, Player.MetaPosition.IN_FIELD_PLAYER, Player.Position.FIRST_BASE_MAN);
        AddMember(1, Player.MetaPosition.IN_FIELD_PLAYER, Player.Position.SECOND_BASE_MAN);
        AddMember(1, Player.MetaPosition.IN_FIELD_PLAYER, Player.Position.SHORT_STOP);
        AddMember(1, Player.MetaPosition.IN_FIELD_PLAYER, Player.Position.THIRD_BASE_MAN);
        AddMember(1, Player.MetaPosition.OUT_FIELD_PLAYER, Player.Position.LEFT_FIELDER);
        AddMember(1, Player.MetaPosition.OUT_FIELD_PLAYER, Player.Position.CENTER_FIELDER);
        AddMember(1, Player.MetaPosition.OUT_FIELD_PLAYER, Player.Position.RIGHT_FIELDER);

        //subs
        AddMember(1, Player.MetaPosition.CATCHER, Player.Position.SUB_CATCHER, true);
        AddMember(2, Player.MetaPosition.IN_FIELD_PLAYER, Player.Position.SUB_IN_FIELD, true);
        AddMember(3, Player.MetaPosition.OUT_FIELD_PLAYER, Player.Position.SUB_OUT_FIELD, true);

        return myStartingMembers;
    }

    /// <summary>
    /// Choose 9 batters randomly in my team, and add it to battingOrder list.
    /// </summary>
    /// <param name="myPlayers"></param>
    /// <returns></returns>
    public static List<Batter> CreateBattingOrder(List<KeyValuePair<int, Player>> myPlayers)
    {
        List<Batter> batters = new List<Batter>();
        
        while(batters.Count < 9)    //NOTE: THIS SHOULD BE 8 AFTER!
        {
            Player player = myPlayers[UnityEngine.Random.Range(0, myPlayers.Count)].Value;
            if (!player.isStartingMember)
            {
                continue;
            }
            else if (player.GetType() != typeof(Batter))
            {
                continue;
            }
            else if (batters.Find(delegate(Batter batter){ return player == batter; }) != null)
            {
                continue;
            }
            else
            {
                batters.Add((Batter)player);
                player.order = batters.Count;
            }
        }

        return batters;
    }

    /// <summary>
    /// Choose 5 pitcher randomly in my team, and add it to startPitchingOrder list.
    /// </summary>
    /// <param name="myPlayers"></param>
    /// <returns></returns>
    public static List<Pitcher> CreateStartPitchingOrder(List<KeyValuePair<int, Player>> myPlayers)
    {
        List<Pitcher> pitchers = new List<Pitcher>();

        while(pitchers.Count < 5)
        {
            Player player = myPlayers[UnityEngine.Random.Range(0, myPlayers.Count)].Value;
            if(!player.isStartingMember)
            {
                continue;
            }
            else if (player.GetType() != typeof(Pitcher))
            {
                continue;
            }
            else if (player.playerData.GetData(PlayerData.PP.POSITION) != Player.Position.STARTER_PITCHER)
            {
                continue;
            }
            else if (pitchers.Find(delegate (Pitcher pitcher) { return player == pitcher; }) != null)
            {
                continue;
            }
            else
            {
                pitchers.Add((Pitcher)player);
                player.order = pitchers.Count;
            }
        }

        return pitchers;
    }
}
