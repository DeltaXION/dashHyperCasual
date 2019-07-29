using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCoin : MonoBehaviour
{
    // Start is called before the first frame update
    public float coinFallRate;

    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale += Time.timeSinceLevelLoad / coinFallRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2f)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
