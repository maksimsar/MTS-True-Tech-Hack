// File: Services/ISchemaRepository.cs
using MTSTrueTechHack.Backend.Models;

namespace MTSTrueTechHack.Backend.Services
{
    public interface ISchemaRepository
    {
        /// <summary>
        /// Retrieves a Schema by its identifier.
        /// </summary>
        /// <param name="id">The ID of the Schema to retrieve.</param>
        /// <returns>The matching Schema or null if not found.</returns>
        Task<Schema?> GetAsync(int id);

        /// <summary>
        /// Adds a new Schema entity to the context.
        /// </summary>
        /// <param name="entity">The Schema to add.</param>
        Task AddAsync(Schema entity);

        /// <summary>
        /// Persists changes to the database.
        /// </summary>
        Task SaveAsync();
    }
}