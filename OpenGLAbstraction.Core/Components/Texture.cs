using OpenTK.Graphics.OpenGL;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Components
{
    public class Texture : IDisposable
    {
        private readonly int textureHandle;
        public readonly int textureWidth;
        public readonly int textureHeight;

        private bool disposed;

        public Texture(string path)
        {
            textureHandle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureHandle);
            // stb_image loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            // This will correct that, making the texture display properly.
            StbImage.stbi_set_flip_vertically_on_load(1);

            // Load the image.
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);
            ImageWarping(image);
            textureWidth = image.Width;
            textureHeight = image.Height;
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
        ~Texture()
        {
            Dispose();
        }
        protected void ImageWarping(ImageResult image)
        {

        }
        public void Use()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureHandle);
        }
        public void Unuse()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            GL.DeleteTexture(textureHandle);
            GC.SuppressFinalize(this);
        }

    }
}
