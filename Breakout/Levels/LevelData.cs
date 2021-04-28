using DIKUArcade.Entities;
using Breakout.Blocks;

namespace Breakout.Levels
{

    public class LevelData {

        private string level;

        public EntityContainer<Block> Blocks;

        public LevelData(string level) {
            this.level = level;

            createBlocks();

        }

        private void createBlocks() {
            var data = new Provider(level).GetDataAsList();
            var rows = new RowParser(data).Parse();
            var meta = new MetaParser(data).Parse();
            var images = new LegendParser(data).Parse();

            Blocks = Generator.GenerateBlocksContainer(rows, meta, images);
        }

    }

}