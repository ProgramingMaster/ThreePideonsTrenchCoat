using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFollowers : MonoBehaviour
{
    public void Summon(string name, Sprite sprite, GameObject higherSlot, GameObject lowerSlot, Vector2 position) {
        Debug.Log("Summoned!");
        GameObject character = GameObject.Find("Characters/" + name);
        character.SetActive(false);
        SpriteRenderer higherSlotSprite = higherSlot.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        Debug.Log(higherSlotSprite.sprite);
        if (higherSlotSprite.sprite == null) {
            higherSlotSprite.sprite = sprite;
            higherSlot.transform.position = position;
        } else {
            SpriteRenderer lowerSlotSprite = lowerSlot.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            lowerSlotSprite.sprite = sprite;
            lowerSlot.transform.position = position;
        }
    }
}
