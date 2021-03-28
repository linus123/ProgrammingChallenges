﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.TheBlocksProblem
{
    public class BlockSpace
    {
        private int _numberOfBlocks;
        private int[,] _blockSpace;

        public const int EmptySlot = -1;

        public BlockSpace(
            int numberOfBlocks)
        {
            _numberOfBlocks = numberOfBlocks;
            _blockSpace = new int[numberOfBlocks, 25];

            for (int i = 0; i < numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    _blockSpace[i, j] = EmptySlot;
                }
            }

            for (int i = 0; i < numberOfBlocks; i++)
            {
                _blockSpace[i, 0] = i;
            }
        }

        public BlockLocation FindBlock(
            int blockNumber)
        {
            for (int i = 0; i < _numberOfBlocks; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (_blockSpace[i, j] == blockNumber)
                        return new BlockLocation()
                        {
                            RowIndex = i,
                            ColumnIndex = j
                        };
                }
            }

            throw new Exception("Block not found.");
        }

        public IEnumerable<string> GetLines()
        {
            for (int i = 0; i < _numberOfBlocks; i++)
            {
                var line = $"{i}:";

                for (int j = 0; j < 25; j++)
                {
                    if (_blockSpace[i, j] != EmptySlot)
                        line += $" {_blockSpace[i, j]}";
                }

                yield return line;
            }
        }

        public void MoveOnto(
            int sourceBlockNumber,
            int destBlockNumber)
        {
            var sourceLocation = FindBlock(sourceBlockNumber);
            var destLocation = FindBlock(destBlockNumber);

            if (_blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] == EmptySlot)
            {
                _blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] = sourceBlockNumber;
            }
            else
            {
                // slide blocks up
                for (int i = 23; i >= (destLocation.ColumnIndex + 1); i--)
                {
                    _blockSpace[destLocation.RowIndex, i + 1] = _blockSpace[destLocation.RowIndex, i];
                }

                _blockSpace[destLocation.RowIndex, destLocation.ColumnIndex + 1] = sourceBlockNumber;
            }

            _blockSpace[sourceLocation.RowIndex, sourceLocation.ColumnIndex] = EmptySlot;
        }
    }
}