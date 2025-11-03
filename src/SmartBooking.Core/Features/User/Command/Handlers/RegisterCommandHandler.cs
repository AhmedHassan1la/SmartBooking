using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartBooking.Application.Responses;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Features.User.Command.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBooking.Core.Features.User.Command.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // التحقق من أن الـ request مش null
                if (request == null)
                {
                    return new Response<string>("Request data is missing");
                }

                // التحقق من البيانات الأساسية
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new Response<string>("Email is required");
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new Response<string>("Password is required");
                }

                // 1. Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new Response<string>("User already exists with this email");
                }

                // 2. Create new user مع كل الحقول المطلوبة
                var user = new AppUser
                {
                    FirstName = request.FirstName ?? string.Empty,
                    LastName = request.LastName ?? string.Empty,
                    DisplayName = $"{request.FirstName} {request.LastName}" ?? "User",
                    Gender = "Not Specified",
                    Address = "Not Provided",
                    ImageUrl = string.Empty,
                    Email = request.Email,
                    UserName = request.Email,
                    PhoneNumber = request.PhoneNumber ?? string.Empty,
                    CreatedAt = DateTime.UtcNow
                };

                // 3. Create user with password
                var result = await _userManager.CreateAsync(user, request.Password);

                // التحقق من أن result مش null
                if (result == null)
                {
                    return new Response<string>("Registration failed - no result returned");
                }

                if (result.Succeeded)
                {
                    return new Response<string>(user.Id, "User registered successfully");
                }

                // 4. If failed, return errors مع تحقق كامل من الـ null
                var response = new Response<string>("User registration failed");

                // التحقق من أن result.Errors مش null قبل الـ loop
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        // التحقق من أن error مش null
                        if (error != null)
                        {
                            response.Errors.Add(error.Description ?? "Unknown error");
                        }
                    }
                }
                else
                {
                    response.Errors.Add("No specific error information available");
                }

                return response;
            }
            catch (Exception ex)
            {
                // في حالة حدوث أي exception
                var errorResponse = new Response<string>("An error occurred during registration");
                errorResponse.Errors.Add($"Exception: {ex.Message}");

                // لأغراض الـ debugging
                Console.WriteLine($"Exception in RegisterCommandHandler: {ex}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return errorResponse;
            }
        }
    }
}