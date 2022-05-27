using System.Text.Json.Serialization;

namespace Entities;

public enum Status
{
    Waiting,
    Denied,
    Approved,
    Fulfilled
}