using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFaceControll : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] newSprite;
    public void ChangeSprite(int i)
    {
        spriteRenderer.sprite = newSprite[i];
    }
    public void Reset()
    {
        spriteRenderer.sprite = null;
    }
}
