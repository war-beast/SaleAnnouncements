﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.Models;

namespace SaleAnnouncements.Controllers
{
	[Route("api/account")]
	[ApiController]
	[Authorize]
	public class ApiAccountController : ControllerBase
	{
		#region private members

		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signManager;
		private readonly ICustomerService _customerService;

		#endregion

		#region constructor

		public ApiAccountController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signManager, 
			ICustomerService customerService)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_signManager = signManager ?? throw new ArgumentNullException(nameof(signManager));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}

		#endregion

		[HttpGet]
		[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signManager.SignOutAsync();
			return Ok();
		}

		[Route("getLogin")]
		public async Task<IActionResult> GetLogin()
		{
			await _signManager.SignInAsync(await _userManager.FindByEmailAsync(User.Identity.Name), true);
			return Ok(User.Identity.Name);
		}

		[AllowAnonymous]
		[Route("register")]
		public async Task Register([FromBody] RegisterBindingModel model)
		{
			Response.StatusCode = 400;

			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.Select(x => x.Errors).First().Select(x => x.ErrorMessage);
				await Response.WriteAsync(JsonConvert.SerializeObject(errors, new JsonSerializerSettings { Formatting = Formatting.Indented }));
				return;
			}

			var user = new IdentityUser()
			{
				UserName = model.Email,
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				//Из коллекции ошибок исключаем дублирование имени, т.к. имя и email пользователя совпадают в БД
				var errors = result.Errors
					.Where(x => !x.Code.Equals("DuplicateUserName", StringComparison.InvariantCultureIgnoreCase))
					.Select(x => x.Description);
				await Response.WriteAsync(JsonConvert.SerializeObject(errors, new JsonSerializerSettings { Formatting = Formatting.Indented }));
				return;
			}

			await _customerService.Create(user.Id);

			Response.StatusCode = 200;
			return;
		}

		[AllowAnonymous]
		[Route("token")]
		public async Task Token([FromBody] LoginBindingModel model)
		{
			//TODO: Вынести получение токена в отдельный сервис
			Response.StatusCode = 400;

			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.Select(x => x.Errors)?.First().Select(x => x.ErrorMessage);
				await Response.WriteAsync(JsonConvert.SerializeObject(errors, new JsonSerializerSettings { Formatting = Formatting.Indented }));
				return;
			}

			var identity = await GetIdentity(model.Email, model.Password, model.RememberMe);
			if (identity == null)
			{
				await Response.WriteAsync("Неправильное имя пользователя или пароль. ");
				return;
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					notBefore: now,
					claims: identity.Claims,
					expires: model.RememberMe ? now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)) : now.Add(TimeSpan.FromMinutes(15)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			await _signManager.PasswordSignInAsync(await _userManager.FindByEmailAsync(model.Email), model.Password, model.RememberMe, false);

			// сериализация ответа
			Response.ContentType = "application/json";
			Response.StatusCode = 200;

			await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
		}

		#region private methods

		private async Task<ClaimsIdentity> GetIdentity(string email, string password, bool isPersistent = false)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentNullException(nameof(email));
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentNullException(nameof(password));

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return null;

			#endregion

			var result = await _signManager.PasswordSignInAsync(email, password, isPersistent, false);

			if (!result.Succeeded)
				return null;

			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
			};
			
			var claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
			return claimsIdentity;
		}

		#endregion
	}
}
