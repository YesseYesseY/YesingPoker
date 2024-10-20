using SDL2;

namespace YesingPokerCommon
{
    public class WindowRenderer
    {
        public nint ptr;

        public WindowRenderer(nint windowPtr)
        {
            ptr = SDL.SDL_CreateRenderer(windowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (ptr == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create renderer");
                Close();
                return;
            }
        }

        public void Close()
        {
            if (ptr != IntPtr.Zero)
                SDL.SDL_DestroyRenderer(ptr);
        }

        public void SetRenderDrawColor(byte r, byte g, byte b, byte a)
        {
            SDL.SDL_SetRenderDrawColor(ptr, r, g, b, a);
        }

        public void RenderPresent()
        {
            SDL.SDL_RenderPresent(ptr);
        }

        public void RenderClear()
        {
            SDL.SDL_RenderClear(ptr);
        }

        public void DrawTexture(Texture texture, int x, int y)
        {
            var rect = new SDL.SDL_Rect { h = texture.height, w = texture.width, x = x, y = y };
            SDL.SDL_RenderCopy(ptr, texture.texturePtr, nint.Zero, ref rect);
        }
    }
}
