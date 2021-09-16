using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float Power;
    public float SpeedX;
    public float SpeedY;
    public float MaxSpeedX;
    public float MaxSpeedY;
    public float dist;
    public float TimeDeath;
    public float Interval;

    public bool die;
    public GameObject DeathScreen;
    public GameObject control;
    public bool grounded;
    public RaycastHit2D hit;
    public Ray2D ray;
    private GameObject player;
    public Text myText;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    public AvalancheMove avalancheMovement;

    public void Start()
    {
        //CreateInv = GameObject.Find("InvControl").GetComponent<Inventory>();
        //CreateInv.AddGraphics();
        control = GameObject.Find("Control");
        die = false;
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        avalancheMovement = GameObject.FindWithTag("Avalanche").GetComponent<AvalancheMove>();
    }

    private void FixedUpdate()
    {
        grounded = rb.IsTouchingLayers(groundLayer.value);
        
        if (grounded & (rb.velocity.x <= MaxSpeedX))
        {
            Force(new Vector3(1.0f, 0.0f, 0.0f));
        }

        if ((grounded == false) & (rb.velocity.y + 20 <= MaxSpeedX))
        {
            Force(new Vector3(0.0f, -1.0f, 0.0f) * 1.2f);
        }

        if (rb.velocity.x > MaxSpeedX & rb.velocity.y < -MaxSpeedY)
        {
            Force(new Vector3(-1.0f, 0.0f, 0.0f) * 10);
            Force(new Vector3(0.0f, 1.0f, 0.0f) * 10);
        }
        else if (rb.velocity.x > MaxSpeedX & rb.velocity.y > -MaxSpeedY)
        {
            Force(new Vector3(-1.0f, 0.0f, 0.0f) * 10);
        }
        else if (rb.velocity.x < MaxSpeedX & rb.velocity.y < -MaxSpeedY)
        {
            Force(new Vector3(0.0f, 1.0f, 0.0f) * 20);
        }

        SpeedX = rb.velocity.x;
        SpeedY = rb.velocity.y;
        ray = new Ray2D(player.transform.position, -transform.up);
        hit = Physics2D.Raycast(player.transform.position, new Vector2(0, -1), 5000, groundLayer);
        dist = Vector3.Distance(player.transform.position, hit.point);
        myText.text = "Height: " + (int)dist;

        if (dist > 1000)
        {
            TimeDeath += Interval;
        }
        else
            TimeDeath = 0;

        if (TimeDeath >= 50 & die == false)
        {
            die = true;
            TimeDeath = 0;
            avalancheMovement.enabled = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            DeathScreen.SetActive(true);
            control.SetActive(false);
        }

    }

    private void Force(Vector3 movement)
    { 
        rb.AddForce(movement * Power);
    }
}
