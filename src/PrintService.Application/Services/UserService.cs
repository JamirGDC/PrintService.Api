using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Services;
using PrintService.Application.Utilities.Mappers;
using PrintService.Shared.Result;
using PrintService.Domain.Entities;
using PrintService.Domain.Enums;
using System.Net;
using Microsoft.EntityFrameworkCore;
using PrintService.Application.Interfaces.Repositories;


namespace PrintService.Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    IRequestContext requestContext,
    IGenericRepository<UserPrinter> userPrinterRepository)
    : IUserService
{

    //public async Task<Result<UserPrinterResponseDto>> GetUserPrinterAsync(CancellationToken cancellationToken)
    //{
       
    //}

    //public async Task<Result<UserPrinterResponseDto>> SetUserPrinterAsync(SetUserPrinterRequestDto request, CancellationToken cancellationToken)
    //{
       
    //}

}