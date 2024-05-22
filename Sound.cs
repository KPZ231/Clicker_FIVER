using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Sound", order = 0)]
public class Sound : ScriptableObject
{
    public string _name = string.Empty;
    public AudioClip clip = null;
    [Range(0f, 1f)] public float volume = 0.5f;
    
}