//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static Enemy;
//using static UnityEngine.EventSystems.EventTrigger;

//public class IdleState : IState
//{

//    public void EnterState(Enemy enemy)
//    {
//        enemy.animator.SetInteger("state", (int)MovementState.idle);

//        // Enter Patrol state behavior

//    }

//    public void ExitState(Enemy enemy)
//    {
//        Debug.Log("incor");
//        enemy.StartCoroutine(WaitBeforeTransition());

//        // Exit Patrol state behavior
//    }

//    public IState InputState(Enemy enemy)
//    {
//        return this;
//    }

//    public IState UpdateState(Enemy enemy)
//    {
//            return enemy.PatrolState;
//    }
//    private IEnumerator WaitBeforeTransition()
//    {
//        // Wait for the animation to finish
//        yield return new WaitForSeconds(2.0f);

//    }
//}

