using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTrenchcoat : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    Sprite leftSprite;
    Sprite rightSprite;

    void Start() {
        leftSprite = (left.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite;
        rightSprite = (right.GetComponent(typeof (SpriteRenderer)) as SpriteRenderer).sprite;
    }
 
    public void Setup(Sprite head) {
        Debug.Log("Who called me?");
        //This is for the head. Isn't called right now
        if ((left.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite == null)
            (left.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = head;
        else
            (right.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = head;
    }
}
