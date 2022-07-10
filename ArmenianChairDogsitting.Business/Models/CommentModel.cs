﻿using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business.Models;

public class CommentModel
{
    public int Id { get; set; }
    public Client Client { get; set; }
    public string Title { get; set; }
    public Order Order { get; set; }
    public DateTime TimeCreated { get; set; }
    public bool IsDeleted { get; set; }
}