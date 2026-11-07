using CashBook.BuildingBlocks.Domain;

namespace CashBook.Ledger.UnitTests.BuildingBlocks;

public sealed class EntityTests
{
    private sealed class SampleEntity(Guid id) : Entity(id);

    private sealed class OtherEntity(Guid id) : Entity(id);

    [Fact]
    public void Constructor_WithEmptyId_Throws()
    {
        Assert.Throws<ArgumentException>(() => new SampleEntity(Guid.Empty));
    }

    [Fact]
    public void Entities_WithSameIdAndType_AreEqual()
    {
        var id = Guid.NewGuid();
        var first = new SampleEntity(id);
        var second = new SampleEntity(id);

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void Entities_WithDifferentIds_AreNotEqual()
    {
        var first = new SampleEntity(Guid.NewGuid());
        var second = new SampleEntity(Guid.NewGuid());

        Assert.NotEqual(first, second);
        Assert.True(first != second);
    }

    [Fact]
    public void Entities_WithSameIdAndDifferentTypes_AreNotEqual()
    {
        var id = Guid.NewGuid();

        Assert.False(new SampleEntity(id).Equals(new OtherEntity(id)));
    }

    [Fact]
    public void Entity_ComparedWithNull_IsNotEqual()
    {
        var entity = new SampleEntity(Guid.NewGuid());

        Assert.False(entity.Equals(null));
        Assert.True(entity != null);
    }
}
