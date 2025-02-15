using StudioTG.Application.DTO.Responses;
using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    /// <summary>
    /// Реализует сериализатор для преобразования Field в FieldStateResponse
    /// </summary>
    public interface IFieldSerializationService
    {
        /// <summary>
        /// Позволяет сериализовать Field
        /// </summary>
        FieldStateResponse Serialize(Field field);
    }
}
