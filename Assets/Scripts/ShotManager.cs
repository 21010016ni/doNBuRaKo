using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShotManager : MonoBehaviour
{
	private Rigidbody rb;

	private int team;
	private int atk;

	public void Initialize(int team, int atk, float speed, float duration)
	{
		rb = GetComponent<Rigidbody>();
		this.team = team;
		this.atk = atk;
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, duration);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<ShotManager>() == null)
		{
			Debug.Log(other);
			var player = other.GetComponent<PlayerManager>();
			if (player != null)
			{
				player.Damage(atk);
			}
			else
			{
				var base_object = other.GetComponent<PlayerManager>();
				if (base_object != null)
				{
					base_object.Damage(atk);
				}
			}

			if (other.GetComponent<WaterServer>() == null)
			{
				// 飛び散るパーティクル生成

				Destroy(gameObject);
			}
		}
	}
}
