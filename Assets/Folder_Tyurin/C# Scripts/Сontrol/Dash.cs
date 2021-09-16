using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dash : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    public bool click;
    public Rigidbody2D rb;
    public float Power;
    public float Reload;
    public float Timer;
    public GameObject Panel;
    public Vector2 DotDirection;
    public Vector2 PosPlayer;

    public void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        Panel = GameObject.Find("Dash");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        click = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        click = false;
    }

    public void FixedUpdate()
    {
        PosPlayer = player.transform.position;
        DotDirection = GameObject.Find("Direction").transform.position;
        Vector2 Direction = DotDirection - PosPlayer;
        if (click & (Timer >= Reload))
        {
            Dsh(Direction);
            Timer = 0;
        }

        if (Timer > Reload)
        {
            Timer = Reload;
            Panel.GetComponent<Graphic>().color = new Color32(255, 255, 255, 70);
        }
        else if (Timer < Reload)
        {
            Timer += 0.005f;
            Panel.GetComponent<Graphic>().color = new Color32(0, 0, 0, 50);
        }
    }

    public void Dsh(Vector2 movement)
    {
        rb.AddForce(movement * Power);
        click = false;
    }
}
