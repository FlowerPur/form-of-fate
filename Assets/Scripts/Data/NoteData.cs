public struct NoteData : ISlotData
{
    public NoteData(string name, string content)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; set; }
    public string Content { get; set; }
}