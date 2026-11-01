public struct Message
{
    public Message(ChatRole role, string content)
    {
        Role = role;
        Content = content;
    }

    public ChatRole Role { get; set; }
    public string Content { get; set; }
}
