using UnityEngine;

[CreateAssetMenu (fileName = "New Settings", menuName = "Custom/Settings")]
public class Settings : ScriptableObject
{
    //user settings
    [Range(0.0001f, 1)] public float masterVolume = 1;
    [Range(0.0001f, 1)] public float musicVolume = 1;
    [Range(0.0001f, 1)] public float sfxVolume = 1;
    [Range(0.0001f, 1)] public float ambienceVolume = 1;

    //game data
    public int loop;
    public int level;

    public int lastSceneIndex;
    public Weapon weapon;


    //difficulty level
    public int flareCount = 4;
    public int tokens = 0;

    public int[] unlocks = { 1, 0, 0, 0 };

    //level data
    [NonReorderable] public LoopData[] loops = new LoopData[5];
}
