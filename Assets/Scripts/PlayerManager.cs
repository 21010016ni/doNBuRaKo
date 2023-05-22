using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
	[System.Serializable]
	public class Guage
	{
		public int max;
		public int now;

		public void change(int value)
		{
			now += value;
			now = Mathf.Clamp(now, 0, max);
		}
	}

	private Rigidbody rb;
	private static GameObject shot = null;

	public int team;
	[SerializeField] private Guage hp;  // HP
	[SerializeField] private Guage tank;    // …ƒ^ƒ“ƒN
	[SerializeField] private int cost;  // ËŒ‚Á”ï—Ê
	[SerializeField] private float cool;    // ËŒ‚ŠÔŠu
	[SerializeField] private int atk;   // UŒ‚—Í
	[SerializeField] private float shot_speed;  // ’e‘¬
	[SerializeField] private float speed;   // ˆÚ“®‘¬“x
	[SerializeField] private float speed_decreace;  // ˆÚ“®‘¬“xŒ¸Š—¦

	public float gun_length = 2.0f;

	private float _cool = 0.0f;
	private float _invincible = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (shot == null)
		{
			shot = (GameObject)Resources.Load("Shot");
		}
	}

	// Update is called once per frame
	void Update()
	{
		var dt = Time.deltaTime;
		if (_cool > 0.0f)
			_cool -= dt;
		if (_invincible > 0.0f)
			_invincible -= dt;
	}

	public void Move(Vector2 value)
	{
		var vec = new Vector3(value.x, 0, value.y) * speed;
		// 0:1 ~ 1:dec(0~1)
		vec.x *= 1.0f - speed_decreace * tank.now / tank.max;
		vec.y = rb.velocity.y;
		vec.z *= 1.0f - speed_decreace * tank.now / tank.max;
		rb.velocity = vec;
	}

	public void SetRotate(Vector2 value)
	{
		var vec = new Vector3(value.x, 0, value.y);
		transform.rotation = Quaternion.LookRotation(vec);
	}

	public void Shot()
	{
		//if (tank.now >= cost && _cool <= 0)
		{
			//Debug.Log("ËŒ‚");
			Instantiate(shot, transform.localPosition + transform.forward * gun_length + Random.insideUnitSphere * 0.2f, transform.rotation)
				.GetComponent<ShotManager>()
				.Initialize(team, atk, shot_speed, 5.0f);

			//tank.change(-cost);
			_cool = cool;
		}
	}

	public void Damage(int value)
	{
		if (_invincible <= 0)
		{
			hp.change(-value);
			_invincible = 0.1f;
		}
	}
}
