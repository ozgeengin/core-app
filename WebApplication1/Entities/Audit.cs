using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    public class Audit : BaseEntity
    {
        public string TableName { get; set; }
        public int UserId { get; set; }
        public string ColumnName { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
    }
}
