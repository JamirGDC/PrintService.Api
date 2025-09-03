using System.Net;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.IServices;
using PrintService.Application.Utilities;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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