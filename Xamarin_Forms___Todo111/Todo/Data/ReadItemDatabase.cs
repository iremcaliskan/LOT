using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo
{
    public class ReadItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database2 => lazyInitializer.Value;
        static bool initialized = false;

        public ReadItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database2.TableMappings.Any(m => m.MappedType.Name == typeof(ReadItem).Name))
                {
                    await Database2.CreateTablesAsync(CreateFlags.None, typeof(ReadItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ReadItem>> GetItemsAsync()
        {
            return Database2.Table<ReadItem>().ToListAsync();
        }

        public Task<List<ReadItem>> GetItemsNotDoneAsync()
        {
            return Database2.QueryAsync<ReadItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<ReadItem> GetItemAsync(int id)
        {
            return Database2.Table<ReadItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ReadItem item)
        {
            if (item.ID != 0)
            {
                return Database2.UpdateAsync(item);
            }
            else
            {
                return Database2.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ReadItem item)
        {
            return Database2.DeleteAsync(item);
        }
    }
}

