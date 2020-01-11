using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo
{
    public class WatchItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database1 => lazyInitializer.Value;
        static bool initialized = false;

        public WatchItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database1.TableMappings.Any(m => m.MappedType.Name == typeof(WatchItem).Name))
                {
                    await Database1.CreateTablesAsync(CreateFlags.None, typeof(WatchItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<WatchItem>> GetItemsAsync()
        {
            return Database1.Table<WatchItem>().ToListAsync();
        }

        public Task<List<WatchItem>> GetItemsNotDoneAsync()
        {
            return Database1.QueryAsync<WatchItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<WatchItem> GetItemAsync(int id)
        {
            return Database1.Table<WatchItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(WatchItem item)
        {
            if (item.ID != 0)
            {
                return Database1.UpdateAsync(item);
            }
            else
            {
                return Database1.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(WatchItem item)
        {
            return Database1.DeleteAsync(item);
        }
    }
}

