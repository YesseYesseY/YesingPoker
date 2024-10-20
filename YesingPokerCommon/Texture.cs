using System.Runtime.InteropServices;
using SDL2;

namespace YesingPokerCommon
{
    public class Texture
    {
        public nint texturePtr;
        public int width { get; private set;}
        public int height { get; private set; }
        private Texture()
        {
        }

        ~Texture()
        {
            Destroy();
        }

        public static Texture CreateRGBA(WindowRenderer Renderer, uint[] data, int _width, int _height)
        {
            Texture texture = new Texture();
            texture.width = _width;
            texture.height = _height;
            var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var dataPtr = dataHandle.AddrOfPinnedObject();
            var surfacePtr = SDL.SDL_CreateRGBSurfaceFrom(dataPtr, _width, _height, 32, 4 * _width, 0xff000000, 0x00ff0000, 0x0000ff00, 0x000000ff);
            texture.texturePtr = SDL.SDL_CreateTextureFromSurface(Renderer.ptr, surfacePtr);
            dataHandle.Free();
            SDL.SDL_FreeSurface(surfacePtr);
            return texture;
        }
        public static Texture CreateSolidColorTexture(WindowRenderer Renderer, int width, int height, uint color)
        {
            uint[] data = new uint[width * height];
            for (int i = 0; i < width * height; i++)
                data[i] = color;
            return CreateRGBA(Renderer, data, width, height);
        }
        // TODO: Make smoother noise option
        public static Texture CreateNoise(WindowRenderer Renderer, int width, int height, uint mask = 0xffffffff, int min = 0, int max = 255)
        {
            uint[] data = new uint[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                var val = (uint)Random.Shared.Next(min, max);
                data[i] = ((val << 24) | (val << 16) | (val << 8) | 0xff) & mask; // Alpha channel is also ur responsibility in the mask
            }
            return CreateRGBA(Renderer, data, width, height);
        }

        public void Destroy()
        {
            SDL.SDL_DestroyTexture(texturePtr);
        }
    }
}
