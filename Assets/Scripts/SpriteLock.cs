using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLock : MonoBehaviour
{
	void Update()
	{
		transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles - transform.parent.transform.localRotation.eulerAngles);
	}
}
