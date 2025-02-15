using Microsoft.Extensions.Options;
using Serilog;
using StudioTG.Application.Interfaces;
using StudioTG.Domain.Entities.Cells;
using StudioTG.Domain.Entities.Fields;
using StudioTG.Domain.Enums;
using StudioTG.Infrastructure.Common;
using System.Diagnostics;
using System.IO;

namespace StudioTG.Infrastructure.Services
{
    public class FieldSerivce(IFieldRepository fieldsRepository, IOptions<FieldOptions> fieldOptions) : IFieldService
    {
        [DebuggerStepThrough]
        public Task<Field> CreateFieldAsync(int width, int height, int minesCount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!FieldIsValid(width, height, minesCount)) throw new ArgumentException("Неверные аргументы");
            Field field = fieldsRepository.Create(width, height, minesCount);
            Log.Information("[{Service}] Field {Id} created", nameof(FieldSerivce), field.Id);
            Log.Information("[{Service}] Filling field", nameof(FieldSerivce));
            FillField(field);
            Log.Information("[{Service}] Set {minesCount} mines", nameof(FieldSerivce), minesCount);
            SetMines(field, minesCount);
            Log.Information("[{Service}] Calculating around mines", nameof(FieldSerivce));
            SetMinesAround(field);
            Log.Information("[{Service}] Field ready", nameof(FieldSerivce));

            return Task.FromResult(field);
        }
        [DebuggerStepThrough]
        public Task<Field> MakeTurnAsync(Guid Id, int row, int cell, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Log.Information("[{Service}] Read field", nameof(FieldSerivce));
            Field field = fieldsRepository.Read(Id);
            Log.Information("[{Service}] Field {Id}", nameof(FieldSerivce), field.Id);

            if (!CellIndexIsValid(field, row, cell)) throw new ArgumentException("Неверные аргументы");

            Cell fieldCell = field.Cells[row, cell];

            if (fieldCell.IsOpen) throw new InvalidOperationException("Уже открытая ячейка");

            if (fieldCell.CellType == CellType.Mine)
            {
                Log.Information("[{Service}] Mine cell", nameof(FieldSerivce));
                fieldCell.IsOpen = true;
                field.State = FieldState.Lose;
            }
            else
            {
                Log.Information("[{Service}] Empty cell, opening around", nameof(FieldSerivce));
                OpenCellsAround(field, new CellIndex { X = row, Y = cell });
                if (ClosedEmptyRemains(field) == 0)
                {
                    Log.Information("[{Service}] No empty cells remains, win", nameof(FieldSerivce));
                    field.State = FieldState.Win;
                }
            }

            return Task.FromResult(field);
        }

        private bool FieldIsValid(int width, int height, int minesCount)
        {
            if (width < 0 || width > fieldOptions.Value.MaxWidth) return false;
            if (height < 0 || height > fieldOptions.Value.MaxHeight) return false;
            if (minesCount > width * height - 1) return false;
            return true;
        }
        private bool CellIndexIsValid(Field field, int row, int cell)
        {
            if (row < 0 || row > field.Width - 1) return false;
            if (cell < 0 || cell > field.Height - 1) return false;
            return true;
        }
        private void FillField(Field field)
        {
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    field.Cells[x, y] = new Cell { CellType = CellType.Empty };
                }
            }
        }
        private void SetMines(Field field, int minesCount)
        {
            int placedMines = 0;
            Random rand = new Random();

            while (placedMines < minesCount)
            {
                int x = rand.Next(field.Width);
                int y = rand.Next(field.Height);

                Cell cell = field.Cells[x, y];

                if (cell.CellType != CellType.Mine)
                {
                    cell.CellType = CellType.Mine;
                    placedMines++;
                }
            }
        }
        private void SetMinesAround(Field field)
        {
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Cell cell = field.Cells[x, y];
                    if (cell.CellType == CellType.Mine) continue;

                    int minesAround = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int ix = x + i;
                            int jy = y + j;

                            if (!CellIndexIsValid(field, ix, jy)) continue;

                            Cell cellCheck = field.Cells[ix, jy];
                            if (cellCheck.CellType == CellType.Mine)
                            {
                                minesAround++;
                            }
                        }
                    }
                    cell.MinesAround = minesAround;
                }
            }
        }
        private void OpenCellsAround(Field field, CellIndex startCell)
        {
            Queue<CellIndex> cellsQueue = new();
            cellsQueue.Enqueue(startCell);

            while (cellsQueue.Count > 0)
            {
                CellIndex cellIndex = cellsQueue.Dequeue();
                Cell currentCell = field.Cells[cellIndex.X, cellIndex.Y];

                if (currentCell.IsOpen || currentCell.CellType == CellType.Mine) continue;

                currentCell.IsOpen = true;
                if (currentCell.MinesAround == 0)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            int currentX = cellIndex.X + x;
                            int currentY = cellIndex.Y + y;

                            if (CellIndexIsValid(field, currentX, currentY))
                                cellsQueue.Enqueue(
                                    new CellIndex { X = currentX, Y = currentY });
                        }
                    }
                }
            }
        }
        private static int ClosedEmptyRemains(Field field)
        {
            int remains = 0;

            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Cell cell = field.Cells[x, y];
                    if (!cell.IsOpen && cell.CellType == CellType.Empty)
                    {
                        remains++;
                    }
                }
            }
            return remains;
        }
    }
}
