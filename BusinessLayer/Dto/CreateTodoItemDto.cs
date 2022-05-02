namespace BusinessLayer.Dto
{
    /// <summary>
    /// Contract for creating a specific TodoItem
    /// </summary>
    public class CreateTodoItemDto
    {
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