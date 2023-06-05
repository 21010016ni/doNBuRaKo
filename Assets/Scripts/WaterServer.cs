using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterServer : MonoBehaviour
{
	public int team;
	public int power;

	private void OnTriggerStay(Collider other)
	{
		var pl = other.GetComponent<PlayerManager>();
		if (pl != null && pl.team == team)
		{
			pl.WaterServe(power);
		}
	}
}
