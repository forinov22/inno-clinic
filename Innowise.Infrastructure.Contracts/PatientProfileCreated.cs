﻿namespace InnoClinic.Contracts;

public class PatientProfileCreated
{
    public Guid ProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
}