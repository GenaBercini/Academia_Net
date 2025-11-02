//namespace API.Clients
//{
//    public static class AuthServiceProvider
//    {
//        private static IAuthService? _instance;
//        private static readonly object _lock = new object();
//        private readonly IAuthService _authService;

//        public AuthServiceProvider(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        public IAuthService GetService() => _authService;

//        public static IAuthService Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    throw new InvalidOperationException(
//                        "AuthService has not been registered. Call AuthServiceProvider.Register() first.");
//                }
//                return _instance;
//            }
//        }

//        public static void Register(IAuthService authService)
//        {
//            lock (_lock)
//            {
//                _instance = authService;
//            }
//        }

//        public static void Clear()
//        {
//            lock (_lock)
//            {
//                _instance = null;
//            }
//        }
//    }
//}

using System;

namespace API.Clients
{
    public static class AuthServiceProvider
    {
        private static IAuthService? _instance;
        private static readonly object _lock = new object();

        public static IAuthService Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException(
                        "AuthService has not been registered. Call AuthServiceProvider.Register() first.");
                }
                return _instance;
            }
        }

        public static void Register(IAuthService authService)
        {
            if (authService == null) throw new ArgumentNullException(nameof(authService));
            lock (_lock)
            {
                _instance = authService;
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _instance = null;
            }
        }
    }
}