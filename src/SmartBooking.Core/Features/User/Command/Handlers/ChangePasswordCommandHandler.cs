using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Features.User.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Command.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return new Response<string>("User not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return new Response<string>("Password changed successfully");
            }

            var response = new Response<string>("Password change failed");
            foreach (var error in result.Errors)
            {
                response.Errors.Add(error.Description);
            }
            return response;
        }
    }
}
