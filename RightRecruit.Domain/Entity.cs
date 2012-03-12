using System;

namespace RightRecruit.Domain
{
    public class Entity : INamedDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedUserId { get; set; }
    }
}