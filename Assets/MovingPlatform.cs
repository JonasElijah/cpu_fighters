using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed = 1.0f;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(start.position, end.position, Mathf.PingPong(fractionOfJourney, 1));
    }

    private IEnumerator SetParentNextFrame(Transform child, Transform newParent)
    {
        yield return null;
        child.SetParent(newParent);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if ((collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo")) && gameObject.activeInHierarchy)
    //     {
    //         StartCoroutine(SetParentNextFrame(collision.transform, transform));
    //     }
    // }


    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo") && gameObject.activeInHierarchy)
    //     {
    //         StartCoroutine(DelayedSetParent(collision.transform, null));
    //     }
    // }

    IEnumerator DelayedSetParent(Transform child, Transform newParent)
    {
        yield return null; 
        child.SetParent(newParent);
    }


   
}
