using Domain.Models.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Extensions
{
    /// <summary>
    /// Represents the extension of the controller class that holds logic
    /// for providing proper response.
    /// </summary>
    public static class ResultExtension
    {
        /// <summary>
        /// Mapping error results to proper response codes along with the error message.
        /// </summary>
        /// <param name="result">FluentResult with LocationResponse object.</param>
        /// <returns>ObjectResult that contains HTTP response code and list of errors that occurred.</returns>
        public static ObjectResult MapErrorsToResponse(this Result<LocationResponse> result)
        {
            switch (result.Errors.First().Message)
            {
                case "Validation Failure":
                    return CreateObjectResult(
                        (int)HttpStatusCode.BadRequest, result.Errors.First().Reasons.Select(x => x.Message));

                case "Duplicated key":
                    return CreateObjectResult(
                        (int)HttpStatusCode.Conflict, result.Errors.First().Reasons.Select(x => x.Message));

                default:
                    return CreateObjectResult(
                        (int)HttpStatusCode.InternalServerError, result.Errors.First().Reasons.Select(x => x.Message));
            }
        }

        /// <summary>
        /// Mapping error results to proper response codes along with the error message.
        /// </summary>
        /// <param name="result">FluentResult object.</param>
        /// <returns>ObjectResult that contains HTTP response code and list of errors that occurred.</returns>
        public static ObjectResult MapErrorsToResponse(this Result result)
        {
            switch (result.Errors.First().Message)
            {
                case "Missing key":
                    return CreateObjectResult(
                        (int)HttpStatusCode.NotFound, result.Errors.First().Reasons.Select(x => x.Message));

                default:
                    return CreateObjectResult(
                        (int)HttpStatusCode.InternalServerError, result.Errors.First().Reasons.Select(x => x.Message));
            }
        }

        /// <summary>
        /// Creating ObjectResult filled with proper response code and the error message.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="reason">List of errors.</param>
        /// <returns>ObjectResult that contains HTTP response code and list of errors that occurred.</returns>
        private static ObjectResult CreateObjectResult(int statusCode, IEnumerable<string> reason)
        {
            return new ObjectResult(statusCode)
            {
                StatusCode = statusCode,
                Value = reason
            };
        }
    }
}
