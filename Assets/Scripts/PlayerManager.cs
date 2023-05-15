using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerManager : MonoBehaviour
{
	[System.Serializable] public class Guage
	{
		public int max;
		public int now;
	}

	// Start is called before the first frame update
	private GameObject player;

	[SerializeField] private Guage hp;
	[SerializeField] private Guage tank;
	[SerializeField] private int cost;
	[SerializeField] private float cool;
	[SerializeField] private int atk;
	[SerializeField] private float speed;
	[SerializeField] private float speed_decreace;

	private float _cool;

	void Start()
	{
		player = gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		var dt = Time.deltaTime;
		if (_cool > 0)
			_cool -= dt;
	}

	public void Move(Vector2 value)
	{
		var pos = transform.localPosition;
		pos.y += value.y * speed - speed * (tank.now / tank.max) * speed_decreace;
		pos.x += value.x * speed - speed * (tank.now / tank.max) * speed_decreace;
		transform.localPosition = pos;
	}

	public void Shot()
	{
		if (tank.now >= cost && _cool <= 0)
		{
			// �Ȃ񂩎ˏo

			tank.now -= cost;
			_cool = cool;
		}
	}
}
