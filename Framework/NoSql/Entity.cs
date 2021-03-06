using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArkApplication.Framework.NoSql
{

    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>The id for this object (the primary record for an entity).</value>
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string _id { get; set; }
    }

}