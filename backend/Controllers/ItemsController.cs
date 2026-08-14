using CampusLostFound.Api.Data;
using CampusLostFound.Api.DTOs;
using CampusLostFound.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusLostFound.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ItemsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> GetItems(
        [FromQuery] string? search,
        [FromQuery] string? type,
        [FromQuery] string? status,
        [FromQuery] string? category)
    {
        var query = _db.LostFoundItems.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search}%";
            query = query.Where(i => EF.Functions.ILike(i.Title, pattern) || EF.Functions.ILike(i.Description, pattern));
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            if (!Enum.TryParse<ItemType>(type, ignoreCase: true, out var parsedType))
            {
                return BadRequest(new { message = $"Invalid type '{type}'. Must be LOST or FOUND." });
            }
            query = query.Where(i => i.Type == parsedType);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (!Enum.TryParse<ItemStatus>(status, ignoreCase: true, out var parsedStatus))
            {
                return BadRequest(new { message = $"Invalid status '{status}'. Must be ACTIVE or RETURNED." });
            }
            query = query.Where(i => i.Status == parsedStatus);
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(i => EF.Functions.ILike(i.Category, category));
        }

        var items = await query
            .OrderByDescending(i => i.DateReported)
            .ThenByDescending(i => i.Id)
            .ToListAsync();

        return Ok(items.Select(ItemResponse.FromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ItemResponse>> GetItem(int id)
    {
        var item = await _db.LostFoundItems.FindAsync(id);
        if (item is null)
        {
            return NotFound(new { message = $"Item with id {id} was not found." });
        }

        return Ok(ItemResponse.FromEntity(item));
    }

    [HttpPost]
    public async Task<ActionResult<ItemResponse>> CreateItem(CreateItemRequest request)
    {
        var item = new LostFoundItem
        {
            Title = request.Title!.Trim(),
            Description = request.Description!.Trim(),
            Category = request.Category!.Trim(),
            Location = request.Location!.Trim(),
            DateReported = request.DateReported!.Value,
            Type = request.Type!.Value,
            Status = ItemStatus.ACTIVE,
            ContactName = request.ContactName!.Trim(),
            ContactEmail = request.ContactEmail!.Trim()
        };

        _db.LostFoundItems.Add(item);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, ItemResponse.FromEntity(item));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ItemResponse>> UpdateItem(int id, UpdateItemRequest request)
    {
        var item = await _db.LostFoundItems.FindAsync(id);
        if (item is null)
        {
            return NotFound(new { message = $"Item with id {id} was not found." });
        }

        if (request.Title is not null) item.Title = request.Title.Trim();
        if (request.Description is not null) item.Description = request.Description.Trim();
        if (request.Category is not null) item.Category = request.Category.Trim();
        if (request.Location is not null) item.Location = request.Location.Trim();
        if (request.DateReported is not null) item.DateReported = request.DateReported.Value;
        if (request.Type is not null) item.Type = request.Type.Value;
        if (request.Status is not null) item.Status = request.Status.Value;
        if (request.ContactName is not null) item.ContactName = request.ContactName.Trim();
        if (request.ContactEmail is not null) item.ContactEmail = request.ContactEmail.Trim();

        await _db.SaveChangesAsync();

        return Ok(ItemResponse.FromEntity(item));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _db.LostFoundItems.FindAsync(id);
        if (item is null)
        {
            return NotFound(new { message = $"Item with id {id} was not found." });
        }

        _db.LostFoundItems.Remove(item);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
