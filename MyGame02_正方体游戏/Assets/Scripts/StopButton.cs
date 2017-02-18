using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class StopButton : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{

    private Button stopButton;
    private RectTransform button;
    private Image img;
    public static bool isPointEnter = false;
    public Sprite stop, start;

    void Awake ()
    {
        stopButton = GetComponent<Button>();
        img = GetComponent<Image>();
        img.sprite = stop;
        button = gameObject.GetComponent<RectTransform>();
        button.position = new Vector2(100f, 100f);
        stopButton.onClick.AddListener(OnClick);
    }
	
    void Start()
    {
        stopButton.transform.localPosition = new Vector2((button.rect.size.x/2) - (GameOverScreen.size_x/2), (GameOverScreen.size_y/2) - (button.rect.size.y/2));
    }
	void OnClick()
    {
        if (!StopGame.pause)
        {
            StopGame.pause = true;
            img.sprite = start;
        } else
        {
            StopGame.pause = false;
            img.sprite = stop;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointEnter = false;
    }
}
