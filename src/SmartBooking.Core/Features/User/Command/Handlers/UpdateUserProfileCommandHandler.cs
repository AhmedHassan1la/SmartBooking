using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Application.DTOs;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Features.User.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Command.Handlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Response<UserProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserProfileDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return new Response<UserProfileDto>("User not found");
            }

            // Update user properties
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var updatedUser = await _userManager.FindByIdAsync(request.UserId);
                var userProfile = new UserProfileDto
                {
                    Id = updatedUser.Id,
                    FirstName = updatedUser.FirstName,
                    LastName = updatedUser.LastName,
                    Email = updatedUser.Email,
                    PhoneNumber = updatedUser.PhoneNumber
                };

                return new Response<UserProfileDto>(userProfile, "Profile updated successfully");
            }

            var response = new Response<UserProfileDto>("Profile update failed");
            foreach (var error in result.Errors)
            {
                response.Errors.Add(error.Description);
            }
            return response;
        }
    }
}
