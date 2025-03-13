using System;
using System.Collections.Generic;
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
            if(chart == null) chart = Resources.Load<TeamOppositionChartSO>("team_opposition_chart");
            return chart;
        }
    }
}
