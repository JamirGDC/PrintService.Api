using System.Net;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Services;
using PrintService.Application.Utilities.Mappers;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestContext _requestContext;

    public DeviceService(IUnitOfWork unitOfWork, IRequestContext requestContext)
    {
        _unitOfWork = unitOfWork;
        _requestContext = requestContext;
    }

    public async Task<Result<RegisterDeviceResponseDto>> RegisterDeviceAsync(RegisterDeviceRequestDto registerDevice, CancellationToken cancellationToken)
    {
        var deviceInDb = await _unitOfWork.DeviceRepository.GetById(registerDevice.DeviceId);

        if (deviceInDb != null)
            return Result<RegisterDeviceResponseDto>.Failure(HttpStatusCode.Conflict).WithDescription("Device already registered");
        
        var newDevice = await _unitOfWork.DeviceRepository.Create(registerDevice.ToDomain(), cancellationToken);
        if (newDevice == null)
            return Result<RegisterDeviceResponseDto>.Failure(HttpStatusCode.BadRequest);
        
        await _unitOfWork.Complete(cancellationToken);

        return Result<RegisterDeviceResponseDto>.Success(HttpStatusCode.OK).WithPayload(newDevice.ToResponse());
    }

}