using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject TCG;
    public GameObject BG;

    void Start() {
        TCG.SetActive(false);
        BG.SetActive(true);
    }

    
    public void ToTrenchcoat() {
        TCG.SetActive(true);
        BG.SetActive(false);
    }

    public void ToBird() {
        TCG.SetActive(false);
        BG.SetActive(true);
    }
}
