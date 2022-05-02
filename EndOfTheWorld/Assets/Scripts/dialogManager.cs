using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogManager : MonoBehaviour
{
    #region Singleton
    public static dialogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
       
    public Image avatar;
    public TextMeshProUGUI messageText;
    public RectTransform dialogueBox;
    public static bool isActive;

    private Message[] _currentMessages;
    private int _activeMessage = 0;
    private bool _isTyping = false;

    public void OpenDialogue(Message[] messages)
    {
        _currentMessages = messages;
        _activeMessage = 0;

        isActive = true;

        Debug.Log("Started dialogue");
        dialogueBox.transform.localScale = Vector3.one;
        StartCoroutine(PlayText());
    }

    private IEnumerator PlayText()
	{
        _isTyping = true;
        messageText.text = "";

        Message _messageToDisplay = _currentMessages[_activeMessage];
        Message _avatarToDisplay = _currentMessages[_activeMessage];

        avatar.sprite = _avatarToDisplay.avatar;

        foreach (char i in _messageToDisplay.message)
		{
            messageText.text += i;
			audioManager.Instance.Play("Typing");
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
        _isTyping = false;
    }


    private void NextMessage()
    {
        _activeMessage++;
        if (_activeMessage < _currentMessages.Length)
        {
            StartCoroutine(PlayText());
        }
        else
        {
            dialogueBox.transform.localScale = Vector3.zero;
            isActive = false;
            Debug.Log("Finished dialogue");
        }
    }

    private void Update()
    {
        if (!_isTyping && isActive && Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage();
        }
    }
}
