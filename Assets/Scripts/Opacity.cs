using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    public Transform player;
    public float range;
    public float fadeDuration;
    private SpriteRenderer spriteRenderer;
    private Color targetColor;
    [SerializeField]
    private bool isFading;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetColor = spriteRenderer.color;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= range && !isFading)
        {
            targetColor.a = 1f;
            LeanTween.value(gameObject, UpdateOpacity, spriteRenderer.color.a, targetColor.a, fadeDuration)
                .setOnStart(() =>   isFading = true)
                .setOnComplete(() => isFading = false);
        }
        else if (distance > range && !isFading)
        {
            targetColor.a = 0f;
            LeanTween.value(gameObject, UpdateOpacity, spriteRenderer.color.a, targetColor.a, fadeDuration)
                .setOnStart(() => isFading = true)
                .setOnComplete(() => isFading = false);
        }
    }

    private void UpdateOpacity(float opacity)
    {
        Color color = spriteRenderer.color;
        color.a = opacity;
        spriteRenderer.color = color;
    }
}
