using Entities;

namespace Interfaces;

public interface IAlertService
{
    public Task AddAlertAsync(Alert alert);
}