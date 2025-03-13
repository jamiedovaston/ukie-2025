using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "JD/Teams/Data", fileName = "team_NAME")]
public class TeamData : ScriptableObject
{
    public Color color;

    public static List<TeamData> all;
    public static TeamData Get(string _id)
    {
        if (all == null)
        {
            all = Resources.LoadAll<TeamData>("").ToList();
        }

        TeamData data = all.Find(d => d.name == _id);

        Debug.Assert(data != null, "Couldnt find TeamData with ID!");

        return data;
    }

    public static TeamData GetRandom()
    {
        if (all == null)
        {
            all = Resources.LoadAll<TeamData>("").ToList();
        }

        int r = Random.Range(0, all.Count);
        TeamData data = all[r];

        Debug.Assert(data != null, "Random TeamData is null!");

        return data;
    }
}
