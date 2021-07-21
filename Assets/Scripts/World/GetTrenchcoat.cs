using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTrenchcoat : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collider) {
        if(collider.gameObject.CompareTag("Player")) {
            GameManager.Instance.conditions["HasTrenchcoat"] = true;
            Debug.Log(GameManager.Instance.conditions["HasTrenchcoat"]);
        }
    }
}
