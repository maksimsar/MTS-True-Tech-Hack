// File: Services/SchemaRepository.cs
using MTSTrueTechHack.Data;
using MTSTrueTechHack.Backend.Models;

namespace MTSTrueTechHack.Backend.Services
{
    public class SchemaRepository : ISchemaRepository
    {
        private readonly AppDbContext _context;

        public SchemaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a Schema by its identifier.
        /// </summary>
        /// <param name="id">The ID of the Schema to retrieve.</param>
        /// <returns>The matching Schema or null if not found.</returns>
        public async Task<Schema?> GetAsync(int id)
        {
            // Simple lookup by primary key
            return await _context.Schemas.FindAsync(id);
        }

        /// <summary>
        /// Adds a new Schema entity to the context.
        /// </summary>
        /// <param name="entity">The Schema to add.</param>
        public async Task AddAsync(Schema entity)
        {
            await _context.Schemas.AddAsync(entity);
        }

        /// <summary>
        /// Persists pending changes to the database.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
