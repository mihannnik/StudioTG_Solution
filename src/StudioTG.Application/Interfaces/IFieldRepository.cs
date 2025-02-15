using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    /// <summary>
    /// Реализует репозиторий для хранения игровых полей
    /// </summary>
    public interface IFieldRepository
    {
        /// <summary>
        /// Создаёт и сохраняет новый экземпляр Field
        /// </summary>
        public Field Create(int width, int height, int minesCount);

        /// <summary>
        /// Позволяет получить экземпляр Field по Guid Id
        /// </summary>
        public Field Read(Guid Id);

        /// <summary>
        /// Позволяет обновить Field
        /// </summary>
        public void Update(Field field);

        /// <summary>
        /// Позволяет удалить экземпляр Field по Guid Id
        /// </summary>
        public void Delete(Guid Id);
    }
}
