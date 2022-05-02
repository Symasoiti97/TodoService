namespace BusinessLayer.Dto
{
    /// <summary>
    /// Contract for updating a specific TodoItem
    /// </summary>
    public class UpdateTodoItemDto
    {
        /// <summary>
        /// Name TodoItem
        /// </summary>
        /// <example>My day 2</example>
        public string Name { get; set; }

        /// <summary>
        /// Status TodoItem
        /// </summary>
        /// <example>false</example>
        public bool IsComplete { get; set; }
    }
}