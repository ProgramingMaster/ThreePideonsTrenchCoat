using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitSetup : MonoBehaviour
{
    public GameObject higherSlot;
    public GameObject lowerSlot;

    void Start() {
        Debug.Log("asdf");
        if (GameManager.Instance.follower1 != null && GameManager.Instance.follower2 != null) {
            Debug.Log("Recruited!");
            GameManager.Instance.Summon(GameManager.Instance.follower1, higherSlot, transform.position);
            GameManager.Instance.Summon(GameManager.Instance.follower2, lowerSlot, transform.position);
        }
    }

}
