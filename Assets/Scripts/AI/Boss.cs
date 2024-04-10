using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject startZone;
    public GameObject endZone;
    void Start()
    {
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
    }

    public void OnPlayerEnterTrigger()
    {
        startZone.active = true;
        endZone.active = true;
        stateManager.ChangeState(SpawnState);
    }

}
