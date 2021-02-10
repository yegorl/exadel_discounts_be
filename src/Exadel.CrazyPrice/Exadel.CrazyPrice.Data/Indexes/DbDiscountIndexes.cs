using Exadel.CrazyPrice.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Indexes
{
    /// <summary>
    /// Determines discount indexes.
    /// </summary>
    public static class DbDiscountIndexes
    {
        /// <summary>
        /// Gets discount indexes.
        /// </summary>
        public static List<CreateIndexModel<DbDiscount>> GetIndexes => new()
        {
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>
                            .IndexKeys
                            .Text(c => c.Name)
                            .Text(c => c.Description)
                            .Text(c => c.Tags)
                            .Text(c => c.Address.Country)
                            .Text(c => c.Address.City)
                            .Text(c => c.Company.Name)
                            .Text("translations.name")
                            .Text("translations.description")
                            .Text("translations.address.country")
                            .Text("translations.address.city")
                            .Text("translations.company.name")
                            .Text("translations.tags"), new CreateIndexOptions
                            {
                                Name = "idx_text",
                                Weights = new BsonDocument(new List<BsonElement>
                                {
                                    new ("name", 10),
                                    new ("tags", 10),
                                    new ("translations.name", 10),
                                    new ("translations.tags", 10),
                                })
                            }),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.Name)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.Name)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.Company.Name)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.Company.Name)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.AmountOfDiscount)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.AmountOfDiscount)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.StartDate)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.StartDate)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.EndDate)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.EndDate)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(c => c.RatingTotal)),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(c => c.RatingTotal)),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(new StringFieldDefinition<DbDiscount>("address.country"))),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(new StringFieldDefinition<DbDiscount>("address.city"))),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(new StringFieldDefinition<DbDiscount>("translations.name"))),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(new StringFieldDefinition<DbDiscount>("translations.name"))),

            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Ascending(new StringFieldDefinition<DbDiscount>("translations.company.name"))),
            new CreateIndexModel<DbDiscount>(Builders<DbDiscount>.IndexKeys.Descending(new StringFieldDefinition<DbDiscount>("translations.company.name")))
        };
    }
}
