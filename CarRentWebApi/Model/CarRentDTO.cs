using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarRentWebApi.Model
{
	public class CarRentDTO
	{
		[ValidateNever]
		public int Id { get; set; }

		[Required(ErrorMessage = "Plate Number  is Required")]
		[StringLength(30)]
		public string PlateNumber { get; set; }

		[Required(ErrorMessage = "Car Model is Required")]
		[StringLength(30)]
		public string CarModel { get; set; }

		[Required(ErrorMessage = "Renter name is Required")]
		[StringLength(30)]
		public string RenterName { get; set; }

		[Required(ErrorMessage ="Phone is Required")]
		[Phone]
		public string RenterPhone { get; set;}

		[EmailAddress(ErrorMessage = " Please enter valid Email address")]
		public string RenterEmail { get; set;}
		[Required(ErrorMessage = "Address is Required")]
		[StringLength(200)]
		public string RenterAddress { get; set;}

	}
}
