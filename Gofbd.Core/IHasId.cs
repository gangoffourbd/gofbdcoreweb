namespace Gofbd.Core
{
    public interface IHasId<out TId>
    {
        TId Id { get; }
    }
}