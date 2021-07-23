using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SummonFollowers : MonoBehaviour
{
    public void Summon(string name, Sprite sprite, RuntimeAnimatorController anim, GameObject higherSlot, GameObject lowerSlot, Vector2 position) {
        Debug.Log("Summoned!");
        GameObject character = GameObject.Find("Characters/" + name);
        character.SetActive(false);
        SpriteRenderer higherSlotSprite = higherSlot.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        Animator higherSlotAnimator = higherSlot.GetComponent(typeof(Animator)) as Animator;
        Debug.Log(higherSlotSprite.sprite);
        if (higherSlotSprite.sprite == null) {
            higherSlotSprite.sprite = sprite;
            higherSlotAnimator.runtimeAnimatorController = anim;
            higherSlot.transform.position = position;
        } else {
            SpriteRenderer lowerSlotSprite = lowerSlot.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            Animator lowerSlotAnimator = lowerSlot.GetComponent(typeof(Animator)) as Animator;
            lowerSlotSprite.sprite = sprite;
            lowerSlotAnimator.runtimeAnimatorController = anim;
            lowerSlot.transform.position = position;
        }
    }
}
