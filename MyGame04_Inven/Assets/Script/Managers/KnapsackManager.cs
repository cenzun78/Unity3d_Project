using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KnapsackManager : MonoBehaviour {

	public static Dictionary<int, BaseItem> ItemList;
	GameObject item;
	public GameObject[] UCells;
	Image imageSingle;
	bool isNothing;
	public GameObject PoolCanvas;
	static float time = 0f;

	void Awake() {
		Load ();

	}

	void Update () {
		time += Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.F) && time > 0.5f) {
			int index = Random.Range (0,5);
			Pickup (ItemList[index]);
			time = 0f;
		}
	}

	private void Load() {
		ItemList = new Dictionary<int,BaseItem> ();
		Weapons sword_blade = new Weapons (0,"刀剑","锋利的刀剑",20,10,"Pictures/blade_sword",100);
		Weapons sword_blood = new Weapons (1,"嗜血剑","杀戮之剑",25,12,"Pictures/blood_sword",150);
		Weapons sword_double = new Weapons (2,"双手剑","多为女子用剑",30,15,"Pictures/double_sword",200);

		Consumables HP_back = new Consumables (3,"培元术","回复气血",20,10,"Pictures/add_HP",20,0);
		Consumables MP_back = new Consumables (4,"无忧仙丹","恢复能量",20,10,"Pictures/add_MP",0,20);

		ItemList.Add (sword_blade.ID, sword_blade);
		ItemList.Add (sword_blood.ID, sword_blood);
		ItemList.Add (sword_double.ID, sword_double);
		ItemList.Add (HP_back.ID, HP_back);
		ItemList.Add (MP_back.ID, MP_back);

	}

	public void Pickup(BaseItem baseItem) {
		isNothing = false;
		item = ObjectPool.Get ("UItem",transform.position, transform.rotation) as GameObject;
//		item = Instantiate (Resources.Load("Prefabs/UItem"), transform.position, transform.rotation)as GameObject;
		imageSingle = item.GetComponent<Image> ();
		imageSingle.overrideSprite = Resources.Load (baseItem.Icon, typeof(Sprite)) as Sprite;

		for (int i = 0; i < UCells.Length; i++) {
			if (UCells [i].transform.childCount > 0) {
				if (UCells [i].transform.GetChild (0).GetComponent<Image> ().overrideSprite == imageSingle.overrideSprite) {
					isNothing = true;
					string num = UCells [i].transform.GetChild (0).GetChild (0).GetComponent<Text> ().text;
					UCells [i].transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = (int.Parse (num) + 1).ToString ();
//					Destroy (item);
//					ObjectPool.Return(item);
					StartCoroutine (ReturnToPool ());
					item.transform.SetParent (PoolCanvas.transform);
					break;
				}
			}
		}

		if (!isNothing) {
			for (int i = 0; i < UCells.Length; i++) {
				if (UCells[i].transform.childCount == 0) {
					StoreItem.Store (UCells[i].name, baseItem);
					item.transform.SetParent (UCells[i].transform);
					item.transform.localPosition = Vector3.zero;
					break;
				}
			}
		}
	}

	// 协成
	IEnumerator ReturnToPool(){
		yield return new WaitForSeconds (0.0f);   // 延迟时间
		ObjectPool.Return (item);
	}
}
