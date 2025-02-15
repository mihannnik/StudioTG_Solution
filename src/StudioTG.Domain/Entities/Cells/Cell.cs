using StudioTG.Domain.Enums;

namespace StudioTG.Domain.Entities.Cells
{
    public class Cell
    {
        public required CellType CellType { get; set; }
        public bool IsOpen { get; set; } = false;
        public int MinesAround { get; set; } = 0;
    }
}
