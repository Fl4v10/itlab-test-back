using System;
using testeItLab.domain.Models.Enums;

namespace testeItLab.domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public EProductType Type { get; set; }
        public bool Sex { get; set; }
        public DateTime RegisterAt { get; set; }

        internal void UpdateData(Product updateEntity)
        {
            Name = updateEntity.Name;
            Value = updateEntity.Value;
            Type = updateEntity.Type;
            Sex = updateEntity.Sex;
        }
    }
}
