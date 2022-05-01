using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgroundPicker : MonoBehaviour
{
	[SerializeField] private Sprite[] bg;
	[SerializeField] private Image _currentBg;

	private void Start()
	{
		int i = Random.Range(0, bg.Length);
		_currentBg.sprite = bg[i];
	}
}
