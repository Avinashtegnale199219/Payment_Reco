using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FD_PaymentReconciliation_V2.App_Code.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in context.ModelState)
                {
                    foreach (var err in state.Value.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                context.Result = new OkObjectResult(new { status = 0, result = "FAILED" , errors= errors });
                return;
            }
        }
    }

    public sealed class CheckYorN : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "Y" || value.ToString() == "N")
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid value passed, possible values Y/N");
            }
        }
    }

    public sealed class CheckDateFormat : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = new DateTime();
            var isValid = DateTime.TryParseExact(value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in valid format,");
            }
        }
    }
}
