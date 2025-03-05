using System.ComponentModel.DataAnnotations;

namespace E_commerce.Helpers
{
    public class ValidationHelper
    {

        public static void Validate<T>(T? model) where T : class
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model) + " can't be null ");

            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, context, results, true))
            {
                throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);
            }

        }


    }
}
