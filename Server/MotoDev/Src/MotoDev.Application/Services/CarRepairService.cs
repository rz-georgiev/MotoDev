using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Constants;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Common.Extensions;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Application.Services
{
    public class CarRepairService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        MotoDevDbContext dbContext) : ICarRepairService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarRepairsAsync()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            var a = _dbContext.RepairShops.Where(x => x.OwnerUserId == userId)
                .Select(repairShop => new
                {
                    RepairShop = repairShop,
                    RepairShopUsers = repairShop.RepairShopUsers.Select(repairShopUser => new
                    {

                    })
                }).ToList();

            

            return null;
        }
    }
}