using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLedges : MonoBehaviour
{
    static bool active = true;
    static BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    static public void toggle() {
        active = !active;
        bc.gameObject.SetActive(active);
    }
}
