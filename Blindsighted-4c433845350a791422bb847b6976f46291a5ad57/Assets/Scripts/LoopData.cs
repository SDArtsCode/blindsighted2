using UnityEngine;

[System.Serializable]
public class LoopData
{
    public int loopNumber;

    [NonReorderable] public LevelData[] levels = new LevelData[3];
}
