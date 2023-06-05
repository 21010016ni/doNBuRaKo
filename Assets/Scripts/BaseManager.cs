using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
	public Guage hp;

	private void Update()
	{
		// この辺でHPを監視して、0になったらゲーム終了メッセージを送る
	}

	public void Damage(int value)
	{
		hp.change(value);
	}
}
