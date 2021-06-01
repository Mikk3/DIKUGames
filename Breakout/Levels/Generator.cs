using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace Breakout.Levels {

    public class Generator {
        /// <summary>
        /// Generates a block container based on parsed data from a .txt file in a specified format
        /// </summary>
        /// <param name="rows">The "map" part of the .txt file parsed as a list of strings</param>
        /// <param name="meta">The "meta" part of the .txt file used to check for special block
        /// types</param>
        /// <param name="images">The "legend" part of the .txt file used to connect each symbol
        /// from the level with an image</param>
        /// <returns>Returns the block entity container containing all blocks rendered in the game </returns>
        public static EntityContainer<Block> GenerateBlocksContainer(
            List<string> rows,
            Dictionary<string, string> meta,
            Dictionary<char, string> images) {

            rows.Reverse();

            // Add relevant characters to the list of special blocks
            List<char> hardened = new List<char>();
            List<char> powerUp = new List<char>();
            List<char> unbreakable = new List<char>();

            // Populate lists with relevant characters
            foreach (var pair in meta) {
                switch (pair.Key) {

                    case "Hardened":
                        foreach (char ch in pair.Value) {
                            hardened.Add(ch);
                        }
                        break;

                    case "PowerUp":
                        foreach (char ch in pair.Value) {
                            powerUp.Add(ch);
                        }
                        break;

                    case "Unbreakable":
                        foreach (char ch in pair.Value) {
                            unbreakable.Add(ch);
                        }
                        break;
                }
            }


            var Entities = new EntityContainer<Block>();

            var blockSize = calculateBlockSize(rows);

            // Iterate over each symbol in the "map" data to check for blocks to be added to the resulting container
            for (int y = 0; y < rows.Count; y++) {
                for (int x = 0; x < rows[y].Length; x++) {
                    var position = calculatePosition(x, y, blockSize);
                    var symbol = rows[y][x];

                    // Checking if symbol is within a special block type list
                    if (hardened.Contains(symbol)) {
                        Entities.AddEntity(new HardenedBlock(
                            new DynamicShape(position, blockSize),
                            new Image(Path.Combine("Assets", "Images", images[symbol])),
                            new Image(Path.Combine("Assets", "Images", images[symbol].Substring(0, images[symbol].Length - 4) + "-damaged.png")
                        )));
                    } else if (powerUp.Contains(symbol)) {
                        Entities.AddEntity(new PowerUpBlock(
                            new DynamicShape(position, blockSize),
                            new Image(Path.Combine("Assets", "Images", images[symbol])
                        )));
                    } else if (unbreakable.Contains(symbol)) {
                        Entities.AddEntity(new UnbreakableBlock(
                            new DynamicShape(position, blockSize),
                            new Image(Path.Combine("Assets", "Images", images[symbol])
                        )));
                    // If not special block type or "-", normal block type is added.
                    } else if (symbol != '-') {
                        Entities.AddEntity(new NormalBlock(
                            new DynamicShape(position, blockSize),
                            new Image(Path.Combine("Assets", "Images", images[symbol]))
                        ));
                    }

                }
            }

            return Entities;
        }

        // Based on dimensions of .txt level files calculates the extent of the added blocks
        private static Vec2F calculateBlockSize(List<string> data) {
            return new Vec2F(1f / data[0].Length, 1f / data.Count);
        }

        // Based on dimensions of .txt level files calculates the position of the added blocks
        private static Vec2F calculatePosition(int x, int y, Vec2F size) {
            return new Vec2F(x * size.X, y * size.Y);
        }

    }
}