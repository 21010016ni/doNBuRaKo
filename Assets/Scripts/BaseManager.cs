using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
	public Guage hp;

	private void Update()
	{
		// ���̕ӂ�HP���Ď����āA0�ɂȂ�����Q�[���I�����b�Z�[�W�𑗂�
	}

	public void Damage(int value)
	{
		hp.change(value);
	}
}
