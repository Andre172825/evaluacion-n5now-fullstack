using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using n5now_api.Application.DTOs;
using n5now_api.Controllers;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Queries;
using n5now_api.Infrastructure.Repositories;
using Nest;

namespace n5now_test
{
    public class ApiTests
    {
        private readonly IESClientProvider _clientProvider;
        private readonly Mock<IMediator> _mediatorMock;
        public ApiTests() 
        {
            _clientProvider = new ESClientProviderMock();
            _mediatorMock = new Mock<IMediator>();
        }
        [Fact]
        public async Task Create_ReturnsOk_WhenPermissionIsCreated()
        {
            // Arrange
            var controller = new PermissionsController(_mediatorMock.Object, _clientProvider);

            var command = new CreatePermissionCommand("Jorge", "Perez", 1);
            var createdPermission = new PermissionDto { Id = 1, EmployeeName = "Jorge", EmployeeLastName = "Perez", PermissionTypeId = 1 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePermissionCommand>(), default(CancellationToken)))
                        .ReturnsAsync(createdPermission);

            // Act
            var result = await controller.Create(command);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenPermissionIsUpdated()
        {
            // Arrange
            var controller = new PermissionsController(_mediatorMock.Object, _clientProvider);

            var id = 1;
            var command = new UpdatePermissionCommand(id, "Luis", "Ramirez", 2);
            var updatedPermission = new PermissionDto { Id = id, EmployeeName = "Luis", EmployeeLastName = "Ramirez", PermissionTypeId = 2 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePermissionCommand>(), default(CancellationToken)))
                        .ReturnsAsync(updatedPermission);

            // Act
            var result = await controller.Update(id, command);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithPermissions_WhenPermissionsExist()
        {
            // Arrange
            var controller = new PermissionsController(_mediatorMock.Object, _clientProvider);

            var permissions = new List<PermissionDto>
            {
                new PermissionDto { Id = 1, EmployeeName = "Julio", EmployeeLastName = "Castro", PermissionTypeDescription = "Admin Access", PermissionTypeId = 1, PermissionDate = DateTime.Now },
                new PermissionDto { Id = 2, EmployeeName = "Abraham", EmployeeLastName = "Gomez", PermissionTypeDescription = "Approve project Access", PermissionTypeId = 2, PermissionDate = DateTime.Now},
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPermissionsQuery>(), default(CancellationToken)))
                        .ReturnsAsync(permissions);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<PermissionDto>>(okResult.Value);
            Assert.Equal(permissions.Count, model.Count());
        }
    }
}