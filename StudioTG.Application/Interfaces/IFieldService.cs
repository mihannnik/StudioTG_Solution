using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    public interface IFieldService
    {
        public Task<Field> CreateFieldAsync(int width, int height, int mines_count, CancellationToken cancellationToken);
        public Task<Field> MakeTurnAsync(Guid Id, int row, int cell, CancellationToken cancellationToken);
    }
}
