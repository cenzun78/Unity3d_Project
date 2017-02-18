using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMove : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

	GameObject PoolCanvas;
	GameObject OriginalUCell;

	void Start () {
		PoolCanvas = GameObject.Find ("PoolCanvas");
	}

	void Update () {
	
	}

	public void OnBeginDrag(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left) {
			transform.localScale = new Vector2 (0.8f, 0.8f);
			OriginalUCell = transform.parent.gameObject;
			gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			transform.SetParent (PoolCanvas.transform);
		}
	}

	public void OnDrag(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left) {   // 鼠标左键
//			GetComponent<RectTransform>().pivot.Set(0,0);
			transform.position = Input.mousePosition;
		}
	}


	public void OnEndDrag(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left) {
			if (eventData.pointerCurrentRaycast.gameObject.tag != null &&eventData.pointerCurrentRaycast.gameObject.tag == "UCell" && 
				eventData.pointerCurrentRaycast.gameObject.transform.childCount == 0) {
				BaseItem item = StoreItem.GetItem (OriginalUCell.name);
				StoreItem.DeleteItem (OriginalUCell.name);
				StoreItem.Store (eventData.pointerCurrentRaycast.gameObject.name,item);
				transform.SetParent (eventData.pointerCurrentRaycast.gameObject.transform);
			} else if (eventData.pointerCurrentRaycast.gameObject.tag == "UItem") {
				BaseItem originItem = StoreItem.GetItem (OriginalUCell.name);
				BaseItem newItem = StoreItem.GetItem (eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
				StoreItem.DeleteItem (OriginalUCell.name);
				StoreItem.DeleteItem (eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
				StoreItem.Store (OriginalUCell.name,newItem);
				StoreItem.Store (eventData.pointerCurrentRaycast.gameObject.transform.parent.name,originItem);
				transform.SetParent (eventData.pointerCurrentRaycast.gameObject.transform.parent);
				eventData.pointerCurrentRaycast.gameObject.transform.SetParent (OriginalUCell.transform);
				eventData.pointerCurrentRaycast.gameObject.transform.localPosition = Vector3.zero;
			} else {
				transform.SetParent (OriginalUCell.transform);
			}
		}
		transform.localScale = new Vector2 (1f, 1f);
		transform.localPosition = Vector3.zero;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
