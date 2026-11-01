using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class HistoryMenuPanel : MenuPanel
{
    private readonly List<MessageItem> _messageItems = new();

    [SerializeField] private Transform _content;

    [SerializeField] private MessageItem _messageItemPrefab;

    public void UpdateMessageItems(List<Message> messages)
    {
        ClearMessageItems();
        CreateMessageItems(messages);
    }

    private void ClearMessageItems()
    {
        foreach (MessageItem item in _messageItems)
            Destroy(item.gameObject);

        _messageItems.Clear();
    }

    private void CreateMessageItems(List<Message> messages)
    {
        IEnumerable<Message> recentMessages = messages
            .AsEnumerable()
            .Reverse()
            .Where(m => m.Role != ChatRole.System)
            .Take(50);

        int index = 1;

        foreach (Message message in recentMessages)
        {
            MessageItem item = Instantiate(_messageItemPrefab, _content);
            item.transform.SetSiblingIndex(index++);
            item.Message = message;

            _messageItems.Add(item);
        }
    }
}
