using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;
    public int Tm;
    public float current = 0;

    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        if (Tm < 20)
        {
            Tm += 1;
        }
        else
        {
            current = (1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            display_Text.text = "FPS: " + avgFrameRate.ToString();
            Tm = 0;
        }
        
    }
}