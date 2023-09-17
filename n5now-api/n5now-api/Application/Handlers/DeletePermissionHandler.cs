using MediatR;
using n5now_api.Infrastructure.Commands;
using n5now_api.Infrastructure.Data;

namespace n5now_api.Application.Handlers
{
    public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.PermissionRepository.DeletePermission(request.Id);
            await _unitOfWork.Save();
            return response;
        }
    }
}
