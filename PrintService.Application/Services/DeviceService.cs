using System.Net;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Repositories;
using PrintService.Application.Interfaces.Services;
using PrintService.Application.Utilities.Mappers;
using PrintService.Shared.Result;
using PrintService.Domain.Entities;

namespace PrintService.Application.Services;

public class DeviceService(
    IUnitOfWork unitOfWork,
    IRequestContext requestContext,
    IGenericRepository<Device> deviceRepository)
    : IDeviceService
{
    private readonly IRequestContext _requestContext = requestContext;
    private readonly IGenericRepository<Device> _deviceRepository = deviceRepository;

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