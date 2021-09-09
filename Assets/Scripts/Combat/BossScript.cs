using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossScript : MonoBehaviour
{
    public bool agro;
    public GameObject Goo1;
    public GameObject Goo2;
    public GameObject GooBig;

    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Player") {
            agro = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
