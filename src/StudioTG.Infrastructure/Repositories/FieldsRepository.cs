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
            if (Fields.TryGetValue(Id, out Field field))
            {
                return field;
            }
            throw new KeyNotFoundException(message: $"No field with id {Id.ToString()}");
        }

        public void Delete(Guid Id)
        {
            if (!Fields.TryRemove(Id, out _))
                throw new KeyNotFoundException(message: $"No field with id {Id.ToString()}");
        }

        public void Update(Field field)
        {
            if (Fields.ContainsKey(field.Id)) Fields[field.Id] = field;
            throw new KeyNotFoundException(message: $"No field with id {field.Id.ToString()}");
        }
    }
}
