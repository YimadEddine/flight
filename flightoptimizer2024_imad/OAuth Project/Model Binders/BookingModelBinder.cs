using Microsoft.AspNetCore.Mvc.ModelBinding;
using OAuth_Project.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OAuth_Project.Model_Binders
{
    public class BookingModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
            var jsonBody = await reader.ReadToEndAsync();

            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };


                var BookingModel = JsonSerializer.Deserialize<BookingDTO>(jsonBody, options);

                if (BookingModel == null)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
                }

                var Booking = new Booking
                {
                    FlightId = BookingModel.flightId,
                    Flight = BookingModel.Flight,
                   
                    
                    Reserver = BookingModel.Reserver,
                 
                    Family= BookingModel.Family,
                     
                    ReservedSeats = BookingModel.Seats ?? null
                


            };

                bindingContext.Result = ModelBindingResult.Success(Booking);
            }
            catch (JsonException ex)
            {
                Debug.WriteLine("booking binder:", ex.Message);
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
