using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Application.DTOs;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Features.User.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler :
         IRequestHandler<GetUserProfileQuery, Response<UserProfileDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public UserQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return new Response<UserProfileDto>("User not found");
            }

            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt 
            };

            return new Response<UserProfileDto>(userProfile, "User profile retrieved successfully");
        }
    }
}
