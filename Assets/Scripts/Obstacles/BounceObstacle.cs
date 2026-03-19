using UnityEngine;

public class BounceObstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public float bounceForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                Vector3 bounceDirection = collision.GetContact(0).normal;
                playerRigidbody.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
            }
            Debug.Log("Player hit");
        }
    }
}
