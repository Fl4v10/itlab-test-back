using System;
using testeItLab.domain.Models.Enums;

namespace testeItLab.domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public bool TargetGender { get; set; }
        public EProductType Type { get; set; }

        public DateTime RegisterAt { get; set; }
    }
}
