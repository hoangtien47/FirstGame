using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPreObj : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    public void Reset()
    {
        spriteRenderer.sprite = null;
    }
}
