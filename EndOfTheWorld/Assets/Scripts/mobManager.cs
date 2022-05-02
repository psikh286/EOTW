using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class mobManager : MonoBehaviour
{
    public Mob[] mobs;

	[SerializeField] private Image image;
	[SerializeField] private GameObject planet;
	[SerializeField] private GameObject background;
	[SerializeField] private GameObject[] prefab;
	[SerializeField] private dialogTrigger win;
	[SerializeField] private dialogTrigger lose;
	[SerializeField] private int rounds;
	[SerializeField] private int index;

	private int score = 0;
	private int roundPassed = 0;
	private bool _started = false;
	private Mob _currentMob;

	private void Start()
	{
		StartCoroutine(Activate());
	}

	private IEnumerator Activate()
	{
		yield return null;
		yield return new WaitUntil(() => dialogManager.isActive == false);
		GetComponent<RectTransform>().localScale = Vector3.one;

		_currentMob = mobs[Random.Range(0, mobs.Length)];
		image.sprite = _currentMob.sprite;
		_started = true;
	}

	private IEnumerator Switch(bool _exp)
	{
		if (_exp)
		{
			planet.transform.localScale = Vector3.zero;
			audioManager.Instance.Play("Explosion");
			GameObject _explosion = Instantiate(prefab[Random.Range(0, 2)], planet.transform.position, Quaternion.identity);
			var anim = _explosion.GetComponent<Animator>();
			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
			Destroy(_explosion);
			planet.transform.localScale = Vector3.one;
		}

		roundPassed++;
		Count();
		planet.GetComponent<planetPicker>().Start();
		background.GetComponent<backgroundPicker>().Start();
		_currentMob = mobs[Random.Range(0, mobs.Length)];
		image.sprite = _currentMob.sprite;
		yield return null;
		_started = true;
	}

	private void Update()
	{
		if (!_started)
			return;

		if (Input.GetKeyDown(KeyCode.A))
		{
			if (_currentMob.bad)
			{
				score++;
			}
			else {audioManager.Instance.Play("Wrong");}
			StartCoroutine(Switch(true));
			_started = false;			
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			if (!_currentMob.bad)
			{
				score++;
			}
			else { audioManager.Instance.Play("Wrong"); }
			StartCoroutine(Switch(false));
			_started = false;
		}
	}

	private void Count()
	{
		if (roundPassed >= rounds)
		{
			StopAllCoroutines();
			_started = false;
			GetComponent<RectTransform>().localScale = Vector3.zero;

			if (score / rounds >= .8)
			{
				win.StartDialogue();
				StartCoroutine(End());
			}
			else
			{
				lose.StartDialogue();
				StartCoroutine(End());
			}
		}
	}

	private IEnumerator End()
	{
		yield return null;
		yield return new WaitUntil(() => dialogManager.isActive == false);		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);		
	}
}
