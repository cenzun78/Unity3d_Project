using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    private RectTransform canvas;
    private Image img;
    public static float size_x = 0f, size_y = 0f;

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("GameOverCanvas").GetComponent<RectTransform>();
        img = gameObject.GetComponent<Image>();
        size_x = canvas.rect.size.x;
        size_y = canvas.rect.size.y;
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size_x);
        img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size_y);
        print("GameOverScreen");
    }
}
