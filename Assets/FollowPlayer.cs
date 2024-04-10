using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float MinModifier;
    public float MaxModifier;
    Vector2 velocity = Vector2.zero;
    bool isFollowing;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        isFollowing = false;
    }

    public void StartFollow()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(target.gameObject))
        {
            target.GetComponent<PlayerController>().increaseMana(10);
            Destroy(this.gameObject);
        }
    }
}
