using System.Collections.Generic;
using UnityEngine;

public class planetPicker : MonoBehaviour
{
	[SerializeField] private RuntimeAnimatorController[] anim;
	[SerializeField] private Animator _currentAnim;

	private void Start()
	{
		int i = Random.Range(0, anim.Length);
		_currentAnim.runtimeAnimatorController = anim[i];
	}
}
