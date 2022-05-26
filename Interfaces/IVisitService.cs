﻿using Entities;

namespace Interfaces;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(Visit visit);
    Task<Visit> GetVisitByAccessCodeAsync(string code);
    Task<Visit> UpdateVisitStatusAsync(long id, Status status, string accessCode);
    Task<ICollection<Visit>> GetVisitsAsync(int pageNumber, int pageSize);
    Task<List<int>> GetNumVisitsTodayAsync();
}