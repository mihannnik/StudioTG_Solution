using StudioTG.Application.Interfaces;
using StudioTG.Domain.Entities.Cells;
using StudioTG.Domain.Entities.Fields;
using System.Collections.Concurrent;

namespace StudioTG.Infrastructure.Repositories
{
    public class FieldsRepository : IFieldRepository
    {
        private ConcurrentDictionary<Guid, Field> Fields = new();

        public Field Create(int width, int height, int minesCount)
        {
            Field field = new Field
            {
                Height = height,
                Width = width,
                MinesCount = minesCount,
                Cells = new Cell[width, height]
            };
            Fields[field.Id] = field;
            return field;
        }

        public Field Read(Guid Id)
        {
            if (Fields.TryGetValue(Id, out var field))
            {
                return field;
            }
            throw new KeyNotFoundException($"No field with id {Id}");
        }

        public void Delete(Guid Id)
        {
            if (!Fields.TryRemove(Id, out _))
                throw new KeyNotFoundException(message: $"No field with id {Id.ToString()}");
        }

        public void Update(Field field)
        {
            if (!Fields.TryUpdate(field.Id, field, Fields[field.Id]))
            {
                throw new KeyNotFoundException($"No field with id {field.Id}");
            }
        }
    }
}
