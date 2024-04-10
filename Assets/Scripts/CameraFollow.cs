using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offSet;
    public float smoothTime;
    public Vector3 minValue, maxValue;
    //
    public PlayerMovement pMoverment;

    [SerializeField] public Transform target;
    // Update is called once per frame
    void Update()
    {
        if (pMoverment.isFacingRight)
        {
            offSet = new Vector3(3f, 2f, 10f);
        }
        else if (!pMoverment.isFacingRight)
        {
            offSet = new Vector3(-3f, 2f, 10f);
        }
        Vector3 targetPositon = target.position + offSet;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPositon.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPositon.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPositon.z, minValue.z, maxValue.z));

        transform.position = Vector3.Lerp(transform.position, boundPosition, smoothTime * Time.deltaTime);
    }
}
