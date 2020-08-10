using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Models;

namespace ImpNotes.Interface
{
    public interface IEntryStore
    {
        Task<NotesModel> GetByIdAsync(string id);
        Task<IEnumerable<NotesModel>> GetAllAsync();
        Task AddAsync(NotesModel entry);
        Task UpdateAsync(NotesModel entry);
        Task DeleteAsync(NotesModel entry);
    }
}
