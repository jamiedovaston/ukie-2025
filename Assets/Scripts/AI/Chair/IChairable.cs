using UnityEngine;

public interface IChairable
{
    public Transform sitSocket { get; }
    public Vector3 position { get; }
    public bool occupied { get; set; }

}
