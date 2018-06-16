using System;

namespace WebApplication1.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
