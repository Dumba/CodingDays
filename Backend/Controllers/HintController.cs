using System;
using System.Linq;
using System.Threading.Tasks;
using CodingDays.Database;
using CodingDays.Database.Entities;
using CodingDays.Exceptions;
using CodingDays.Models.Config;
using CodingDays.Models.Dto.Hint;
using CodingDays.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Controllers;
public class HintController : ControllerBase
{
    public HintController(DB db, SecretHolder secretHolder)
    {
        _db = db;
        _secretHolder = secretHolder;
    }

    private readonly DB _db;
    private readonly SecretHolder _secretHolder;

    [Authorize]
    public async Task<ActionResult<TryResp>> Try([FromBody]TryReq param)
    {
        Team team = _db.Teams.Find(param?.TeamId)
            ?? throw new UsageException("TeamId je chybné");
        if (team.CurrentStep == ESteps.NotStarted)
            throw new UsageException("Hra ještě nezačala");
        Cypher cypher = _db.Cyphers.Find(param?.CypherResult)
            ?? throw new UsageException("Výsledek šifry je chybný");

        Hint? hint = _db.CypherUsages
            .Include(cu => cu.Hint)
            .FirstOrDefault(cu => cu.CypherId == cypher.Id && cu.TeamId == team.Id)
            ?.Hint;

        // already used this code -> send same hint
        if (hint is not null)
            return new TryResp(true, hint.Text, hint.ImageUrl);

        // try get hint
        hint = _db.Hints
            .OrderBy(h => h.Order)
            .FirstOrDefault(h => h.Step == team.CurrentStep && !h.CypherUsages.Any());

        // all hints are used
        if (hint is null) {
            hint = _db.Hints.Find(Guid.Parse("00000000-0000-0000-0000-000000000001")) ?? throw new AdminException("Missing default hint");
            return new TryResp(false, hint.Text, hint.ImageUrl);
        }
        
        // register new usage
        CypherUsage newUsage = new CypherUsage()
        {
            Cypher = cypher,
            Team = team,
            Hint = hint,
        };
        _db.Add(newUsage);
        await _db.SaveChangesAsync();

        return new TryResp(false, hint.Text, hint.ImageUrl);
    }

    public async Task<InsertResp> InsertCypher([FromBody]InsertReq param)
    {
        _secretHolder.ValidateSecret(param.Secret);

        string hash = CypherHasher.HashCypherResult(param.CypherResult);

        // already exists
        bool alreadyExists = _db.Cyphers.Any(c => c.Id == hash);
        if (alreadyExists)
            throw new UsageException("Šifra už existuje");

        // create
        Cypher cypher = new Cypher(hash, param.CypherResult);
        _db.Add(cypher);
        
        await _db.SaveChangesAsync();

        return new InsertResp(hash);
    }
}
