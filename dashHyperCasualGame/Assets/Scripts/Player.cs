using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 15f;
    public float timeSlowness = 10f;
    public float mapWidthX = 5f;
    public float mapWidthY = 8f;
    public float dashForce = .5f;
    private float dashDuration ;
    public float dashStartTime = 0.3f;
    private Vector2 change;
    private Vector2 newPosition;

    private Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    private void Dash()
    {
        while (dashStartTime <= dashDuration)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode2D.Impulse);
            //yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector2.zero;
            dashStartTime += Time.deltaTime;
        }
        dashStartTime = 0f;
    }*/

    void FixedUpdate ()
	{
        //GetInput();
        MoveCharacter();

        /*float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.fixedDeltaTime * speed;
        

        Vector2 newPosition = rb.position + (Vector2.up * y + Vector2.right * x);
        //Vector2 newPosition = rb.position + Vector2.right * x;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidthX, mapWidthX);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, mapWidthY);
        rb.MovePosition(newPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(newPosition);
        }*/

    }

    void GetInput()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        change.x = Mathf.Clamp(change.x, -mapWidthX, mapWidthX);
        change.y = Mathf.Clamp(change.y, 0, mapWidthY);

        //send player back to base on "k"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashCo());
          //  Dash(change);

        }
    }

    void MoveCharacter()
    {

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.fixedDeltaTime * speed;


        newPosition = rb.position + (Vector2.up * y + Vector2.right * x);
        //Vector2 newPosition = rb.position + Vector2.right * x;

        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidthX, mapWidthX);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, mapWidthY);
        rb.MovePosition(newPosition);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashCo());
            //  Dash(change);

        }
        //change.Normalize();
        //rb.MovePosition(rb.position + change * speed * Time.deltaTime);
        // if (Input.GetButton("dash"))// && (dashTime != 0))
        // {        
        //      HandleDash();
        // } 

    }

    private IEnumerator DashCo()
    {
        
        change.Normalize();
        //myRigidbody.AddForce(change * dashSpeedMultiplier);
        rb.velocity = newPosition * dashForce * speed;
        //Debug.Log(myRigidbody.velocity);
        //change = Vector3.zero;
        yield return null;
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector2.zero;
    }

    private void Dash(Vector2 dir)
    {
        dir.Normalize();
        if (dashDuration <=0)
        {
            dashDuration = dashStartTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            dashDuration -= Time.deltaTime;
            rb.AddForce(dir, ForceMode2D.Force);
           // rb.MovePosition(dir * dashForce);
            //rb.velocity = rb.position * dashForce;
        }
    }

    void OnCollisionEnter2D ()
	{

        FindObjectOfType<GameManager>().EndGame();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("blocks"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
        else if (collision.gameObject.CompareTag("timeSlow"))
        {
            StartCoroutine(Slow());
           // FindObjectOfType<GameManager>().TimeSlow();
        }
    }

    IEnumerator Slow()
    {
        Time.timeScale = 1f / timeSlowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / timeSlowness;

        yield return new WaitForSeconds(1f / timeSlowness);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * timeSlowness;
    }
}
