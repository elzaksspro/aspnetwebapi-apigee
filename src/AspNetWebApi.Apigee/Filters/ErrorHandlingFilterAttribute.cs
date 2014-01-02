﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace AspNetWebApi.ApiGee.Filters
{
	/// <summary>
	/// Filter to encapsulate any error in a response with 400 status code.
	/// <remarks>
	/// This filters is about this part of Apigee - Web API Design:
	/// Start by using the following 3 codes.
	///	• 400 - Bad Request
	/// </remarks>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
	{
		#region Public Methods
		/// <summary>
		/// Raises the exception event.
		/// </summary>
		/// <param name="filterContext">Filter context.</param>
		public override void OnException(HttpActionExecutedContext filterContext)
		{
			if (filterContext.Exception != null)
			{
				// To avoid memory leak as described here: http://stackoverflow.com/a/20762570/956886
				if (filterContext.Response != null) {
					filterContext.Response.Dispose ();
				}

				var exception = filterContext.Exception;

				filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { code = 400, message = exception.Message }, new JsonMediaTypeFormatter());
			}
		}
		#endregion
	}
}