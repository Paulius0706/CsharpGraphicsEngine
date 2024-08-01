using OpenGLAbstraction.Core.Objects;
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
        


        public Dictionary<char, TransformUV> LettersUVs = new Dictionary<char, TransformUV>();
        public int SpaceWidth { get; private set; }
        public int SpaceHeight { get; private set; }
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
                    TransformUV letter = new TransformUV(this, row.UpperPixel, lowerPixel, column.LeftPixel, rightPixel);
                    LettersUVs.Add(FontOrderFormat[i], letter);
                    i++;
                }
            }
            if(!LettersUVs.ContainsKey(' '))
            {
                SpaceWidth = (int)LettersUVs.Values.Select(o => o.Width).Average();
                SpaceHeight = (int)LettersUVs.Values.Select(o => o.Height).Average();
                LettersUVs.Add(' ', new TransformUV(this, 0, 0, 0, 0));
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
