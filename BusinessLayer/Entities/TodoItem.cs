namespace BusinessLayer.Entities
{
    public class TodoItem
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }
        public string Secret { get; private set; }

        public TodoItem(string name, bool isComplete)
        {
            Name = name;
            IsComplete = isComplete;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetStatusComplete(bool isComplete)
        {
            IsComplete = isComplete;
        }
    }
}