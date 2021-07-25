using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BulgarianProducers.Models.Events
{
    public class AddAgriculturalEventFormModel:IValidatableObject
    {
 
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Place { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(20)]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Url]
        public string Image1 { get; set; }
        [Url]
        public string Image2 { get; set; }
        [Url]
        public string Image3 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var startDate = new DateTime();
            var endDate = new DateTime();

            var isValidStartDate = DateTime
                .TryParseExact(StartDate,
                "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDate);
            var isValidEndDate = DateTime
                .TryParseExact(EndDate,
                "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out endDate);

            if (!isValidStartDate) 
            {
                yield return new ValidationResult("Невалидна стартова дата, моля въведете датата в следния формат 'dd/MM/yyyy'! ");
            }
            if (!isValidEndDate) 
            {
                yield return new ValidationResult("Невалидна стартова дата, моля въведете датата в следния формат 'dd/MM/yyyy'! ");
            }
            if (isValidStartDate && isValidEndDate) 
            {
                if (startDate > endDate) 
                {
                    yield return new ValidationResult("Не е валидно началната дата да е по - късно от крайната!");
                }
            }
        }
    }
}
