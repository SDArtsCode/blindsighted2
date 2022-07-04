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
    public int lastSceneIndex;
    public Weapon weapon;
    public int flareCount = 4;
}
