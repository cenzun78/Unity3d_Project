using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Text ItemInfo;

	void Start () {
		ItemInfo = GameObject.Find ("Info").GetComponent<Text> ();
	}

	void Update () {

	}

	public void OnPointerEnter(PointerEventData eventData) {
		BaseItem item = StoreItem.GetItem (eventData.pointerEnter.transform.parent.name);
		ItemInfo.text = item.Description;
	}

	public void OnPointerExit(PointerEventData eventData) {

	}
}
