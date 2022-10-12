using System;
using MinAPI.Models;

namespace MinAPI.Data
{
    public interface ICommandRepo
    {
        Task SaveChangesAsync();
        Task<Command?> GetCommandByIdAsync(int id);
        Task<IEnumerable<Command>> GetAllCommandsAsync();
        Task CreateCommandAsync(Command cmd);

        void DeleteCommand(Command cmd);
    }
}

