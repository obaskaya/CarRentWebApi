namespace CarRentWebApi.Model
{
	public class CarRentRepository
	{
		public static List<CarRent> Rents { get; set; } = new List<CarRent>()
		{
			new CarRent
			{
				Id = 1,
				PlateNumber= "34ef2344",
				CarModel="audi",
				RenterName = "Oğuzhan Başkaya",
				RenterPhone = "555500",
				RenterEmail= "sample@email.com",
				RenterAddress="sampleaddress"

			},
			new CarRent
			{
				 Id= 2,
				 PlateNumber="34yha44",
				 CarModel="bmw",
				 RenterName="Kübra",
				 RenterPhone="444444",
				 RenterEmail="email@fdfd.com",
				 RenterAddress="adress"
			}

		};
	}
}
