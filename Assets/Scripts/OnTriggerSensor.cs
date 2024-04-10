using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerSensor : MonoBehaviour
{
    private string tag = "Player";
    public UnityEvent OnTriggerEnterEvent, onTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            onTriggerExitEvent?.Invoke();
        }
    }
}
