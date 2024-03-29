﻿using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class SitterUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
    [MaxLength(30)]
    public string LastName { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.PhoneIsRequired)]
    [MinLength(11)]
    [MaxLength(11)]
    public string Phone { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.AgeIsRequired)]
    [Range(14, 130, ErrorMessage = ApiErrorMessage.AgeIsRange)]
    public int Age { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.ExperienceIsRequired)]
    [Range(0, 130, ErrorMessage = ApiErrorMessage.ExperienceRange)]
    public int Experience { get; set; }

    [Required(ErrorMessage = ApiErrorMessage.SexIsRequired)]
    [EnumRange<Sex>]
    public Sex Sex { get; set; }

    public string Description { get; set; }
}
