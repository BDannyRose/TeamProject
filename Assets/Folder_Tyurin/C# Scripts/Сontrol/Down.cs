using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Down : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool click = false;
    public bool grounded = false;
    public float DownForce;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
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

    void Update()
    {
        grounded = GameObject.FindWithTag("Player").GetComponent<Collider2D>().IsTouchingLayers(groundLayer.value);
        if ((grounded == false) & click)
        {
            rb.AddForce(Vector3.down * DownForce);
        }
    }
}
