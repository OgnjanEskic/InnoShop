﻿using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;

namespace Domain.Interfaces
{
    /// <summary>
    /// Represents the interface for LocationRepository class from Infrastructure layer.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Calls the method to insert LocationRequest object into the db.
        /// </summary>
        /// <param name="locationRequest">The type of the object to be written.</param>
        /// <returns>A Task with the object that contains ID of the inserted row.</returns>
        public Task<Result<LocationResponse>> CreateLocation(LocationRequest locationRequest);
    }
}