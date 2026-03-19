using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObstacle : MonoBehaviour
{
    [Header("Spawn Point")]
    public Transform spawnPoint;

    private Coroutine RespawnCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        {
            if (RespawnCoroutine != null)
            {
                StopCoroutine(RespawnCoroutine);
            } else
            {
                RespawnCoroutine = StartCoroutine(RespawnPlayer(collision));
                StartCoroutine(RespawnPlayer(collision));
            }
        }
    }

    IEnumerator RespawnPlayer(Collision collision)
    {
        yield return new WaitForSeconds(1f);
        collision.gameObject.transform.position = spawnPoint.position;
    }
}
