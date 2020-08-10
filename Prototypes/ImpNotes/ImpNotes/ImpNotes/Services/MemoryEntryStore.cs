using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Interface;
using ImpNotes.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ImpNotes.Services
{
    public class MemoryEntryStore : IEntryStore
    {
        private readonly Dictionary<int, NotesModel> entries 
            = new Dictionary<int, NotesModel>();

        public Task<IEnumerable<NotesModel>> GetAllAsync()
        {
            IEnumerable<NotesModel> result = entries.Values.ToList();
            return Task.FromResult(result);
        }
        public Task AddAsync(NotesModel entry)
        {
            entries.Add(entry.Id, entry);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(NotesModel entry)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(NotesModel entry)
        {
            entries.Remove(entry.Id);
            return Task.CompletedTask;
        }

        public Task<NotesModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        //public Task<NotesModel> GetByIdAsync(string id)
        //{
        //    NotesModel entry = null;
        //    entries..GetValue(id, out entry);
        //    return Task.FromResult(entry);
        //}

    }
}
