using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "JD/Teams/Opposition Chart", fileName = "team_opposition_NAME")]
public class TeamOppositionChartSO : ScriptableObject
{
    [Serializable]
    public struct TeamOpposition_Data
    {
        public TeamData[] Teams;
    }

    public List<TeamOpposition_Data> data;

    public TeamOpposition_Data GetRandomTeamData()
    {
        int random = UnityEngine.Random.Range(0, data.Count);
        return data[random];
    }

    public static TeamOppositionChartSO chart = null;
    public static TeamOppositionChartSO instance
    {
        get
        {
            if (chart == null) chart = Resources.Load<TeamOppositionChartSO>("team_opposition_chart");
            return chart;
        }
    }

    public static bool IsOpposingTeam(string id1, string id2)
    {
        if (id1 == id2) return false;
        foreach (TeamOpposition_Data oppositionData in instance.data)
        {
            List<string> teamIds = oppositionData.Teams.Select(team => team.name).ToList();
            if (teamIds.Contains(id1) && teamIds.Contains(id2))
            {
                return true;
            }
        }
        return false;
    }
}
