﻿using System.Text.Json;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PrisonerController : ControllerBase
{
    private IPrisonerService _prisonerService;

    public PrisonerController(IPrisonerService prisonerService)
    {
        _prisonerService = prisonerService;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Prisoner>> CreatePrisoner([FromBody] Prisoner prisoner)
    {
        try
        {
            Prisoner toAdd = await _prisonerService.CreatePrisonerAsync(prisoner);
            return Created($"/Prisoner/{toAdd.Id}", toAdd);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<String>> RemovePrisoner([FromRoute] long id)
    {
        try
        {
            await _prisonerService.RemovePrisonerAsync(id);
            return Ok("Prisoner " + id + " deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<Prisoner>> UpdatePrisoner([FromBody] Prisoner? prisoner)
    {
        try
        {
            await _prisonerService.UpdatePrisonerAsync(prisoner);
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    // [HttpGet]
    // public async Task<ActionResult<PrisonersList>> GetPrisoners()
    // {
    //     try
    //     {
    //         ICollection<Prisoner> prisoners = await _prisonerService.GetPrisonersAsync();
    //         PrisonersList prisonersList = new(prisoners);
    //         return Ok(prisonersList);
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(500, e.Message);
    //     }
    // }
    //get prisoners with pagination
    [HttpGet]
    public async Task<ActionResult<PrisonersList>> GetPrisoners([FromQuery]int pageNumber, [FromQuery]int pageSize)
    {
        try
        {
            ICollection<Prisoner> prisoners;
            if (pageNumber==0 && pageSize==0)
            {
                prisoners = await _prisonerService.GetPrisonersAsync();
            }
            else
            {
                prisoners = await _prisonerService.GetPrisonersAsync(pageNumber, pageSize);
            }
            PrisonersList prisonersList = new(prisoners);
            return Ok(prisonersList);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Prisoner>> GetPrisonerById(long id)
    {
        try
        {
            Prisoner toGet = await _prisonerService.GetPrisonerByIdAsync(id);
            Console.WriteLine(toGet.FirstName);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("ssn/{ssn}")]
    public async Task<ActionResult<String>> GetPrisonerBySSN([FromRoute]string ssn)
    {
        try
        {
            Prisoner toGet = await _prisonerService.GetPrisonerBySsnAsync(ssn);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}