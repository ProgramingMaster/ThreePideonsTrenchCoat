using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Follower", menuName = "Follower")]
public class Follower: ScriptableObject {
    public string name;
    public Sprite sprite;
    public Sprite head;
    public RuntimeAnimatorController anim;
}
