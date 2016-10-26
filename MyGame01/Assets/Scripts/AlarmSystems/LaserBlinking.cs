using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

	public float onTime;				//开关时的时间
	public float offTime;				//关闭时的时间

	private float timer;             	  // 计时器
	Renderer rd;
	Light l;

	void Awake () {
		rd = gameObject.GetComponent<Renderer> ();
		l = gameObject.GetComponent<Light> ();
	}

	void Update () {
		timer += Time.deltaTime;

		if (rd.enabled && timer >= onTime) {
			SwitchBeam ();
		}

		if (!rd.enabled && timer >= onTime) {
			SwitchBeam ();
		}
	}

	void SwitchBeam () {
		timer = 0f;

		rd.enabled = !rd.enabled;
		l.enabled = !l.enabled;
	}
}
