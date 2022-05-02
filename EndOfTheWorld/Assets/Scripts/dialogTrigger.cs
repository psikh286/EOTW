using UnityEngine;

public class dialogTrigger : MonoBehaviour
{
    public Message[] messages;

    [SerializeField] private bool play;

	private void Start()
	{
        if (!play)
            return;
        StartDialogue();
	}

	public void StartDialogue()
    {
        dialogManager.Instance.OpenDialogue(messages);
    }
}

[System.Serializable]
public class Message
{
    public string message;
    public Sprite avatar;
}