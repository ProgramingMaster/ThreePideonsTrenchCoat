using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerToTrenchcoat : MonoBehaviour
{
    public GameObject Lower;
    public GameObject Higher;

    void Start() {
        if (GameManager.Instance.follower1 == null && GameManager.Instance.follower2 == null)
            ToTrenchcoat();
    }

    public void ToTrenchcoat() {
        Lower.SetActive(false);
        Higher.SetActive(false);
    }

    public void ToBird() {
        if (GameManager.Instance.follower1 != null && GameManager.Instance.follower2 != null) {
            Lower.SetActive(true);
            Higher.SetActive(true);
        }
    }
}
