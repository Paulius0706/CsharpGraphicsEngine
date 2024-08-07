﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Components
{
    public class Layout<Atributes, Uniforms> where Atributes : struct where  Uniforms : struct
    {
        public const int MinVertexCount = 1;
        public const int MaxVertexCount = 100_000;

        private bool disposed;

        private readonly int VertexArrayHandle;
        private readonly int VertexBufferHandle;
        private readonly int IndexBufferHandle;
        private readonly Shader<Atributes, Uniforms> shader;
        public int VertexBufferCount { get; private set; }
        public int IndexBufferCount { get; private set; }

        public Layout(Shader<Atributes, Uniforms> shader, IEnumerable<Atributes> vertices, IEnumerable<int> indices = null)
        {
            this.shader = shader;
            VertexBufferCount = vertices == null ? 0 : vertices.Count();
            IndexBufferCount = indices == null ? 0 : indices.Count();

            VertexArrayHandle = GL.GenVertexArray();
            VertexBufferHandle = GL.GenBuffer();
            if (IndexBufferCount != 0)
            {
                IndexBufferHandle = GL.GenBuffer();
            }

            GL.BindVertexArray(VertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexBufferCount * Marshal.SizeOf<Atributes>(), vertices.ToArray(), BufferUsageHint.StaticDraw);

            if (IndexBufferCount != 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferHandle);
                GL.BufferData(BufferTarget.ElementArrayBuffer, IndexBufferCount * sizeof(int), indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            AssignAtributes();

            GL.BindVertexArray(0);

        }
        ~Layout()
        {
            Dispose();
        }

        public void Use()
        {
            GL.BindVertexArray(VertexArrayHandle);
        }
        public void UnUse()
        {
            GL.BindVertexArray(0);
        }
        public void Render()
        {
            if (IndexBufferCount != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, IndexBufferCount, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, VertexBufferCount);
            }
        }

        private void AssignAtributes()
        {
            Type vertexStructType = typeof(Atributes);
            FieldInfo[] vertexStructFields = vertexStructType.GetFields();

            var shaderAtributes = shader.ShaderAttributes;

            foreach (var shaderAttribute in shaderAtributes.OrderBy(o => o.Location))
            {
                int atribSize = 1;
                switch (shaderAttribute.Type)
                {
                    case ActiveAttribType.Float: atribSize = 1; break;
                    case ActiveAttribType.FloatVec2: atribSize = 2; break;
                    case ActiveAttribType.FloatVec3: atribSize = 3; break;
                    case ActiveAttribType.FloatVec4: atribSize = 4; break;
                }
                FieldInfo vertexStructField = vertexStructFields.FirstOrDefault(o => o.Name == shaderAttribute.Name);
                object[] attributes = vertexStructField.GetCustomAttributes(typeof(FieldOffsetAttribute), true);

                GL.EnableVertexAttribArray(shaderAttribute.Location);
                // if offset dont work use Marshal.OffsetOf(typeof(IMAGE_DOS_HEADER), "e_lfanew")
                GL.VertexAttribPointer(shaderAttribute.Location, atribSize, VertexAttribPointerType.Float, false, Marshal.SizeOf<Atributes>(), Marshal.OffsetOf(typeof(Atributes), shaderAttribute.Name));
            }
        }


        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(VertexArrayHandle);

            GC.SuppressFinalize(this);
        }
    }
}
