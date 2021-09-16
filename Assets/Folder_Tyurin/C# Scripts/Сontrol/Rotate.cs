using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Rotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private GameObject player;
    public bool click = false;
    public bool grounded = false;
    public float Speed;
    public float JumpForce;
    private float z;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
        player = GameObject.FindWithTag("Player");
        JumpLogic( Vector3.up);
        GetComponent<Graphic>().color = new Color32(0, 255, 0, 70);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        click = false;
        GetComponent<Graphic>().color = new Color32(255, 255, 255, 70);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        click = false;
        GetComponent<Graphic>().color = new Color32(255, 255, 255, 70);
    }

    public void FixedUpdate()
    {
        grounded = GameObject.FindWithTag("Player").GetComponent<Collider2D>().IsTouchingLayers(groundLayer.value);
        if (click & (grounded == false))
        {
            z = Speed * Time.fixedDeltaTime;
            player.transform.Rotate(0, 0, z);
        }
    }

    private void JumpLogic(Vector3 direction)
    {
        if (grounded)
        {
            rb.AddForce(direction * JumpForce * 10);
        }

    }
}