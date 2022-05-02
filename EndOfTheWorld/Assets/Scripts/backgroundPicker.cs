using UnityEngine;
using UnityEngine.UI;

public class backgroundPicker : MonoBehaviour
{
	[SerializeField] private Sprite[] bg;

	public void Start()
	{
		int i = Random.Range(0, bg.Length);
		GetComponent<SpriteRenderer>().sprite = bg[i];
	}
}
