using Microsoft.Extensions.Primitives;
using StudioTG.Application.DTO.Responses;
using StudioTG.Application.Interfaces;
using StudioTG.Domain.Entities.Cells;
using StudioTG.Domain.Entities.Fields;
using StudioTG.Domain.Enums;
using System.Text;

namespace StudioTG.Infrastructure.Services
{
    public class FieldSerializationService : IFieldSerializationService
    {
        public FieldStateResponse Serialize(Field field)
        {
            return new FieldStateResponse
            {
                Id = field.Id,
                Width = field.Width,
                Height = field.Height,
                MinesCount = field.MinesCount,
                IsCompleted = field.State == FieldState.Progress ? false : true,
                Cells = ConvertCells(field)
            };
        }
        private char[][] ConvertCells(Field field)
        {
            var cells = field.Cells;
            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);

            char[][] result = new char[rows][];

            for (int x = 0; x < rows; x++)
            {
                result[x] = new char[cols];
                for (int y = 0; y < cols; y++)
                {
                    Cell cell = cells[x, y];
                    if (cell.IsOpen || field.State != FieldState.Progress)
                    {
                        if (cell.CellType == CellType.Mine) result[x][y] = field.State == FieldState.Lose ? 'X' : 'M';
                        else result[x][y] = (char)('0' + cell.MinesAround);
                    }
                    else result[x][y] = ' ';
                }
            }

            return result;
        }
    }
}
