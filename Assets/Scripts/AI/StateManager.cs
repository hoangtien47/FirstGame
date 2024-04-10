using System.Collections;
using UnityEngine;


public interface IState
{
    void EnterState();
    void ExitState();
    IState UpdateState();

}

public class StateManager : MonoBehaviour
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.ExitState();

        currentState = newState;
        currentState.EnterState();
    }



    private void Update()
    {
        if (currentState != null)
        {
            IState nextState = currentState.UpdateState();
            if (nextState != currentState)
            {
                ChangeState(nextState);
            }
        }
    }
}
