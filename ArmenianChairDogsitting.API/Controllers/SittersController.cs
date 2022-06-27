﻿using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SittersController : Controller
{
    [HttpPost]
    public void AddSitter()
    {

    }

    [HttpGet("{id}")]
    public Sitter GetSitterById(int id)
    {
        return Sitter;           
    }

    [HttpGet]
    public List<Sitter> GetAllSitters()
    {
        return List<Sitter>;
    }

    [HttpPut("{id}")]
    public void UpdateSitterById(int id)
    {

    }

    [HttpDelete("{id}")]
    public void RemoveSitterById(int id)
    {

    }

    [HttpGet]
    public void GetAllSettersWithWorkTimes()
    {

    }

    [HttpPatch("{id}")]
    public void DeactivateSitterById(int id)
    {

    }
}
