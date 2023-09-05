using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ExerciseTimer.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class FirebaseTokenVerificationAttribute : ActionFilterAttribute {
	public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
		string? authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

		if (string.IsNullOrWhiteSpace(authorizationHeader)) {
			context.Result = new UnauthorizedResult();
			return;
		}

		// Strip Bearer
		string idToken = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring(7) : authorizationHeader;

		// Verify the Firebase ID token
		#pragma warning disable CS8600
		#pragma warning disable CS8602
		var firebaseService = (FirebaseService)context.HttpContext.RequestServices.GetService(typeof(FirebaseService));
		string? uid = await firebaseService.VerifyFirebaseIdTokenAsync(idToken);
		#pragma warning restore CS8602
		#pragma warning restore CS8600

		if (uid == null) {
			context.Result = new UnauthorizedResult();
			return;
		} else {
			Console.WriteLine("Token verified.");
		}

		context.HttpContext.Items["uid"] = uid;

		await base.OnActionExecutionAsync(context, next);
	}
}

// [HttpGet]
// public async Task<IActionResult> GetAll()
// {
//   string? idToken = Request.Headers["Authorization"];

//   if (string.IsNullOrWhiteSpace(idToken))
//   {
//     return Unauthorized("Missing or invalid token");
//   }

//   // Strip Bearer
//   idToken = idToken.StartsWith("Bearer ") ? idToken.Substring(7) : idToken;

//   // Verify the Firebase ID token
//   string? uid = await _firebaseService.VerifyFirebaseIdTokenAsync(idToken);
//   if (uid == null)
//   {
//     return Unauthorized("Invalid token");
//   }

//   // Token is valid; proceed with fetching exercises
//   var exercises = _service.GetAll();
//   return Ok(exercises);
// }