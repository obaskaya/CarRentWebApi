using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarRentWebApi.Model
{
	public class CarRent
	{
		public int Id { get; set; }

		public string PlateNumber { get; set; }


		public string CarModel { get; set; }


		public string RenterName { get; set; }


		public string RenterPhone { get; set; }


		public string RenterEmail { get; set; }

		public string RenterAddress { get; set; }
	}
}
