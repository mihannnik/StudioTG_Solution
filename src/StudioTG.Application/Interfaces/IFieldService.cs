using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    /// <summary>
    /// Реализует сериализатор сервис для работы с игровыми полями типа Field
    /// </summary>
    public interface IFieldService
    {
        /// <summary>
        /// Позволяет создать игровое поле на основе width, height и minesCount, результатом выполнения является Field
        /// </summary>
        public Task<Field> CreateFieldAsync(int width, int height, int minesCount, CancellationToken cancellationToken);
        /// <summary>
        /// Позволяет выполнить действие, отправленное от игрока на основе Guid Id поля, row и cell, 
        /// результатом выполнения является обновлённый Field 
        /// </summary>
        public Task<Field> MakeTurnAsync(Guid Id, int row, int cell, CancellationToken cancellationToken);
    }
}
