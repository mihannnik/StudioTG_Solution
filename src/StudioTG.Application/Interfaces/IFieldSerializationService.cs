using StudioTG.Application.DTO.Responses;
using StudioTG.Domain.Entities.Fields;

namespace StudioTG.Application.Interfaces
{
    public interface IFieldSerializationService
    {
        FieldStateResponse Serialize(Field field);
    }
}
