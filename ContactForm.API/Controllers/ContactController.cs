using ContactForm.Application.Contacts.Commands.Create;
using ContactForm.Application.Contacts.Commands.Delete;
using ContactForm.Application.Contacts.Queries;
using ContactForm.Application.Contacts.Queries.DTOs;
using ContactForm.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactForm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController(
        ICreateContactCommandHandler commandHandler,
        IGetUserContactQueryHandler queryHandler,
        IDeleteContactCommandHandler deleteContactHandler) : ControllerBase
    {
        /// <summary>
        /// Saves a new contact submission.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
        {
            try
            {
                await commandHandler.HandleAsync(command);
                return Ok(new { Message = "Contact submission successful." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", ex.Message });
            }
        }

        /// <summary>
        /// Gets a user contact by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserContact(string email)
        {
            try
            {
                var command = new GetUserContactQuery(email);
                var contact = await queryHandler.HandleAsync(command);

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", ex.Message });
            }
        }


        /// <summary>
        /// Deletes a contact by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("{email}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteContact(string email)
        {
            try
            {
                var command = new DeleteContactCommand(email);
                await deleteContactHandler.HandleAsync(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", ex.Message });
            }
        }
    }
}
