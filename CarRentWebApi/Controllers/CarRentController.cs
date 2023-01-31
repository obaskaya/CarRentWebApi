using CarRentWebApi.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CarRentWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarRentController : ControllerBase
	{
		//get all

		[HttpGet]
		[Route("All", Name = "GetAllRents ")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]


		public ActionResult<IEnumerable<CarRentDTO>> GetRents()
		{
			var carrent = CarRentRepository.Rents.Select(s => new CarRentDTO()
			{
				Id = s.Id,
				PlateNumber = s.PlateNumber,
				CarModel = s.CarModel,
				RenterName = s.RenterName,
				RenterPhone = s.RenterPhone,
				RenterEmail = s.RenterEmail,
				RenterAddress = s.RenterAddress

			});

			//Ok 200
			return Ok(carrent);
		}

		//get by id

		[HttpGet]
		[Route("{id:int}", Name = "GetRentById ")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<CarRentDTO> GetRentById(int id)
		{
			//400 Client error
			if (id <= 0)
				return BadRequest("Please enter greater number than 0");

			var carrent = CarRentRepository.Rents.Where(n => n.Id == id).FirstOrDefault();



			// 404 error
			if (carrent == null)
			{
				return NotFound($"The car rent with id '{id}' is not found");
			}
			var carrentDTO = new CarRentDTO()
			{
				Id = carrent.Id,
				PlateNumber = carrent.PlateNumber,
				CarModel = carrent.CarModel,
				RenterName = carrent.RenterName,
				RenterPhone = carrent.RenterPhone,
				RenterEmail = carrent.RenterEmail,
				RenterAddress = carrent.RenterAddress
			};

			//Ok 200
			return Ok(carrentDTO);


		}

		//get by name

		[HttpGet("{model:alpha}", Name = "GetRentByModel ")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<CarRentDTO> GetRentByName(string model)
		{
			//400 Client error
			if (model == null)
				return BadRequest("Please enter greater number than 0");

				var carrent = CarRentRepository.Rents.Where(n => n.CarModel == model).FirstOrDefault();

			// 404 error
			if (carrent == null)
				return NotFound($"The car rent with plate number '{carrent}' is not found");

			else
			{

				
				var carrentDTO = new CarRentDTO()
				{

					Id = carrent.Id,
					PlateNumber = carrent.PlateNumber,
					CarModel = carrent.CarModel,
					RenterName = carrent.RenterName,
					RenterPhone = carrent.RenterPhone,
					RenterEmail = carrent.RenterEmail,
					RenterAddress = carrent.RenterAddress
				};
				//Ok 200
				return Ok(carrentDTO);
			}
		}
		//create
		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public ActionResult<CarRentDTO> CreateRent([FromBody] CarRentDTO model)
		{
			if (model == null)
			{
				return BadRequest();
			}

			int newId = CarRentRepository.Rents.LastOrDefault().Id + 1;
			CarRent carrent = new CarRent
			{
				Id = model.Id,
				PlateNumber = model.PlateNumber,
				CarModel = model.CarModel,
				RenterName = model.RenterName,
				RenterPhone = model.RenterPhone,
				RenterEmail = model.RenterEmail,
				RenterAddress = model.RenterAddress
			};
			CarRentRepository.Rents.Add(carrent);

			model.Id = carrent.Id;
			// Created and for new carrent url
			return CreatedAtRoute("GetRentByPlateNumber", new { id = model.Id }, model);

		}
		// update
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public ActionResult UpdateRent([FromBody] CarRentDTO model)
		{
			if (model == null || model.Id <= 0)
			{
				return BadRequest();
			}
			var existingRent = CarRentRepository.Rents.Where(s => s.Id == model.Id).FirstOrDefault();

			if (existingRent == null)
				return NotFound();

			existingRent.Id = model.Id;
			existingRent.PlateNumber = model.PlateNumber;
			existingRent.CarModel = model.CarModel;
			existingRent.RenterName = model.RenterName;
			existingRent.RenterPhone = model.RenterPhone;
			existingRent.RenterEmail = model.RenterEmail;
			existingRent.RenterAddress = model.RenterAddress;
			return NoContent();
		}

		//partially update
		[HttpPatch]
		[Route("{id:int}/UpdatePartial")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]

		public ActionResult PartiallyUpdateStudent(int id, [FromBody] JsonPatchDocument<CarRentDTO> patchDocument)
		{
			if (patchDocument == null || id <= 0)
			{
				return BadRequest();
			}
			var existingRent = CarRentRepository.Rents.Where(s => s.Id == id).FirstOrDefault();

			if (existingRent == null)
				return NotFound();

			var carrentDTO = new CarRentDTO
			{

				Id = existingRent.Id,
				PlateNumber = existingRent.PlateNumber,
				CarModel = existingRent.CarModel,
				RenterName = existingRent.RenterName,
				RenterPhone = existingRent.RenterPhone,
				RenterEmail = existingRent.RenterEmail,
				RenterAddress = existingRent.RenterAddress
			};
			patchDocument.ApplyTo(carrentDTO, ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			existingRent.Id = carrentDTO.Id;
			existingRent.PlateNumber = carrentDTO.PlateNumber;
			existingRent.CarModel = carrentDTO.CarModel;
			existingRent.RenterName = carrentDTO.RenterName;
			existingRent.RenterPhone = carrentDTO.RenterPhone;
			existingRent.RenterEmail = carrentDTO.RenterEmail;
			existingRent.RenterAddress = carrentDTO.RenterAddress;
			return NoContent();
		}

		//delete
		[HttpDelete("{id:int}", Name = "DeleteRentById ")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<CarRentDTO> DeleteStudent(int id)
		{
			//400 Client error
			if (id <= 0)
				return BadRequest("Please enter greater number than 0");

			var student = CarRentRepository.Rents.Where(n => n.Id == id).FirstOrDefault();

			//checking 
			if (student != null)
			{
				CarRentRepository.Rents.Remove(student);
				//Ok 200
				return Ok($"You removed car rent with id '{id}' succesfully");
			}
			else
			{
				//404 error
				return NotFound($"The car rent with id '{id}' is not found");
			}
		}


	}
}

