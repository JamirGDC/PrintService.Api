namespace PrintService.Application.Services;

using System.Net;
using DTOs.Request;
using DTOs.Response;
using Interfaces;
using Interfaces.Repositories;
using PrintService.Application.Interfaces.Services;
using Utilities.Mappers;
using Shared.Result;
using Domain.Entities;

public class DeviceService(IUnitOfWork unitOfWork, IRequestContext requestContext, IGenericRepository<Device> deviceRepository) : IDeviceService
{
    public async Task<Result<RegisterDeviceResponseDto>> RegisterDeviceAsync(RegisterDeviceRequestDto registerDevice, CancellationToken cancellationToken)
    {
        var deviceInDb = await deviceRepository.GetById(registerDevice.DeviceId);
        if (deviceInDb != null)
            return Result<RegisterDeviceResponseDto>.Failure(HttpStatusCode.Conflict).WithDescription("Device already registered");
        
        var newDevice = await deviceRepository.Create(registerDevice.ToDomain(requestContext.Region), cancellationToken);
        if (newDevice == null)
            return Result<RegisterDeviceResponseDto>.Failure(HttpStatusCode.BadRequest);
        
        await unitOfWork.Complete(cancellationToken);

        return Result<RegisterDeviceResponseDto>.Success(HttpStatusCode.OK).WithPayload(newDevice.ToResponse());
    }
}