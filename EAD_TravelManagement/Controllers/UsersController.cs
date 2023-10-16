/*
 * File: UsersController.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the UsersController class, which provides various utility functions.
 */

using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;


namespace EAD_TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService) =>
            _usersService = usersService;

        //Get all users
        [HttpGet]
        public async Task<List<User>> Get() =>
            await _usersService.GetAsync();

        //Get a specific user
        [HttpGet("{nic}")]
        public async Task<ActionResult<User>> Get(string nic)
        {
            var user = await _usersService.GetAsync(nic);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        //Add a new user
        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _usersService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        //Update a specific user
        [HttpPut("{nic}")]
        public async Task<IActionResult> Update(string nic, User updatedUser)
        {
            var user = await _usersService.GetAsync(nic);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.NIC = user.NIC;

            await _usersService.UpdateAsync(nic, updatedUser);

            return Ok(updatedUser);
        }

        //Delete a specific user
        [HttpDelete("{nic}")]
        public async Task<IActionResult> Delete(string nic)
        {
            var user = await _usersService.GetAsync(nic);

            if (user is null)
            {
                return NotFound();
            }

            await _usersService.RemoveAsync(nic);

            return Ok("Successfully Deleted");
        }
    }
}
