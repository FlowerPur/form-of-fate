using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class MessageItem : MonoBehaviour
{
    [SerializeField] private Sprite _userSprite;
    [SerializeField] private Sprite _assistantSprite;

    [SerializeField] private TextMeshProUGUI _contentText;

    private Image _image;

    public Message Message { get; set; }

    private void Awake() => _image = GetComponent<Image>();

    private void Start()
    {
        _contentText.text = CommandHandler.RemoveTextInBraces(Message.Content);

        if (Message.Role == ChatRole.User)
            _image.sprite = _userSprite;
        else if (Message.Role == ChatRole.Assistant)
            _image.sprite = _assistantSprite;
    }
}
