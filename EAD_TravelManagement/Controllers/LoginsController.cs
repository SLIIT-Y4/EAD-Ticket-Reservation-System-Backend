/*
 * File: LoginsController.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the LoginsController class, which provides various utility functions.
 */

using EAD_TravelManagement.Services;
using EAD_TravelManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace EAD_TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class LoginsController : ControllerBase
    {
        private readonly LoginsService _loginsService;
        private readonly UsersService _usersService;

        public LoginsController(LoginsService loginsService, UsersService usersService) 
        {

            _loginsService = loginsService;
            _usersService = usersService;
        }

        //SignUp Function
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(Login login)
        {
            await _loginsService.RegisterUserAsync(login,login.Password);

            return Ok(login);
        }

        //Login Function
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var nic = login.NIC;
            var data = await _usersService.GetAsync(nic);
            
            if (data == null)
            {
                return NotFound("User not found");
            }

            if (data.AccountStatus == "Active")
            {
                var authenticatedUser = await _loginsService.AuthenticateAsync(login.NIC, login.Password);
                if (authenticatedUser == null)
                {
                    return Unauthorized("Invalid Credentials");
                }


                return Ok(data);
            }
            else
            {
                return NotFound("Account Already Deactivated");
            }
        }
    }
}
