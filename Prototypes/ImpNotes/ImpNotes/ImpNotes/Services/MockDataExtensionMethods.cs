using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Interface;
using ImpNotes.Models;

namespace ImpNotes.Services
{
    public static class MockDataExtensionMethods
    {
        public static void LoadMockData(this IEntryStore store)
        {
            NotesModel a = new NotesModel
            {
                Title = "Sprint Planning Meeting",
                Text = "1. Scope 2. Backlog 3. Duration"
            };

            NotesModel b = new NotesModel
            {
                Title = "Daily Scrum Stand-up",
                Text = "1. Yesterday 2. Today 3. Impediments"
            };

            NotesModel c = new NotesModel
            {
                Title = "Sprint Retrospective",
                Text = "1. Reflection 2. Actions"
            };

            Task.WhenAll(
                    store.AddAsync(a),
                    store.AddAsync(b),
                    store.AddAsync(c))
                .ConfigureAwait(false);
        }
    }
}
