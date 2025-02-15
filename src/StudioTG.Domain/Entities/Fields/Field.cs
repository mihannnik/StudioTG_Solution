using StudioTG.Domain.Entities.Cells;
using StudioTG.Domain.Enums;

namespace StudioTG.Domain.Entities.Fields
{
    public class Field
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required int Width { get; init; }
        public required int Height { get; init; }
        public required int MinesCount { get; init; }
        public required Cell[,] Cells { get; set; }
        public FieldState State { get; set; } = FieldState.Progress;
    }
}
