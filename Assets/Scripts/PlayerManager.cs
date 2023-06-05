using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
	private Rigidbody rb;
	private static GameObject shot = null;

	public int team;

	[SerializeField] private Sprite body_up;
	[SerializeField] private Sprite body_side;
	//[SerializeField] private Sprite body_right;
	[SerializeField] private Sprite body_down;
	//[SerializeField] private Sprite body_left;
	[SerializeField] private Sprite arm_up;
	[SerializeField] private Sprite arm_side;
	//[SerializeField] private Sprite arm_right;
	[SerializeField] private Sprite arm_down;
	//[SerializeField] private Sprite arm_left;

	[SerializeField] private Guage hp;  // HP
	[SerializeField] private Guage tank;    // êÖÉ^ÉìÉN
	[SerializeField] private int cost;  // éÀåÇè¡îÔó 
	[SerializeField] private float cool;    // éÀåÇä‘äu
	[SerializeField] private int atk;   // çUåÇóÕ
	[SerializeField] private float shot_speed;  // íeë¨
	[SerializeField] private float speed;   // à⁄ìÆë¨ìx
	[SerializeField] private float speed_decreace;  // à⁄ìÆë¨ìxå∏êäó¶
	[SerializeField] private float gun_length = 2.0f;	// èeêgí∑Ç≥

	private float _cool = 0.0f;
	private float _invincible = 0.0f;

	private SpriteRenderer _body;
	private SpriteRenderer _arm;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (shot == null)
		{
			shot = (GameObject)Resources.Load("Shot");
		}
		_body = transform.GetChild(0).GetComponent<SpriteRenderer>();
		_arm = transform.GetChild(1).GetComponent<SpriteRenderer>();
		SetRotate(Vector2.down);
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
		var dt = Time.deltaTime;
		if (_cool > 0.0f)
			_cool -= dt;
		if (_invincible > 0.0f)
			_invincible -= dt;
	}

	public void Move(Vector2 value)
	{
		var vec = new Vector3(value.x, 0, value.y) * speed;
		vec.x *= 1.0f - speed_decreace * tank.now / tank.max;
		vec.y = rb.velocity.y;
		vec.z *= 1.0f - speed_decreace * tank.now / tank.max;
		rb.velocity = vec;
	}

	public void SetRotate(Vector2 value)
	{
		var vec = new Vector3(value.x, 0, value.y);
		transform.rotation = Quaternion.LookRotation(vec);
		float r = (transform.eulerAngles.y + 45.0f) % 360.0f;
		Debug.Log(transform.eulerAngles.y);
		if (r < 90.0f)  // up
		{
			_body.sprite = body_up;
			_arm.sprite = arm_up;
			_body.flipX = false;
			_arm.flipX = false;
			_body.transform.SetAsFirstSibling();
			_arm.transform.eulerAngles.Set(_arm.transform.eulerAngles.x, _arm.transform.eulerAngles.y, r - 90.0f - 30.0f);
		}
		else if (r < 180.0f)    // right
		{
			_body.sprite = body_side;
			_arm.sprite = arm_side;
			_body.flipX = false;
			_arm.flipX = false;
			_body.transform.SetAsFirstSibling();
			_arm.transform.eulerAngles.Set(_arm.transform.eulerAngles.x, _arm.transform.eulerAngles.y, r - 90.0f - 30.0f);
		}
		else if (r < 270.0f)    // down
		{
			_body.sprite = body_down;
			_arm.sprite = arm_down;
			_body.flipX = false;
			_arm.flipX = false;
			_arm.transform.SetAsFirstSibling();
			_arm.transform.eulerAngles.Set(_arm.transform.eulerAngles.x, _arm.transform.eulerAngles.y, r - 180.0f + 45.0f);
		}
		else    // left
		{
			_body.sprite = body_side;
			_arm.sprite = arm_side;
			_body.flipX = true;
			_arm.flipX = true;
			_body.transform.SetAsFirstSibling();
			_arm.transform.eulerAngles.Set(_arm.transform.eulerAngles.x, _arm.transform.eulerAngles.y, -r + 90.0f + 30.0f);
		}
	}

	public void Shot()
	{
		if (tank.now >= cost && _cool <= 0)
		{
			Instantiate(shot, transform.localPosition + transform.forward * gun_length + UnityEngine.Random.insideUnitSphere * 0.2f, transform.rotation)
				.GetComponent<ShotManager>()
				.Initialize(team, atk, shot_speed, 5.0f);

			tank.change(-cost);
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

	public void WaterServe(int value)
	{
		tank.change(value);
	}
}
