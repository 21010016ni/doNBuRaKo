using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLock : MonoBehaviour
{
	void Update()
	{
		var r = Camera.main.transform.eulerAngles;
		transform.eulerAngles = new Vector3(r.x, r.y, transform.eulerAngles.z);
	}
}
