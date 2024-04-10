using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{

    public SpriteRenderer sprite;

    public ChaseState ChaseState;
    public AttackState AttackState;
    public PatrolState PatrolState;
    public HurtState HurtState;
    public DieState DieState;
    public SpawnState SpawnState;

    public GameObject point;

    //public IdleState IdleState = new IdleState();

    public AIPath aiPath;

    public Animator animator; // Unity Animator component
    public WayPointFollower wayPointFollower;
    public GameObject player;
    private Rigidbody2D rb;

    public float sightDistance = 7f; // Adjust the value as needed
    public float attackDistance = 2f; // Adjust the value as needed
    public StateManager stateManager;
    public Transform hitPoint;

    public GameObject destroyGameObject;
    string name;
    public AudioManager AudioManager;
    public enum MovementState { idle, running, attack, hurt, death, spawn };

    public LayerMask sightLayerMask; // Assign the appropriate layer mask in the Inspector
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        wayPointFollower = GetComponent<WayPointFollower>();
        player = GameObject.FindGameObjectWithTag("Player");

        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);
        PatrolState = new PatrolState(this);
        HurtState = new HurtState(this);
        DieState = new DieState(this);
        SpawnState = new SpawnState(this);

        AudioManager = FindObjectOfType<AudioManager>();

        name = gameObject.name;
        
        stateManager = GetComponent<StateManager>();
        InitializeState();
    }

    void Update()
    {

    }


    public void FlipSprite(Transform transform)
    {
        bool shouldFlip = transform.position.x < this.transform.position.x;

        // Flip the entire object
        this.transform.localScale = new Vector3((shouldFlip ? -1 : 1), 1, 1);
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitPoint.position, attackDistance);
    }
    public void spawnPoint()
    {
        Instantiate(point, transform.position, Quaternion.identity);
    }
    public void InitializeState()
    {
        stateManager.ChangeState(PatrolState);
    }
}
