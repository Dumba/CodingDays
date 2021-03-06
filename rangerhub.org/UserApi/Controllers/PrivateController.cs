using System;
using CodingDays.UserApi.Database;
using CodingDays.UserApi.Database.Entities;
using CodingDays.UserApi.Exceptions;
using CodingDays.UserApi.Models;
using CodingDays.UserApi.Models.Dto.Private;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.UserApi.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrivateController : ControllerBase
{
    public PrivateController(DB db, AuthHolder authHolder)
    {
        _db = db;
        _authHolder = authHolder;
    }

    private readonly DB _db;
    private readonly AuthHolder _authHolder;

    [HttpGet("[action]")]
    public GetDashboardResp GetDashboard()
    {
        User user = _db.Users.Find(_authHolder.AuthId)
            ?? throw new UsageException("Uživatel nenalezen");

        string? dashboard = Environment.GetEnvironmentVariable($"DASHBOARD_{user.UserName.ToUpper()}");
        return new GetDashboardResp(dashboard);
    }
}
