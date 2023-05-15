using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerControll : MonoBehaviour
{
	public int id_controller = -1;

	private Gamepad gamepad;
	private PlayerManager player;
	private Vector3 pos;

	// Start is called before the first frame update
	void Start()
	{
		if (id_controller != -1 && Gamepad.all.Count > id_controller)
		{
			gamepad = Gamepad.all[id_controller];
		}
		else
		{
			throw new System.Exception("指定したコントローラーが接続されていません");
		}
		player = GetComponent<PlayerManager>();
	}

	// Update is called once per frame
	void Update()
	{
		player.Move(gamepad.leftStick.ReadValue());
		if(gamepad.rightTrigger.isPressed)
		{
			player.Shot();
		}
	}
}
