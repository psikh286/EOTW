using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desicion : MonoBehaviour
{
	private bool _started = false;

	private void Start()
	{
		StartCoroutine(Activate());
	}

	private IEnumerator Activate()
	{
		yield return null;
		yield return new WaitUntil(() => dialogManager.isActive == false);
		GetComponent<RectTransform>().localScale = Vector3.one;
		_started = true;
	}

	private void Update()
	{
		if (!_started)
			return;

		if (Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("aaaa");
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			Debug.Log("ddd");
		}

	}
}
