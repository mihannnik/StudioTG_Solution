using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    public interface IFieldRepository
    {
        public Field Create(int width, int height, int minesCount);
        public Field Read(Guid Id);
        public void Update(Field field);
        public void Delete(Guid Id);
    }
}
