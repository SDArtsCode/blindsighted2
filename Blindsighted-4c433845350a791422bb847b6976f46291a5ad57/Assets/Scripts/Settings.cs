using UnityEngine;

[CreateAssetMenu (fileName = "New Settings", menuName = "Custom/Settings")]
public class Settings : ScriptableObject
{
    //user settings
    [Range(0.0001f, 1)] public float masterVolume;
    [Range(0.0001f, 1)] public float musicVolume;
    [Range(0.0001f, 1)] public float sfxVolume;
    [Range(0.0001f, 1)] public float ambienceVolume;

    //game data
    public int loop;
    public Weapon weapon;
}
