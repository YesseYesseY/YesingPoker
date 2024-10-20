using SDL2;

namespace YesingPokerCommon
{
    public class BaseWindow
    {
        protected Vector2i WindowPos, WindowSize, MousePos;
        protected string title;
        protected bool running;

        protected nint windowPtr;
        protected WindowRenderer Renderer;

        public BaseWindow(string _title, int _width, int _height)
        {
            title = _title;
            WindowSize.X = _width;
            WindowSize.Y = _height;

            windowPtr = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, _width, _height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            
            if (windowPtr == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create window");
                Close();
                return;
            }

            Renderer = new WindowRenderer(windowPtr);

            running = true;
            Renderer.SetRenderDrawColor(255, 255, 255, 0);
            SDL.SDL_GetWindowPosition(windowPtr, out WindowPos.X, out WindowPos.Y);
            SDL.SDL_GetWindowSize(windowPtr, out WindowSize.X, out WindowSize.Y);
        }

        ~BaseWindow()
        {
            Close();
        }

        private void HandleWindowEvent(SDL.SDL_Event _event)
        {
            switch (_event.window.windowEvent)
            {
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                    WindowPos.X = _event.window.data1;
                    WindowPos.Y = _event.window.data2;
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                    WindowSize.X = _event.window.data1;
                    WindowSize.Y = _event.window.data2;
                    OnWindowResize();
                    break;
                default:
                    break;
            }
        }

        private void HandleEvent(SDL.SDL_Event _event)
        {
            switch (_event.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
                case SDL.SDL_EventType.SDL_WINDOWEVENT:
                    HandleWindowEvent(_event);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    MousePos.X = _event.motion.x;
                    MousePos.Y = _event.motion.y;
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    OnMouseUp();
                    break;
                default:
                    break;
            }
        }

        public void Start()
        {
            SDL.SDL_Event _event;
            while (running)
            {
                while (SDL.SDL_PollEvent(out _event) != 0)
                {
                    HandleEvent(_event);
                }

                Renderer.RenderClear();
                Render();
                Renderer.RenderPresent();

                Update();

                SDL.SDL_Delay(10);
            }
        }

        public void Close()
        {
            running = false;

            Renderer.Close();
            if (windowPtr != IntPtr.Zero)
                SDL.SDL_DestroyWindow(windowPtr);
        }

        public virtual void Render()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnWindowResize()
        {

        }

        public virtual void OnMouseUp()
        {

        }
    }
}
