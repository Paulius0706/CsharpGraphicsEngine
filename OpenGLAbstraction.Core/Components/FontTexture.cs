using OpenTK.Mathematics;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Components
{
    public class FontTexture : Texture
    {
        public const string FontOrderFormat = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        private class FontRow
        {
            public int UpperPixel { get; set; }
            public int LowerPixel { get; set; }
            public int Height => UpperPixel - LowerPixel;
            public List<FontColumn> Columns { get; set; } = new List<FontColumn>();
        }
        private class FontColumn
        {
            public int LeftPixel { get; set; }
            public int RightPixel { get; set; }
        }
        public class Letter
        {
            private readonly FontTexture FontTexture;
            public readonly char Character;
            
            public readonly int UpperPixel;
            public readonly int LowerPixel;
            public readonly int LeftPixel;
            public readonly int RightPixel;
            public readonly int Height;
            public readonly int Width;
            public readonly Vector2 Position;
            public readonly Vector2 Size;

            public readonly int RealUpperPixel;
            public readonly int RealLowerPixel;
            public readonly int RealLeftPixel;
            public readonly int RealRightPixel;
            
            public readonly int RealHeight;
            public readonly int RealWidth ;
            public readonly Vector2 RealPosition;
            public readonly Vector2 RealSize;


            public Letter(FontTexture fontTexture, char character, int upperPixel, int lowerPixel, int leftPixel, int rightPixel)
            {
                this.FontTexture = fontTexture;
                Character = character;
                UpperPixel = upperPixel;
                LowerPixel = lowerPixel;
                LeftPixel = leftPixel;
                RightPixel = rightPixel;

                Height = UpperPixel - LowerPixel + 1;
                Width = RightPixel - LeftPixel + 1;
                
                RealUpperPixel = UpperPixel / FontTexture.Height;
                RealLowerPixel = LowerPixel / FontTexture.Height;
                RealLeftPixel = LeftPixel / FontTexture.Width;
                RealRightPixel = RightPixel / FontTexture.Width;
                
                RealHeight = Height / FontTexture.Height;
                RealWidth = Width / FontTexture.Width;

                Position = new Vector2(LeftPixel, LowerPixel);
                Size = new Vector2(Width, Height);

                RealPosition = new Vector2(RealLeftPixel, RealLowerPixel);
                RealSize = new Vector2(RealWidth, RealHeight);
            }
        }

        public Dictionary<char, Letter> Letters = new Dictionary<char, Letter>();
        public FontTexture(string path) : base(path)
        {

        }
        protected override void ImageWarping(ImageResult image)
        {
            Color4[] colors = GetRGBAColorsArray(image);
            List<FontRow> fontRows = GetFontRows(colors, image.Width, image.Height);
            foreach (FontRow row in fontRows) 
            {
                row.Columns = GetFontColumns(colors, image.Width, image.Height, row);
            }
            int i = 0;
            var maxHeight = fontRows.Max(o => o.Height);
            foreach (FontRow row in fontRows)
            {
                for (int index = 0; index < row.Columns.Count; index++) 
                {
                    FontColumn column = row.Columns[index];
                    if (i >= FontOrderFormat.Length) break;
                    int rightPixel = column.RightPixel;
                    int lowerPixel = row.LowerPixel;
                    //if(row.UpperPixel - row.LowerPixel < maxHeight)
                    //{
                    //    lowerPixel = row.UpperPixel - maxHeight;
                    //}
                    if (FontOrderFormat[i] == '"')
                    {
                        rightPixel = row.Columns[index + 1].RightPixel;
                        index++;
                    }
                    Letter letter = new Letter(this, FontOrderFormat[i], row.UpperPixel, lowerPixel, column.LeftPixel, rightPixel);
                    Letters.Add(letter.Character, letter);
                    i++;
                }
            }
            

        }
        private List<FontRow> GetFontRows(Color4[] colors, int width, int height)
        {
            List<FontRow> rows = new List<FontRow>();

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if(GetRow(colors, width, height, rowIndex).All(o => o.A == 0)) { continue; }
                FontRow fontRow = new FontRow();
                fontRow.LowerPixel = rowIndex;
                for(int rowIndex1 = rowIndex + 1; rowIndex1 < height; rowIndex1++)
                {
                    if (GetRow(colors, width, height, rowIndex1).All(o => o.A == 0)) 
                    {
                        break; 
                    }
                    fontRow.UpperPixel = rowIndex1;
                }
                rowIndex = fontRow.UpperPixel;
                rows.Add(fontRow);
            }
            return rows;
        }
        private List<FontColumn> GetFontColumns(Color4[] colors, int width, int height, FontRow row)
        {
            var subColors = colors.Skip(width * row.LowerPixel).Take(width * row.Height).ToArray();
            List<FontColumn> fontColumns = new List<FontColumn>();
            for (int columnIndex = 0; columnIndex < width; columnIndex++)
            {
                if (GetColumn(subColors, width, row.Height, columnIndex).All(o => o.A == 0)) { continue; }
                FontColumn fontRow = new FontColumn();
                fontRow.LeftPixel = columnIndex;
                for (int columnIndex1 = columnIndex + 3; columnIndex1 < width; columnIndex1++)
                {
                    fontRow.RightPixel = columnIndex1;
                    float alphaLimit = 0.0f;
                    var col1 = GetColumn(subColors, width, row.Height, columnIndex1);
                    var col2 = GetColumn(subColors, width, row.Height, columnIndex1 - 1);
                    var col3 = GetColumn(subColors, width, row.Height, columnIndex1 - 2);
                    var col = col1.Zip(col2,col3);
                    var counter = col.Count(o => o.First.A <= alphaLimit || o.Second.A <= alphaLimit || o.Third.A <= alphaLimit);
                    if (counter >= row.Height - 1)
                    {
                        break;
                    }
                }
                columnIndex = fontRow.RightPixel;
                fontColumns.Add(fontRow);
            }
            return fontColumns;
        }
        private Color4[] GetRGBAColorsArray(ImageResult image)
        {
            List<Color4> data = new List<Color4>();
            for (int i = 0; i < image.Data.Length; i += 4)
            {
                data.Add(new Color4(image.Data[i], image.Data[i + 1], image.Data[i + 2], image.Data[i + 3]));
            }
            return data.ToArray();
        }
        private Color4[] GetRow(Color4[] imageData, int width, int height, int row, int count = 1) 
        {
            return imageData.Skip(width * row).Take(width * count).ToArray();
        }
        private Color4[] GetColumn(Color4[] imageData, int width, int height, int column)
        {
            List<Color4> columnList = new List<Color4>();
            for(int i =column; i< imageData.Length; i+= width)
            {
                columnList.Add(imageData[i]);
            }
            return columnList.ToArray();
        }
    }
}
