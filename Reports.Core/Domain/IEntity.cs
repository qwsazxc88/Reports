namespace Reports.Core.Domain
{
    /// <summary>
    /// Defines identifier property for all entities with own lifecycle;
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; }
    }
}