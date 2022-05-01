namespace TodoApi.Models
{
    /// <summary>
    /// TodoItem
    /// </summary>
    public class TodoItemDto
    {
        /// <summary>
        /// ID TodoItem
        /// </summary>
        /// <example>1</example>
        public long Id { get; set; }

        /// <summary>
        /// Name TodoItem
        /// </summary>
        /// <example>My day</example>
        public string Name { get; set; }

        /// <summary>
        /// Status TodoItem
        /// </summary>
        /// <example>false</example>
        public bool IsComplete { get; set; }
    }
}