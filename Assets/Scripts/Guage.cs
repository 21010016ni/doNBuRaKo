using UnityEngine;

[System.Serializable]
public class Guage
{
	public int max;
	public int now;

	public void change(int value) { now = Mathf.Clamp(now + value, 0, max); }
	public float par() { return (float)now / (float)max; }
}
