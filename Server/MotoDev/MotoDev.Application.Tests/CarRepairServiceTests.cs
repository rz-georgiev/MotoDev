using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MotoDev.Application.Interfaces;
using MotoDev.Application.Services;
using MotoDev.Common.Enums;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

public class CarRepairServiceTests
{
    private readonly Mock<MotoDevDbContext> _dbContextMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
    private readonly CarRepairService _carRepairService;

    public CarRepairServiceTests()
    {
        //_dbContextMock = new Mock<MotoDevDbContext>();

        _configurationMock = new Mock<IConfiguration>();
        _userServiceMock = new Mock<IUserService>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _emailServiceMock = new Mock<IEmailService>();
        _cloudinaryServiceMock = new Mock<ICloudinaryService>();
        
        var options = new DbContextOptionsBuilder<MotoDevDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;
        _dbContextMock = new Mock<MotoDevDbContext>(options);

        _carRepairService = new CarRepairService(
            _configurationMock.Object,
            _emailServiceMock.Object,
            _httpContextAccessorMock.Object,
            _cloudinaryServiceMock.Object,
            _userServiceMock.Object,
            _dbContextMock.Object);


    }

    [Fact]
    public async Task GetAllCarsRepairsAsync_ReturnsCorrectResult()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(u => u.CurrentUserId).Returns(userId);

        //var repairShops = new List<RepairShop>
        //{
        //    new RepairShop
        //    {
        //        OwnerUserId = userId,
        //        RepairShopUsers = new List<RepairShopUser>
        //        {
        //            new RepairShopUser
        //            {
        //                User = new User { RoleId = (int)RoleOption.Client, FirstName = "John", LastName = "Doe", UserId = 2 },
        //                UserId = 2
        //            }
        //        }
        //    }
        //};

        //var clients = new List<Client>
        //{
        //    new Client
        //    {
        //        UserId = 2,
        //        ClientCars = new List<ClientCar>
        //        {
        //            new ClientCar
        //            {
        //                LicensePlateNumber = "ABC123",
        //                ClientCarRepairs = new List<ClientCarRepair>
        //                {
        //                    new ClientCarRepair
        //                    {
        //                        Id = 1,
        //                        IsActive = true,
        //                        CreatedAt = DateTime.Now,
        //                        RepairStatus = new RepairStatus { Name = "Completed" },
        //                        RepairStatusId = 1
        //                    }
        //                }
        //            }
        //        }
        //    }
        //};

        //var repairShopsQueryable = repairShops.AsQueryable();
        //var clientsQueryable = clients.AsQueryable();

        //_dbContextMock.Setup(db => db.RepairShops).Returns(repairShopsQueryable);
        //_dbContextMock.Setup(db => db.Clients).Returns(clientsQueryable.Object);

        // Act
        var response = await _carRepairService.GetAllCarsRepairsAsync();

        // Assert
        Assert.True(response.IsOk);
        var carRepairs = response.Result.ToList();
        Assert.Single(carRepairs);
        Assert.Equal("John", carRepairs[0].FirstName);
        Assert.Equal("Doe", carRepairs[0].LastName);
        Assert.Equal("ABC123", carRepairs[0].LicensePlateNumber);
        Assert.Equal(1, carRepairs[0].CarRepairId);
        Assert.Equal("Completed", carRepairs[0].Status);
        Assert.Equal(1, carRepairs[0].StatusId);
    }
}

public static class DbSetMockingExtensions
{
    public static DbSet<T> ReturnsDbSet<T>(this Mock<DbSet<T>> dbSet, IEnumerable<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return dbSet.Object;
    }

    public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<MotoDevDbContext> dbContext, IEnumerable<T> data) where T : class
    {
        var dbSet = new Mock<DbSet<T>>();
        dbSet.ReturnsDbSet(data);
        dbContext.Setup(c => c.Set<T>()).Returns(dbSet.Object);
        return dbSet;
    }
}