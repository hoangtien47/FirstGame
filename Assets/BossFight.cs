using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public Transform sspawn1;
    public Transform sspawn2;
    public Transform sspawn3;
    public GameObject[] E1;
    public GameObject E2;
    public GameObject End;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnMob());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator spawnMob()
    {
        yield return new WaitForSeconds(20f);
        int pos1 = Random.RandomRange(0, 3);
        int pos2 = Random.RandomRange(0, 3);
        Instantiate(E1[pos1], sspawn1.position, sspawn1.rotation);
        Instantiate(E1[pos2], sspawn2.position, sspawn2.rotation);
        Instantiate(E2, sspawn3.position, sspawn3.rotation);
        StartCoroutine(spawnMob());
    }
}
