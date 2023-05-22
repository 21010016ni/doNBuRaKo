using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerManager))]
public class PlayerControll : MonoBehaviour
{
	public int id_controller = -1;
	private bool connect = false;

	private PlayerManager player;
	private Gamepad gamepad;

	// Start is called before the first frame update
	void Start()
	{
		player = GetComponent<PlayerManager>();
		if (id_controller != -1 && Gamepad.all.Count > id_controller)
		{
			gamepad = Gamepad.all[id_controller];
			connect = true;
		}
		else
		{
			throw new System.Exception("指定したコントローラーが接続されていません");
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (id_controller != -1)
		{
			// スティック入力されているならそちらを優先する
			if (connect && gamepad.leftStick.IsActuated())
			{
				player.Move(gamepad.leftStick.value);
				player.SetRotate(gamepad.leftStick.value);
			}
			else
			{
				Vector2 vec = new();
				Key up = new(), down = new(), left = new(), right = new();
				switch (id_controller)
				{
					case 0: up = Key.W; down = Key.S; left = Key.A; right = Key.D; break;
					case 1: up = Key.Y; down = Key.H; left = Key.G; right = Key.J; break;
					case 2: up = Key.P; down = Key.Semicolon; left = Key.L; right = Key.Quote; break;
					case 3: up = Key.UpArrow; down = Key.DownArrow; left = Key.LeftArrow; right = Key.RightArrow; break;
				}
				if (Keyboard.current[up].isPressed)
					vec.y = 1.0f;
				else if (Keyboard.current[down].isPressed)
					vec.y = -1.0f;
				if (Keyboard.current[left].isPressed)
					vec.x = -1.0f;
				else if (Keyboard.current[right].isPressed)
					vec.x = 1.0f;
				if (vec != Vector2.zero)
				{
					player.Move(vec.normalized);
					player.SetRotate(vec);
				}
			}

			if (connect && gamepad.rightStick.IsActuated())
			{
				player.SetRotate(gamepad.rightStick.value);
			}

			if (connect && gamepad.rightTrigger.isPressed)
			{
				player.Shot();
			}
			else
			{
				Key shot = new();
				switch (id_controller)
				{
					case 0: shot = Key.E; break;
					case 1: shot = Key.U; break;
					case 2: shot = Key.LeftBracket; break;
					case 3: shot = Key.PageDown; break;
				}
				if (Keyboard.current[shot].isPressed)
				{
					player.Shot();
				}
			}
		}
	}
}
