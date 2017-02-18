using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeManager : MonoBehaviour, IPointerDownHandler {

	GameObject item;
	Text num;
	GameObject PoolCanvas;

	void Start () {
		PoolCanvas = GameObject.Find ("PoolCanvas").gameObject;
	}
		
	void Update () {
	
	}

	public void OnPointerDown(PointerEventData eventData) {
		if (Input.GetMouseButtonDown(1)) {
			num = eventData.pointerCurrentRaycast.gameObject.transform.GetChild (0).GetComponent<Text> ();
			if (int.Parse(num.text) > 1) {
				num.text = (int.Parse (num.text) - 1).ToString ();
			} else {
				item = eventData.pointerCurrentRaycast.gameObject;
				StoreItem.DeleteItem (item.transform.parent.name);
//				Destroy (item);
//				ObjectPool.Return(item);
				StartCoroutine(ReturnToPool());
				item.transform.SetParent (PoolCanvas.transform);
			}
		}
	}

	IEnumerator ReturnToPool(){
		yield return new WaitForSeconds (0.0f);
		ObjectPool.Return (item);
	}
}
